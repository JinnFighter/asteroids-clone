using Ecs;

namespace Logic.InputCommands
{
    public interface IInputCommand
    {
        void Execute(EcsWorld world);
    }
}
