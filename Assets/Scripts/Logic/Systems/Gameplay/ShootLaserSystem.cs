using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Input;
using Logic.Components.Physics;
using Logic.Config;

namespace Logic.Systems.Gameplay
{
    public class ShootLaserSystem : IEcsRunSystem
    {
        private readonly ShipConfig _shipConfig;

        public ShootLaserSystem(ShipConfig shipConfig)
        {
            _shipConfig = shipConfig;
        }
        
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
                    entity.AddComponent(new CreateLaserEvent{ Position = transform.Position + transform.Direction * _shipConfig.GunPositionOffset,
                        Rotation =  transform.Rotation, Direction = transform.Direction });
                }
            }
        }
    }
}
