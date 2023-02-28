from datetime import timedelta
from src.literals import Literals
from src.race_track_manager import RaceTrackManager
from src.utility import Utility
from src.vehicle_types import VehicleFactory


class CommandFactory:
    def get(cmdline):
        commands = cmdline.split(" ")
        command = commands[0]
        if command == Literals.BOOK:
            return BookCommand(commands[1], commands[2], commands[3])
        elif command == Literals.ADDITIONAL:
            return AdditionalCommand(commands[1], commands[2])
        elif command == Literals.REVENUE:
            return RevenueCommand()
        #return commands[0]
        return VoidCommand()

class Command:
    name: str
    def execute(self):
        pass

class VoidCommand(Command):
    def __init__(self):
        pass
    def execute(self):
        return ""

class BookCommand(Command):

    def __init__(self, vehicleType, vehicleName, entryTimeStr):
        self.name = Literals.BOOK
        #self._vehicleType = vehicleType
        entryTime = Utility.parse_time(entryTimeStr)
        self._vehicle = VehicleFactory.get(vehicleType, vehicleName, entryTime)
    
    def execute(self):
        try:
            ##raise Exception("EX")
            RaceTrackManager.Add(self._vehicle)
            return Literals.SUCCESS
        
        except Exception as ex:
            return ex.args[0]

class AdditionalCommand(Command):
    
    _vehicleName: str
    _additionalTime: timedelta
    def __init__(self, vehicleName, additionalTime):
        self._vehicleName = vehicleName
        self._additionalTime = Utility.parse_time(additionalTime)
    
    def execute(self):
        try:
            RaceTrackManager.AdditionalTime(self._vehicleName, self._additionalTime)
            return Literals.SUCCESS
        except Exception as ex:
            return ex.args[0]
        
        
    

class RevenueCommand(Command):

    def __init__(self):
        pass
    def execute(self):
        try:
            return RaceTrackManager.GetRevenue()
        except Exception as ex:
            return ex.args[0]
            
        
    