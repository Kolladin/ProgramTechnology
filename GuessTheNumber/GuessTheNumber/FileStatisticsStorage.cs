
namespace GuessTheNumber;

internal class FileStatisticsStorage : IStatisticsStorage
{
    private string filePath = "statistics.txt";


    public void Save(GameStatistics stats)
    {
        try
        {
            string[] lines =
            {
            stats.Wins.ToString(),
            stats.Losses.ToString(),
            stats.BestAttempts.ToString()
        };

            File.WriteAllLines(filePath, lines);
        }
        catch (IOException)
        {
            Console.WriteLine("Error saving statistics!");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Access denied when saving statistics!");
        }
    }



    public GameStatistics Load()
    {
        GameStatistics stats = new GameStatistics();

        try
        {
            if (!File.Exists(filePath))
                return stats;

            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length != 3)
                return stats;

            if (int.TryParse(lines[0], out int wins) &&
                int.TryParse(lines[1], out int losses) &&
                int.TryParse(lines[2], out int best) &&
                wins >= 0 && losses >= 0 && best >= 0)
            {
                stats.Wins = wins;
                stats.Losses = losses;
                stats.BestAttempts = best;
            }
        }
        catch (IOException)
        {
            Console.WriteLine("Error loading statistics!");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Access denied when loading statistics!");
        }

        return stats;
    }

}
