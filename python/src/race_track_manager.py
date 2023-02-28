from datetime import timedelta
from src.literals import Literals
from src.settings import TrackSettings
from src.utility import Utility

from src.track_types import BikeRegularTrack, CarRegularTrack, CarVIPTrack, RegularTrack, SUVRegularTrack, SUVVIPTrack, Track, VIPTrack
from src.vehicle_types import Vehicle


class RaceTrackManager:    
    StartTime: timedelta
    EndTime: timedelta
    # BookingDuration: timedelta
    # AdditionalCost = 50
    vehicles: list[Vehicle]

    TrackDictionary: dict[str, Track] = {}
    # Dictionary<string, Track>()
    def Init():
    
        RaceTrackManager.StartTime = Utility.parse_time("13:00")
        RaceTrackManager.EndTime = Utility.parse_time("20:00")
        TrackSettings.AdditionalCost = 50
        TrackSettings.BookingDuration = timedelta(hours = 3)
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
        return ts >= RaceTrackManager.StartTime and ts <= RaceTrackManager.EndTime - TrackSettings.BookingDuration
    
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