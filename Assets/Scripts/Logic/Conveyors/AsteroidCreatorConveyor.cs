using Ecs;
using Logic.Components.Gameplay;

namespace Logic.Conveyors
{
    public abstract class AsteroidCreatorConveyor : Conveyor<EcsEntity, CreateAsteroidEvent>
    {
    }
}
