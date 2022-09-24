using System;

namespace GeekTrust
{
    class VehicleFactory
    {
        public static Vehicle Get(string vehicleType, string vehicleName, TimeSpan entryTime)
        {
            switch (vehicleType)
            {
                case Literals.BIKE:
                    return new Bike(vehicleName, entryTime);
                case Literals.CAR:
                    return new Car(vehicleName, entryTime);
                case Literals.SUV:
                    return new Suv(vehicleName, entryTime);
                default:
                    break;
            }
            throw new Exception(Literals.INVALID_ENTRY_TIME);

        }
    }
    public abstract class Vehicle
    {
        public string Name { get; set; }
        public TimeSpan EntryTime { get; set; }
        public TimeSpan AdditionalTime { get; set; }

        public TimeSpan ExitTime
        {
            get
            {
                return AdditionalTime == default(TimeSpan) ? EntryTime + RaceTrackManager.BookingDuration : AdditionalTime;
            }
        }
        public Track Track { get; set; }
        public void Print()
        {
            var t = this.GetType().Name;
            var track = this.Track.GetType().Name;
            Console.WriteLine($"{t} {track} {Name} {EntryTime} {AdditionalTime} {ExitTime} - Cost = {GetCost()}");
        }
        public int GetCost()
        {
            return Track.COST_PER_HOUR * RaceTrackManager.BookingDuration.Hours + RaceTrackManager.AdditionalCost * getFactor();
        }
        private int getFactor()
        {
            int factor = 0;
            var duration = ExitTime - EntryTime;
            if (duration.Hours == RaceTrackManager.BookingDuration.Hours)
            {
                if (duration.Minutes > 15)
                {
                    factor = 1;
                }
            }
            else if (duration.Hours > RaceTrackManager.BookingDuration.Hours)
            {
                factor = duration.Hours - RaceTrackManager.BookingDuration.Hours;
                if (duration.Minutes > 0)
                {
                    factor += 1;
                }
            }
            return factor;
        }
    }
    class Bike : Vehicle
    {
        public Bike(string name, TimeSpan entryTime)
        {
            Name = name;
            EntryTime = entryTime;
        }
    }
    class Car : Vehicle
    {
        public Car(string name, TimeSpan entryTime)
        {
            Name = name;
            EntryTime = entryTime;
        }
    }
    class Suv : Vehicle
    {
        public Suv(string name, TimeSpan entryTime)
        {
            Name = name;
            EntryTime = entryTime;
        }
    }
}