namespace Rosana
{
    internal class CommandResult
    {
        public bool Success { get; }
        public string Command { get; }

        public CommandResult(bool success, string command)
        {
            this.Success = success;
            this.Command = command;
        }
    }
}