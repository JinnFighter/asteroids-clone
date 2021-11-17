using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Physics;

namespace Logic.Systems.Gameplay
{
    public class CheckSaucerDirectionSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<Saucer, PhysicsBody>();

            foreach (var index in filter)
            {
                var saucer = filter.Get1(index);
                var physicsBody = filter.Get2(index);

                var targetTransform = saucer.TargetTransform;
                var saucerTransform = physicsBody.Transform;
                var nextDirection = targetTransform.Position - saucerTransform.Position;
                saucerTransform.Direction = nextDirection;
                var rigidBody = physicsBody.RigidBody;
                rigidBody.Force += saucerTransform.Direction;
                var angle = saucerTransform.Position.GetRotationAngle(targetTransform.Position);
                if (angle != 0f)
                {
                    var entity = filter.GetEntity(index);
                    entity.AddComponent(new RotateEvent{ Angle = angle - saucerTransform.Rotation });
                }
            }
        }
    }
}
