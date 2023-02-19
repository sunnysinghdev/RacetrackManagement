using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekTrust.Tests
{
    public class TestCase1
    {
        [SetUp]
        public void Test_Case_1_Init()
        {
            RaceTrackManager.Reset();
        }
        [Test]
        public void Test_Case1()
        {
            string[] commands = {
                "BOOK BIKE M40 14:00",
                "BOOK CAR O34 15:00",
                "BOOK SUV A66 11:00",
                "ADDITIONAL M40 17:40",
                "ADDITIONAL O34 20:50",
                "REVENUE"
            };
            string[] expected = {
                "SUCCESS",
                "SUCCESS",
                "INVALID_ENTRY_TIME",
                "SUCCESS",
                "INVALID_EXIT_TIME",
                "590 0"
            };
            int i = 0;
            foreach (var cmd in commands)
            {
                Command c = CommandFactory.Get(cmd);
                Console.WriteLine(cmd + " " + expected[i]);
                Assert.AreEqual(expected[i], c.Execute());
                i++;
            }
        }
    }

    public class TestCase2
    {
        [SetUp]
        public void Test_Case_2_Init()
        {
            RaceTrackManager.Reset();
        }
        [Test]
        public void Teast_Case2()
        {
            string[] commands = {
                "BOOK SUV XY4 12:30",
                "BOOK SUV A56 13:10",
                "BOOK CAR AB1 14:20",
                "BOOK BIKE BIK1 13:00",
                "BOOK BIKE BIK2 14:00",
                "ADDITIONAL BIK2 17:50",
                "REVENUE"
            };
            string[] expected = {
                "INVALID_ENTRY_TIME",
                "SUCCESS",
                "SUCCESS",
                "SUCCESS",
                "SUCCESS",
                "SUCCESS",
                "1370 0"
            };
            int i = 0;
            foreach (var cmd in commands)
            {
                Command c = CommandFactory.Get(cmd);
                Console.WriteLine(cmd + " " + expected[i]);
                Assert.AreEqual(expected[i], c.Execute());
                i++;
            }
        }
    }
    public class TestCase3
    {
        [SetUp]
        public void Test_Case_3_Init()
        {
            RaceTrackManager.Reset();
        }
        [Test]
        public void Test_Case_3()
        {

            string[] commands = {
                "BOOK SUV M40 14:00",
                "BOOK SUV O34 15:00",
                "BOOK SUV XY4 13:00",
                "BOOK SUV A56 13:10",
                "BOOK SUV AB1 14:20",
                "BOOK SUV S45 15:30",
                "BOOK SUV XY22 17:00",
                "BOOK SUV B56 18:00",
                "REVENUE"
            };
            string[] expected = {
                "SUCCESS",
                "SUCCESS",
                "SUCCESS",
                "RACETRACK_FULL",
                "RACETRACK_FULL",
                "RACETRACK_FULL",
                "SUCCESS",
                "INVALID_ENTRY_TIME",
                "1800 900"
            };
            int i = 0;
            foreach (var cmd in commands)
            {
                Command c = CommandFactory.Get(cmd);
                Assert.AreEqual(expected[i], c.Execute());
                i++;
            }

        }
    }
}

