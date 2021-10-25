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

        public void AttachEvent<T>(object sender, in T obj) where T : struct
        {
            var command = _playerEntitiesContainer.TryGetValue(sender, out var entity)
                ? (IInputCommand)new AttachEventToConcreteEntityCommand<T>(obj, entity)
                : new AttachEventToNewEntityCommand<T>(obj);

            _inputCommandQueue.Enqueue(command);
        }
    }
}
