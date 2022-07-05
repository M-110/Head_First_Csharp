namespace AmazingExtensions;

public static class ExtendAHuman
{
    public static bool IsDistressCall(this string s)
    {
        return s.Contains("HELP!");
    }
}