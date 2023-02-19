using System;
using System.Collections;
using System.Collections.Generic;
namespace GeekTrust
{
     class CarRegularTrack : RegularTrack
    {
        public CarRegularTrack(int maxVehicle, int costPerHour)
        {
            MAX_VEHICLE = maxVehicle;
            COST_PER_HOUR = costPerHour;
        }
    }
}