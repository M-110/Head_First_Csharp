namespace Birds;

class Duck : Bird
{
    public int Size { get; set; }
    public KindOfDuck Kind { get; set; }

    public Duck(int size, KindOfDuck kind)
    {
        Size = size;
        Kind = kind;
    }

    public override string ToString()
    {
        return $"A {Size} inch {Kind}";
    }

}

enum KindOfDuck
{
    Mallard,
    Muscovy,
    Loon
}
