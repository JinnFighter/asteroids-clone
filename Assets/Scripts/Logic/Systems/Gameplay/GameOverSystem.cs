using Ecs;
using Ecs.Components;
using Ecs.Interfaces;
using Logic.Components.Gameplay;

namespace Logic.Systems.Gameplay
{
    public class GameOverSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<GameOverEvent>();
            if (!filter.IsEmpty())
            {
                var entity = ecsWorld.CreateEntity();
                entity.AddComponent(new DisableSystemsEvent{Tag = "DisableOnGameOver"});
            }
        }
    }
}
