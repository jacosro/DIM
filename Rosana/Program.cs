using System;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Globalization;

namespace Rosana
{
    class Program
    {
        // static CultureInfo cultureInfo = new CultureInfo("es-ES", true);
        static SpeechSynthesizer synth = new SpeechSynthesizer();
        static SpeechRecognitionEngine activationRecognizer = new SpeechRecognitionEngine();
        static bool run = true;
        static RosanaCommand commands;

        static void Main(string[] args)
        {
            /*
            synth.Speak("Hola");
            synth.Speak("Soy tu asistente virtual");
            synth.Speak("Me llamo Rosana");
            synth.Speak("Puedes captar mi atención diciendo mi nombre");
            synth.Speak("Después dime algo que pueda entender y lo haré");
            */
            Setup();
        }

        private static void Setup()
        {
            // Activation grammar ("Rosana")
            // activationRecognizer.UnloadAllGrammars();


            activationRecognizer.SetInputToDefaultAudioDevice();
            // activationRecognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(OnSpeechRecognized);
            // activationRecognizer.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>((sender, e) => synth.Speak("No he oído nada"));
            // activationRecognizer.BabbleTimeout = TimeSpan.FromSeconds(2);

            activationRecognizer.LoadGrammar(LoadActivationGrammar());

            activationRecognizer.BabbleTimeout = TimeSpan.FromSeconds(5);
            activationRecognizer.InitialSilenceTimeout = TimeSpan.FromSeconds(1);
            activationRecognizer.EndSilenceTimeout = TimeSpan.FromSeconds(1);

            // Console.WriteLine("Reconociendo");
            // activationRecognizer.RecognizeAsync(RecognizeMode.Multiple);

            
            while (run) {
                Console.WriteLine("Reconociendo");
                RecognitionResult result = activationRecognizer.Recognize(); // TimeSpan.FromSeconds(1.5));

                if (result != null)
                {
                    Console.WriteLine("Recognized: " + result.Text);
                }
            }
        }

        private static Grammar LoadActivationGrammar()
        {
            GrammarBuilder grammarBuilder = new GrammarBuilder("Rosana");
            // grammarBuilder.Culture = cultureInfo;
            Grammar grammar = new Grammar(grammarBuilder);
            grammar.Name = "Rosana";
            return grammar;
        }

        private static void OnSpeechRecognized(object sender, SpeechRecognizedEventArgs speechRecognizedEvent)
        {
            activationRecognizer.RecognizeAsyncCancel();

            synth.Speak("Te escucho");

            RosanaCommand command = new RosanaCommand();

            // If Async
            /*
            command.Callback = new EventHandler<CommandResult>((_sender, result) =>
            {
                if (result.Success)
                {
                    synth.Speak(result.Text);
                } else
                {
                    synth.Speak("Lo siento, no te he entendido");
                }
            });
            */

            CommandResult result = command.Listen();

            if (result.Success)
            {
                synth.Speak("En seguida");
                // Execute command
                // result.Command....
            } else
            {
                synth.Speak("No te he entendido");
            }
        }


    }
}
