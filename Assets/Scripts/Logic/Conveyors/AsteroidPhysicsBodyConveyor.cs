using Ecs;
using Logic.Components.Gameplay;
using Logic.Components.Physics;
using Physics;
using Vector2 = Physics.Vector2;

namespace Logic.Conveyors
{
    public class AsteroidPhysicsBodyConveyor : AsteroidCreatorConveyor
    {
        protected override void UpdateItemInternal(EcsEntity item, CreateAsteroidEvent param)
        {
            var transform = new BodyTransform { Position = param.Position, Direction = param.Direction };
            var physicsBody = new PhysicsBody
            {
                Transform = transform,
                Force = Vector2.Zero, 
                Mass = param.Mass,
                Velocity = param.Direction,
                UseGravity = false
            };
            
            item.AddComponent(physicsBody);
        }
    }
}
