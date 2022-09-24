using System;
using System.Collections.Generic;

namespace GeekTrust
{
    public static class RaceTrackManager
    {
        static public TimeSpan StartTime;
        static public TimeSpan EndTime;
        static public TimeSpan BookingDuration;
        static public int AdditionalCost = 50;
        static public List<Vehicle> vehicles;
        static Dictionary<string, Track> TrackDictionary = new Dictionary<string, Track>();
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
            TrackDictionary[Literals.BIKE] = new BikeRegularTrack(4, 60);
            TrackDictionary[Literals.CAR] = new CarRegularTrack(2, 120);
            TrackDictionary[Literals.SUV] = new SUVRegularTrack(2, 200);
            TrackDictionary[Literals.VIP_CAR] = new CarVIPTrack(1, 250);
            TrackDictionary[Literals.VIP_SUV] = new SUVVIPTrack(1, 300);
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
            foreach(Vehicle v in vehicles)
            {
                if(v.Track is RegularTrack)
                {
                    regularRevenue += v.GetCost();
                }
                else if (v.Track is VIPTrack)
                {
                    VIPRevenue += v.GetCost();
                }
            }
            return $"{regularRevenue} {VIPRevenue}";
        }
        public static Vehicle FindVehicle(string vehicleName)
        {
            return vehicles.Find(v => v.Name == vehicleName);
        }
        public static void AdditionalTime(string vehicleName, TimeSpan additionalTime)
        {
            var vehicle = RaceTrackManager.FindVehicle(vehicleName);
            if(IsInTrackTime(additionalTime) && vehicle.ExitTime < additionalTime)
            {
                vehicle.AdditionalTime = additionalTime;
            }
            else
            {
                //vehicle.Print();
                throw new Exception(Literals.INVALID_EXIT_TIME);
            }
        }
        public static bool IsInTrackTime(TimeSpan ts)
        {
            return ts >= StartTime && ts <= EndTime;
        }
        public static bool IsValidTrackTime(TimeSpan ts)
        {
            return ts >= StartTime && ts <= EndTime - BookingDuration;
        }
        public static void Add(Vehicle vehicle)
        {
            string vehicleType = vehicle.GetType().Name.ToUpper();
            var track = RaceTrackManager.GetTrack(vehicle);
            if (IsValidTrackTime(vehicle.EntryTime))
            {
                track.Add(vehicle);
                vehicles.Add(vehicle);
            }
            else 
            {
                throw new Exception(Literals.INVALID_ENTRY_TIME);
            }
        }
        public static Track GetTrack(Vehicle vehicle)
        {
            string vehicleType = vehicle.GetType().Name.ToUpper();
            switch (vehicleType)
            {
                case Literals.BIKE:
                    return TrackDictionary[Literals.BIKE];
                case Literals.CAR:
                    var track = TrackDictionary[Literals.CAR];
                    if (track.IsTrackFull(vehicle))
                    {
                        //Upgrade
                        track = TrackDictionary[Literals.VIP_CAR];
                    }
                    return track;
                case Literals.SUV:
                    var trackSuv = TrackDictionary[Literals.SUV];
                    if (trackSuv.IsTrackFull(vehicle))
                    {
                        //Upgrade
                        trackSuv = TrackDictionary[Literals.VIP_SUV];
                    }
                    return trackSuv;
                default:
                    throw new Exception(Literals.INVALID_ENTRY_TIME);
            }
        }
    }

}