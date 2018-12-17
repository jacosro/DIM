using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace Rosana
{
    class RosanaGrammar
    {
        private Grammar grammar;

        public RosanaGrammar()
        {
            Choices programChoice = new Choices();


            SemanticResultValue choiceResultValue = new SemanticResultValue("Explorador de archivos", "explorer.exe");
            GrammarBuilder resultValueBuilder = new GrammarBuilder(choiceResultValue);

            programChoice.Add(resultValueBuilder);
        }
    }
}
