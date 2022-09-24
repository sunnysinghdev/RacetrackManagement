using System;
using System.Collections;
using System.Collections.Generic;
namespace GeekTrust
{
    public abstract class Track
    {
        public List<Vehicle> vehicles = new List<Vehicle>();
        protected int MAX_VEHICLE;
        public int COST_PER_HOUR;
        public bool IsFull()
        {
            return vehicles.Count >= MAX_VEHICLE;
        }
        public bool IsTrackFull(Vehicle vehicle)
        {
            var v = vehicles.FindAll(v => vehicle.EntryTime >= v.EntryTime &&  vehicle.EntryTime < v.ExitTime || 
            vehicle.ExitTime > v.EntryTime && vehicle.ExitTime <= v.ExitTime);
            if (v.Count >= MAX_VEHICLE)
            {
                return true;
            }
            return false;
        }
        public void TryAdd(Vehicle vehicle)
        {
            if (IsTrackFull(vehicle))
            {
                throw new Exception(Literals.RACETRACK_FULL);
            }
            else
            {
                if (IsFull())
                {
                    var removeVehicle = vehicles.Find(v => v.ExitTime <= vehicle.EntryTime);
                    if (removeVehicle == null)
                    {
                        throw new Exception(Literals.RACETRACK_FULL);
                    }
                    vehicles.Remove(removeVehicle);
                }
                vehicle.Track = this;
                vehicles.Add(vehicle);
            }


        }
        public void Add(Vehicle vehicle)
        {
            TryAdd(vehicle);
            // if (!IsFull())
            // {
            //     vehicle.Track = this;
            //     vehicles.Add(vehicle);
            // }
            // else
            // {
            //     throw new Exception(Literals.RACETRACK_FULL);
            // }
        }
    }
    class RegularTrack : Track { }
    class VIPTrack : Track { }
    class BikeRegularTrack : RegularTrack
    {
        public BikeRegularTrack(int maxVehicle, int costPerHour)
        {
            MAX_VEHICLE = maxVehicle;
            COST_PER_HOUR = costPerHour;
        }
    }
    class CarRegularTrack : RegularTrack
    {
        public CarRegularTrack(int maxVehicle, int costPerHour)
        {
            MAX_VEHICLE = maxVehicle;
            COST_PER_HOUR = costPerHour;
        }
    }
    class SUVRegularTrack : RegularTrack
    {
        public SUVRegularTrack(int maxVehicle, int costPerHour)
        {
            MAX_VEHICLE = maxVehicle;
            COST_PER_HOUR = costPerHour;
        }
    }

    class CarVIPTrack : VIPTrack
    {
        public CarVIPTrack(int maxVehicle, int costPerHour)
        {
            MAX_VEHICLE = maxVehicle;
            COST_PER_HOUR = costPerHour;
        }
    }
    class SUVVIPTrack : VIPTrack
    {
        public SUVVIPTrack(int maxVehicle, int costPerHour)
        {
            MAX_VEHICLE = maxVehicle;
            COST_PER_HOUR = costPerHour;
        }
    }

}