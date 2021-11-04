using Ecs;
using Logic.Components.Gameplay;
using Logic.Components.Physics;
using Physics;

namespace Logic.Conveyors
{
    public class ShipConveyor : EntityConveyor
    {
        protected override void UpdateItemInternal(EcsEntity item)
        {
            var ship = new Ship { Speed = 1f };
            item.AddComponent(ship);
            var bodyTransform = new BodyTransform { Position = Vector2.Zero, Direction = new Vector2(0, 1) };
            var physicsComponent = new PhysicsBody
            {
                Transform = bodyTransform,
                Velocity = Vector2.Zero,
                Force = Vector2.Zero,
                Mass = 1f,
                UseGravity = false
            };
            
            item.AddComponent(physicsComponent);
        }
    }
}
