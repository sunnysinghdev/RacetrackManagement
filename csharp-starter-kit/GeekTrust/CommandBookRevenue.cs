using System;

namespace GeekTrust
{
    class RevenueCommand : Command
    {
        public RevenueCommand()
        {
        }
        public override string Execute()
        {
            try
            {
                return RaceTrackManager.GetRevenue();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}