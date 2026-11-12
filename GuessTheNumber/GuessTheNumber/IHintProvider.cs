namespace GuessTheNumber;

internal interface IHintProvider
{
    string GetHint(int result);
}