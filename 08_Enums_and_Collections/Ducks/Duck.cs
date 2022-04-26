using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ducks;

class Duck : IComparable<Duck>
{
    public int Size { get; set; }
    public KindOfDuck Kind { get; set; }

    public int CompareTo(Duck other)
    {
        return Size < other.Size ? 1 : Size > other.Size ? -1 : 0;
    }
}

enum KindOfDuck
{
    Mallard,
    Muscovy,
    Loon
}
