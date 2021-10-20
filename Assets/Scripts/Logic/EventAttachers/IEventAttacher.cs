namespace Logic.EventAttachers
{
    public interface IEventAttacher
    {
        void AttachEvent<T>(T eventObject) where T : struct;
    }
}
