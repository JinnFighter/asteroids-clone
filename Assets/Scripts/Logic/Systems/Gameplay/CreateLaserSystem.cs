using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Time;

namespace Logic.Systems.Gameplay
{
    public class CreateLaserSystem : IEcsInitSystem
    {
        public void Init(EcsWorld world)
        {
            var filter = world.GetFilter<Ship>();

            foreach (var index in filter)
            {
                var entity = filter.GetEntity(index);
                entity.AddComponent(new Laser{ CurrentAmmo = 0, MaxAmmo = 3 });
                entity.AddComponent(new Timer{ CurrentTime = 7f });
            }
        }
    }
}
