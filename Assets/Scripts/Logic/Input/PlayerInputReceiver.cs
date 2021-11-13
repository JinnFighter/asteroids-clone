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

        public void AcceptInputEvent<T>(ref T eventObject) where T : struct => _entity.AddComponent(eventObject);
    }
}
