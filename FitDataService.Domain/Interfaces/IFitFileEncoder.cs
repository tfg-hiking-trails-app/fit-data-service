using FitDataService.Domain.Models;

namespace FitDataService.Domain.Interfaces;

public interface IFitFileEncoder
{
    byte[] Encode(FitFileData fitFileData);
}
