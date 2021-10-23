using Ecs;
using Logic.Components.Physics;
using Physics;

namespace Logic.Conveyors
{
    public class ShipConveyor : EntityConveyor
    {
        protected override void UpdateItemInternal(EcsEntity item)
        {
            var physicsComponent = new PhysicsBody
            {
                Position = Vector2.Zero,
                Velocity = Vector2.Zero,
                Force = Vector2.Zero,
                Mass = 1f,
                UseGravity = false
            };
            
            item.AddComponent(physicsComponent);
        }
    }
}
