using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ecs
{
    public class EcsFilter : IEnumerable<EcsEntity>
    {
        private readonly List<EcsEntity> _entities;

        public EcsFilter(IEnumerable<EcsEntity> entities)
        {
            _entities = new List<EcsEntity>(entities);
        }

        public EcsFilter Exclude<T>() where T : struct => new EcsFilter(_entities.Where(entity => !entity.HasComponent<T>()));
    
        public EcsFilter Exclude<T, T1>() 
            where T : struct
            where T1 : struct => new EcsFilter(_entities.Where(entity => !(entity.HasComponent<T>() || entity.HasComponent<T1>())));

        public IEnumerator<EcsEntity> GetEnumerator() => new EcsFilterEnumerator(_entities);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
