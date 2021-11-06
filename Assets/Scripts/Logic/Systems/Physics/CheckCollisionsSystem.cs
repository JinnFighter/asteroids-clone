using System.Collections.Generic;
using Ecs;
using Ecs.Interfaces;
using Logic.Components.Physics;
using Physics;

namespace Logic.Systems.Physics
{
    public class CheckCollisionsSystem : IEcsRunSystem
    {
        private readonly CollisionsContainer _collisionsContainer;

        public CheckCollisionsSystem(CollisionsContainer collisionsContainer)
        {
            _collisionsContainer = collisionsContainer;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<PhysicsBody>();

            foreach (var i in filter)
            {
                foreach (var j in filter)
                {
                    if (i != j)
                    {
                        ref var firstBody = ref filter.Get1(i);
                        ref var secondBody = ref filter.Get1(j);
                        var firstCollider = firstBody.Collider;
                        var secondCollider = secondBody.Collider;
                        if (firstBody.Collider.HasCollision(firstBody.Transform.Position, secondCollider,
                            secondBody.Transform.Position))
                            CreateCollisionInfo(firstCollider, secondCollider);
                    }
                }
            }
        }

        private void CreateCollisionInfo(PhysicsCollider caller, PhysicsCollider other)
        {
            var info = new CollisionInfo { OtherCollider = other };
            List<CollisionInfo> collisionInfos;
            if (_collisionsContainer.HasKey(caller))
                collisionInfos = _collisionsContainer.GetData(caller);
            else
            {
                collisionInfos = new List<CollisionInfo>();
                _collisionsContainer.AddData(caller, collisionInfos);
            }
            
            collisionInfos.Add(info);
        }
    }
}
