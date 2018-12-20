using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace Rosana
{
    class RosanaCommand
    {
        private SpeechRecognitionEngine recognizer;
        public EventHandler<CommandResult> Callback { get; set; }

        public RosanaCommand()
        {
            recognizer = new SpeechRecognitionEngine();

            recognizer.UnloadAllGrammars();
            recognizer.LoadGrammar(LoadGrammar());
            recognizer.SetInputToDefaultAudioDevice();

            // If async
            /*
            recognizer.RecognizeCompleted += new EventHandler<RecognizeCompletedEventArgs>((_sender, _event) =>
            {
                this.Callback.Invoke(this, new CommandResult(true, _event.Result.Text));
            });

            recognizer.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>((_sender, _event) =>
            {
                this.Callback.Invoke(this, new CommandResult(false, null));
            });
            */
        }

        public CommandResult Listen()
        {
            // Sync
            RecognitionResult result = recognizer.Recognize();

            CommandResult commandResult;

            if (result == null)
            {
                commandResult = new CommandResult(false, null);
            } else
            {
                // Extract command
                commandResult = new CommandResult(true, result.Text);
            }

            return commandResult;
        }

        public void ListenAsync()
        {
            // If Async
            recognizer.RecognizeAsync(RecognizeMode.Single);
        }

        private Grammar LoadGrammar()
        {
            Choices programChoice = new Choices();


            SemanticResultValue choiceResultValue = new SemanticResultValue("Explorador de archivos", "explorer.exe");
            GrammarBuilder resultValueBuilder = new GrammarBuilder(choiceResultValue);

            programChoice.Add(resultValueBuilder);

            return null;
        }

    }
}
