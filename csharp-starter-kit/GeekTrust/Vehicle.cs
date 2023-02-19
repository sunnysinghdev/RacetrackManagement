using System;

namespace GeekTrust
{
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

        public int GetCost()
        {
            return Track.COST_PER_HOUR * RaceTrackManager.BookingDuration.Hours + RaceTrackManager.AdditionalCost * getFactor();
        }
        private int getFactor()
        {
            int factor = 0;
            var duration = ExitTime - EntryTime;
            if (duration.Hours == RaceTrackManager.BookingDuration.Hours && duration.Minutes > 15)
            {
                factor = 1;
            }
            else if (duration.Hours > RaceTrackManager.BookingDuration.Hours && duration.Minutes > 0)
            {
                factor = duration.Hours - RaceTrackManager.BookingDuration.Hours;
                factor += 1;
            }
            return factor;
        }
       
    }

}