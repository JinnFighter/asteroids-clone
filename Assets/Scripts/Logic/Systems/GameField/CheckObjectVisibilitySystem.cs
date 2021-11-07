using Ecs;
using Ecs.Interfaces;
using Logic.Components.GameField;
using Logic.Components.Physics;
using Logic.Config;

namespace Logic.Systems.GameField
{
    public class CheckObjectVisibilitySystem : IEcsRunSystem
    {
        private readonly GameFieldConfig _gameFieldConfig;

        public CheckObjectVisibilitySystem(GameFieldConfig gameFieldConfig)
        {
            _gameFieldConfig = gameFieldConfig;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var topLeft = _gameFieldConfig.TopLeft;
            var downRight = _gameFieldConfig.DownRight;
            var filter = ecsWorld.GetFilter<PhysicsBody>().Exclude<IsOffScreen>();

            foreach (var index in filter)
            {
                var physicsBody = filter.Get1(index);
                var transform = physicsBody.Transform;
                var position = transform.Position;

                if (position.X < topLeft.X || position.X > downRight.X || position.Y < topLeft.Y ||
                    position.Y > downRight.Y)
                {
                    var entity = filter.GetEntity(index);
                    entity.AddComponent(new IsOffScreen());
                }
            }
        }
    }
}
