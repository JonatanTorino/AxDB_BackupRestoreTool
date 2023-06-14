using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AxDB_BackupRestoreTool
{
    internal class RestoreDB
    {
        private string WorkDirectory;
        private string BacthFileName;
        public RestoreDB(string workDirectory, string bacthFileName)
        {
            this.WorkDirectory = workDirectory;
            this.BacthFileName = bacthFileName;
        }

        public void Run()
        {
            Console.WriteLine("\nComienza el restore\n");

            var filesName = this.GetFilesCollection();
            var positionTop = Console.CursorTop;
            int fileSelected;
            do
            {
                ClearFromPosition(positionTop);
                fileSelected = this.SelectFile(filesName);
            } while (fileSelected < 0);

            if (fileSelected > 0)
            {
                this.RunProcessInfo(this.BacthFileName, filesName[fileSelected - 1]);
            }

            Console.WriteLine("\nFinalizado el restore\n");
        }

        private void ClearFromPosition(int positionTop)
        {
            var positionBottom = Console.CursorTop;
            for (var i = positionTop; i <= positionBottom; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new String(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, positionTop);
        }

        List<BackupFile> GetFilesCollection()
        {
            var dir = new DirectoryInfo(WorkDirectory);
            List<BackupFile> fileList = new List<BackupFile>();
            foreach (var file in dir.GetFiles("*.bak"))
            {
                fileList.Add(new BackupFile(fileList.Count + 1, file.Name));
            }

            return fileList;
        }

        int SelectFile(List<BackupFile> filesName)
        {
            Console.WriteLine($"Id 0: CANCELAR");
            foreach (var f in filesName)
            {
                Console.WriteLine($"Id {f.Id}: {f.Name}");
            }

            Console.Write("\tIntroduzca el nro de archivo a restaurar: ");

            int keyIntInput;
            bool isInt = int.TryParse(Console.ReadLine(), out keyIntInput);
            bool isBoundUpper = keyIntInput > filesName.Count;
            if (isInt == false || (keyIntInput > 0 && isBoundUpper))
            {
                keyIntInput = -1;
            }

            if (keyIntInput == -1)
            {
                Console.WriteLine("\tValor ingresado incorrecto");
                Console.ReadKey();
            }

            return keyIntInput;
        }

        private void RunProcessInfo(string bacthFileName, BackupFile backupFile)
        {

            ProcessStartInfo startInfo = new();
            startInfo.WorkingDirectory = this.WorkDirectory;
            startInfo.FileName = this.BacthFileName;
            startInfo.Arguments = $"\"\" {WorkDirectory} {backupFile.Name}";

            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = true;

            Process process = Process.Start(startInfo);
            process.WaitForExit();
        }
    }
}