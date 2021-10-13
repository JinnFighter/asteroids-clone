internal class RemoveOneFrameSystem<T> : IEcsRunSystem where T : struct
{
    public void Run(EcsWorld ecsWorld)
    {
        var filter = ecsWorld.GetFilter<T>();
        foreach (var entity in filter)
            entity.RemoveComponent<T>();
    }
}
