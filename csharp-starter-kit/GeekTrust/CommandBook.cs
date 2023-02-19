using System;

namespace GeekTrust
{
    class BookCommand : Command
    {
        private Vehicle _vehicle;
        private string _vehicleType;
        public BookCommand(string vehicleType, string vehicleName, string entryTimeStr)
        {
            _vehicleType = vehicleType;
            var entryTime = TimeSpan.Parse(entryTimeStr);
            _vehicle = VehicleFactory.Get(vehicleType, vehicleName, entryTime);
        }
        public override string Execute()
        {
            try
            {
                RaceTrackManager.Add(_vehicle);
                return Literals.SUCCESS;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}