namespace Logic.Input
{
    public interface IPlayerInputReceiver
    {
        void AcceptInputEvent<T>(T eventObject) where T : struct;
    }
}
