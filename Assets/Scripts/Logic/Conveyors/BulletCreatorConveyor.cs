using Ecs;
using Logic.Components.Gameplay;

namespace Logic.Conveyors
{
    public abstract class BulletCreatorConveyor : Conveyor<EcsEntity, CreateBulletEvent>
    {
    }
}
