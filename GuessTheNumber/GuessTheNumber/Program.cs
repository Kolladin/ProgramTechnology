namespace GuessTheNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            Console.WriteLine("Guess the number from 1 to 100!");
            Console.WriteLine($"Test: {game.SecretNumber}");
        }
    }
}
