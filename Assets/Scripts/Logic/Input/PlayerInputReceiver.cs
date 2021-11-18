using Ecs;

namespace Logic.Input
{
    public class PlayerInputReceiver : IPlayerInputReceiver
    {
        private readonly EcsEntity _entity;

        public PlayerInputReceiver(EcsEntity entity)
        {
            _entity = entity;
        }

        public void AcceptInputEvent<T>(T eventObject) where T : struct => _entity.AddComponent(eventObject);
        
        public void RemoveInputEvent<T>() where T : struct
        {
            if(_entity.HasComponent<T>())
                _entity.RemoveComponent<T>();
        }
    }
}
