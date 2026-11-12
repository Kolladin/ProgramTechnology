namespace GuessTheNumber;

internal class GameStatistics
{
    public int Wins { get; set; } = 0;
    public int Losses { get; set; } = 0;
    public int BestAttempts { get; set; } = 0;

    public void AddWin(int attempts)
    {
        Wins++;

        if (BestAttempts == 0 || attempts < BestAttempts)
            BestAttempts = attempts;
    }

    public void AddLoss()
    {
        Losses++;
    }
}
