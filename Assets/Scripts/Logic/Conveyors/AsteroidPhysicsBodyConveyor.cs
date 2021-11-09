using Common;
using Ecs;
using Logic.Components.Gameplay;
using Logic.Components.Physics;
using Physics;

namespace Logic.Conveyors
{
    public class AsteroidPhysicsBodyConveyor : AsteroidCreatorConveyor
    {
        protected override void UpdateItemInternal(EcsEntity item, CreateAsteroidEvent param)
        {
            var position = param.Position;
            var direction = param.Direction.Normalized * (param.Mass - 3 * param.Stage);
            var transform = new BodyTransform { Position = position, Direction = direction };
            var rigidBody = new PhysicsRigidBody { Mass = param.Mass, Velocity = direction, UseGravity = false };
            var collider = new BoxPhysicsCollider(position, 10, 10);
            var physicsBody = new PhysicsBody
            {
                Transform = transform,
                RigidBody = rigidBody,
                Collider = collider
            };
            
            item.AddComponent(physicsBody);
        }
    }
}
