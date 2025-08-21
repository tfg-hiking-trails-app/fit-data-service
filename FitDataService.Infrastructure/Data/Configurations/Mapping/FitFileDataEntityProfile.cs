using AutoMapper;
using FitDataService.Application.DTOs;
using FitDataService.Application.DTOs.RecordData;
using FitDataService.Domain.Models;

namespace FitDataService.Infrastructure.Data.Configurations.Mapping;

public class FitFileDataEntityProfile : Profile
{
    public FitFileDataEntityProfile()
    {
        CreateMap<ActivityEntityDto, Activity>().ReverseMap();
        CreateMap<FiledIdEntityDto, FileId>().ReverseMap();
        CreateMap<LapEntityDto, Lap>().ReverseMap();
        CreateMap<RecordEntityDto, Record>()
            .ForMember(dest => dest.PositionLat, opt => opt.MapFrom(
                src => ConvertSemicirclesToDegrees(src.PositionLat)))
            .ForMember(dest => dest.PositionLong, opt => opt.MapFrom(
                src => ConvertSemicirclesToDegrees(src.PositionLong)))
            .ReverseMap();
        CreateMap<SessionEntityDto, Session>().ReverseMap();
        CreateMap<CoordinateEntityDto, Record>()
            .ForMember(dest => dest.PositionLat, opt => opt.MapFrom(
                src => ConvertSemicirclesToDegrees(src.PositionLat)))
            .ForMember(dest => dest.PositionLong, opt => opt.MapFrom(
                src => ConvertSemicirclesToDegrees(src.PositionLong)))
            .ReverseMap();
        CreateMap<GraphicsDataEntityDto, Record>().ReverseMap();
    }

    private double? ConvertSemicirclesToDegrees(double? value)
    {
        return value * (180 / Math.Pow(2, 31));
    }
    
}