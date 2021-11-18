using System.Collections.Generic;
using System.Linq;

namespace Ecs
{
    public class EcsWorld
    {
        private readonly List<EcsEntity> _entities;
        private readonly EcsComponentManager _componentManager;
        private readonly List<EcsFilter> _filters;
        private readonly EntityPool _entityPool;

        public EcsWorld()
        {
            _entities = new List<EcsEntity>();
            _componentManager = new EcsComponentManager();
            _filters = new List<EcsFilter>();
            _entityPool = new EntityPool();
        }

        public EcsEntity CreateEntity()
        {
            var entity = _entityPool.Count() > 0 ? _entityPool.GetItem() : new EcsEntity(_componentManager);
            _entities.Add(entity);
            return entity;
        }

        public EcsFilter<T> GetFilter<T>() where T: struct
        {
            var neededFilters = _filters.Where(filter => filter.GetType() == typeof(EcsFilter<T>)).Cast<EcsFilter<T>>().ToList();
            if (neededFilters.Any())
            {
                var filter = neededFilters.First();
                filter.UpdateFilter(this);
                return filter;
            }

            var res = new EcsFilter<T>(GetEntitiesForFilter<T>());
            _filters.Add(res);
            return res;
        }
        
        public EcsFilter<T, T1> GetFilter<T, T1>() 
            where T: struct 
            where T1 : struct
        {
            var neededFilters = _filters.Where(filter => filter.GetType() == typeof(EcsFilter<T, T1>)).Cast<EcsFilter<T, T1>>().ToList();
            if (neededFilters.Any())
            {
                var filter = neededFilters.First();
                filter.UpdateFilter(this);
                return filter;
            }
            
            var res = new EcsFilter<T, T1>(GetEntitiesForFilter<T, T1>());
            _filters.Add(res);
            return res;
        }

        public EcsFilter<T, T1, T2> GetFilter<T, T1, T2>()
            where T : struct
            where T1 : struct
            where T2 : struct
        {
            var neededFilters = _filters.Where(filter => filter.GetType() == typeof(EcsFilter<T, T1, T2>)).Cast<EcsFilter<T, T1, T2>>().ToList();
            if (neededFilters.Any())
            {
                var filter = neededFilters.First();
                filter.UpdateFilter(this);
                return filter;
            }
            
            var res = new EcsFilter<T, T1, T2>(GetEntitiesForFilter<T, T1, T2>());
            _filters.Add(res);
            return res;
        }

        public EcsFilter<T, T1, T2, T3> GetFilter<T, T1, T2, T3>()
            where T : struct
            where T1 : struct
            where T2 : struct
            where T3 : struct
        {
            var neededFilters = _filters.Where(filter => filter.GetType() == typeof(EcsFilter<T, T1, T2, T3>)).Cast<EcsFilter<T, T1, T2, T3>>().ToList();
            if (neededFilters.Any())
            {
                var filter = neededFilters.First();
                filter.UpdateFilter(this);
                return filter;
            }
            
            var res = new EcsFilter<T, T1, T2, T3>(GetEntitiesForFilter<T, T1, T2, T3>());
            _filters.Add(res);
            return res;
        }

        internal IEnumerable<EcsEntity> GetEntitiesForFilter<T>() where T : struct 
            => _entities.Where(entity => entity.HasComponent<T>());
        
        internal IEnumerable<EcsEntity> GetEntitiesForFilter<T, T1>() 
            where T : struct
            where T1 : struct
            => _entities.Where(entity => entity.HasComponent<T>() && entity.HasComponent<T1>());
        
        internal IEnumerable<EcsEntity> GetEntitiesForFilter<T, T1, T2>() 
            where T : struct
            where T1 : struct
            where T2 : struct
            => _entities.Where(entity => entity.HasComponent<T>() && entity.HasComponent<T1>() && entity.HasComponent<T2>());
        
        internal IEnumerable<EcsEntity> GetEntitiesForFilter<T, T1, T2, T3>() 
            where T : struct
            where T1 : struct
            where T2 : struct
            where T3 : struct
            => _entities.Where(entity => entity.HasComponent<T>() 
                                         && entity.HasComponent<T1>() 
                                         && entity.HasComponent<T2>() 
                                         && entity.HasComponent<T3>());
        

        internal void RemoveEmptyEntities()
        {
            var emptyEntities = _entities.Where(entity => entity.GetComponentsCount() == 0).ToList();

            foreach (var emptyEntity in emptyEntities)
            {
                _entities.Remove(emptyEntity);
                _entityPool.AddItem(emptyEntity);
            }
        }

        public void Destroy() => _entities.Clear();
    }
}
