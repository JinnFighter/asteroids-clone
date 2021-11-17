using Logic.InputCommands;
using Logic.Services;
using UnityScripts.Containers;

namespace UnityScripts.InputActions
{
    public class InputActionVisitor
    {
        private readonly PlayerEntitiesDataContainer _playerEntitiesContainer;
        private readonly InputCommandQueue _inputCommandQueue;

        public InputActionVisitor(PlayerEntitiesDataContainer container,
            InputCommandQueue inputCommandQueue)
        {
            _playerEntitiesContainer = container;
            _inputCommandQueue = inputCommandQueue;
        }

        public void AttachEvent<T>(object sender, T obj) where T : struct
        {
            var command = _playerEntitiesContainer.TryGetValue(sender, out var entity)
                ? (IInputCommand)new AttachEventToPlayerInputReceiverCommand<T>(obj, entity)
                : new AttachEventToNewEntityCommand<T>(obj);

            _inputCommandQueue.Enqueue(command);
        }
        
        public void RemoveEvent<T>(object sender) where T : struct
        {
            if (_playerEntitiesContainer.TryGetValue(sender, out var entity))
                _inputCommandQueue.Enqueue(new RemoveEventFromPlayerInputReceiverCommand<T>(entity));
        }
    }
}
