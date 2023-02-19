using System;

namespace GeekTrust
{
    class Bike : Vehicle
    {
        public Bike(string name, TimeSpan entryTime)
        {
            Name = name;
            EntryTime = entryTime;
        }
    }
}