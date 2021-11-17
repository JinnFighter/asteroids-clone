using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
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
        private readonly PlayerInputHandlerKeeper _playerInputHandlerKeeper;
        private readonly GameObjectEventHandlerContainer _gameObjectHandlerContainer;
        

        public InitPlayerInputHandlersSystem(InputCommandQueue inputCommandQueue,
            PlayerInputHandlerKeeper playerInputHandlerKeeper,
            GameObjectEventHandlerContainer gameObjectHandlerContainer)
        {
            _inputCommandQueue = inputCommandQueue;
            _playerInputHandlerKeeper = playerInputHandlerKeeper;
            _gameObjectHandlerContainer = gameObjectHandlerContainer;
        }
        
        public void Init(EcsWorld world)
        {
            var playerEntitiesContainer = new PlayerEntitiesDataContainer();
            var inputEventEmitter = new InputEventEmitter(playerEntitiesContainer, _inputCommandQueue);
            
            var playerInputHandler = new PlayerInputEventHandler(playerEntitiesContainer, inputEventEmitter);
            _playerInputHandlerKeeper.AddHandler<Ship>(playerInputHandler);
            _gameObjectHandlerContainer.AddHandler(playerInputHandler);
        }
    }
}
