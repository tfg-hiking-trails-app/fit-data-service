using System.Collections.ObjectModel;
using Dynastream.Fit;
using FitDataService.Domain.Models;
using FitDataService.Infrastructure.Exceptions;
using Activity = FitDataService.Domain.Models.Activity;
using DateTime = System.DateTime;

namespace FitDataService.Infrastructure.Garmin;

public class DecodeFitFile
{
    public FileId CreateFileId(ReadOnlyCollection<FileIdMesg> collection)
    {
        if (collection.Count == 0)
            throw new ArgumentException("Collection File Id is empty", nameof(collection));
        
        FileIdMesg fileIdMesg = collection[0];
        
        return new FileId
        {
            Type = fileIdMesg.GetType().HasValue 
                ? GetEnumValue(fileIdMesg.GetType()) 
                : throw new Exception("The fit file must have an associated file type."),
            Manufacturer = fileIdMesg.GetManufacturer(),
            Product = fileIdMesg.GetProduct(),
            FaveroProduct = fileIdMesg.GetFaveroProduct(),
            GarminProduct = fileIdMesg.GetGarminProduct(),
            SerialNumber = fileIdMesg.GetSerialNumber(),
            TimeCreated = fileIdMesg.GetTimeCreated().GetDateTime(),
            Number = fileIdMesg.GetNumber(),
            ProductName = fileIdMesg.GetProductNameAsString()
        };
    }

    public Activity CreateActivity(ReadOnlyCollection<ActivityMesg> collection)
    {
        if (collection.Count != 1)
            throw new ArgumentException("Collection Activity must have 1 item", nameof(collection));
        
        ActivityMesg activityMesg = collection[0];

        return new Activity
        {
            Timestamp = activityMesg.GetTimestamp().GetDateTime(),
            TotalTimerTime = activityMesg.GetTotalTimerTime(),
            NumSessions = activityMesg.GetNumSessions(),
            Type = GetEnumValue(activityMesg.GetType()),
            Event = GetEnumValue(activityMesg.GetEvent()),
            EventType = GetEnumValue(activityMesg.GetEventType()),
            LocalTimestamp = activityMesg.GetLocalTimestamp().HasValue 
                ? new Dynastream.Fit.DateTime((uint) activityMesg.GetLocalTimestamp()!).GetDateTime()
                : null,
            EventGroup = activityMesg.GetEventGroup(),
        };
    }

    public List<Session> CreateSessions(ReadOnlyCollection<SessionMesg> collection)
    {
        if (collection.Count == 0)
            throw new ArgumentException("Collection Session is empty", nameof(collection));

        List<Session> sessions = collection.Select(sessionMesg => new Session
            {
                MessageIndex = sessionMesg.GetMessageIndex(),
                Timestamp = sessionMesg.GetTimestamp().GetDateTime(),
                Event = GetEnumValue(sessionMesg.GetEvent()),
                EventType = GetEnumValue(sessionMesg.GetEventType()),
                StartTime = sessionMesg.GetStartTime().GetDateTime(),
                StartPositionLat = sessionMesg.GetStartPositionLat(),
                StartPositionLong = sessionMesg.GetStartPositionLong(),
                Sport = GetEnumValue(sessionMesg.GetSport()),
                SubSport = GetEnumValue(sessionMesg.GetSubSport()),
                TotalElapsedTime = Convert.ToDouble(sessionMesg.GetTotalElapsedTime()),
                TotalTimerTime = Convert.ToDouble(sessionMesg.GetTotalTimerTime()),
                TotalDistance = sessionMesg.GetTotalDistance(),
                TotalCycles = sessionMesg.GetTotalCycles(),
                TotalStrides = sessionMesg.GetTotalStrides(),
                TotalStrokes = sessionMesg.GetTotalStrokes(),
                TotalCalories = sessionMesg.GetTotalCalories(),
                TotalFatCalories = sessionMesg.GetTotalFatCalories(),
                AvgSpeed = sessionMesg.GetAvgSpeed(),
                MaxSpeed = sessionMesg.GetMaxSpeed(),
                AvgHeartRate = sessionMesg.GetAvgHeartRate(),
                MaxHeartRate = sessionMesg.GetMaxHeartRate(),
                MinHeartRate = sessionMesg.GetMinHeartRate(),
                AvgCadence = sessionMesg.GetAvgCadence(),
                AvgRunningCadence = sessionMesg.GetAvgRunningCadence(),
                MaxCadence = sessionMesg.GetMaxCadence(),
                MaxRunningCadence = sessionMesg.GetMaxRunningCadence(),
                AvgPower = sessionMesg.GetAvgPower(),
                MaxPower = sessionMesg.GetMaxPower(),
                TotalAscent = sessionMesg.GetTotalAscent(),
                TotalDescent = sessionMesg.GetTotalDescent(),
                AvgAltitude = sessionMesg.GetAvgAltitude(),
                MaxAltitude = sessionMesg.GetMaxAltitude(),
                MinAltitude = sessionMesg.GetMinAltitude(),
                TotalTrainingEffect = sessionMesg.GetTotalTrainingEffect(),
                FirstLapIndex = sessionMesg.GetFirstLapIndex(),
                NumLaps = sessionMesg.GetNumLaps(),
                EventGroup = sessionMesg.GetEventGroup(),
                Trigger = GetEnumValue(sessionMesg.GetTrigger()),
                NecLat = sessionMesg.GetNecLat(),
                NecLong = sessionMesg.GetNecLong(),
                SwcLat = sessionMesg.GetSwcLat(),
                SwcLong = sessionMesg.GetSwcLong(),
                NumLengths = sessionMesg.GetNumLengths(),
                NormalizedPower = sessionMesg.GetNormalizedPower(),
                TrainingStressScore = sessionMesg.GetTrainingStressScore(),
                IntensityFactor = sessionMesg.GetIntensityFactor(),
                LeftRightBalance = sessionMesg.GetLeftRightBalance(),
                EndPositionLat = sessionMesg.GetEndPositionLat(),
                EndPositionLong = sessionMesg.GetEndPositionLong()
            })
            .Where(session => session.Sport is not null && session.Sport == (int) Sport.Hiking)
            .ToList();

        if (sessions.Count == 0)
            throw new NotAnHikingTrailActivityException();
        
        return sessions;
    }
    
    public List<Lap> CreateLaps(ReadOnlyCollection<LapMesg> collection)
    {
        return collection.Select(lapMesg => new Lap
            {
                Timestamp = lapMesg.GetTimestamp().GetDateTime(),
                StartTime = lapMesg.GetStartTime().GetDateTime(),
                TotalElapsedTime = lapMesg.GetTotalElapsedTime(),
                TotalTimerTime = lapMesg.GetTotalTimerTime(),
                TotalDistance = lapMesg.GetTotalDistance(),
                TotalCalories = lapMesg.GetTotalCalories(),
                AvgSpeed = lapMesg.GetAvgSpeed(),
                MaxSpeed = lapMesg.GetMaxSpeed(),
                AvgHeartRate = lapMesg.GetAvgHeartRate(),
                MaxHeartRate = lapMesg.GetMaxHeartRate(),
                AvgCadence = lapMesg.GetAvgCadence(),
                MaxCadence = lapMesg.GetMaxCadence(),
                AvgPower = lapMesg.GetAvgPower(),
                MaxPower = lapMesg.GetMaxPower(),
                TotalAscent = lapMesg.GetTotalAscent(),
                TotalDescent = lapMesg.GetTotalDescent(),
                Intensity = GetEnumValue(lapMesg.GetIntensity()),
                LapTrigger = GetEnumValue(lapMesg.GetLapTrigger()),
                Sport = GetEnumValue(lapMesg.GetSport()),
                AvgTemperature = lapMesg.GetAvgTemperature(),
                MaxTemperature = lapMesg.GetMaxTemperature(),
            })
            .ToList();
    }
    
    public List<Record> CreateRecords(ReadOnlyCollection<RecordMesg> collection)
    {
        if (collection.Count == 0)
            throw new ArgumentException("Collection Record is empty", nameof(collection));

        return collection.Select(recordMesg => new Record
            {
                Timestamp = recordMesg.GetTimestamp().GetDateTime(),
                PositionLat = recordMesg.GetPositionLat(),
                PositionLong = recordMesg.GetPositionLong(),
                Altitude = recordMesg.GetAltitude(),
                HeartRate = recordMesg.GetHeartRate(),
                Cadence = recordMesg.GetCadence(),
                Distance = recordMesg.GetDistance(),
                Speed = recordMesg.GetSpeed(),
                Power = recordMesg.GetPower(),
                Temperature = recordMesg.GetTemperature(),
                GpsAccuracy = recordMesg.GetGpsAccuracy(),
                Calories = recordMesg.GetCalories(),
                VerticalSpeed = recordMesg.GetVerticalSpeed(),
                
            }).ToList();
    }
    
    private int? GetEnumValue<TEnum>(TEnum? param) where TEnum : struct, Enum
    {
        if (!param.HasValue)
            return null;
        
        return Convert.ToInt32(param.Value);
    }
    
}