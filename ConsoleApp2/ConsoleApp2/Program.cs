using System;
using System.Collections.Generic;
using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using System.Diagnostics;
using System.IO;
namespace ConsoleApp2
{
    class Program
    {
        private static void Main()
        {
            RunPythonFromCS New = new RunPythonFromCS();
            New.run_cmd(@"C:\Users\t-prgaur\AppData\Local\Programs\Python\Python37-32\python.exe","../../../abc.py mytextFile");
        }
    }
    
}
