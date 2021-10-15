using Ecs;
using Logic.Conveyors;
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
            _systems.AddService(physicsWorld);
            _systems.AddService(new ShipConveyor(physicsWorld));
        }

        public void Init() => _systems.Init(_world);

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
