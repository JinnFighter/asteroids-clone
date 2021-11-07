using Common;
using Ecs;
using Ecs.Interfaces;
using Logic.Components.GameField;
using Logic.Components.Physics;
using Logic.Config;

namespace Logic.Systems.GameField
{
    public class WrapOffScreenObjectsSystem : IEcsRunSystem
    {
        private readonly GameFieldConfig _gameFieldConfig;

        public WrapOffScreenObjectsSystem(GameFieldConfig gameFieldConfig)
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
                var isWrappingX = TryWrap(x, topLeft.X, downRight.X, out x);
                var isWrappingY = TryWrap(y, topLeft.Y, downRight.Y, out y);
                
                transform.Position = new Vector2(x, y);
                if (isWrappingX || isWrappingY)
                {
                    var entity = filter.GetEntity(index);
                    entity.AddComponent(new IsWrapped{ IsWrappingX = isWrappingX, IsWrappingY = isWrappingY });
                }
            }
        }
        
        private bool TryWrap(float val, float begin, float end, out float wrappedVal)
        {
            var isOutside = val < begin || val > end;
            if (isOutside)
                wrappedVal = -val;
            else
                wrappedVal = val;

            return isOutside;
        }
    }
}
