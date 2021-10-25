namespace Logic.EventAttachers
{
    public interface IEventAttacher
    {
        void AttachEvent<T>(object sender, T eventObject) where T : struct;
    }
}
