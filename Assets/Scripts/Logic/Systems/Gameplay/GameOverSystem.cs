using Ecs;
using Ecs.Components;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Events;

namespace Logic.Systems.Gameplay
{
    public class GameOverSystem : IEcsRunSystem
    {
        private readonly ComponentEventListener _componentEventListener;

        public GameOverSystem(ComponentEventListener componentEventListener)
        {
            _componentEventListener = componentEventListener;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<GameOverEvent>();
            if (!filter.IsEmpty())
            {
                var entity = ecsWorld.CreateEntity();
                entity.AddComponent(new DisableSystemsEvent{Tag = "DisableOnGameOver"});
                _componentEventListener.HandleEvent(ref filter.Get1(0));
            }
        }
    }
}
