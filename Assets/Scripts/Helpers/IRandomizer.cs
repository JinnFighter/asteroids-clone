namespace Helpers
{
    public interface IRandomizer
    {
        int Range(int minValue, int maxValue);
        bool IsProc(int chance);
    }
}
