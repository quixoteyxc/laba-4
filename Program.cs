using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Укажите путь к каталогу в качестве аргумента командной строки.");
                return;
            }

            string directoryPath = args[0];
            long totalSize = CalculateDirectorySize(directoryPath);

            Console.WriteLine($"Общий объем файлов в каталоге: {FormatBytes(totalSize)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }


    static long CalculateDirectorySize(string directoryPath)
    {
        long totalSize = 0;

        DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);

        foreach (var file in directoryInfo.GetFiles())
        {
            totalSize += file.Length;
        }

        foreach (var subDirectory in directoryInfo.GetDirectories())
        {
            totalSize += CalculateDirectorySize(subDirectory.FullName);
        }

        return totalSize;
    }

    static string FormatBytes(long bytes)
    {
        string[] suffixes = { "B", "KB", "MB", "GB", "TB" };

        int suffixIndex = 0;
        double size = bytes;

        while (size >= 1024 && suffixIndex < suffixes.Length - 1)
        {
            size /= 1024;
            suffixIndex++;
        }

        return $"{size:0.##} {suffixes[suffixIndex]}";
    }
}