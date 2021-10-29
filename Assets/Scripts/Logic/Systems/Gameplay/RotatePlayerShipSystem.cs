using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Input;
using Logic.Components.Physics;

namespace Logic.Systems.Gameplay
{
    public class RotatePlayerShipSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<Ship, PhysicsBody, LookInputAction>();
            
            foreach (var index in filter)
            {
                var physicsBody = filter.Get2(index);
                var position = physicsBody.Position;
                var lookInputAction = filter.Get3(index);
                var lookAtPoint = lookInputAction.LookAtPoint;
                var angle = position.GetAngleBetween(lookAtPoint);
                var entity = filter.GetEntity(index);
                if (angle != 0f)
                    entity.AddComponent(new RotateEvent{ Angle = angle });
            }
        }
    }
}
