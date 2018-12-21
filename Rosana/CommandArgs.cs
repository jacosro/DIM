using System.Speech.Recognition;

namespace Rosana
{
    internal class CommandResult
    {
        public bool Success { get; }
        public RecognitionResult Result { get; }

        public CommandResult(bool success, RecognitionResult result)
        {
            this.Success = success;
            this.Result = result;
        }
    }
}