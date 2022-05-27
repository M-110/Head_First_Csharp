class Program
{
    static void Main(string[] args)
    {
        var position = 0;
        using var reader = new StreamReader("textdata.txt");
        while (!reader.EndOfStream)
        {
            var buffer = new char[16];
            var bytesRead = reader.ReadBlock(buffer, 0, 16);
            
            Console.Write($"{position:x4}: ");
            
            position += bytesRead;

            for (var i = 0; i < 16; i++)
            {
                if (i < bytesRead)
                    Console.Write($"{(byte) buffer[i]:x2} ");
                else
                    Console.Write("   ");
                if (i == 7) Console.Write("-- ");
            }

            var bufferContents = new string(buffer);
            Console.WriteLine($"  {bufferContents.Substring(0, bytesRead)}");
        }

        Console.ReadKey();
    }
}