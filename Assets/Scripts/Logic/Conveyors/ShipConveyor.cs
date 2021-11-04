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

            var rigidBody = new PhysicsRigidBody { Mass = 1f, UseGravity = false };
            var physicsComponent = new PhysicsBody
            {
                Transform = bodyTransform,
                RigidBody = rigidBody
            };
            
            item.AddComponent(physicsComponent);
        }
    }
}
