using System.Collections.Generic;
using System.Linq;

namespace GOFPatternExamples.Command
{
    /// <summary>
    /// Invoker
    /// </summary>
    class ControlUnit
    {
        private List<Command> _commands = new List<Command>();

        public void StoreCommand(Command command)
        {
            _commands.Add(command);
        }

        public void ExecuteCommand()
        {
            _commands.Last().Execute();
        }

        public void Undo()
        {
            _commands.Last().UnExecute();
            _commands.Remove(_commands.Last());
        }

        public void Redo()
        {
            _commands.Last().Execute();
            _commands.Add(_commands.Last());
        }
    }
}
