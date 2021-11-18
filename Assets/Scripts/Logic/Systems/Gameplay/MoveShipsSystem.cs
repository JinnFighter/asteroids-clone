using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Input;
using Logic.Components.Physics;

namespace Logic.Systems.Gameplay
{
    public class MoveShipsSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<Ship, PhysicsBody, MovementInputAction>();

            foreach (var index in filter)
            {
                var ship = filter.Get1(index);
                ref var physicsBody = ref filter.Get2(index);
                var rigidBody = physicsBody.RigidBody;
                var transform = physicsBody.Transform;
                rigidBody.Force += transform.Direction * ship.Speed;
            }
        }
    }
}
