using Dynastream.Fit;
using FitDataService.Application.DTOs.Messaging;
using FitDataService.Domain.Models;
using DateTime = System.DateTime;
using Profile = AutoMapper.Profile;

namespace FitDataService.Infrastructure.Data.Configurations.Mapping;

public class FitFileDataProfile : Profile
{
    public FitFileDataProfile()
    {
        CreateMap<FitFileData, FitFileDataEntityDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(
                src => GetName(src)))
            .ForMember(dest => dest.Distance, opt => opt.MapFrom(
                src => GetDistance(src)))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(
                src => GetStartTime(src)))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(
                src => GetEndTime(src)))
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(
                src => GetDuration(src)))
            .ForMember(dest => dest.UbicationLatitude, opt => opt.MapFrom(
                src => GetUbicationLatitude(src)))
            .ForMember(dest => dest.UbicationLongitude, opt => opt.MapFrom(
                src => GetUbicationLongitude(src)));
    }

    private string GetName(FitFileData data)
    {
        DateTime? localTimestamp = data.Activity.LocalTimestamp;
        
        string sport = Enum.GetName(typeof(Sport), data.Session.FirstOrDefault()?.Sport!) ?? "Sport";
        string product = data.FileId.ProductName ?? "Unknown product";
        
        return $"{GetMomentOfDay(localTimestamp)} {sport} by {product}".Trim();
    }

    private double GetDistance(FitFileData data)
    {
        double totalDistance = 0;

        foreach (Session session in data.Session)
            totalDistance += session.TotalDistance ?? 0;
        
        return totalDistance;
    }

    private DateTime GetStartTime(FitFileData data)
    {
        Session? session = data.Session.FirstOrDefault();
        
        if (session is null)
            throw new Exception("Session not found");
        
        return session.StartTime;
    }
    
    private DateTime GetEndTime(FitFileData data)
    {
        IList<Session> sessions = data.Session;
        
        if (sessions.Count == 0)
            throw new Exception("Session not found");

        double totalElapsedTime = GetDuration(data);
        
        return sessions[0].StartTime.AddSeconds(totalElapsedTime);
    }

    private double GetDuration(FitFileData data)
    {
        IList<Session> sessions = data.Session;
        
        if (sessions.Count == 0)
            throw new Exception("Session not found");
        
        double totalElapsedTime = 0;
        
        foreach (Session session in sessions)
            totalElapsedTime += session.TotalElapsedTime;
        
        return totalElapsedTime;
    }

    private double GetUbicationLatitude(FitFileData data)
    {
        Record? record = data.Records.FirstOrDefault(r => r.PositionLat.HasValue);
        
        if (record is null)
            throw new Exception("Record not found");
        
        return record.PositionLat!.Value / 1e7;
    }

    private double GetUbicationLongitude(FitFileData data)
    {
        Record? record = data.Records.FirstOrDefault(r => r.PositionLong.HasValue);
        
        if (record is null)
            throw new Exception("Record not found");
        
        return record.PositionLong!.Value / 1e7;
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