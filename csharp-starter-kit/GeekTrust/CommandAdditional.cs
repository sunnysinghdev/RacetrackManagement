using System;

namespace GeekTrust
{
    class AdditionalCommand : Command
    {
        private string _vehicleName;
        private TimeSpan _additionalTime;
        public AdditionalCommand(string vehicleName, string additionalTime)
        {
            _vehicleName = vehicleName;
            _additionalTime = TimeSpan.Parse(additionalTime);
        }
        public override string Execute()
        {
            try
            {
                RaceTrackManager.AdditionalTime(_vehicleName, _additionalTime);
                return Literals.SUCCESS;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}