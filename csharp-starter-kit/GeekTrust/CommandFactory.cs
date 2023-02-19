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
}