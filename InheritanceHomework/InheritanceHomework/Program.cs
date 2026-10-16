namespace InheritanceHomework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Clothing> clothes = new List<Clothing>
            {
                new Jacket("J001", "North Face", 25000m, 8),
                new TShirt("T001", "Adidas", 3000m, "XL"),
                new Shoes("S001", "Nike", 12000m, 4)
            };

            foreach (Clothing item in clothes)
            {
                Console.WriteLine(item);
                Console.WriteLine($"  price with discount: {item.PriceWithDiscount}");

                item.Wear();

                if (item is IWashable washable)
                {
                    washable.Wash();
                }
                else
                {
                    Console.WriteLine($"{item.Name} cannot be washed.");
                }

                Console.WriteLine("----------------------------------------");
            }
        }
    }
}
