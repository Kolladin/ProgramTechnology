namespace GuessTheNumber;

internal class DetailedHintProvider : IHintProvider
{
    public string GetHint(int result)
    {
        if (result == 1)
            return "Your number is too high. Try a smaller one!";

        return "Your number is too low. Try a bigger one!";
    }
}