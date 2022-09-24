using System;

namespace GeekTrust
{
    public class CommandFactory
    {
        public static Command Get(string commandLine)
        {
            var parameters = commandLine.Split(' ');
            switch (parameters[0])
            {
                case Literals.BOOK:
                    return new BookCommand(parameters[1], parameters[2], parameters[3]);
                case Literals.ADDITIONAL:
                    return new AdditionalCommand(parameters[1], parameters[2]);
                case Literals.REVENUE:
                    return new RevenueCommand();
                default:
                    break;
            }
            return new VoidCommand();
        }

    }
    public abstract class Command
    {
        public string Name { get; set; }
        abstract public string Execute();
    }
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
    class VoidCommand : Command
    {
        public VoidCommand()
        {
            Name = "";
        }
        public override string Execute()
        {
            return "";
        }
    }
    class BookCommand : Command
    {
        private Vehicle _vehicle;
        private string _vehicleType;
        public BookCommand(string vehicleType, string vehicleName, string entryTimeStr)
        {
            Name = Literals.BOOK;
            _vehicleType = vehicleType;
            var entryTime = TimeSpan.Parse(entryTimeStr);
            _vehicle = VehicleFactory.Get(vehicleType, vehicleName, entryTime);
            //_vehicle.Print();

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