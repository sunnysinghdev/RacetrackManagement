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
}