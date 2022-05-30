using System.Collections;

namespace SportIEnumerable;

class Program
{
    static void Main(string[] args)
    {
        var sports = new ManualSportSequence();

        foreach (var sport in sports)
            Console.WriteLine(sport);
        
    }
}

// class ManualSportSequence : IEnumerable<Sport>
// {
//     public IEnumerator<Sport> GetEnumerator() => new ManualSportEnumerator();
//     IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
// }

class ManualSportSequence : IEnumerable<Sport>
{
    public IEnumerator<Sport> GetEnumerator()
    {
        for (int i = 0; i < Enum.GetValues(typeof(Sport)).Length; i++)
            yield return (Sport)i;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    public Sport this[int index] => (Sport)index;
}
// class ManualSportEnumerator : IEnumerator<Sport>
// {
//     int current = -1;
//     public Sport Current => (Sport)current;
//
//     public void Dispose()
//     {
//     }
//
//     object IEnumerator.Current => Current;
//
//     public bool MoveNext()
//     {
//         var maxEnumValue = Enum.GetValues(typeof(Sport)).Length;
//         if (current >= maxEnumValue - 1)
//             return false;
//         current++;
//         return true;
//     }
//
//     public void Reset() => current = 0;
// }

public enum Sport
{
    Football,
    Baseball,
    Basketball,
    Hockey,
    Boxing,
    Rugby,
    Fencing
}