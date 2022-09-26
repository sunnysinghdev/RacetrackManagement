from __future__ import annotations
from datetime import timedelta
from src.literals import Literals
from src.utility import Utility
#####################################################################



class RaceTrackManager:    
    StartTime: timedelta
    EndTime: timedelta
    BookingDuration: timedelta
    AdditionalCost = 50
    vehicles: list[Vehicle]

    TrackDictionary: dict[str, Track] = {}
    # Dictionary<string, Track>()
    def Init():
    
        RaceTrackManager.StartTime = Utility.parse_time("13:00")
        RaceTrackManager.EndTime = Utility.parse_time("20:00")
        RaceTrackManager.AdditionalCost = 50
        RaceTrackManager.BookingDuration = timedelta(hours = 3)
        RaceTrackManager.TrackDictionary[Literals.BIKE] = BikeRegularTrack(4, 60)
        RaceTrackManager.TrackDictionary[Literals.CAR] = CarRegularTrack(2, 120)
        RaceTrackManager.TrackDictionary[Literals.SUV] = SUVRegularTrack(2, 200)
        RaceTrackManager.TrackDictionary[Literals.VIP_CAR] = CarVIPTrack(1, 250)
        RaceTrackManager.TrackDictionary[Literals.VIP_SUV] = SUVVIPTrack(1, 300)
        RaceTrackManager.vehicles = []
    
    def Reset():
        RaceTrackManager.Init()
    
    def GetRevenue():
    
        regularRevenue = 0
        VIPRevenue = 0
        for v in RaceTrackManager.vehicles:
        
            if isinstance(v.track, RegularTrack):
                regularRevenue = regularRevenue + v.get_cost()
            elif isinstance(v.track,  VIPTrack):
                VIPRevenue = VIPRevenue + v.get_cost()
        return f"{regularRevenue} {VIPRevenue}"
    
    def FindVehicle(vehicleName):
        for v in RaceTrackManager.vehicles:
            if v.name == vehicleName:
                return v
        return None
    
    def AdditionalTime(vehicleName, additionalTime: timedelta):
    
        vehicle = RaceTrackManager.FindVehicle(vehicleName)
        if(RaceTrackManager.IsInTrackTime(additionalTime) and vehicle.exitTime() < additionalTime):
            vehicle.additionalTime = additionalTime
        else:
            raise Exception(Literals.INVALID_EXIT_TIME)
        
    
    def IsInTrackTime(ts: timedelta):
        return ts >= RaceTrackManager.StartTime and ts <= RaceTrackManager.EndTime
    
    def IsValidTrackTime(ts: timedelta):
        return ts >= RaceTrackManager.StartTime and ts <= RaceTrackManager.EndTime - RaceTrackManager.BookingDuration
    
    def Add(vehicle: Vehicle):
        vehicleType = type(vehicle).__name__.upper()
        track: Track = RaceTrackManager.GetTrack(vehicle)
        if (RaceTrackManager.IsValidTrackTime(vehicle.entryTime)):
            track.add(vehicle)
            RaceTrackManager.vehicles.append(vehicle)
        else:
            raise Exception(Literals.INVALID_ENTRY_TIME)
    def GetTrack(vehicle: Vehicle):
        vehicleType = type(vehicle).__name__.upper()
        if vehicleType == Literals.BIKE:
            return RaceTrackManager.TrackDictionary[Literals.BIKE]
        elif vehicleType == Literals.CAR:
            #return RaceTrackManager.TrackDictionary[Literals.CAR]
            track: Track = RaceTrackManager.TrackDictionary[Literals.CAR]
            if track.is_track_full(vehicle):
                track = RaceTrackManager.TrackDictionary[Literals.VIP_CAR]
            return track

        elif vehicleType == Literals.SUV:
            suvtrack: Track = RaceTrackManager.TrackDictionary[Literals.SUV]
            if suvtrack.is_track_full(vehicle):
                suvtrack = RaceTrackManager.TrackDictionary[Literals.VIP_SUV]
            return suvtrack
        else:
            raise Exception(Literals.INVALID_ENTRY_TIME)
#####################################################################
class VehicleFactory:
    def get(vehicleType, vehicleName, entryTime):
        if vehicleType == Literals.BIKE:
            return Bike(vehicleName, entryTime)
        elif vehicleType == Literals.CAR:
            return Car(vehicleName, entryTime)
        elif vehicleType == Literals.SUV:
            return Suv(vehicleName, entryTime)
        raise Exception(Literals.INVALID_ENTRY_TIME)

#####################################################################       
    
class Vehicle:
    name: str 
    entryTime: timedelta 
    additionalTime = timedelta(hours=0)
    track:  Track
    def exitTime(self):
        if self.additionalTime == timedelta(hours=0):
            return self.entryTime + RaceTrackManager.BookingDuration
        else: 
            return self.additionalTime
            
    def print(self):
        #t = type(self).__name__
        ltrack = type(self.track).__name__
        print(f" {ltrack} {self.name} {self.entryTime} {self.additionalTime} {self.exitTime()}")
        
    def get_cost(self):
        return self.track.COST_PER_HOUR * Utility.get_hours(RaceTrackManager.BookingDuration) + RaceTrackManager.AdditionalCost * self.get_factor()
        
    def get_factor(self):
        factor: int = 0
        try:
            duration = self.exitTime() - self.entryTime
            if Utility.get_hours(duration) == Utility.get_hours(RaceTrackManager.BookingDuration):
                if Utility.get_minutes(duration) > 15:
                    factor = 1
            elif Utility.get_hours(duration) > Utility.get_hours(RaceTrackManager.BookingDuration):
                factor = Utility.get_hours(duration) - Utility.get_hours(RaceTrackManager.BookingDuration)                
                if Utility.get_minutes(duration) > 0:
                    factor += 1
        except Exception as ex:
            pass       
        return factor 
    

class Bike(Vehicle):
    def __init__(self, name, entryTime):
        self.name = name
        self.entryTime = entryTime


class Car(Vehicle):
    def __init__(self, name, entryTime):
        self.name = name
        self.entryTime = entryTime
    

class Suv(Vehicle):
    def __init__(self, name, entryTime):
        self.name = name
        self.entryTime = entryTime

##################################################

class Track:

    vehicles: list[Vehicle] = []
    MAX_VEHICLE: int
    COST_PER_HOUR: int
    
    def is_full(self):
        return len(self.vehicles) >= self.MAX_VEHICLE
    
    def is_track_full(self, vehicle: Vehicle):
        vehiclesInTrack: list[Vehicle] = []
        for v in self.vehicles:
            if vehicle.entryTime >= v.entryTime and vehicle.entryTime < v.exitTime() or \
                vehicle.exitTime() > v.entryTime and vehicle.exitTime() <= v.exitTime():
                vehiclesInTrack.append(v)

        #var v = vehicles.FindAll(v => vehicle.EntryTime >= v.EntryTime &&  vehicle.EntryTime < v.ExitTime || 
        #vehicle.ExitTime > v.EntryTime && vehicle.ExitTime <= v.ExitTime)
        if (len(vehiclesInTrack) >= self.MAX_VEHICLE):
            return True
        return False
    
    def __tryAdd(self, vehicle: Vehicle):
        if self.is_track_full(vehicle):
            raise Exception(Literals.RACETRACK_FULL)
        else:
            if self.is_full():
                removeVehicle: Vehicle = None
                for v in self.vehicles:
                    if v.exitTime() <= vehicle.entryTime:
                        removeVehicle = v
                        break
                #removeVehicle = self.vehicles.Find(v => v.ExitTime <= vehicle.EntryTime)
                if removeVehicle == None:
                    raise Exception(Literals.RACETRACK_FULL)
                
                self.vehicles.remove(removeVehicle)

            vehicle.track = self
            self.vehicles.append(vehicle)
    
    def add(self, vehicle: Vehicle):
        self.__tryAdd(vehicle)

class RegularTrack(Track):
    pass

class VIPTrack(Track):
    pass


class BikeRegularTrack(RegularTrack):
    def __init__(self, maxVehicle, costPerHour):
        self.vehicles = []
        self.MAX_VEHICLE = maxVehicle
        self.COST_PER_HOUR = costPerHour    

class CarRegularTrack(RegularTrack):
    def __init__(self, maxVehicle, costPerHour):
        self.vehicles = []
        self.MAX_VEHICLE = maxVehicle
        self.COST_PER_HOUR = costPerHour
    
class SUVRegularTrack(RegularTrack):
    def __init__(self, maxVehicle, costPerHour):
        self.vehicles = []
        self.MAX_VEHICLE = maxVehicle
        self.COST_PER_HOUR = costPerHour  


class CarVIPTrack(VIPTrack):
    def __init__(self, maxVehicle, costPerHour):
        self.vehicles = []
        self.MAX_VEHICLE = maxVehicle
        self.COST_PER_HOUR = costPerHour
    

class SUVVIPTrack(VIPTrack):
    def __init__(self, maxVehicle, costPerHour):
        self.vehicles = []
        self.MAX_VEHICLE = maxVehicle
        self.COST_PER_HOUR = costPerHour
    
