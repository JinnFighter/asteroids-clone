using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ecs
{
    public abstract class EcsFilter : IEnumerable<int>
    {
        protected readonly List<EcsEntity> Entities;

        protected EcsFilter(IEnumerable<EcsEntity> entities)
        {
            Entities = new List<EcsEntity>(entities);
        }

        internal void UpdateFilter(EcsWorld world)
        {
            RemoveEntitiesWithoutComponents();
            foreach (var newEntity in GetNewEntities(world).Except(Entities))
                Entities.Add(newEntity);
        }

        protected abstract void RemoveEntitiesWithoutComponents();
        protected abstract IEnumerable<EcsEntity> GetNewEntities(EcsWorld world);
        
        public IEnumerator<int> GetEnumerator() => new EcsFilterEnumerator(Entities.Count);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public int GetEntitiesCount() => Entities.Count;

        public bool IsEmpty() => Entities.Count == 0;

        public EcsEntity GetEntity(int index) => Entities[index];

        protected void TryRemoveComponent<T>(EcsEntity entity) where T : struct
        {
            if(entity.HasComponent<T>())
                entity.RemoveComponent<T>();
        }

        protected IEnumerable<EcsEntity> ExcludeInternal<C>() where C : struct =>
            Entities.Where(entity => !entity.HasComponent<C>());

        protected IEnumerable<EcsEntity> ExcludeInternal<C, C1>() where C : struct
            where C1 : struct =>
            Entities.Where(entity => !(entity.HasComponent<C>() || entity.HasComponent<C1>()));

        protected abstract void ClearComponents(EcsEntity entity);
        internal void Clear()
        {
            foreach (var entity in Entities)
                ClearComponents(entity);

            Entities.Clear();
        }
    }
    
    public class EcsFilter<C> : EcsFilter where C : struct
    {
        internal EcsFilter(IEnumerable<EcsEntity> entities) : base(entities)
        {
        }

        protected override void RemoveEntitiesWithoutComponents()
        {
            var entitiesToRemove = Entities.Where(entity => !entity.HasComponent<C>()).ToList();
            foreach (var entity in entitiesToRemove)
                Entities.Remove(entity);
        }

        protected override IEnumerable<EcsEntity> GetNewEntities(EcsWorld world) => world.GetEntitiesForFilter<C>();
        protected override void ClearComponents(EcsEntity entity) => TryRemoveComponent<C>(entity);

        public ref C Get1(int index) => ref Entities[index].GetComponent<C>();

        public EcsFilter<C> Exclude<T>() where T : struct =>
            new EcsFilter<C>(ExcludeInternal<T>());
        
        public EcsFilter<C> Exclude<T1, T2>() 
            where T1 : struct 
            where T2 : struct
            => new EcsFilter<C>(ExcludeInternal<T1, T2>());
    }
    
    public class EcsFilter<C, C1> : EcsFilter 
        where C : struct
        where C1 : struct
    {
        internal EcsFilter(IEnumerable<EcsEntity> entities) : base(entities)
        {
        }

        protected override void RemoveEntitiesWithoutComponents()
        {
            var entitiesToRemove = Entities.Where(entity => !(entity.HasComponent<C>() && entity.HasComponent<C1>())).ToList();
            foreach (var entity in entitiesToRemove)
                Entities.Remove(entity);
        }

        protected override IEnumerable<EcsEntity> GetNewEntities(EcsWorld world) => world.GetEntitiesForFilter<C, C1>();
        protected override void ClearComponents(EcsEntity entity)
        {
            TryRemoveComponent<C>(entity);
            TryRemoveComponent<C1>(entity);
        }

        public ref C Get1(int index) => ref Entities[index].GetComponent<C>();
        public ref C1 Get2(int index) => ref Entities[index].GetComponent<C1>();

        public EcsFilter<C, C1> Exclude<T>() where T : struct =>
            new EcsFilter<C, C1>(ExcludeInternal<T>());
        
        public EcsFilter<C, C1> Exclude<T1, T2>() 
            where T1 : struct 
            where T2 : struct
            => new EcsFilter<C, C1>(ExcludeInternal<T1, T2>());
    }
    
    public class EcsFilter<C, C1, C2> : EcsFilter 
        where C : struct
        where C1 : struct
        where C2 : struct
    {
        internal EcsFilter(IEnumerable<EcsEntity> entities) : base(entities)
        {
        }

        protected override void RemoveEntitiesWithoutComponents()
        {
            var entitiesToRemove = Entities.Where(entity => !(entity.HasComponent<C>() && entity.HasComponent<C1>() && entity.HasComponent<C2>())).ToList();
            foreach (var entity in entitiesToRemove)
                Entities.Remove(entity);
        }

        protected override IEnumerable<EcsEntity> GetNewEntities(EcsWorld world) => world.GetEntitiesForFilter<C, C1, C2>();
        protected override void ClearComponents(EcsEntity entity)
        {
            TryRemoveComponent<C>(entity);
            TryRemoveComponent<C1>(entity);
            TryRemoveComponent<C2>(entity);
        }

        public ref C Get1(int index) => ref Entities[index].GetComponent<C>();
        public ref C1 Get2(int index) => ref Entities[index].GetComponent<C1>();
        public ref C2 Get3(int index) => ref Entities[index].GetComponent<C2>();

        public EcsFilter<C, C1, C2> Exclude<T>() where T : struct =>
            new EcsFilter<C, C1, C2>(ExcludeInternal<T>());
        
        public EcsFilter<C, C1, C2> Exclude<T1, T2>() 
            where T1 : struct 
            where T2 : struct
            => new EcsFilter<C, C1, C2>(ExcludeInternal<T1, T2>());
    }
    
    public class EcsFilter<C, C1, C2, C3> : EcsFilter 
        where C : struct
        where C1 : struct
        where C2 : struct
        where C3 : struct
    {
        internal EcsFilter(IEnumerable<EcsEntity> entities) : base(entities)
        {
        }

        protected override void RemoveEntitiesWithoutComponents()
        {
            var entitiesToRemove = Entities.Where(entity => !(entity.HasComponent<C>() 
                                                              && entity.HasComponent<C1>() 
                                                              && entity.HasComponent<C2>() 
                                                              && entity.HasComponent<C3>())).ToList();
            foreach (var entity in entitiesToRemove)
                Entities.Remove(entity);
        }

        protected override IEnumerable<EcsEntity> GetNewEntities(EcsWorld world) => world.GetEntitiesForFilter<C, C1, C2, C3>();
        protected override void ClearComponents(EcsEntity entity)
        {
            TryRemoveComponent<C>(entity);
            TryRemoveComponent<C1>(entity);
            TryRemoveComponent<C2>(entity);
            TryRemoveComponent<C3>(entity);
        }

        public ref C Get1(int index) => ref Entities[index].GetComponent<C>();
        public ref C1 Get2(int index) => ref Entities[index].GetComponent<C1>();
        public ref C2 Get3(int index) => ref Entities[index].GetComponent<C2>();
        public ref C3 Get4(int index) => ref Entities[index].GetComponent<C3>();

        public EcsFilter<C, C1, C2, C3> Exclude<T>() where T : struct =>
            new EcsFilter<C, C1, C2, C3>(ExcludeInternal<T>());
        
        public EcsFilter<C, C1, C2, C3> Exclude<T1, T2>() 
            where T1 : struct 
            where T2 : struct
            => new EcsFilter<C, C1, C2, C3>(ExcludeInternal<T1, T2>());
    }
}
