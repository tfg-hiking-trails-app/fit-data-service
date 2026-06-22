using FitDataService.Domain.Models;

namespace FitDataService.Domain.Interfaces;

public interface IFitFileDataRepository : IRepository<FitFileData>
{
    Task<FitFileData?> GetByHikingTrailCodeAsync(Guid hikingTrailCode);
    Task<FileId> GetFileIdByHikingTrailCodeAsync(Guid hikingTrailCode);
    Task<Activity> GetActivityByHikingTrailCodeAsync(Guid hikingTrailCode);
    Task<IEnumerable<Session>> GetSessionsByHikingTrailCodeAsync(Guid hikingTrailCode);
    Task<IEnumerable<Lap>> GetLapsByHikingTrailCodeAsync(Guid hikingTrailCode);
    Task<IEnumerable<Record>> GetRecordsByHikingTrailCodeAsync(Guid hikingTrailCode);
    Task<IEnumerable<Record>> GetCoordinatesByHikingTrailCodeAsync(Guid hikingTrailCode);
    Task<IEnumerable<Record>> GetGraphicsDataByHikingTrailCodeAsync(Guid hikingTrailCode);
    Task<IEnumerable<Record>> GetAltitudesByHikingTrailCodeAsync(Guid hikingTrailCode);
    Task<IEnumerable<Record>> GetHeartRatesByHikingTrailCodeAsync(Guid hikingTrailCode);
    Task<IEnumerable<Record>> GetCadencesByHikingTrailCodeAsync(Guid hikingTrailCode);
    Task<IEnumerable<Record>> GetDistancesByHikingTrailCodeAsync(Guid hikingTrailCode);
    Task<IEnumerable<Record>> GetSpeedByHikingTrailCodeAsync(Guid hikingTrailCode);
}