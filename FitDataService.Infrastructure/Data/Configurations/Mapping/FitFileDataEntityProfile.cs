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
        CreateMap<RecordEntityDto, Record>().ReverseMap();
        CreateMap<SessionEntityDto, Session>().ReverseMap();
        CreateMap<CoordinateEntityDto, Record>().ReverseMap();
        CreateMap<GraphicsDataEntityDto, Record>().ReverseMap();
    }
}