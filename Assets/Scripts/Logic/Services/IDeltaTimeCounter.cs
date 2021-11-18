namespace Logic.Services
{
    public interface IDeltaTimeCounter
    {
        void Reset();
        float GetDeltaTime();
    }
}
