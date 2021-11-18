using Ecs;
using Ecs.Interfaces;
using Logic.Components.Physics;

namespace Logic.Systems.Physics
{
    public class DestroyTransformSystem : IEcsOnDestroySystem
    {
        public void OnDestroy(EcsWorld world)
        {
            var filter = world.GetFilter<PhysicsBody>();

            foreach (var index in filter)
            {
                var physicsBody = filter.Get1(index);
                physicsBody.Transform.Destroy();
            }
        }
    }
}
