namespace FitDataService.Domain.Exceptions;

public class NotAnHikingTrailActivityException : Exception
{
    public NotAnHikingTrailActivityException() : base("Not an Hiking Trail Activity")
    {
    }
}
