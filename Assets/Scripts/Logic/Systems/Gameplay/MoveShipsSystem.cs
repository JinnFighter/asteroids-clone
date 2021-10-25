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

            foreach (var entity in filter)
            {
                var movementAction = entity.GetComponent<MovementInputAction>();
                ref var physicsBody = ref entity.GetComponent<PhysicsBody>();
                physicsBody.Velocity = movementAction.Direction;
            }
        }
    }
}
