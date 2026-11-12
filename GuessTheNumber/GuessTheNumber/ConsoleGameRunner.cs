
namespace GuessTheNumber;

internal class ConsoleGameRunner
{
    public void Play(
        Game game,
        GameSettings settings,
        IHintProvider hints)
    {
        int attempts = 0;

        Console.WriteLine($"Guess the number from {settings.MinNumber} to {settings.MaxNumber}!");

        while (true)
        {
            Console.WriteLine("Enter your Guess: ");
            string? guess = Console.ReadLine();

            if (guess == null)
                break;

            bool success = int.TryParse(guess, out int userNumber);

            if (!success)
            {
                Console.WriteLine("Invalid input!");
                continue;
            }

            if (userNumber < settings.MinNumber || userNumber > settings.MaxNumber)
            {
                Console.WriteLine($"Enter a number from {settings.MinNumber} to {settings.MaxNumber}!");
                continue;
            }

            attempts++;

            Console.WriteLine($"Attempts: {attempts}/{game.MaxAttempts}");

            int result = game.CheckGuess(userNumber);

            if (result == 0)
            {
                Console.WriteLine($"You win! Attempts: {attempts}");

                game.Finish(true, attempts);
                break;
            }

            Console.WriteLine(hints.GetHint(result));

            if (attempts >= game.MaxAttempts)
            {
                Console.WriteLine($"Game over! The number was {game.SecretNumber}");

                game.Finish(false, attempts);
                break;
            }
        }
    }
}
