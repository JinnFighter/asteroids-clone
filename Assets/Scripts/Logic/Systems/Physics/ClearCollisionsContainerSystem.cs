using Ecs;
using Ecs.Interfaces;
using Physics;

namespace Logic.Systems.Physics
{
    public class ClearCollisionsContainerSystem : IEcsRunSystem
    {
        private readonly CollisionsContainer _collisionsContainer;

        public ClearCollisionsContainerSystem(CollisionsContainer container)
        {
            _collisionsContainer = container;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            _collisionsContainer.Clear();
        }
    }
}
