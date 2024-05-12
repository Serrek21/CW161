using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("Оберіть операцію:");
        Console.WriteLine("1. Копіювання файлу");
        Console.WriteLine("2. Переміщення файлу");
        Console.WriteLine("3. Копіювання папки");
        Console.WriteLine("4. Переміщення папки");
        Console.Write("Введіть номер операції: ");

        int o;
        if (!int.TryParse(Console.ReadLine(), out o) || o < 1 || o > 4)
        {
            Console.WriteLine("Невірний ввід.");
            return;
        }

        switch (o)
        {
            case 1:
                CopyFile();
                break;
            case 2:
                MoveFile();
                break;
            case 3:
                CopyD();
                break;
            case 4:
                MoveD();
                break;
        }
    }

    static void CopyFile()
    {
        Console.WriteLine("Введіть шлях до оригінального файлу:");
        string sourceFP = Console.ReadLine();

        Console.WriteLine("Введіть шлях, куди потрібно скопіювати файл:");
        string FilePath1 = Console.ReadLine();

        try
        {
            File.Copy(sourceFP, FilePath1, true);
            Console.WriteLine("Файл скопійовано успішно!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка копіювання файлу: {ex.Message}");
        }
    }

    static void MoveFile()
    {
        Console.WriteLine("Введіть шлях до оригінального файлу:");
        string sourceFP = Console.ReadLine();

        Console.WriteLine("Введіть шлях, куди потрібно перемістити файл:");
        string FilePath1 = Console.ReadLine();

        try
        {
            File.Move(sourceFP, FilePath1);
            Console.WriteLine("Файл переміщено успішно!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка переміщення файлу: {ex.Message}");
        }
    }

    static void CopyD()
    {
        Console.WriteLine("Введіть шлях до початкової папки:");
        string sourceD = Console.ReadLine();

        Console.WriteLine("Введіть шлях, куди потрібно скопіювати папку:");
        string Directory1 = Console.ReadLine();

        try
        {
            CopyDR(sourceD, Directory1);
            Console.WriteLine("Папку скопійовано успішно!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка копіювання папки: {ex.Message}");
        }
    }

    static void MoveD()
    {
        Console.WriteLine("Введіть шлях до оригінальної папки:");
        string sourceD = Console.ReadLine();

        Console.WriteLine("Введіть шлях, куди потрібно перемістити папку:");
        string Directory1 = Console.ReadLine();

        try
        {
            MoveDR(sourceD, Directory1);
            Console.WriteLine("Папку переміщено успішно!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка переміщення папки: {ex.Message}");
        }
    }

    static void CopyDR(string sourceD, string Directory1)
    {
        if (!Directory.Exists(sourceD))
        {
            throw new DirectoryNotFoundException($"Початкова папка '{sourceD}' не існує.");
        }

        if (!Directory.Exists(Directory1))
        {
            Directory.CreateDirectory(Directory1);
        }

        string[] files = Directory.GetFiles(sourceD);
        foreach (string file in files)
        {
            string fileName = Path.GetFileName(file);
            string destFile = Path.Combine(Directory1, fileName);
            File.Copy(file, destFile, true);
        }

        string[] subDirectories = Directory.GetDirectories(sourceD);
        foreach (string subDir in subDirectories)
        {
            string subDirName = Path.GetFileName(subDir);
            string destSubDir = Path.Combine(Directory1, subDirName);
            CopyDR(subDir, destSubDir);
        }
    }

    static void MoveDR(string sourceDir, string destDir)
    {
        if (!Directory.Exists(sourceDir))
        {
            throw new DirectoryNotFoundException($"Початкова папка '{sourceDir}' не існує.");
        }

        if (!Directory.Exists(destDir))
        {
            Directory.CreateDirectory(destDir);
        }

        string[] files = Directory.GetFiles(sourceDir);
        foreach (string file in files)
        {
            string fileName = Path.GetFileName(file);
            string destFile = Path.Combine(destDir, fileName);
            File.Move(file, destFile);
        }

        string[] subDirectories = Directory.GetDirectories(sourceDir);
        foreach (string subDir in subDirectories)
        {
            string subDirName = Path.GetFileName(subDir);
            string destSubDir = Path.Combine(destDir, subDirName);
            MoveDR(subDir, destSubDir);
        }

        Directory.Delete(sourceDir);
    }
}