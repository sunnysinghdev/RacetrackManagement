using System;

namespace GeekTrust
{
    class Car : Vehicle
    {
        public Car(string name, TimeSpan entryTime)
        {
            Name = name;
            EntryTime = entryTime;
        }
    }
}