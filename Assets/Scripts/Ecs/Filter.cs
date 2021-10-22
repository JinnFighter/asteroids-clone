using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ecs
{
    public abstract class Filter : IEnumerable<EcsEntity>
    {
        protected readonly List<EcsEntity> Entities;

        protected Filter(IEnumerable<EcsEntity> entities)
        {
            Entities = new List<EcsEntity>(entities);
        }

        public void UpdateFilter(EcsWorld world)
        {
            RemoveEntitiesWithoutComponents();
            var newEntities = GetNewEntities(world).Except(Entities).ToList();
            foreach (var newEntity in newEntities)
                Entities.Add(newEntity);
        }

        protected abstract void RemoveEntitiesWithoutComponents();
        protected abstract IEnumerable<EcsEntity> GetNewEntities(EcsWorld world);
        
        public IEnumerator<EcsEntity> GetEnumerator() => new EcsFilterEnumerator(Entities);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public int GetEntitiesCount() => Entities.Count;

        public bool IsEmpty() => Entities.Count == 0;
    }
    
    public class Filter<C> : Filter where C : struct
    {
        public Filter(IEnumerable<EcsEntity> entities) : base(entities)
        {
        }

        protected override void RemoveEntitiesWithoutComponents()
        {
            var entitiesToRemove = Entities.Where(entity => !entity.HasComponent<C>()).ToList();
            foreach (var entity in entitiesToRemove)
                Entities.Remove(entity);
        }

        protected override IEnumerable<EcsEntity> GetNewEntities(EcsWorld world) => world.GetEntitiesForFilter<C>();

        public Filter<C> Exclude<T>() where T : struct =>
            new Filter<C>(Entities.Where(entity => !entity.HasComponent<T>()));
        
        public Filter<C> Exclude<T1, T2>() 
            where T1 : struct 
            where T2 : struct
            => new Filter<C>(Entities.Where(entity => !(entity.HasComponent<T1>() || entity.HasComponent<T2>())));
    }
    
    public class Filter<C, C1> : Filter 
        where C : struct
        where C1 : struct
    {
        public Filter(IEnumerable<EcsEntity> entities) : base(entities)
        {
        }

        protected override void RemoveEntitiesWithoutComponents()
        {
            var entitiesToRemove = Entities.Where(entity => !(entity.HasComponent<C>() || entity.HasComponent<C1>())).ToList();
            foreach (var entity in entitiesToRemove)
                Entities.Remove(entity);
        }

        protected override IEnumerable<EcsEntity> GetNewEntities(EcsWorld world) => world.GetEntitiesForFilter<C, C1>();

        public Filter<C, C1> Exclude<T>() where T : struct =>
            new Filter<C, C1>(Entities.Where(entity => !entity.HasComponent<T>()));
        
        public Filter<C, C1> Exclude<T, T1>() 
            where T : struct 
            where T1 : struct
            => new Filter<C, C1>(Entities.Where(entity => !(entity.HasComponent<T>() || entity.HasComponent<T1>())));
    }
    
    public class Filter<C, C1, C2> : Filter 
        where C : struct
        where C1 : struct
        where C2 : struct
    {
        public Filter(IEnumerable<EcsEntity> entities) : base(entities)
        {
        }

        protected override void RemoveEntitiesWithoutComponents()
        {
            var entitiesToRemove = Entities.Where(entity => !(entity.HasComponent<C>() || entity.HasComponent<C1>())).ToList();
            foreach (var entity in entitiesToRemove)
                Entities.Remove(entity);
        }

        protected override IEnumerable<EcsEntity> GetNewEntities(EcsWorld world) => world.GetEntitiesForFilter<C, C1, C2>();

        public Filter<C, C1, C2> Exclude<T>() where T : struct =>
            new Filter<C, C1, C2>(Entities.Where(entity => !entity.HasComponent<T>()));
        
        public Filter<C, C1, C2> Exclude<T, T1>() 
            where T : struct 
            where T1 : struct
            => new Filter<C, C1, C2>(Entities.Where(entity => !(entity.HasComponent<T>() || entity.HasComponent<T1>())));
    }
}
