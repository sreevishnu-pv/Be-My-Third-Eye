import os
import sys
print(os.getcwd())
print(sys.argv)
fo = open("../../../"+sys.argv[1]+".txt", "r")
print(fo.read())
fo.close()
