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
        public static string ABRIR = "abrir";
        public static string CHISTE = "chiste";
        public static string CURIOSIDAD = "curiosidad";
        public static string DESCANSAR = "descansar";


        private SpeechRecognitionEngine recognizer;
        public EventHandler<CommandResult> Callback { get; set; }

        public RosanaCommand()
        {
            recognizer = new SpeechRecognitionEngine();

            recognizer.UnloadAllGrammars();
            recognizer.LoadGrammar(LoadGrammar());
            recognizer.SetInputToDefaultAudioDevice();
            recognizer.UpdateRecognizerSetting("CFGConfidenceRejectionThreshold", 60);
        }

        public CommandResult Listen()
        {
            RecognitionResult result = recognizer.Recognize();

            CommandResult commandResult;

            if (result == null)
            {
                commandResult = new CommandResult(false, null);
            } else
            {
                // Extract command
                commandResult = new CommandResult(true, result);
            }

            return commandResult;
        }

        private Grammar LoadGrammar()
        {
            GrammarBuilder frasesGrammarBuilder = new GrammarBuilder();
            Choices frasesChoice = new Choices();

            // Abrir programa

            Choices abrirChoices = new Choices();

            GrammarBuilder abrirGrammarBuilder = new GrammarBuilder("Abre el");

            SemanticResultValue explorerSemanticValue = new SemanticResultValue("explorador de archivos", "start explorer.exe");
            GrammarBuilder explorerBuilder = new GrammarBuilder(explorerSemanticValue);

            SemanticResultValue notepadSemanticValue = new SemanticResultValue("bloc de notas", "start notepad.exe");
            GrammarBuilder notepadBuilder = new GrammarBuilder(notepadSemanticValue);

            SemanticResultValue chromeSemanticValue = new SemanticResultValue("navegador", "start https://www.upv.es");
            GrammarBuilder chromeBuilder = new GrammarBuilder(chromeSemanticValue);

            abrirChoices.Add(explorerBuilder, notepadBuilder, chromeBuilder);

            SemanticResultKey semanticResultKey = new SemanticResultKey(ABRIR, abrirChoices);

            abrirGrammarBuilder.Append(semanticResultKey);

            // ---

            // Cuéntame un chiste

            GrammarBuilder chisteGrammarBuilder = new GrammarBuilder("Cuéntame un chiste");

            // ---

            // Curiosidad

            GrammarBuilder curiosidadGrammarBuilder = new GrammarBuilder("Dime un dato curioso");

            // ---

            // Descansa

            GrammarBuilder descansaGrammarBuilder = new GrammarBuilder("Descansa");

            // ---

            SemanticResultValue abrirResultValue = new SemanticResultValue(abrirGrammarBuilder, ABRIR);
            SemanticResultValue chisteResultValue = new SemanticResultValue(chisteGrammarBuilder, CHISTE);
            SemanticResultValue curiosidadResultValue = new SemanticResultValue(curiosidadGrammarBuilder, CURIOSIDAD);
            SemanticResultValue descansaResultValue = new SemanticResultValue(descansaGrammarBuilder, DESCANSAR);

            frasesChoice.Add(abrirResultValue, chisteResultValue, curiosidadResultValue, descansaResultValue);

            SemanticResultKey frasesResultKey = new SemanticResultKey("frase", frasesChoice);

            frasesGrammarBuilder.Append(frasesResultKey);

            return new Grammar(frasesGrammarBuilder);
        }

    }
}
