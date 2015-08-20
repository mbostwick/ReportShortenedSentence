using System;
using System.Collections.Generic;
using System.Text;

namespace ReportShortenedSentence
{
    class CommandLineProgram 
    {
        const int program_had_no_error = 0;
        const int program_had_error = 1;

        //A solution that parses a sentence(s) and produces an output of a shortened sentence:
        //Input a sentence.
            //*parses the first letter
            //*the number of distinct characters between the beginning character and the ending character,
            //*parser the last letter
       //Output the shortened word
       //An example input: The smooth cat programmed. 
       //An example output: T1e s4h c1t p8d
        static int Main(string[] args)
        {
            System.Diagnostics.Trace.Listeners.Add(new System.Diagnostics.ConsoleTraceListener());
            //TODO:implement a an nugget command line parser to handle input
            bool errorInCommandLine = false;
            if (args == null || args.Length < 1 || String.IsNullOrEmpty(args[0]))
            {
                ShowHelp();
                errorInCommandLine = true;
            }
            var outputMethod = SentenceBufferOutput.getOutput();
            if (!errorInCommandLine)
            {
                foreach (var givenInput in args)
                {
                    var inputIsSetenceFile = SentenceTextFile.IsStringASetenceFile(givenInput);
                    if (inputIsSetenceFile)
                    {
                        var fileToHandle = new SentenceTextFile(givenInput);
                        fileToHandle.FindShortenedSentencesForFile(outputMethod);
                    }
                    else if (Sentence.IsStringASetence(givenInput))
                    {
                        var setenceToHandle = new Sentence(givenInput);
                        outputMethod.WriteShortenedSentence(setenceToHandle.short_sentence);
                    }
                    //TODO:add command line option to control invalid input or throw exception when errors are found
                    //for now this will run clean
                }
            }
            outputMethod.finshedOutput();
            return errorInCommandLine ? program_had_error : program_had_no_error;
        }

        private static void ShowHelp()
        {
            System.Diagnostics.Trace.WriteLine(OutputMessage.help_message);
        }

        
    }
}
