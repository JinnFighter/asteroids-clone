using Ecs;
using Logic.Components.Input;
using Logic.Conveyors;
using Logic.EventAttachers;
using Logic.Factories;
using Logic.Services;
using Logic.Systems.Gameplay;
using Physics;

namespace Logic
{
    public class RuntimeCore
    {
        private readonly EcsWorld _world;
        private readonly EcsSystems _systems;

        public RuntimeCore()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems();
        }

        public void Setup()
        {
            var physicsWorld = new PhysicsWorld();
            var physicsObjectFactory = new RigidbodyFactory(physicsWorld);
            var timeContainer = new TimeContainer();
            _systems
                .AddService(physicsObjectFactory)
                .AddService(new ShipConveyor(physicsObjectFactory))
                .AddService(new DefaultEventAttacher(_world))
                .AddService(timeContainer);
        }

        public void Init() => _systems
            .AddInitSystem(new CreatePlayerShipSystem(_systems.GetService<ShipConveyor>()))
            .OneFrame<InputAction>()
            .Init(_world);

        public void Run()
        {
            _systems.Run(_world);
            _world.RemoveEmptyEntities();
        }

        public void AddService(object service) => _systems.AddService(service);

        public T GetService<T>() => _systems.GetService<T>();

        public void Destroy()
        {
            _systems.Destroy(_world);
            _world.Destroy();
        }
    }
}
