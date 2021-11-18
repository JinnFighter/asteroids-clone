using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Events;

namespace UnityScripts.Startups.InitSystems
{
    public class InitAsteroidHandlerSystem : IEcsInitSystem
    {
        private readonly ComponentEventHandlerContainer _componentEventHandlerContainer;
        private readonly IComponentEventHandler<CreateAsteroidEvent> _asteroidObjectFactory;

        public InitAsteroidHandlerSystem(ComponentEventHandlerContainer componentEventHandlerContainer,
            IComponentEventHandler<CreateAsteroidEvent> asteroidObjectFactory)
        {
            _componentEventHandlerContainer = componentEventHandlerContainer;
            _asteroidObjectFactory = asteroidObjectFactory;
        }

        public void Init(EcsWorld world) => _componentEventHandlerContainer.AddHandler(_asteroidObjectFactory);
    }
}
