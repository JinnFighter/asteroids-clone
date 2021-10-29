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
            var physicsComponent = new PhysicsBody
            {
                Position = Vector2.Zero,
                Velocity = Vector2.Zero,
                Force = Vector2.Zero,
                Direction = new Vector2(0, 1),
                Mass = 1f,
                UseGravity = false
            };
            
            item.AddComponent(physicsComponent);
        }
    }
}
