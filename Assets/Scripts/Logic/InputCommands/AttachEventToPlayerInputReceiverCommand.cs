using Ecs;
using Logic.Input;

namespace Logic.InputCommands
{
    public class AttachEventToPlayerInputReceiverCommand<T> : IInputCommand where T : struct
    {
        private readonly T _component;
        private readonly IPlayerInputReceiver _playerInputReceiver;

        public AttachEventToPlayerInputReceiverCommand(T component, IPlayerInputReceiver playerInputReceiver)
        {
            _component = component;
            _playerInputReceiver = playerInputReceiver;
        }

        public void Execute(EcsWorld world) => _playerInputReceiver.AcceptInputEvent(_component);
    }
}
