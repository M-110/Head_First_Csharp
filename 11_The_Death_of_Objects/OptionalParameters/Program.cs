static void CheckTemperature(
    double temp,
    double tooHigh = 99.5,
    double tooLow = 96.5)
{
    if (temp < tooHigh && temp > tooLow)
        Console.WriteLine($"{temp} degrees F - Feeling good!");
    else
        Console.WriteLine($"Uh-oh {temp} degrees F -- see a doctor!");
}

CheckTemperature(100);
CheckTemperature(100, 105);
CheckTemperature(100, 105, 102);
CheckTemperature(50, tooLow: 45);