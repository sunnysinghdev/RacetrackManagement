import unittest
from os import listdir
from os.path import isfile, join
import geektrust
from src.command import CommandFactory
class TestCommand(unittest.TestCase):
    inputs = []
    sample_dir = "sample_input"
    def setUp(self):
        self.inputs = []
        onlyfiles = [f for f in listdir(self.sample_dir) if isfile(join(self.sample_dir, f))]
        for f in onlyfiles:
            self.inputs.append(join(self.sample_dir, f))
    def test_case_1(self):
        #geektrust.main()
        expected = [
                "SUCCESS",
                "SUCCESS",
                "INVALID_ENTRY_TIME",
                "SUCCESS",
                "INVALID_EXIT_TIME",
                "590 0"
        ]
        inputFilePath = self.inputs[1]
        f = open(inputFilePath, 'r')
        Lines = f.readlines()
        f.close()
        for idx, cmdLine in enumerate(Lines):
            cmdLine = cmdLine.replace("\n","")
            cmd = CommandFactory.get(cmdLine)
            val = cmd.execute()
            #print(f"{cmdLine} {val}")
            #self.assertEqual("", cmd.execute(), cmdLine)
            #self.assertEqual(expected[idx], cmd.execute(), cmdLine)
    

if __name__ == '__main__':
    unittest.main()