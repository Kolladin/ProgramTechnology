namespace GuessTheNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            int attempts = 0;

            Console.WriteLine("Guess the number from 1 to 100!");

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
                    if (userNumber < 1 || userNumber > 100)
                    {
                        Console.WriteLine("Enter a number from 1 to 100!");
                        continue;
                    }

                    attempts++;

                    if (userNumber > game.SecretNumber)
                        Console.WriteLine("Too High!");
                    else if (userNumber < game.SecretNumber)
                        Console.WriteLine("Too Low!");
                    else
                    {
                        Console.WriteLine($"You win! Attempts: {attempts}");
                        break;
                    }
                }

            }
        }
    }
}
