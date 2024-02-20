using AssetRipper.Export.UnityProjects;
using AssetRipper.Export.UnityProjects.Configuration;
using System.Diagnostics;

Utils.ExportFrom("C:\\builds");
Utils.SearchFile("VAG");

public static class Utils
{
    public static void ExportFrom(string path)
    {
        var root = Directory.CreateDirectory("exported");

        var exportHandler = new ExportHandler(new LibraryConfiguration());

        foreach (var folder in Directory.GetDirectories(path))
        {
            Console.WriteLine($"Exporting {folder}");
            try
            {
                var gameData = exportHandler.LoadAndProcess([folder]);
                exportHandler.Export(gameData, "exported\\" + Path.GetFileName(folder));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed exporting folder {folder}");
                Console.WriteLine(ex.Message);
            }
        }

        Console.WriteLine($"Finishing exporting projects to path {root.FullName}");
        Process.Start(root.FullName);
    }

    public static void SearchFile(string contains)
    {
        foreach (var dir in Directory.GetDirectories("exported"))
        {
            Console.ForegroundColor = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories)
                            .Where(f => {
                                return f.Contains(contains);
                            }).Any() ? ConsoleColor.Red : ConsoleColor.Green;
            Console.WriteLine(Path.GetFileName(dir));
        }
        Console.ForegroundColor = ConsoleColor.White;
    }
}
