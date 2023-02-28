from __future__ import annotations
from datetime import timedelta
from src.literals import Literals
from src.settings import TrackSettings
from src.track_types import Track
from src.utility import Utility
#####################################################################
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
            return self.entryTime + TrackSettings.BookingDuration
        else: 
            return self.additionalTime
            
    def print(self):
        #t = type(self).__name__
        ltrack = type(self.track).__name__
        print(f" {ltrack} {self.name} {self.entryTime} {self.additionalTime} {self.exitTime()}")
        
    def get_cost(self):
        return self.track.COST_PER_HOUR * Utility.get_hours(TrackSettings.BookingDuration) + TrackSettings.AdditionalCost * self.get_factor()
        
    def get_factor(self):
        factor: int = 0
        try:
            duration = self.exitTime() - self.entryTime
            if Utility.get_hours(duration) == Utility.get_hours(TrackSettings.BookingDuration):
                if Utility.get_minutes(duration) > 15:
                    factor = 1
            elif Utility.get_hours(duration) > Utility.get_hours(TrackSettings.BookingDuration):
                factor = Utility.get_hours(duration) - Utility.get_hours(TrackSettings.BookingDuration)                
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

    
