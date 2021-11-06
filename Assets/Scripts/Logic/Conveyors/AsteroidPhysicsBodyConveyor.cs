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
            var transform = new BodyTransform { Position = param.Position, Direction = param.Direction };
            var rigidBody = new PhysicsRigidBody { Mass = param.Mass, Velocity = param.Direction, UseGravity = false };
            var collider = new BoxPhysicsCollider(param.Position, 10, 10);
            var physicsBody = new PhysicsBody
            {
                Transform = transform,
                RigidBody = rigidBody
            };
            
            item.AddComponent(physicsBody);
        }
    }
}
