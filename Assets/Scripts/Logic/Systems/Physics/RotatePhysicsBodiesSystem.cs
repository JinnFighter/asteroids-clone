using Ecs;
using Ecs.Interfaces;
using Logic.Components.Physics;

namespace Logic.Systems.Physics
{
    public class RotatePhysicsBodiesSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<PhysicsBody, RotateEvent>();

            foreach (var index in filter)
            {
                ref var physicsBody = ref filter.Get1(index);
                var rotateEvent = filter.Get2(index);
                physicsBody.Transform.Rotation += rotateEvent.Angle;
            }
        }
    }
}
