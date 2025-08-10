using AutoMapper;
using FitDataService.Application.DTOs;
using FitDataService.Application.DTOs.RecordData;
using FitDataService.Application.Interfaces;
using FitDataService.Domain.Interfaces;
using FitDataService.Domain.Models;

namespace FitDataService.Application.Services;

public class FitFileDataService : IFitFileDataService
{
    private readonly IMapper _mapper;
    private readonly IFitFileDataRepository _fitFileDataRepository;

    public FitFileDataService(
        IMapper mapper, 
        IFitFileDataRepository fitFileDataRepository)
    {
        _mapper = mapper;
        _fitFileDataRepository = fitFileDataRepository;
    }

    public async Task<FiledIdEntityDto> GetFileIdByHikingTrailCodeAsync(Guid hikingTrailCode)
    {
        FileId fileId = await _fitFileDataRepository.GetFileIdByHikingTrailCodeAsync(hikingTrailCode);
        
        return _mapper.Map<FiledIdEntityDto>(fileId);
    }

    public async Task<ActivityEntityDto> GetActivityByHikingTrailCodeAsync(Guid hikingTrailCode)
    {
        Activity activity = await _fitFileDataRepository.GetActivityByHikingTrailCodeAsync(hikingTrailCode);
        
        return _mapper.Map<ActivityEntityDto>(activity);
    }

    public async Task<IEnumerable<SessionEntityDto>> GetSessionsByHikingTrailCodeAsync(Guid hikingTrailCode)
    {
        IEnumerable<Session> sessions = await _fitFileDataRepository.GetSessionsByHikingTrailCodeAsync(hikingTrailCode);
        
        return _mapper.Map<IEnumerable<SessionEntityDto>>(sessions);
    }

    public async Task<IEnumerable<LapEntityDto>> GetLapsByHikingTrailCodeAsync(Guid hikingTrailCode)
    {
        IEnumerable<Lap> laps = await _fitFileDataRepository.GetLapsByHikingTrailCodeAsync(hikingTrailCode);
        
        return _mapper.Map<IEnumerable<LapEntityDto>>(laps);
    }

    public async Task<IEnumerable<RecordEntityDto>> GetRecordsByHikingTrailCodeAsync(Guid hikingTrailCode)
    {
        IEnumerable<Record> records = await _fitFileDataRepository.GetRecordsByHikingTrailCodeAsync(hikingTrailCode);
        
        return _mapper.Map<IEnumerable<RecordEntityDto>>(records);
    }

    public async Task<IEnumerable<CoordinateEntityDto>> GetCoordinatesByHikingTrailCodeAsync(Guid hikingTrailCode)
    {
        IEnumerable<Record> records = await _fitFileDataRepository.GetCoordinatesByHikingTrailCodeAsync(hikingTrailCode);
        
        return _mapper.Map<IEnumerable<CoordinateEntityDto>>(records);
    }

    public async Task<IEnumerable<GraphicsDataEntityDto>> GetGraphicsDataByHikingTrailCodeAsync(Guid hikingTrailCode)
    {
        IEnumerable<Record> records = await _fitFileDataRepository.GetGraphicsDataByHikingTrailCodeAsync(hikingTrailCode);
        
        return _mapper.Map<IEnumerable<GraphicsDataEntityDto>>(records);
    }

    public async Task<IEnumerable<GraphicsDataEntityDto>> GetAltitudesByHikingTrailCodeAsync(Guid hikingTrailCode)
    {
        IEnumerable<Record> altitudes = await _fitFileDataRepository.GetAltitudesByHikingTrailCodeAsync(hikingTrailCode);
        
        return _mapper.Map<IEnumerable<GraphicsDataEntityDto>>(altitudes);
    }

    public async Task<IEnumerable<GraphicsDataEntityDto>> GetHearthRatesByHikingTrailCodeAsync(Guid hikingTrailCode)
    {
        IEnumerable<Record> hearthRates = await _fitFileDataRepository.GetHearthRatesByHikingTrailCodeAsync(hikingTrailCode);
        
        return _mapper.Map<IEnumerable<GraphicsDataEntityDto>>(hearthRates);
    }

    public async Task<IEnumerable<GraphicsDataEntityDto>> GetCadencesByHikingTrailCodeAsync(Guid hikingTrailCode)
    {
        IEnumerable<Record> cadences = await _fitFileDataRepository.GetCadencesByHikingTrailCodeAsync(hikingTrailCode);
        
        return _mapper.Map<IEnumerable<GraphicsDataEntityDto>>(cadences);
    }

    public async Task<IEnumerable<GraphicsDataEntityDto>> GetDistancesByHikingTrailCodeAsync(Guid hikingTrailCode)
    {
        IEnumerable<Record> distances = await _fitFileDataRepository.GetDistancesByHikingTrailCodeAsync(hikingTrailCode);
        
        return _mapper.Map<IEnumerable<GraphicsDataEntityDto>>(distances);
    }

    public async Task<IEnumerable<GraphicsDataEntityDto>> GetSpeedByHikingTrailCodeAsync(Guid hikingTrailCode)
    {
        IEnumerable<Record> speeds = await _fitFileDataRepository.GetSpeedByHikingTrailCodeAsync(hikingTrailCode);
        
        return _mapper.Map<IEnumerable<GraphicsDataEntityDto>>(speeds);
    }
}