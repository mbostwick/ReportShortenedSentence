using System;
using System.Collections.Generic;
using System.Text;

namespace ReportShortenedSentence
{
    public interface ISentenceShortenerFileInput
    {
        void FindShortenedSentencesForFile(ISentenceShortenerOutput outputToReturnTo);
    }
}
