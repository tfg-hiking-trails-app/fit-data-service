using Dynastream.Fit;
using FitDataService.Application.DTOs.Messaging;
using FitDataService.Domain.Models;
using DateTime = System.DateTime;
using Profile = AutoMapper.Profile;

namespace FitDataService.Infrastructure.Data.Configurations.Mapping;

public class FitFileDataProfile : Profile
{
    private int Distance { get; set; }
    private long Steps { get; set; }
    private int Calories { get; set; }
    private double AveragePace { get; set; }
    private double MaxPace { get; set; }
    private double ElevationGain { get; set; }
    private double ElevationLoss { get; set; }
    private double AverageSpeed { get; set; }
    private double MaxSpeed { get; set; }
    private int AverageHeartRate { get; set; }
    private int MaxHeartRate { get; set; }
    private int MinHeartRate { get; set; }
    private double AverageCadence { get; set; }
    private double MaxCadence { get; set; }
    private double MaxAltitude { get; set; }
    private double MinAltitude { get; set; }
    private double TotalTrainingEffect { get; set; }
    private double TrainingStressScore { get; set; }
    private double TotalElapsedTime { get; set; }
    
    public FitFileDataProfile()
    {
        CreateMap<FitFileData, FitFileDataEntityDto>()
            .BeforeMap((data, dto) => CalculateData(data))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(
                src => GetName(src)))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(
                src => GetStartTime(GetSessions(src))))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(
                src => GetEndTime(GetSessions(src))))
            .ForMember(dest => dest.UbicationLatitude, opt => opt.MapFrom(
                src => GetUbicationLatitude(src)))
            .ForMember(dest => dest.UbicationLongitude, opt => opt.MapFrom(
                src => GetUbicationLongitude(src)))
            .ForMember(dest => dest.GeneratedByFitFile, opt => opt.MapFrom(
                src => true))
            .ForMember(dest => dest.Distance, opt => opt.MapFrom(
                src => Distance))
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(
                src => TotalElapsedTime))
            .ForMember(dest => dest.Steps, opt => opt.MapFrom(
                src => Steps))
            .ForMember(dest => dest.Calories, opt => opt.MapFrom(
                src => Calories))
            .ForMember(dest => dest.AveragePace, opt => opt.MapFrom(
                src => AveragePace))
            .ForMember(dest => dest.MaxPace, opt => opt.MapFrom(
                src => MaxPace))
            .ForMember(dest => dest.ElevationGain, opt => opt.MapFrom(
                src => ElevationGain))
            .ForMember(dest => dest.ElevationLoss, opt => opt.MapFrom(
                src => ElevationLoss))
            .ForMember(dest => dest.AverageSpeed, opt => opt.MapFrom(
                src => AverageSpeed))
            .ForMember(dest => dest.MaxSpeed, opt => opt.MapFrom(
                src => MaxSpeed))
            .ForMember(dest => dest.AverageHeartRate, opt => opt.MapFrom(
                src => AverageHeartRate))
            .ForMember(dest => dest.MaxHeartRate, opt => opt.MapFrom(
                src => MaxHeartRate))
            .ForMember(dest => dest.MinHeartRate, opt => opt.MapFrom(
                src => MinHeartRate))
            .ForMember(dest => dest.AverageCadence, opt => opt.MapFrom(
                src => AverageCadence))
            .ForMember(dest => dest.MaxCadence, opt => opt.MapFrom(
                src => MaxCadence))
            .ForMember(dest => dest.MaxAltitude, opt => opt.MapFrom(
                src => MaxAltitude))
            .ForMember(dest => dest.MinAltitude, opt => opt.MapFrom(
                src => MinAltitude))
            .ForMember(dest => dest.TotalTrainingEffect, opt => opt.MapFrom(
                src => TotalTrainingEffect))
            .ForMember(dest => dest.TrainingStressScore, opt => opt.MapFrom(
                src => TrainingStressScore));
    }
    
    private void CalculateData(FitFileData data)
    {
        IList<Session> sessions = GetSessions(data);
        
        if (sessions.Count == 0)
            return;
        
        // Totals
        double totalDistance = 0;
        double totalElapsed = 0;
        int totalCalories = 0;
        long totalSteps = 0;
        int totalAscent = 0;
        int totalDescent = 0;
        
        // Averages
        double sumHeartRate = 0;
        double sumCadence = 0;
        double sumSpeed = 0;
        double weightTime = 0;
        
        // Max and Min
        double maxSpeed = 0;
        double maxHeartRate = 0;
        double maxCadence = 0;
        double minHeartRate = double.PositiveInfinity;
        double maxAltitude = double.NegativeInfinity;
        double minAltitude = double.PositiveInfinity;
        
        double sumTrainingEffect = 0;
        int countTrainingEffect = 0;
        double totalTrainingStressScore = 0;

        foreach (var session in sessions)
        {
            totalDistance += session.TotalDistance ?? 0;
            totalElapsed += session.TotalElapsedTime;
            totalCalories += session.TotalCalories ?? 0;
            totalSteps += (session.TotalCycles ?? 0) * 2;
            totalAscent += session.TotalAscent ?? 0;
            totalDescent += session.TotalDescent ?? 0;
            
            if (session.TotalElapsedTime > 0)
            {
                sumHeartRate += (session.AvgHeartRate ?? 0) * session.TotalElapsedTime;
                sumCadence += (session.AvgCadence ?? 0) * session.TotalElapsedTime;
                sumSpeed += (session.AvgSpeed ?? 0) * session.TotalElapsedTime;
                weightTime += session.TotalElapsedTime;
            }

            maxSpeed = Math.Max(maxSpeed, session.MaxSpeed ?? 0);
            maxHeartRate = Math.Max(maxHeartRate, session.MaxHeartRate ?? 0);
            maxCadence = Math.Max(maxCadence, session.MaxCadence ?? 0);
            minHeartRate = Math.Min(minHeartRate, session.MinHeartRate ?? 0);
            maxAltitude = Math.Max(maxAltitude, session.MaxAltitude ?? 0);
            minAltitude = Math.Min(minAltitude, session.MinAltitude ?? 0);
            
            totalTrainingStressScore += session.TrainingStressScore ?? 0;
            
            if (session.TotalTrainingEffect.HasValue)
            {
                sumTrainingEffect += session.TotalTrainingEffect.Value;
                countTrainingEffect++;
            }
        }

        // Average Speed (m/s)
        double avgSpeed = totalElapsed > 0
            ? totalDistance / totalElapsed
            : (weightTime > 0 ? sumSpeed / weightTime : 0);

        // Average Pace (s/km)
        double avgPace = (totalDistance > 0 && totalElapsed > 0)
            ? totalElapsed / (totalDistance / 1000.0)
            : 0;

        // Max Pace (s/km)
        double fastestPace = maxSpeed > 0
            ? 1000.0 / maxSpeed
            : 0;
        
        double avgHeartRate = weightTime > 0 ? sumHeartRate / weightTime : 0;
        double avgCadence = weightTime > 0 ? sumCadence / weightTime : 0;
        
        double avgTrainingEffect = countTrainingEffect > 0
            ? sumTrainingEffect / countTrainingEffect
            : 0;

        Distance = (int) Math.Round(totalDistance);
        TotalElapsedTime = totalElapsed;
        Steps = totalSteps;
        Calories = totalCalories;
        AveragePace = avgPace;
        MaxPace = fastestPace;
        ElevationGain = totalAscent;
        ElevationLoss = totalDescent;
        AverageSpeed = avgSpeed;
        MaxSpeed = maxSpeed;
        AverageHeartRate = Convert.ToInt32(avgHeartRate);
        MaxHeartRate = Convert.ToInt32(maxHeartRate);
        MinHeartRate = double.IsPositiveInfinity(minHeartRate) ? 0 : Convert.ToInt32(minHeartRate);
        AverageCadence = avgCadence;
        MaxCadence = Math.Round(maxCadence, 1);
        MaxAltitude = double.IsNegativeInfinity(maxAltitude) ? 0 : maxAltitude;
        MinAltitude = double.IsPositiveInfinity(minAltitude) ? 0 : minAltitude;
        TrainingStressScore = totalTrainingStressScore;
        TotalTrainingEffect = avgTrainingEffect;
    }
    
    private string GetName(FitFileData data)
    {
        DateTime? localTimestamp = data.Activity.LocalTimestamp;
        
        string sport = Enum.GetName(typeof(Sport), data.Sessions.FirstOrDefault()?.Sport!) ?? "Sport";
        
        return $"{GetMomentOfDay(localTimestamp)} {sport}".Trim();
    }
    
    private DateTime GetStartTime(IList<Session> sessions)
    {
        return sessions[0].StartTime;
    }
    
    private DateTime GetEndTime(IList<Session> sessions)
    {
        return sessions[0].StartTime.AddSeconds(TotalElapsedTime);
    }
    
    private double GetUbicationLatitude(FitFileData data)
    {
        Record? record = data.Records.FirstOrDefault(r => r.PositionLat.HasValue);
        
        if (record is null)
            throw new Exception("Record not found");
        
        return record.PositionLat!.Value * (180 / Math.Pow(2, 31));
    }

    private double GetUbicationLongitude(FitFileData data)
    {
        Record? record = data.Records.FirstOrDefault(r => r.PositionLong.HasValue);
        
        if (record is null)
            throw new Exception("Record not found");
        
        return record.PositionLong!.Value * (180 / Math.Pow(2, 31));
    }

    private IList<Session> GetSessions(FitFileData data)
    {
        IList<Session> sessions = data.Sessions;

        if (sessions.Count == 0)
            throw new Exception("Sessions not found");
        
        return sessions;
    }

    private string GetMomentOfDay(DateTime? date)
    {
        if (date is null)
            return "";
        
        int hour = date.Value.Hour;
        
        if (hour >= 0 && hour < 6)
            return "Dawn";
        
        if (hour >= 6 && hour < 12)
            return "Morning";
        
        if (hour >= 12 && hour < 18)
            return "Afternoon";
        
        if (hour >= 18 && hour < 21)
            return "Evening";

        return "Night";
    }
    
}