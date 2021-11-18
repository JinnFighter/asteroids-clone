using Ecs;
using Ecs.Interfaces;
using Logic.Components.Physics;
using Physics;

namespace Logic.Systems.Physics
{
    public class FillQuadTreeSystem : IEcsRunSystem
    {
        private readonly QuadTree _quadTree;

        public FillQuadTreeSystem(QuadTree quadTree)
        {
            _quadTree = quadTree;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<PhysicsBody>();

            _quadTree.Clear();

            foreach (var index in filter)
            {
                var physicsBody = filter.Get1(index);
                _quadTree.Insert(physicsBody.Collider);
            }
        }
    }
}
