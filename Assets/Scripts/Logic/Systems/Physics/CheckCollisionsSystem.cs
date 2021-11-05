using Ecs;
using Ecs.Interfaces;
using Logic.Components.Physics;

namespace Logic.Systems.Physics
{
    public class CheckCollisionsSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<PhysicsBody>().Exclude<CollisionEvent>();

            foreach (var i in filter)
            {
                foreach (var j in filter)
                {
                    if (i != j)
                    {
                        ref var firstBody = ref filter.Get1(i);
                        ref var secondBody = ref filter.Get1(j);
                        if (firstBody.Collider.HasCollision(firstBody.Transform.Position, secondBody.Collider,
                            secondBody.Transform.Position))
                        {
                            AddCollisionEvent(filter.GetEntity(i));
                            AddCollisionEvent(filter.GetEntity(j));
                        }
                    }
                }
            }
        }

        private void AddCollisionEvent(EcsEntity entity) => entity.AddComponent(new CollisionEvent());
    }
}
