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
        
        foreach (Session session in sessions)
        {
            Distance += Convert.ToInt32(session.TotalDistance);
            TotalElapsedTime += session.TotalElapsedTime;
            Steps += (session.TotalCycles ?? 0) * 2;
            Calories += session.TotalCalories ?? 0;
            AveragePace += 60 / ((session.AvgSpeed ?? 0) * 3.6);    // Min/Km
            MaxPace += 60 / ((session.MaxSpeed ?? 0) * 3.6);    // Min/Km
            ElevationGain += session.TotalAscent ?? 0;
            ElevationLoss += session.TotalDescent ?? 0;
            AverageSpeed += session.AvgSpeed ?? 0;
            MaxSpeed += session.MaxSpeed ?? 0;
            AverageHeartRate += session.AvgHeartRate ?? 0;
            MaxHeartRate += session.MaxHeartRate ?? 0;
            MinHeartRate += session.MinHeartRate ?? 0;
            AverageCadence += session.AvgCadence ?? 0;
            MaxCadence += session.MaxCadence ?? 0;
            MaxAltitude += session.MaxAltitude ?? 0;
            MinAltitude += session.MinAltitude ?? 0;
            TotalTrainingEffect += session.TotalTrainingEffect ?? 0;
            TrainingStressScore += session.TrainingStressScore ?? 0;
        }
    }
    
    private string GetName(FitFileData data)
    {
        DateTime? localTimestamp = data.Activity.LocalTimestamp;
        
        string sport = Enum.GetName(typeof(Sport), data.Session.FirstOrDefault()?.Sport!) ?? "Sport";
        string product = data.FileId.ProductName ?? "Unknown product";
        
        return $"{GetMomentOfDay(localTimestamp)} {sport} by {product}".Trim();
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
        IList<Session> sessions = data.Session;

        if (sessions.Count == 0)
            throw new Exception("Session not found");
        
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