using System;

namespace GeekTrust
{
    class Suv : Vehicle
    {
        public Suv(string name, TimeSpan entryTime)
        {
            Name = name;
            EntryTime = entryTime;
        }
    }
}