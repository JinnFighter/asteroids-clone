using System.Collections.Generic;
using System.Linq;
using Ecs;
using Ecs.Interfaces;
using Logic.Components.Physics;
using Physics;

namespace Logic.Systems.Physics
{
    public sealed class CheckCollisionsSystem : IEcsRunSystem
    {
        private readonly CollisionsContainer _collisionsContainer;
        private readonly QuadTree _quadTree;

        public CheckCollisionsSystem(CollisionsContainer collisionsContainer, QuadTree quadTree)
        {
            _collisionsContainer = collisionsContainer;
            _quadTree = quadTree;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<PhysicsBody>();
            
            foreach (var index in filter)
            {
                var physicsBody = filter.Get1(index);
                var collider = physicsBody.Collider;
                var possibleCollisions = _quadTree.GetPossibleCollisions(collider);
                foreach (var otherCollider in possibleCollisions.Where(otherCollider => !collider.Equals(otherCollider) 
                    && collider.HasCollision(otherCollider)))
                {
                    CreateCollisionInfo(collider, otherCollider);
                    CreateCollisionInfo(otherCollider, collider);
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
