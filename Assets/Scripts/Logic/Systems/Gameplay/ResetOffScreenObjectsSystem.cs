using Common;
using Ecs;
using Ecs.Interfaces;
using Logic.Components.Physics;
using Logic.Config;

namespace Logic.Systems.Gameplay
{
    public class ResetOffScreenObjectsSystem : IEcsRunSystem
    {
        private readonly GameFieldConfig _gameFieldConfig;

        public ResetOffScreenObjectsSystem(GameFieldConfig gameFieldConfig)
        {
            _gameFieldConfig = gameFieldConfig;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<PhysicsBody>();

            var topLeft = _gameFieldConfig.TopLeft;
            var downRight = _gameFieldConfig.DownRight;
            
            foreach (var index in filter)
            {
                var physicsBody = filter.Get1(index);
                var transform = physicsBody.Transform;
                var oldPosition = transform.Position;
                var x = oldPosition.X;
                var y = oldPosition.Y;
                if (x < topLeft.X || x > downRight.X)
                    x = -x;
                
                if (y < topLeft.Y || y > downRight.Y)
                    y = -y;
                transform.Position = new Vector2(x, y);
            }
        }
    }
}
