using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace ThirdEye.Services.Services
{
    public class PythonProcessingService
    {
        string pythonPath = ConfigurationManager.AppSettings["PythonPath"];

        public string ExecuteExe()
        {
            string result = string.Empty;
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = pythonPath;//cmd is full path to python.exe
            start.Arguments = "ThirdEyeFinal.py";//args is path to .py file and any cmd line args
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                     result = reader.ReadToEnd();
                }
            }
            return result;
        }
    }
}
