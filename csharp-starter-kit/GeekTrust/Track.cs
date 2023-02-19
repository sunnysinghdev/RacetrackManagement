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
        private bool IsFull()
        {
            return vehicles.Count >= MAX_VEHICLE;
        }
        public bool IsTrackFull(Vehicle vehicle)
        {
            var v = vehicles.FindAll(v => vehicle.EntryTime >= v.EntryTime && vehicle.EntryTime < v.ExitTime ||
            vehicle.ExitTime > v.EntryTime && vehicle.ExitTime <= v.ExitTime);
            return (v.Count >= MAX_VEHICLE) ? true : false;
        }
        private void TryAdd(Vehicle vehicle)
        {
            if (IsTrackFull(vehicle))
                throw new Exception(Literals.RACETRACK_FULL);

            if (IsFull())
                TryRemove(vehicle);

            vehicle.Track = this;
            vehicles.Add(vehicle);
        }
        private void TryRemove(Vehicle vehicle)
        {
            var removeVehicle = vehicles.Find(v => v.ExitTime <= vehicle.EntryTime);
            if (removeVehicle == null)
                throw new Exception(Literals.RACETRACK_FULL);
            vehicles.Remove(removeVehicle);
        }
        public void Add(Vehicle vehicle)
        {
            TryAdd(vehicle);
        }
    }

}