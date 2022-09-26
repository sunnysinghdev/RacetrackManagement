from datetime import timedelta
import time


class Utility:
    def get_hours(timeDelta: timedelta):
        return int(timeDelta.seconds / 3600)

    def get_minutes(timeDelta: timedelta):
        return int(timeDelta.seconds / 60) % 60

    def parse_time(timeString):
        parsedTime = time.strptime(timeString, "%H:%M")
        return timedelta(hours=parsedTime.tm_hour, minutes=parsedTime.tm_min)
