using Ecs;
using Ecs.Components;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Events;

namespace Logic.Systems.Gameplay
{
    public class GameOverSystem : IEcsRunSystem
    {
        private readonly ComponentEventHandlerContainer _componentEventHandlerContainer;

        public GameOverSystem(ComponentEventHandlerContainer componentEventHandlerContainer)
        {
            _componentEventHandlerContainer = componentEventHandlerContainer;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<GameOverEvent>();
            if (!filter.IsEmpty())
            {
                var entity = ecsWorld.CreateEntity();
                entity.AddComponent(new DisableSystemsEvent{Tag = "DisableOnGameOver"});
                _componentEventHandlerContainer.HandleEvent(ref filter.Get1(0));
            }
        }
    }
}
