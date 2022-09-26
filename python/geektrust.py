from sys import argv
from src.command import CommandFactory
#from src.command import CommandFactory
from src.vehicle_types import RaceTrackManager

def main():
    
    """
    Sample code to read inputs from the file
    """
    if len(argv) != 2:
        raise Exception("File path not entered")
    file_path = argv[1]
    f = open(file_path, 'r')
    Lines = f.readlines()
    #Add your code here to process the input commands
    RaceTrackManager.Init()
    for cmdLine in Lines:
        cmdLine = cmdLine.replace("\n","")
        cmd = CommandFactory.get(cmdLine)
        print(f"{cmd.execute()}")


def add_magic(a, b):
    return a + b

if __name__ == "__main__":
    main()