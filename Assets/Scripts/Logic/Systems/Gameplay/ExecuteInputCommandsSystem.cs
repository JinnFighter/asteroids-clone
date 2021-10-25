using Ecs;
using Ecs.Interfaces;
using Logic.Services;

namespace Logic.Systems.Gameplay
{
    public class ExecuteInputCommandsSystem : IEcsRunSystem
    {
        private readonly InputCommandQueue _inputCommandsQueue;

        public ExecuteInputCommandsSystem(InputCommandQueue inputCommandQueue)
        {
            _inputCommandsQueue = inputCommandQueue;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            while (_inputCommandsQueue.HasCommands())
            {
                var command = _inputCommandsQueue.Dequeue();
                command.Execute(ecsWorld);
            }
        }
    }
}
