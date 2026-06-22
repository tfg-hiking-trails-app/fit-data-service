using FitDataService.Application.DTOs;
using FitDataService.Application.DTOs.RecordData;

namespace FitDataService.Application.Interfaces;

public interface IFitFileDataService
{
    Task<byte[]?> GetFitFileByHikingTrailCodeAsync(Guid hikingTrailCode);
    Task<FiledIdEntityDto> GetFileIdByHikingTrailCodeAsync(Guid hikingTrailCode);
    Task<ActivityEntityDto> GetActivityByHikingTrailCodeAsync(Guid hikingTrailCode);
    Task<IEnumerable<SessionEntityDto>> GetSessionsByHikingTrailCodeAsync(Guid hikingTrailCode);
    Task<IEnumerable<LapEntityDto>> GetLapsByHikingTrailCodeAsync(Guid hikingTrailCode);
    Task<IEnumerable<RecordEntityDto>> GetRecordsByHikingTrailCodeAsync(Guid hikingTrailCode);
    Task<IEnumerable<CoordinateEntityDto>> GetCoordinatesByHikingTrailCodeAsync(Guid hikingTrailCode);
    Task<IEnumerable<GraphicsDataEntityDto>> GetGraphicsDataByHikingTrailCodeAsync(Guid hikingTrailCode);
    Task<IEnumerable<GraphicsDataEntityDto>> GetAltitudesByHikingTrailCodeAsync(Guid hikingTrailCode);
    Task<IEnumerable<GraphicsDataEntityDto>> GetHeartRatesByHikingTrailCodeAsync(Guid hikingTrailCode);
    Task<IEnumerable<GraphicsDataEntityDto>> GetCadencesByHikingTrailCodeAsync(Guid hikingTrailCode);
    Task<IEnumerable<GraphicsDataEntityDto>> GetDistancesByHikingTrailCodeAsync(Guid hikingTrailCode);
    Task<IEnumerable<GraphicsDataEntityDto>> GetSpeedByHikingTrailCodeAsync(Guid hikingTrailCode);
}