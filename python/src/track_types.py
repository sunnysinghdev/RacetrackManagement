
from __future__ import annotations
from src.literals import Literals
from typing import TYPE_CHECKING
if TYPE_CHECKING:
    from src.vehicle_types import Vehicle


class Track:

    #vehicles: list[Vehicle] = []
    MAX_VEHICLE: int
    COST_PER_HOUR: int
    
    def __init__(self):
        self.vehicles: list[Vehicle] = []

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