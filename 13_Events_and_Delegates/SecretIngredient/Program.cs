namespace SecretIngredient;

delegate string GetSecretIngredient(int amount);

class Program
{
    static void Main(string[] args)
    {
        Adrian adrian = new();
        Harper harper = new();

        GetSecretIngredient addIngredientMethod = null;
        while (true)
        {
            Console.WriteLine("Press A for Adrian or H for Harper or an amount");
            var line = Console.ReadLine();
            switch (line)
            {
                case "A":
                    Console.WriteLine("Selected Adrian");
                    addIngredientMethod = adrian.MySecretIngredientMethod;
                    break;
                case "H":
                    Console.WriteLine("Selected Harper");
                    addIngredientMethod = harper.MySecretIngredientMethod;
                    break;
                default:
                    if (addIngredientMethod == null)
                        Console.WriteLine("You need to select a chef first");
                    else if (int.TryParse(line, out var num))
                        Console.WriteLine(addIngredientMethod(num));
                    else
                        return;
                    break;
                       
            }

        }
    }
}