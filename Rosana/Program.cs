using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace Rosana
{
    class Program
    {
        static SpeechSynthesizer synth = new SpeechSynthesizer();
        static SpeechRecognitionEngine recognizer;
        static bool done = false;
        static bool speechOn = true;

        static void Main(string[] args)
        {
            synth.Speak("Hola");
            synth.Speak("Soy tu asistente virtual");
            synth.Speak("Me llamo Rosana");
            synth.Speak("Puedes captar mi atención diciendo mi nombre");
            synth.Speak("Después dime algo que pueda entender y lo haré");

            Init();
        }

        private static void Init()
        {
            recognizer.SetInputToDefaultAudioDevice();
            recognizer.UnloadAllGrammars();
            recognizer.UpdateRecognizerSetting("CFGConfidenceRejectionThresold", 60);
            // recognizer.LoadGrammar(grammar);
            recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(OnSpeechRecognized);


            return;
        }

        private static void OnSpeechRecognized(object sender, SpeechRecognizedEventArgs speechRecognizedEvent)
        {
            SemanticValue semantics = speechRecognizedEvent.Result.Semantics;

            string rawText = speechRecognizedEvent.Result.Text;
            RecognitionResult result = speechRecognizedEvent.Result;

            if (!semantics.ContainsKey("rgb"))
            {
                
            }

        }


    }
}
