using Ecs.Interfaces;

namespace Ecs
{
    internal class RunSystemContainer
    {
        public readonly IEcsRunSystem System;
        public string Tag;
        public bool IsActive;

        public RunSystemContainer(IEcsRunSystem runSystem, bool isActive = true, string tag = "")
        {
            System = runSystem;
            Tag = tag;
            IsActive = isActive;
        }
    }
}
