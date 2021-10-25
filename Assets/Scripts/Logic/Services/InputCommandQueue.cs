using System.Collections.Generic;
using System.Linq;
using Logic.InputCommands;

namespace Logic.Services
{
    public class InputCommandQueue
    {
        private readonly Queue<IInputCommand> _inputCommands;

        public InputCommandQueue()
        {
            _inputCommands = new Queue<IInputCommand>();
        }

        public bool HasCommands() => _inputCommands.Any();

        public void Enqueue(IInputCommand command) => _inputCommands.Enqueue(command);

        public IInputCommand Dequeue() => _inputCommands.Dequeue();
    }
}
