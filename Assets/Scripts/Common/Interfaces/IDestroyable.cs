namespace Common.Interfaces
{
    public interface IDestroyable
    {
        delegate void OnDestroyEvent();
        event OnDestroyEvent DestroyEvent;
        void Destroy();
    }
}
