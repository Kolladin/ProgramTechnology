namespace GuessTheNumber;

internal class SimpleHintProvider : IHintProvider
{
    public string GetHint(int result)
    {
        if (result == 1)
            return "Too high!";

        return "Too low!";
    }
}