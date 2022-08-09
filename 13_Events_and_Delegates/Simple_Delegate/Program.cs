
class Program
{

    delegate string IntToString(int i);

    static void Main(string[] args)
    {
        IntToString myFunc = AddNumberSign;
        Console.WriteLine(myFunc(23));
        myFunc = PlusOne;
        Console.WriteLine(myFunc(23));


    }
    public static string AddNumberSign(int i) => $"#{i}";
    public static string PlusOne(int i) => $"{i + 1}";
}
