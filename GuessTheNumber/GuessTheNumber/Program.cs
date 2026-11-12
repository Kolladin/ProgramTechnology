namespace GuessTheNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IStatisticsStorage storage = new FileStatisticsStorage();
            GameStatistics stats = storage.Load();

            Console.WriteLine($"Wins: {stats.Wins}, Losses: {stats.Losses}");
            Console.WriteLine($"Best result: {stats.BestAttempts}");

            GameSettings settings = new GameSettings();

            Console.WriteLine("Choose difficulty: 1 - Easy, 2 - Normal, 3 - Hard, 4 - Custom");

            string? choice = Console.ReadLine();

            if (choice == "1")
            {
                settings.MaxNumber = 50;
                settings.MaxAttempts = 10;
            }
            else if (choice == "2")
            {
                settings.MaxNumber = 100;
                settings.MaxAttempts = 7;
            }
            else if (choice == "3")
            {
                settings.MaxNumber = 200;
                settings.MaxAttempts = 5;
            }

            else if (choice == "4")
            {
                Console.WriteLine("Enter maximum number (10-1000):");

                if (int.TryParse(Console.ReadLine(), out int maxNumber)
                    && maxNumber >= 10 && maxNumber <= 1000)
                {
                    settings.MaxNumber = maxNumber;
                }
                else
                {
                    Console.WriteLine("Invalid number. Default: 100");
                }

                Console.WriteLine("Enter maximum attempts (1-20):");

                if (int.TryParse(Console.ReadLine(), out int maxAttempts)
                    && maxAttempts >= 1 && maxAttempts <= 20)
                {
                    settings.MaxAttempts = maxAttempts;
                }
                else
                {
                    Console.WriteLine("Invalid attempts. Default: 10");
                }
            }

            else
            {
                Console.WriteLine("Invalid difficulty. Normal selected.");
                settings.MaxAttempts = 7;
            }


            Console.WriteLine("Choose hints: 1 - Simple, 2 - Detailed");
            string? hintChoice = Console.ReadLine();

            IHintProvider hints;

            if (hintChoice == "2")
                hints = new DetailedHintProvider();
            else
                hints = new SimpleHintProvider();

            Game game = new Game(settings);

            game.GuessChecked += OnGuessChecked;

            int attempts = 0;

            Console.WriteLine($"Guess the number from {settings.MinNumber} to {settings.MaxNumber}!");

            while(true)
            {
                Console.WriteLine("Enter your Guess: ");
                string? guess = Console.ReadLine();
                bool succes = int.TryParse(guess, out int userNumber);


                if (!succes)
                {
                    Console.WriteLine("Invalid error!");
                    continue;
                }
                else
                {
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

                        stats.AddWin(attempts);
                        storage.Save(stats);

                        break;
                    }
                    else
                    {
                        Console.WriteLine(hints.GetHint(result));
                    }


                    if (attempts >= game.MaxAttempts)
                    {
                        Console.WriteLine($"Game over! The number was {game.SecretNumber}");

                        stats.AddLoss();
                        storage.Save(stats);

                        break;
                    }

                }

            }
        }
        static void OnGuessChecked(int number)
        {
            Console.WriteLine($"Event: Player guessed {number}");
        }
    }
}
