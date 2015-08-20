using System;
using System.Text;
using System.Collections.Generic;
using System.IO;


namespace ReportShortenedSentence
{
    public class SentenceTextFile : ISentenceShortenerFileInput
    {
        private int? _number_of_lines;

        protected string file_location{get;set;}

        public int number_of_lines
        {
            get
            {
                if (_number_of_lines == null)
                {
                    ResolveOnlySentenceCount();
                }
                return (int)_number_of_lines;
            }
        }

        public Encoding file_encoding { get; set; }

        public SentenceTextFile(string file_location)
        {
            if (!IsStringASetenceFile(file_location))
            {
                throw new InvalidOperationException();
            }
            else
            {
                this.file_location = file_location;
                file_encoding = Encoding.UTF8;
            }
        }

        
        private void ResolveOnlySentenceCount()
        {
            _number_of_lines = File.ReadAllLines(file_location, file_encoding).Length;
        }


        public Sentence[] ResolveAllSentenceDetail()
        {
            var sentenceDetailToReturn = new List<Sentence>();
            var linesFromFiles = File.ReadAllLines(file_location, file_encoding);
            foreach (var line in linesFromFiles)
            {
                if (String.IsNullOrEmpty(line))
                {
                    var itemToAdd = new Sentence(line);
                    sentenceDetailToReturn.Add(itemToAdd);
                }
            }
            return sentenceDetailToReturn.ToArray();
        }

        public void FindShortenedSentencesForFile(ISentenceShortenerOutput outputToReturnTo)
        {
            if (outputToReturnTo != null)
            {
                var allSentences = ResolveAllSentenceDetail();
                foreach (var sentence in allSentences)
                {
                    outputToReturnTo.WriteShortenedSentence(sentence.short_sentence);
                }
            }
        }


        public static bool IsStringASetenceFile(string stringToReview)
        {
            return (!String.IsNullOrEmpty(stringToReview) && File.Exists(stringToReview)) ;
        }
    }
}
