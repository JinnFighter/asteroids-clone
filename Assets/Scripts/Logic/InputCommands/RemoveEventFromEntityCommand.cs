using Ecs;
using Logic.Input;

namespace Logic.InputCommands
{
    public class RemoveEventFromPlayerInputReceiverCommand<T>: IInputCommand where T : struct
    {
        private readonly IPlayerInputReceiver _playerInputReceiver;

        public RemoveEventFromPlayerInputReceiverCommand(IPlayerInputReceiver playerInputReceiver)
        {
            _playerInputReceiver = playerInputReceiver;
        }

        public void Execute(EcsWorld world) => _playerInputReceiver.RemoveInputEvent<T>();
    }
}
