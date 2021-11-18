using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Input;
using Logic.Components.Physics;

namespace Logic.Systems.Gameplay
{
    public class CheckBulletFireActionSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<Ship, PhysicsBody, FireInputAction>();

            foreach (var index in filter)
            {
                var ship = filter.Get1(index);
                ref var physicsBody = ref filter.Get2(index);
                var transform = physicsBody.Transform;
                
                var entity = filter.GetEntity(index);
                entity.AddComponent(new CreateBulletEvent { Position = transform.Position + transform.Direction * 0.5f,
                    Direction = transform.Direction, Velocity = transform.Direction * ship.Speed * 3 });
            }
        }
    }
}
