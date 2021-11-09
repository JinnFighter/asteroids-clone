using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Input;
using Logic.Components.Physics;

namespace Logic.Systems.Gameplay
{
    public class CheckFireActionSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<Ship, PhysicsBody, FireInputAction>();

            foreach (var index in filter)
            {
                ref var physicsBody = ref filter.Get2(index);
                var transform = physicsBody.Transform;
                
                var entity = filter.GetEntity(index);
                entity.AddComponent(new CreateBulletEvent { Position = transform.Position + transform.Direction * 0.25f,
                    Direction = transform.Direction, Velocity = transform.Direction * 3 });
            }
        }
    }
}
