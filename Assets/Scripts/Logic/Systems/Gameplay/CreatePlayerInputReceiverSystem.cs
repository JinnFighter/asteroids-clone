using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Events;
using Logic.Input;

namespace Logic.Systems.Gameplay
{
    public class CreatePlayerInputReceiverSystem : IEcsInitSystem
    {
        private readonly PlayerInputHandlerKeeper _playerInputHandlerKeeper;

        public CreatePlayerInputReceiverSystem(PlayerInputHandlerKeeper playerInputHandlerKeeper)
        {
            _playerInputHandlerKeeper = playerInputHandlerKeeper;
        }
        
        public void Init(EcsWorld world)
        {
            var filter = world.GetFilter<Ship>();

            foreach (var index in filter)
            {
                var entity = filter.GetEntity(index);
                _playerInputHandlerKeeper.HandleEvent<Ship>(new PlayerInputReceiver(entity));
            }
        }
    }
}
