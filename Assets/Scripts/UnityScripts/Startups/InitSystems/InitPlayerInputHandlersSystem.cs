using Ecs;
using Ecs.Interfaces;
using Logic.Events;
using Logic.Services;
using UnityScripts.Containers;
using UnityScripts.EventHandlers;
using UnityScripts.InputActions;

namespace UnityScripts.Startups.InitSystems
{
    public class InitPlayerInputHandlersSystem : IEcsInitSystem
    {
        private readonly InputCommandQueue _inputCommandQueue;
        private readonly PlayerInputEventHandlerContainer _playerInputHandlerContainer;
        private readonly GameObjectEventHandlerContainer _gameObjectHandlerContainer;

        public InitPlayerInputHandlersSystem(InputCommandQueue inputCommandQueue,
            PlayerInputEventHandlerContainer playerInputHandlerContainer,
            GameObjectEventHandlerContainer gameObjectHandlerContainer)
        {
            _inputCommandQueue = inputCommandQueue;
            _playerInputHandlerContainer = playerInputHandlerContainer;
            _gameObjectHandlerContainer = gameObjectHandlerContainer;
        }
        
        public void Init(EcsWorld world)
        {
            var playerEntitiesContainer = new PlayerEntitiesDataContainer();
            var inputEventEmitter = new InputEventEmitter(playerEntitiesContainer, _inputCommandQueue);
            
            var playerInputHandler = new PlayerInputEventHandler(playerEntitiesContainer, inputEventEmitter);
            _playerInputHandlerContainer.AddHandler(playerInputHandler);
            _gameObjectHandlerContainer.AddHandler(playerInputHandler);
        }
    }
}
