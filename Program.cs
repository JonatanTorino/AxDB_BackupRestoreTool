using System;
using System.IO;

namespace AxDB_BackupRestoreTool
{
    public record BackupFile(int Id, string Name);

    class Program
    {
        static string currDir;
        static string machineName;

        static void Main(string[] args)
        {
            InitializeVariable();
            var keyInput = SelectOption();
            
            int keyIntInput = 0;
            if (!int.TryParse(keyInput, out keyIntInput))
            {
                Console.WriteLine("¡Valor ingresado incorrecto!");
            }
            Console.WriteLine();

            if (keyIntInput == 1 || keyIntInput == 3)
            {
                new BackupDB(currDir, "Backup.bat").Run();
            }
            if (keyIntInput == 2 || keyIntInput == 3)
            {
                new RestoreDB(currDir, "Restore.bat").Run();
            }

            Console.WriteLine("\n\tFin del programa");
            Console.ReadKey();
        }

        private static void InitializeVariable()
        {
            currDir = Directory.GetCurrentDirectory();
            machineName = Environment.MachineName;

            Console.WriteLine("Variables de entorno");
            Console.WriteLine("--------------------");
            Console.WriteLine($"SQLServer: {machineName}");
            Console.WriteLine($"WorkDirectory: {currDir}");
            Console.WriteLine();
        }

        private static string SelectOption()
        {
            int positionTop = Console.CursorTop;
            Console.WriteLine($"{Console.CursorTop - positionTop}: CANCELAR");
            Console.WriteLine($"{Console.CursorTop - positionTop}: Backupear AxDB");
            Console.WriteLine($"{Console.CursorTop - positionTop}: Restaurar AxDB");
            Console.WriteLine($"{Console.CursorTop - positionTop}: Backupear y restaurar AxDB");

            Console.Write("Seleccionar una opción: ");
            var keyInput = Console.ReadLine();
            return keyInput;
        }
    }

}