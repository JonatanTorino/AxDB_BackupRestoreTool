using System;
using System.Diagnostics;

namespace AxDB_BackupRestoreTool
{
    internal class BackupDB
    {
        private string WorkDirectory;
        private string BacthFileName;
        public BackupDB(string workDirectory, string bacthFileName)
        {
            this.WorkDirectory = workDirectory;
            this.BacthFileName = bacthFileName;
        }

        internal void Run()
        {
            Console.WriteLine("\nComienza el backup\n");
            this.runProcessInfo();
            Console.WriteLine("\nFinalizado el backup\n");
        }

        internal void runProcessInfo()
        {
            ProcessStartInfo startInfo = new();
            startInfo.WorkingDirectory = this.WorkDirectory;
            startInfo.FileName = this.BacthFileName;
            startInfo.Arguments = $"\"\" {WorkDirectory} \"\"";

            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = true;

            Process process = Process.Start(startInfo);
            process.WaitForExit();
        }
    }
}