using Logic.EventAttachers;
using UnityScripts.Containers;

namespace UnityScripts.EventAttachers
{
    public class PlayerInputEventAttacher : IEventAttacher
    {
        private readonly IEventAttacher _eventAttacher;
        private readonly PlayerEntitiesDataContainer _playerEntitiesContainer;

        public PlayerInputEventAttacher(IEventAttacher attacher, PlayerEntitiesDataContainer container)
        {
            _eventAttacher = attacher;
            _playerEntitiesContainer = container;
        }
        
        public void AttachEvent<T>(object sender, T eventObject) where T : struct
        {
            if(_playerEntitiesContainer.TryGetValue(sender, out var entity))
                entity.AddComponent(eventObject);
            else
                _eventAttacher.AttachEvent(sender, eventObject);
        }
    }
}
