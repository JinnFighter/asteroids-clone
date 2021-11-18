using Ecs.Interfaces;

namespace Ecs
{
    internal class RunSystemContainer
    {
        public readonly IEcsRunSystem System;
        public string Tag;
        public bool IsActive;
        public bool IsInternal;

        public RunSystemContainer(IEcsRunSystem runSystem, bool isActive = true, bool isInternal = false, string tag = "")
        {
            System = runSystem;
            Tag = tag;
            IsActive = isActive;
            IsInternal = isInternal;
        }
    }
}
