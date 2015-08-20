using System;
using System.Collections.Generic;
using System.Text;

namespace ReportShortenedSentence
{
    public class SentenceBufferOutput : ISentenceShortenerOutput
    {
        protected SentenceBufferOutput() { }

        public void WriteShortenedSentence(string shortenedLine)
        {
            System.Diagnostics.Trace.WriteLine(shortenedLine);
        }

        public void WriteShortenedSentences(string[] shortenedLines)
        {
            foreach (var setence in shortenedLines)
            {
                WriteShortenedSentence(setence);
            }
        }

        public void finshedOutput() { }

        public static ISentenceShortenerOutput getOutput()
        {
            return new SentenceBufferOutput();
        }
    }
}
