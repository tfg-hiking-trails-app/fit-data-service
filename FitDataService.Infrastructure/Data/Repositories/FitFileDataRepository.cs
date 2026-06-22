using FitDataService.Domain.Interfaces;
using FitDataService.Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FitDataService.Infrastructure.Data.Repositories;

public class FitFileDataRepository : AbstractRepository<FitFileData>, IFitFileDataRepository
{
    public FitFileDataRepository(MongoDbContext context) : base(context.GetCollection<FitFileData>())
    {
    }

    public async Task<FitFileData?> GetByHikingTrailCodeAsync(Guid hikingTrailCode)
    {
        return await Collection
            .Find(data => data.HikingTrailCode == hikingTrailCode)
            .FirstOrDefaultAsync();
    }

    public async Task<FileId> GetFileIdByHikingTrailCodeAsync(Guid hikingTrailCode)
    {
        return await Collection
            .Find(data => data.HikingTrailCode == hikingTrailCode)
            .Project(data => data.FileId)
            .FirstOrDefaultAsync();
    }

    public async Task<Activity> GetActivityByHikingTrailCodeAsync(Guid hikingTrailCode)
    {
        return await Collection
            .Find(data => data.HikingTrailCode == hikingTrailCode)
            .Project(data => data.Activity)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Session>> GetSessionsByHikingTrailCodeAsync(Guid hikingTrailCode)
    {
        return await Collection
            .Find(data => data.HikingTrailCode == hikingTrailCode)
            .Project(data => data.Sessions)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Lap>> GetLapsByHikingTrailCodeAsync(Guid hikingTrailCode)
    {
        return await Collection
            .Find(data => data.HikingTrailCode == hikingTrailCode)
            .Project(data => data.Laps)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Record>> GetRecordsByHikingTrailCodeAsync(Guid hikingTrailCode)
    {
        return await Collection
            .Find(data => data.HikingTrailCode == hikingTrailCode)
            .Project(data => data.Records)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Record>> GetCoordinatesByHikingTrailCodeAsync(Guid hikingTrailCode)
    {
        return await Collection
            .Find(data => data.HikingTrailCode == hikingTrailCode &&
                          data.Records.Any(record => record.PositionLat != null && record.PositionLong != null))
            .Project(data => data.Records.Where(record => record.PositionLat != null && record.PositionLong != null))
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Record>> GetGraphicsDataByHikingTrailCodeAsync(Guid hikingTrailCode)
    {
        return await Collection
            .Find(data => data.HikingTrailCode == hikingTrailCode &&
                          data.Records.Any(record => record.Altitude != null && 
                                                     record.HeartRate != null &&
                                                     record.Cadence != null &&
                                                     record.Distance != null &&
                                                     record.Speed != null))
            .Project(data => data.Records.Where(record => record.Altitude != null && 
                                                          record.HeartRate != null &&
                                                          record.Cadence != null &&
                                                          record.Distance != null &&
                                                          record.Speed != null))
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Record>> GetAltitudesByHikingTrailCodeAsync(Guid hikingTrailCode)
    {
        return await Collection
            .Find(data => data.HikingTrailCode == hikingTrailCode &&
                          data.Records.Any(record => record.Altitude != null))
            .Project(data => data.Records.Where(record => record.Altitude != null))
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Record>> GetHeartRatesByHikingTrailCodeAsync(Guid hikingTrailCode)
    {
        return await Collection
            .Find(data => data.HikingTrailCode == hikingTrailCode &&
                          data.Records.Any(record => record.HeartRate != null))
            .Project(data => data.Records.Where(record => record.HeartRate != null))
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Record>> GetCadencesByHikingTrailCodeAsync(Guid hikingTrailCode)
    {
        return await Collection
            .Find(data => data.HikingTrailCode == hikingTrailCode &&
                          data.Records.Any(record => record.Cadence != null))
            .Project(data => data.Records.Where(record => record.Cadence != null))
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Record>> GetDistancesByHikingTrailCodeAsync(Guid hikingTrailCode)
    {
        return await Collection
            .Find(data => data.HikingTrailCode == hikingTrailCode &&
                          data.Records.Any(record => record.Distance != null))
            .Project(data => data.Records.Where(record => record.Distance != null))
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Record>> GetSpeedByHikingTrailCodeAsync(Guid hikingTrailCode)
    {
        return await Collection
            .Find(data => data.HikingTrailCode == hikingTrailCode &&
                          data.Records.Any(record => record.Speed != null))
            .Project(data => data.Records.Where(record => record.Speed != null))
            .FirstOrDefaultAsync();
    }
}