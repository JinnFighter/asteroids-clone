using System.Collections.Generic;
using System.Linq;

namespace Ecs
{
    public class EcsWorld
    {
        private readonly List<EcsEntity> _entities;
        private readonly EcsComponentManager _componentManager;
        private readonly List<Filter> _filters;

        public EcsWorld()
        {
            _entities = new List<EcsEntity>();
            _componentManager = new EcsComponentManager();
            _filters = new List<Filter>();
        }

        public EcsEntity CreateEntity()
        {
            var entity = new EcsEntity(_componentManager);
            _entities.Add(entity);
            return entity;
        }
        
        public Filter<T> AcquireFilter<T>() where T: struct
        {
            var neededFilters = _filters.Where(filter => filter.GetType() == typeof(Filter<T>)).Cast<Filter<T>>().ToList();
            if (neededFilters.Any())
                return neededFilters.First();
            
            var res = new Filter<T>(_entities.Where(entity => entity.HasComponent<T>()));
            _filters.Add(res);
            return res;
        }

        public IEnumerable<EcsEntity> GetEntitiesForFilter<T>() where T : struct 
            => _entities.Where(entity => entity.HasComponent<T>());
        
        public IEnumerable<EcsEntity> GetEntitiesForFilter<T, T1>() 
            where T : struct
            where T1 : struct
            => _entities.Where(entity => entity.HasComponent<T>() && entity.HasComponent<T1>());
        
        public IEnumerable<EcsEntity> GetEntitiesForFilter<T, T1, T2>() 
            where T : struct
            where T1 : struct
            where T2 : struct
            => _entities.Where(entity => entity.HasComponent<T>() && entity.HasComponent<T1>() && entity.HasComponent<T2>());

        public EcsFilter GetFilter<T>() where T: struct => new EcsFilter(_entities.Where(entity => entity.HasComponent<T>()));

        public void RemoveEmptyEntities()
        {
            var emptyEntities = _entities.Where(entity => entity.GetComponentsCount() == 0).ToList();

            foreach (var emptyEntity in emptyEntities)
                _entities.Remove(emptyEntity);
        }

        public void Destroy() => _entities.Clear();
    }
}
