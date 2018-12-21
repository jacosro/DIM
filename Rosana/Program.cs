using System;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Globalization;
using System.Collections.Generic;

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
            Setup();
            Listen();
        }

        private static void Setup()
        {

            activationRecognizer.SetInputToDefaultAudioDevice();
            activationRecognizer.UpdateRecognizerSetting("CFGConfidenceRejectionThreshold", 70);
            activationRecognizer.LoadGrammar(LoadActivationGrammar());

        }

        private static void Listen() { 
            while (run)
            {
                RecognitionResult result = activationRecognizer.Recognize();

                if (result != null)
                {
                    Console.WriteLine("Reconocido: " + result.Text);

                    synth.Speak("Te escucho");

                    RosanaCommand command = new RosanaCommand();

                    CommandResult commandResult = command.Listen();

                    if (commandResult.Success)
                    {
                        Console.WriteLine("Reconocido: " + commandResult.Result.Text);

                        synth.Speak("En seguida");

                        SemanticValue semantics = commandResult.Result.Semantics;

                        if (!semantics.ContainsKey("frase"))
                        {
                            Console.WriteLine("Error: no se ha reconocido ninguna frase"); // Should not happen in any case
                        }
                        else
                        {
                            SemanticValue frase = semantics["frase"];

                            string comando = (string) frase.Value;

                            if (RosanaCommand.ABRIR.Equals(comando))
                            {
                                System.Diagnostics.Process process = new System.Diagnostics.Process();
                                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                                startInfo.FileName = "cmd.exe";
                                startInfo.Arguments = "/C " + (string) frase[RosanaCommand.ABRIR].Value;
                                process.StartInfo = startInfo;
                                process.Start();
                            } else if (RosanaCommand.CHISTE.Equals(comando))
                            {
                                synth.Speak("Van dos y se cae el de en medio");
                            } else if (RosanaCommand.CURIOSIDAD.Equals(comando))
                            {
                                synth.Speak("¿Sabías que los cocodrilos no pueden sacar la lengua?");
                            } else if (RosanaCommand.DESCANSAR.Equals(comando))
                            {
                                synth.Speak("Adiós");
                                run = false;
                            }
                        }
                    }
                    else
                    {
                        synth.Speak("No te he entendido");
                    }
                }
            }
        }

        private static Grammar LoadActivationGrammar()
        {
            GrammarBuilder grammarBuilder = new GrammarBuilder("Rosana");
            Grammar grammar = new Grammar(grammarBuilder);
            grammar.Name = "Rosana";
            return grammar;
        }
    }
}
