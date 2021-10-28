using Ecs;
using Logic.Components.Gameplay;

namespace Logic.Conveyors
{
    public class AsteroidConveyor : AsteroidCreatorConveyor
    {
        protected override void UpdateItemInternal(EcsEntity item, CreateAsteroidEvent param)
        {
            var asteroid = new Asteroid { Stage = param.Stage };

            item.AddComponent(asteroid);
        }
    }
}
