using System;
using System.Collections.Generic;

namespace GeekTrust
{
    public class TrackRecord
    {
        Dictionary<string, Track> TrackDictionary;
        public TrackRecord()
        {
            TrackDictionary = new Dictionary<string, Track>(){
                {Literals.BIKE, new BikeRegularTrack(4, 60)},
                {Literals.CAR, new CarRegularTrack(2, 120)},
                {Literals.SUV, new SUVRegularTrack(2, 200)},
                {Literals.VIP_CAR, new CarVIPTrack(1, 250)},
                {Literals.VIP_SUV, new SUVVIPTrack(1, 300)},

            };
        }
        public Track GetTrack(Vehicle vehicle)
        {
            string vehicleType = vehicle.GetType().Name.ToUpper();
            switch (vehicleType)
            {
                case Literals.BIKE:
                    return GetRegularTrack(vehicle);
                case Literals.CAR:
                case Literals.SUV:
                    return GetRegularOrUpgradedTrack(vehicle);
                default:
                    throw new Exception(Literals.INVALID_ENTRY_TIME);
            }
        }
        private Track GetRegularTrack(Vehicle vehicle)
        {
            string vehicleType = vehicle.GetType().Name.ToUpper();
            return TrackDictionary[vehicleType];
        }
        private Track GetRegularOrUpgradedTrack(Vehicle vehicle)
        {
            var track = GetRegularTrack(vehicle);
            if (track.IsTrackFull(vehicle))
                track = UpgradeTrack(vehicle);
            return track;
        }
        private Track UpgradeTrack(Vehicle vehicle)
        {
            string vehicleType = vehicle.GetType().Name.ToUpper();
            if (vehicleType == Literals.CAR)
                return TrackDictionary[Literals.VIP_CAR];
            if (vehicleType == Literals.SUV)
                return TrackDictionary[Literals.VIP_SUV];
            return null;
        }

    }
}
