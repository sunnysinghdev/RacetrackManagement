using System;
using System.Collections.Generic;

namespace GeekTrust
{
    public static class RaceTrackManager
    {
        static private TimeSpan StartTime;
        static private TimeSpan EndTime;
        static public TimeSpan BookingDuration;
        static public int AdditionalCost = 50;
        static private List<Vehicle> vehicles;
        static TrackRecord _trackRecord;
        static RaceTrackManager()
        {
            Init();
        }
        private static void Init()
        {
            StartTime = TimeSpan.Parse("13:00");
            EndTime = TimeSpan.Parse("20:00");
            AdditionalCost = 50;
            BookingDuration = new TimeSpan(3, 0, 0);
            _trackRecord = new TrackRecord();
            vehicles = new List<Vehicle>();
        }
        public static void Reset()
        {
            Init();
        }
        public static string GetRevenue()
        {
            int regularRevenue = 0;
            int VIPRevenue = 0;
            foreach (Vehicle v in vehicles)
            {
                if (v.Track is RegularTrack)
                    regularRevenue += v.GetCost();
                else if (v.Track is VIPTrack)
                    VIPRevenue += v.GetCost();
            }
            return $"{regularRevenue} {VIPRevenue}";
        }

        public static void AdditionalTime(string vehicleName, TimeSpan additionalTime)
        {
            var vehicle = RaceTrackManager.FindVehicle(vehicleName);
            if (IsInTrackTime(additionalTime) && vehicle.ExitTime < additionalTime)
            {
                vehicle.AdditionalTime = additionalTime;
                return;
            }
            throw new Exception(Literals.INVALID_EXIT_TIME);
        }
        public static void Add(Vehicle vehicle)
        {
            string vehicleType = vehicle.GetType().Name.ToUpper();
            var track = RaceTrackManager._trackRecord.GetTrack(vehicle);
            if (IsValidTrackTime(vehicle.EntryTime))
            {
                track.Add(vehicle);
                vehicles.Add(vehicle);
                return;
            }
            throw new Exception(Literals.INVALID_ENTRY_TIME);
        }
        private static Vehicle FindVehicle(string vehicleName)
        {
            return vehicles.Find(v => v.Name == vehicleName);
        }
        private static bool IsInTrackTime(TimeSpan ts)
        {
            return ts >= StartTime && ts <= EndTime;
        }
        private static bool IsValidTrackTime(TimeSpan ts)
        {
            return ts >= StartTime && ts <= EndTime - BookingDuration;
        }

    }

}