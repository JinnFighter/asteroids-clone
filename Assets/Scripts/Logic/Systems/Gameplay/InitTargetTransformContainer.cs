using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Physics;
using Logic.Containers;

namespace Logic.Systems.Gameplay
{
    public class InitTargetTransformContainer : IEcsInitSystem
    {
        private readonly TargetTransformContainer _targetTransformContainer;

        public InitTargetTransformContainer(TargetTransformContainer targetTransformContainer)
        {
            _targetTransformContainer = targetTransformContainer;
        }
        
        public void Init(EcsWorld world)
        {
            var filter = world.GetFilter<Ship, PhysicsBody>();

            foreach (var index in filter)
            {
                var physicsBody = filter.Get2(index);
                _targetTransformContainer.Transform = physicsBody.Transform;
            }
        }
    }
}
