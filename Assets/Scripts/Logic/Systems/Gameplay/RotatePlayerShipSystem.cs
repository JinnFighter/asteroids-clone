using System;
using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Input;
using Logic.Components.Physics;

namespace Logic.Systems.Gameplay
{
    public class RotatePlayerShipSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<Ship, PhysicsBody, LookInputAction>();
            
            foreach (var index in filter)
            {
                ref var physicsBody = ref filter.Get2(index);
                var transform = physicsBody.Transform;
                var position = transform.Position;
                
                var lookInputAction = filter.Get3(index);
                var lookAtPoint = lookInputAction.LookAtPoint;
                var nextDirection = (lookAtPoint - position).Normalized;
                var angle = (float)(Math.Atan2(nextDirection.Y, nextDirection.X) * 180 / Math.PI);
                var entity = filter.GetEntity(index);
                if (angle != 0f)
                {
                    entity.AddComponent(new RotateEvent{ Angle = angle - transform.Rotation });
                    transform.Direction = nextDirection;
                }
            }
        }
    }
}
