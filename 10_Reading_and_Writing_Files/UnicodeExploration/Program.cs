using System.Text;

File.WriteAllText("eureka.txt", "Eureka!");
var eurekaBytes = File.ReadAllBytes("eureka.txt");
foreach (var b in eurekaBytes)
    Console.Write($"{b:x2} ");
Console.WriteLine(Encoding.UTF8.GetString(eurekaBytes));
Console.ReadKey();