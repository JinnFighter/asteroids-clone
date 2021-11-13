namespace Logic.Input
{
    public interface IPlayerInputReceiver
    {
        void AcceptInputEvent<T>(ref T eventObject) where T : struct;
    }
}
