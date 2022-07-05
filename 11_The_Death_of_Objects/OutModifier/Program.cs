static int ReturnThreeValues(int value, out double half, out int twice)
{
    half = value / 2f;
    twice = value * 2;
    return value + 1;
}

static (int, double, int) ReturnTresValues(int value)
{
    return (value + 1, value / 2f, value * 2);
}

Console.Write("Enter a number: ");
if (int.TryParse(Console.ReadLine(), out int input))
{
    var output1 = ReturnThreeValues(input, out double output2, out int output3);
    var (x, y, z) = ReturnTresValues(input);
    Console.WriteLine($"Outputs: {output1}, half={output2}, twice={output3}");
    Console.WriteLine($"Outputs: {x}, half={y}, twice={z}");
}