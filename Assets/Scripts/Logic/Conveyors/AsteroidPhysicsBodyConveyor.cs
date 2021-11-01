using Ecs;
using Logic.Components.Gameplay;
using Logic.Components.Physics;
using Vector2 = Physics.Vector2;

namespace Logic.Conveyors
{
    public class AsteroidPhysicsBodyConveyor : AsteroidCreatorConveyor
    {
        protected override void UpdateItemInternal(EcsEntity item, CreateAsteroidEvent param)
        {
            var physicsBody = new PhysicsBody
            {
                Force = Vector2.Zero, 
                Mass = param.Mass, 
                Position = param.Position, 
                Velocity = param.Direction,
                Direction = param.Direction,
                UseGravity = false
            };
            
            item.AddComponent(physicsBody);
        }
    }
}
