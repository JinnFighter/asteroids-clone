using System;
using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Input;
using Logic.Components.Physics;
using UnityEngine;

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
                var position = physicsBody.Position;
                var lookInputAction = filter.Get3(index);
                var lookAtPoint = lookInputAction.LookAtPoint;
                var direction = physicsBody.Direction;
                var nextDirection = (lookAtPoint - position).Normalized;
                var angle = (float)(Math.Acos(direction.Dot(nextDirection)) * 180 / Math.PI);
                var entity = filter.GetEntity(index);
                if (angle != 0f)
                {
                    entity.AddComponent(new RotateEvent{ Angle = angle });
                    physicsBody.Direction = nextDirection;
                }
            }
        }
    }
}
