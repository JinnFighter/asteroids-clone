using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ecs
{
    public abstract class EcsFilter : IEnumerable<EcsEntity>
    {
        protected readonly List<EcsEntity> Entities;

        protected EcsFilter(IEnumerable<EcsEntity> entities)
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
    
    public class EcsFilter<C> : EcsFilter where C : struct
    {
        public EcsFilter(IEnumerable<EcsEntity> entities) : base(entities)
        {
        }

        protected override void RemoveEntitiesWithoutComponents()
        {
            var entitiesToRemove = Entities.Where(entity => !entity.HasComponent<C>()).ToList();
            foreach (var entity in entitiesToRemove)
                Entities.Remove(entity);
        }

        protected override IEnumerable<EcsEntity> GetNewEntities(EcsWorld world) => world.GetEntitiesForFilter<C>();

        public EcsFilter<C> Exclude<T>() where T : struct =>
            new EcsFilter<C>(Entities.Where(entity => !entity.HasComponent<T>()));
        
        public EcsFilter<C> Exclude<T1, T2>() 
            where T1 : struct 
            where T2 : struct
            => new EcsFilter<C>(Entities.Where(entity => !(entity.HasComponent<T1>() || entity.HasComponent<T2>())));
    }
    
    public class EcsFilter<C, C1> : EcsFilter 
        where C : struct
        where C1 : struct
    {
        public EcsFilter(IEnumerable<EcsEntity> entities) : base(entities)
        {
        }

        protected override void RemoveEntitiesWithoutComponents()
        {
            var entitiesToRemove = Entities.Where(entity => !(entity.HasComponent<C>() || entity.HasComponent<C1>())).ToList();
            foreach (var entity in entitiesToRemove)
                Entities.Remove(entity);
        }

        protected override IEnumerable<EcsEntity> GetNewEntities(EcsWorld world) => world.GetEntitiesForFilter<C, C1>();

        public EcsFilter<C, C1> Exclude<T>() where T : struct =>
            new EcsFilter<C, C1>(Entities.Where(entity => !entity.HasComponent<T>()));
        
        public EcsFilter<C, C1> Exclude<T, T1>() 
            where T : struct 
            where T1 : struct
            => new EcsFilter<C, C1>(Entities.Where(entity => !(entity.HasComponent<T>() || entity.HasComponent<T1>())));
    }
    
    public class EcsFilter<C, C1, C2> : EcsFilter 
        where C : struct
        where C1 : struct
        where C2 : struct
    {
        public EcsFilter(IEnumerable<EcsEntity> entities) : base(entities)
        {
        }

        protected override void RemoveEntitiesWithoutComponents()
        {
            var entitiesToRemove = Entities.Where(entity => !(entity.HasComponent<C>() || entity.HasComponent<C1>())).ToList();
            foreach (var entity in entitiesToRemove)
                Entities.Remove(entity);
        }

        protected override IEnumerable<EcsEntity> GetNewEntities(EcsWorld world) => world.GetEntitiesForFilter<C, C1, C2>();

        public EcsFilter<C, C1, C2> Exclude<T>() where T : struct =>
            new EcsFilter<C, C1, C2>(Entities.Where(entity => !entity.HasComponent<T>()));
        
        public EcsFilter<C, C1, C2> Exclude<T, T1>() 
            where T : struct 
            where T1 : struct
            => new EcsFilter<C, C1, C2>(Entities.Where(entity => !(entity.HasComponent<T>() || entity.HasComponent<T1>())));
    }
}
