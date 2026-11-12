namespace GuessTheNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameSettings settings = new GameSettings();

            Console.WriteLine("Choose difficulty: 1 - Easy, 2 - Normal, 3 - Hard");

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
            else
            {
                Console.WriteLine("Invalid difficulty. Normal selected.");
                settings.MaxAttempts = 7;
            }

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

                    if (result == 1)
                        Console.WriteLine("Too High!");
                    else if (result == -1)
                        Console.WriteLine("Too Low!");
                    else
                    {
                        Console.WriteLine($"You win! Attempts: {attempts}");
                        break;
                    }


                    if (attempts >= game.MaxAttempts)
                    {
                        Console.WriteLine($"Game over! The number was {game.SecretNumber}");
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
