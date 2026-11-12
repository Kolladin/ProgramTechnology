namespace GuessTheNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IStatisticsStorage storage = new FileStatisticsStorage();
            GameStatistics stats = new GameStatistics();

            try
            {
                stats = storage.Load();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error loading statistics: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Access denied: {ex.Message}");
            }


            Console.WriteLine($"Wins: {stats.Wins}, Losses: {stats.Losses}");
            Console.WriteLine($"Best result: {stats.BestAttempts}");

            GameSettings settings = ChooseSettings();
            IHintProvider hints = ChooseHints();

            Game game = new Game(settings);
            game.GuessChecked += OnGuessChecked;

            game.GameFinished += (isWin, attempts) =>
            {
                if (isWin)
                    stats.AddWin(attempts);
                else
                    stats.AddLoss();

                try
                {
                    storage.Save(stats);
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Error saving statistics: {ex.Message}");
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine($"Access denied: {ex.Message}");
                }
            };


            ConsoleGameRunner runner = new ConsoleGameRunner();
            runner.Play(game, settings, hints);
        }




        static void OnGuessChecked(int number)
        {
            Console.WriteLine($"Event: Player guessed {number}");
        }


        static GameSettings ChooseSettings()
        {
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

            return settings;
        }

        static IHintProvider ChooseHints()
        {
            Console.WriteLine("Choose hints: 1 - Simple, 2 - Detailed");

            string? choice = Console.ReadLine();

            if (choice == "2")
                return new DetailedHintProvider();

            return new SimpleHintProvider();
        }


    }

}
