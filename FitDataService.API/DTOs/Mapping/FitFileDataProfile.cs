using AutoMapper;
using FitDataService.API.DTOs.RecordData;
using FitDataService.Application.DTOs;
using FitDataService.Application.DTOs.RecordData;

namespace FitDataService.API.DTOs.Mapping;

public class FitFileDataProfile : Profile
{
    public FitFileDataProfile()
    {
        CreateMap<ActivityDto, ActivityEntityDto>().ReverseMap();
        CreateMap<FileIdDto, FiledIdEntityDto>().ReverseMap();
        CreateMap<LapDto, LapEntityDto>().ReverseMap();
        CreateMap<RecordDto, RecordEntityDto>().ReverseMap();
        CreateMap<SessionDto, SessionEntityDto>().ReverseMap();
        CreateMap<CoordinateDto, CoordinateEntityDto>().ReverseMap();
        CreateMap<GraphicsDataDto, GraphicsDataEntityDto>().ReverseMap();
    }
}