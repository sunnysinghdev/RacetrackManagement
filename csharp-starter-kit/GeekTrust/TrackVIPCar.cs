using System;
using System.Collections;
using System.Collections.Generic;
namespace GeekTrust
{
   class CarVIPTrack : VIPTrack
    {
        public CarVIPTrack(int maxVehicle, int costPerHour)
        {
            MAX_VEHICLE = maxVehicle;
            COST_PER_HOUR = costPerHour;
        }
    }
}