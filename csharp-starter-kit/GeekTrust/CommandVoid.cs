using System;

namespace GeekTrust
{
    class VoidCommand : Command
    {
        public VoidCommand()
        {
        }
        public override string Execute()
        {
            return "";
        }
    }
    
}