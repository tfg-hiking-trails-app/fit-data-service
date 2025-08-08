using FitDataService.Domain.Interfaces;
using FitDataService.Domain.Models;

namespace FitDataService.Infrastructure.Data.Repositories;

public class FitFileDataRepository : AbstractRepository<FitFileData>, IFitFileDataRepository
{
    public FitFileDataRepository(MongoDbContext context) : base(context.GetCollection<FitFileData>())
    {
    }
    
}