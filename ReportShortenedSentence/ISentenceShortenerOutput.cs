using System;
using System.Collections.Generic;
using System.Text;

namespace ReportShortenedSentence
{
    public interface ISentenceShortenerOutput
    {
        void WriteShortenedSentence(string shortenedLine);

        void WriteShortenedSentences(string[] shortenedLines);

        void finshedOutput();
    }
}
