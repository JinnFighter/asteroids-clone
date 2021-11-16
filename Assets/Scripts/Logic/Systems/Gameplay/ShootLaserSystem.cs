using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Input;
using Logic.Components.Physics;

namespace Logic.Systems.Gameplay
{
    public class ShootLaserSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<Ship, PhysicsBody, LaserGun, LaserFireInputAction>();

            foreach (var index in filter)
            {
                var laserGun = filter.Get3(index);
                var magazine = laserGun.AmmoMagazine;
                if (magazine.CurrentAmmo > 0)
                {
                    var physicsBody = filter.Get2(index);
                    var transform = physicsBody.Transform;
                    magazine.Shoot();
                    var entity = filter.GetEntity(index);
                    entity.AddComponent(new CreateLaserEvent{ Position = transform.Position + transform.Direction * 0.25f,
                        Direction = transform.Direction });
                }
            }
        }
    }
}
