namespace GuessTheNumber;

internal class Game
{
    public int SecretNumber { get; } = Random.Shared.Next(1, 101);
}