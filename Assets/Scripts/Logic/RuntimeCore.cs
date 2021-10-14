using Ecs;

namespace Logic
{
    public class RuntimeCore
    {
        private readonly EcsWorld _world;
        private readonly EcsSystems _systems;

        public RuntimeCore()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
        }
        
        public void Init()
        {
            
        }

        public void Run()
        {
            _systems?.Run();
        }

        public void AddService(object service) => _systems.AddService(service);

        public T GetService<T>() => _systems.GetService<T>();

        public void Destroy()
        {
            
        }
    }
}