using Common;
using Ecs;
using Logic.Components.GameField;
using Logic.Components.Gameplay;
using Logic.Components.Physics;
using Physics;

namespace Logic.Conveyors
{
    public class BulletConveyor : BulletCreatorConveyor
    {
        protected override void UpdateItemInternal(EcsEntity item, CreateBulletEvent param)
        {
            item.AddComponent(new Bullet());
            
            var bodyTransform = new BodyTransform { Position = param.Position, Direction = param.Direction, };
            var rigidBody = new PhysicsRigidBody { Mass = 1f, Velocity = param.Velocity, UseGravity = false };
            var collider = new BoxPhysicsCollider(Vector2.Zero, 7, 7);
            var physicsComponent = new PhysicsBody
            {
                Transform = bodyTransform,
                RigidBody = rigidBody,
                Collider = collider
            };
            
            item.AddComponent(physicsComponent);
            
            item.AddComponent(new Wrappable{ IsWrappingX = false, IsWrappingY = false });
        }
    }
}
