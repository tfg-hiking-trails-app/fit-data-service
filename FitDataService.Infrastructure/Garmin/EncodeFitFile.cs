using Dynastream.Fit;
using FitDataService.Domain.Interfaces;
using FitDateTime = Dynastream.Fit.DateTime;
using ActivityModel = FitDataService.Domain.Models.Activity;
using FileIdModel = FitDataService.Domain.Models.FileId;
using FitFileDataModel = FitDataService.Domain.Models.FitFileData;
using LapModel = FitDataService.Domain.Models.Lap;
using RecordModel = FitDataService.Domain.Models.Record;
using SessionModel = FitDataService.Domain.Models.Session;

namespace FitDataService.Infrastructure.Garmin;

/// <summary>
/// Re-encodes a previously decoded and stored <see cref="FitFileDataModel"/> (BSON document)
/// back into a binary FIT Activity File using the Garmin FIT SDK. It is the inverse of
/// <see cref="DecodeFitFile"/>: the original .fit is deleted after decoding, so the document
/// persisted in MongoDB is the single source of truth for regenerating the file on demand.
/// </summary>
public class EncodeFitFile : IFitFileEncoder
{
    public byte[] Encode(FitFileDataModel fitFileData)
    {
        using MemoryStream stream = new MemoryStream();

        Encode encoder = new Encode(ProtocolVersion.V20);
        encoder.Open(stream);

        // The FileId message must be the first message of every FIT file.
        encoder.Write(CreateFileIdMesg(fitFileData.FileId));

        // Records hold the per-second track samples; they precede the lap/session summaries.
        encoder.Write(fitFileData.Records.Select(CreateRecordMesg));
        encoder.Write(fitFileData.Laps.Select(CreateLapMesg));
        encoder.Write(fitFileData.Sessions.Select(CreateSessionMesg));

        // The Activity message summarises the file and is conventionally the last one.
        encoder.Write(CreateActivityMesg(fitFileData.Activity));

        encoder.Close();

        return stream.ToArray();
    }

    private FileIdMesg CreateFileIdMesg(FileIdModel fileId)
    {
        FileIdMesg mesg = new FileIdMesg();

        mesg.SetType((Dynastream.Fit.File?) fileId.Type);
        mesg.SetManufacturer((ushort?) fileId.Manufacturer);
        mesg.SetProduct((ushort?) fileId.Product);
        mesg.SetSerialNumber((uint?) fileId.SerialNumber);
        mesg.SetNumber((ushort?) fileId.Number);

        if (fileId.TimeCreated.HasValue)
            mesg.SetTimeCreated(new FitDateTime(fileId.TimeCreated.Value));

        if (!string.IsNullOrEmpty(fileId.ProductName))
            mesg.SetProductName(fileId.ProductName);

        return mesg;
    }

    private ActivityMesg CreateActivityMesg(ActivityModel activity)
    {
        ActivityMesg mesg = new ActivityMesg();

        if (activity.Timestamp.HasValue)
            mesg.SetTimestamp(new FitDateTime(activity.Timestamp.Value));

        mesg.SetTotalTimerTime((float?) activity.TotalTimerTime);
        mesg.SetNumSessions((ushort?) activity.NumSessions);
        mesg.SetType((Dynastream.Fit.Activity?) activity.Type);
        mesg.SetEvent((Event?) activity.Event);
        mesg.SetEventType((EventType?) activity.EventType);
        mesg.SetEventGroup((byte?) activity.EventGroup);

        if (activity.LocalTimestamp.HasValue)
            mesg.SetLocalTimestamp(new FitDateTime(activity.LocalTimestamp.Value).GetTimeStamp());

        return mesg;
    }

    private SessionMesg CreateSessionMesg(SessionModel session)
    {
        SessionMesg mesg = new SessionMesg();

        mesg.SetMessageIndex((ushort?) session.MessageIndex);
        mesg.SetTimestamp(new FitDateTime(session.Timestamp));
        mesg.SetEvent((Event?) session.Event);
        mesg.SetEventType((EventType?) session.EventType);
        mesg.SetStartTime(new FitDateTime(session.StartTime));
        mesg.SetStartPositionLat((int?) session.StartPositionLat);
        mesg.SetStartPositionLong((int?) session.StartPositionLong);
        mesg.SetSport((Sport?) session.Sport);
        mesg.SetSubSport((SubSport?) session.SubSport);
        mesg.SetTotalElapsedTime((float?) session.TotalElapsedTime);
        mesg.SetTotalTimerTime((float?) session.TotalTimerTime);
        mesg.SetTotalDistance((float?) session.TotalDistance);
        mesg.SetTotalCycles((uint?) session.TotalCycles);
        mesg.SetTotalStrides((uint?) session.TotalStrides);
        mesg.SetTotalStrokes((uint?) session.TotalStrokes);
        mesg.SetTotalCalories((ushort?) session.TotalCalories);
        mesg.SetTotalFatCalories((ushort?) session.TotalFatCalories);
        mesg.SetAvgSpeed((float?) session.AvgSpeed);
        mesg.SetMaxSpeed((float?) session.MaxSpeed);
        mesg.SetAvgHeartRate((byte?) session.AvgHeartRate);
        mesg.SetMaxHeartRate((byte?) session.MaxHeartRate);
        mesg.SetMinHeartRate((byte?) session.MinHeartRate);
        mesg.SetAvgCadence((byte?) session.AvgCadence);
        mesg.SetAvgRunningCadence((byte?) session.AvgRunningCadence);
        mesg.SetMaxCadence((byte?) session.MaxCadence);
        mesg.SetMaxRunningCadence((byte?) session.MaxRunningCadence);
        mesg.SetAvgPower((ushort?) session.AvgPower);
        mesg.SetMaxPower((ushort?) session.MaxPower);
        mesg.SetTotalAscent((ushort?) session.TotalAscent);
        mesg.SetTotalDescent((ushort?) session.TotalDescent);
        mesg.SetAvgAltitude(session.AvgAltitude);
        mesg.SetMaxAltitude(session.MaxAltitude);
        mesg.SetMinAltitude(session.MinAltitude);
        mesg.SetTotalTrainingEffect((float?) session.TotalTrainingEffect);
        mesg.SetFirstLapIndex((ushort?) session.FirstLapIndex);
        mesg.SetNumLaps((ushort?) session.NumLaps);
        mesg.SetEventGroup((byte?) session.EventGroup);
        mesg.SetTrigger((SessionTrigger?) session.Trigger);
        mesg.SetNecLat((int?) session.NecLat);
        mesg.SetNecLong((int?) session.NecLong);
        mesg.SetSwcLat((int?) session.SwcLat);
        mesg.SetSwcLong((int?) session.SwcLong);
        mesg.SetNumLengths((ushort?) session.NumLengths);
        mesg.SetNormalizedPower((ushort?) session.NormalizedPower);
        mesg.SetTrainingStressScore((float?) session.TrainingStressScore);
        mesg.SetIntensityFactor((float?) session.IntensityFactor);
        mesg.SetLeftRightBalance((ushort?) session.LeftRightBalance);
        mesg.SetEndPositionLat((int?) session.EndPositionLat);
        mesg.SetEndPositionLong((int?) session.EndPositionLong);

        return mesg;
    }

    private LapMesg CreateLapMesg(LapModel lap)
    {
        LapMesg mesg = new LapMesg();

        mesg.SetTimestamp(new FitDateTime(lap.Timestamp));

        if (lap.StartTime.HasValue)
            mesg.SetStartTime(new FitDateTime(lap.StartTime.Value));

        mesg.SetTotalElapsedTime((float?) lap.TotalElapsedTime);
        mesg.SetTotalTimerTime((float?) lap.TotalTimerTime);
        mesg.SetTotalDistance((float?) lap.TotalDistance);
        mesg.SetTotalCalories((ushort?) lap.TotalCalories);
        mesg.SetAvgSpeed((float?) lap.AvgSpeed);
        mesg.SetMaxSpeed((float?) lap.MaxSpeed);
        mesg.SetAvgHeartRate((byte?) lap.AvgHeartRate);
        mesg.SetMaxHeartRate((byte?) lap.MaxHeartRate);
        mesg.SetAvgCadence((byte?) lap.AvgCadence);
        mesg.SetMaxCadence((byte?) lap.MaxCadence);
        mesg.SetAvgPower((ushort?) lap.AvgPower);
        mesg.SetMaxPower((ushort?) lap.MaxPower);
        mesg.SetTotalAscent((ushort?) lap.TotalAscent);
        mesg.SetTotalDescent((ushort?) lap.TotalDescent);
        mesg.SetIntensity((Intensity?) lap.Intensity);
        mesg.SetLapTrigger((LapTrigger?) lap.LapTrigger);
        mesg.SetSport((Sport?) lap.Sport);
        mesg.SetAvgTemperature((sbyte?) lap.AvgTemperature);
        mesg.SetMaxTemperature((sbyte?) lap.MaxTemperature);

        return mesg;
    }

    private RecordMesg CreateRecordMesg(RecordModel record)
    {
        RecordMesg mesg = new RecordMesg();

        mesg.SetTimestamp(new FitDateTime(record.Timestamp));
        mesg.SetPositionLat((int?) record.PositionLat);
        mesg.SetPositionLong((int?) record.PositionLong);
        mesg.SetAltitude((float?) record.Altitude);
        mesg.SetHeartRate((byte?) record.HeartRate);
        mesg.SetCadence((byte?) record.Cadence);
        mesg.SetDistance((float?) record.Distance);
        mesg.SetSpeed((float?) record.Speed);
        mesg.SetPower((ushort?) record.Power);
        mesg.SetTemperature((sbyte?) record.Temperature);
        mesg.SetGpsAccuracy((byte?) record.GpsAccuracy);
        mesg.SetCalories((ushort?) record.Calories);
        mesg.SetVerticalSpeed((float?) record.VerticalSpeed);

        return mesg;
    }
}
