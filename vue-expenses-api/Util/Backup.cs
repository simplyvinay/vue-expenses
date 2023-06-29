using System;
using System.IO;

public class Backup{
    public Backup(){

    }

    public static void CreateBackup(string expensesFilePath){
        string backupDirectory = "App_Data/backups";
        if (!Directory.Exists(backupDirectory))
        {
            Directory.CreateDirectory(backupDirectory);
        }

        expensesFilePath = expensesFilePath.Replace("Data Source=", "");

        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

        string backupFilePath = Path.Combine(backupDirectory, $"{timestamp}.expenses.db");

        File.Copy(expensesFilePath, backupFilePath, true);

        int maxBackups = 5;
        string[] backupFiles = Directory.GetFiles(backupDirectory, "*.expenses.db");

        if (backupFiles.Length > maxBackups)
        {
            Array.Sort(backupFiles);
            for (int i = 0; i < backupFiles.Length - maxBackups; i++)
            {
                File.Delete(backupFiles[i]);
            }
        }
    }
}