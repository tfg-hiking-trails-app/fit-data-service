using System.Diagnostics;
using Dynastream.Fit;
using FitDataService.Domain.Interfaces;

namespace FitDataService.Infrastructure.Garmin;

public class FitParser : IFitParser
{
    private readonly IFitFileDataRepository _fitFileDataRepository;

    public FitParser(IFitFileDataRepository fitFileDataRepository)
    {
        _fitFileDataRepository = fitFileDataRepository;
    }
    
    public void ReadFitFile(string filePath)
    {
        Stopwatch stopwatch = new Stopwatch();
        Console.WriteLine(_fitFileDataRepository.GetAllAsync());
        
        stopwatch.Start();
        
        using (FileStream fitSource = new FileStream(filePath, FileMode.Open))
        {
            Decode decoder = new Decode();
            FitListener fitListener = new FitListener();
            
            decoder.MesgEvent += fitListener.OnMesg;

            if (!decoder.IsFIT(fitSource))
                throw new FileNotFoundException("File is not FIT file", filePath);
            
            if (!decoder.CheckIntegrity(fitSource))
                throw new FileLoadException("FIT File is not pass verification integrity", filePath);

            Console.WriteLine("Decoding...");
            decoder.Read(fitSource);
            
            FitMessages fitMessages = fitListener.FitMessages;
            
            Console.WriteLine("Fit messages...");
            
            foreach (FileIdMesg mesg in fitMessages.FileIdMesgs)
            {
                //PrintFileIdMesg(mesg);
            }
            foreach (ActivityMesg mesg in fitMessages.ActivityMesgs)
            {
                //PrintMonitoringMesg(mesg);
            }
            foreach (SessionMesg mesg in fitMessages.SessionMesgs)
            {
                //Console.WriteLine("Sport: {0}, Total calories: {1}, Fat calories: {2}, Is hiking?: {3}", 
               //     mesg.GetSport(), mesg.GetTotalCalories(), mesg.GetTotalFatCalories(), 
               //     mesg.GetSport().Equals(Sport.Hiking) ? "Yes" : "No");
            }

            Console.WriteLine(fitMessages.LapMesgs.Count);
            foreach (LapMesg mesg in fitMessages.LapMesgs)
            {
                Console.WriteLine(mesg.GetTimestamp().ToString());
            }
            foreach (RecordMesg mesg in fitMessages.RecordMesgs)
            {
                //Console.WriteLine("{0}", mesg.GetTimestamp().ToString());
                //Console.WriteLine("\nDistance: {0}, HearthRate: {1}, Longitude: {2}, Latitude: {3}, Calories: {4}", 
                //    mesg.GetDistance(), mesg.GetHeartRate(), mesg.GetPositionLong(), mesg.GetPositionLat(), mesg.GetCalories());
                //PrintRecordMesg(mesg);
            }
            
        }
        
        Console.WriteLine("Decoded FIT file {0}", filePath);
        stopwatch.Stop();
        
        Console.WriteLine("Elapsed time: {0}", stopwatch.Elapsed);
    }
    
    public static void PrintFileIdMesg(FileIdMesg mesg)
    {
        Console.WriteLine("File ID:");

        if (mesg.GetType() != null)
        {
            Console.Write("   Type: ");
            Console.WriteLine(mesg.GetType()!.Value);
        }

        if (mesg.GetManufacturer() != null)
        {
            Console.Write("   Manufacturer: ");
            Console.WriteLine(mesg.GetManufacturer());
        }

        if (mesg.GetProductNameAsString() != null)
        {
            Console.Write("   Product: ");
            Console.WriteLine(mesg.GetProductNameAsString());
        }

        if (mesg.GetSerialNumber() != null)
        {
            Console.Write("   Serial Number: ");
            Console.WriteLine(mesg.GetSerialNumber());
        }

        if (mesg.GetNumber() != null)
        {
            Console.Write("   Number: ");
            Console.WriteLine(mesg.GetNumber());
        }
    }
    
    public static void PrintUserProfileMesg(UserProfileMesg mesg)
    {
        Console.WriteLine("User profile:");

        if (mesg.GetFriendlyNameAsString() != null)
            Console.WriteLine("\tFriendlyName: \"{0}\"", mesg.GetFriendlyNameAsString());

        if (mesg.GetGender() != null)
            Console.WriteLine("\tGender: {0}", mesg.GetGender().ToString());

        if (mesg.GetAge() != null)
            Console.WriteLine("\tAge: {0}", mesg.GetAge());

        if (mesg.GetWeight() != null)
            Console.WriteLine("\tWeight:  {0}", mesg.GetWeight());
    }
    
    public static void PrintDeviceInfoMesg(DeviceInfoMesg mesg)
    {
        Console.WriteLine("Device info:");
        if (mesg.GetTimestamp() != null)
        {
            Console.WriteLine("\tTimestamp: {0}", mesg.GetTimestamp().ToString());
        }

        if (mesg.GetBatteryStatus() != null)
        {
            Console.Write("\tBattery Status: ");

            switch (mesg.GetBatteryStatus())
            {
                case BatteryStatus.Critical:
                    Console.WriteLine("Critical");
                    break;
                case BatteryStatus.Good:
                    Console.WriteLine("Good");
                    break;
                case BatteryStatus.Low:
                    Console.WriteLine("Low");
                    break;
                case BatteryStatus.New:
                    Console.WriteLine("New");
                    break;
                case BatteryStatus.Ok:
                    Console.WriteLine("OK");
                    break;
                default:
                    Console.WriteLine("Invalid");
                    break;
            }
        }
    }
    
    public static void PrintMonitoringMesg(MonitoringMesg mesg)
    {
        Console.WriteLine("Monitoring:");
        if (mesg.GetTimestamp() != null)
        {
            Console.WriteLine("\tTimestamp: {0}", mesg.GetTimestamp().ToString());
        }

        if (mesg.GetActivityType() != null)
        {
            Console.WriteLine("\tActivityType: {0}", mesg.GetActivityType());
            switch (mesg.GetActivityType()) // Cycles is a dynamic field
            {
                case ActivityType.Walking:
                case ActivityType.Running:
                    Console.WriteLine("\tSteps: {0}", mesg.GetSteps());
                    break;
                case ActivityType.Cycling:
                case ActivityType.Swimming:
                    Console.WriteLine("\tStrokes: {0}", mesg.GetStrokes());
                    break;
                default:
                    Console.WriteLine("\tCycles: {0}", mesg.GetCycles());
                    break;
            }
        }
    }
    
    public static void PrintRecordMesg(RecordMesg mesg)
    {
        Console.WriteLine("Record:");

        PrintFieldWithOverrides(mesg, RecordMesg.FieldDefNum.HeartRate);
        PrintFieldWithOverrides(mesg, RecordMesg.FieldDefNum.Cadence);
        PrintFieldWithOverrides(mesg, RecordMesg.FieldDefNum.Speed);
        PrintFieldWithOverrides(mesg, RecordMesg.FieldDefNum.Distance);
        PrintFieldWithOverrides(mesg, RecordMesg.FieldDefNum.EnhancedAltitude);

        PrintDeveloperFields(mesg);
    }
    
    private static void PrintDeveloperFields(Mesg mesg)
    {
        foreach (var devField in mesg.DeveloperFields)
        {
            if (devField.GetNumValues() <= 0)
            {
                continue;
            }

            if (devField.IsDefined)
            {
                Console.Write("\t{0}", devField.Name);

                if (devField.Units != null)
                {
                    Console.Write(" [{0}]", devField.Units);
                }
                Console.Write(": ");
            }
            else
            {
                Console.Write("\tUndefined Field: ");
            }

            Console.Write("{0}", devField.GetValue(0));
            for (int i = 1; i < devField.GetNumValues(); i++)
            {
                Console.Write(",{0}", devField.GetValue(i));
            }

            Console.WriteLine();
        }
    }
    
    private static void PrintFieldWithOverrides(Mesg mesg, byte fieldNumber)
    {
        Field profileField = Profile.GetField(mesg.Num, fieldNumber);
        bool nameWritten = false;

        if (null == profileField)
        {
            return;
        }

        IEnumerable<FieldBase> fields = mesg.GetOverrideField(fieldNumber);

        foreach (FieldBase field in fields)
        {
            if (!nameWritten)
            {
                Console.WriteLine("   {0}", profileField.GetName());
                nameWritten = true;
            }

            if (field is Field)
                Console.WriteLine("      native: {0}", field.GetValue());
            else
                Console.WriteLine("      override: {0}", field.GetValue());
        }
    }
    
}