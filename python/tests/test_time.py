import unittest
from time import strptime
from datetime import timedelta
from src.command import BookCommand
class TestTime(unittest.TestCase):
    def test_case_parse(self):
        t1 = strptime("01:05","%H:%M")
        t2 = strptime("01:15","%H:%M")
        delta1 = timedelta( hours= t1.tm_hour, minutes= t1.tm_min)
        delta2 = timedelta(hours= t2.tm_hour, minutes= t2.tm_min)
        print((delta2 < delta1))
