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
            var filter = ecsWorld.GetFilter<PhysicsBody, Wrappable>();

            var topLeft = _gameFieldConfig.TopLeft;
            var downRight = _gameFieldConfig.DownRight;
            
            foreach (var index in filter)
            {
                var physicsBody = filter.Get1(index);
                ref var wrappable = ref filter.Get2(index);
                var transform = physicsBody.Transform;
                var oldPosition = transform.Position;
                var x = oldPosition.X;
                var y = oldPosition.Y;

                var isInsideX = IsInside(x, topLeft.X, downRight.X);
                var isInsideY = IsInside(y, topLeft.Y, downRight.Y);
                if (isInsideX && isInsideY)
                {
                    wrappable.IsWrappingX = false;
                    wrappable.IsWrappingY = false;
                }
                else if (!wrappable.IsWrappingX || !wrappable.IsWrappingY)
                {
                    wrappable.IsWrappingX = TryWrap(wrappable.IsWrappingX, x, isInsideX, out x);
                    wrappable.IsWrappingY = TryWrap(wrappable.IsWrappingY, y, isInsideY, out y);
                }

                transform.Position = new Vector2(x, y);
            }
        }

        private bool TryWrap(bool isWrapping, float val, bool isInside, out float wrappedVal)
        {
            var res = isWrapping;
            var canWrap = !isWrapping && !isInside;
            if (canWrap)
            {
                wrappedVal = -val;
                res = true;
            }
            else
                wrappedVal = val;

            return res;
        }
        
        private bool IsInside(float val, float begin, float end) => val >= begin && val <= end;
    }
}
