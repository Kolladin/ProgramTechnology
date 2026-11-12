namespace GuessTheNumber;

internal class Game
{
    public event Action<int>? GuessChecked;
    public event Action<bool, int>? GameFinished;
    public void Finish(bool isWin, int attempts)
    {
        GameFinished?.Invoke(isWin, attempts);
    }

    private GameSettings settings;

    public int SecretNumber { get; }

    public Game(GameSettings settings)
    {
        this.settings = settings;
        SecretNumber = Random.Shared.Next(settings.MinNumber, settings.MaxNumber + 1);
    }

    public int MaxAttempts
    {
        get { return settings.MaxAttempts; }
    }

    public int CheckGuess(int number)
    {
        GuessChecked?.Invoke(number);

        if (number > SecretNumber)
            return 1;

        if (number < SecretNumber)
            return -1;

        return 0;
    }
    public void Finish(bool isWin)
    {
        GameFinished?.Invoke(isWin);
    }



}