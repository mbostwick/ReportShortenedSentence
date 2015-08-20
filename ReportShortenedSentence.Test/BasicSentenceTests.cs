using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportShortenedSentence;

namespace ReportShortenedSentence.Test
{
    //TODO: this test should have better sentence generation..
    [TestClass]
    public class BasicSentenceTests : Sentence
    {
        private const string test_sentence = "I think words are Cool! do U";
        private const string resolved_test_sentence = "I t3k w3s a1e C2l! do U";
        private static string[] test_sentence_result = new string[] { "I", "t3k", "w3s", "a1e", "C2l","do","U"};
        private static Dictionary<int, int> test_sentence_word_possitions = getchStaticWordPossitions();

        public BasicSentenceTests()
            : base(test_sentence)
        {
            
        }

        [TestMethod]
        public void TestStaticSentenceResolution()
        {
            Assert.IsTrue(resolved_test_sentence.Equals(short_sentence));
        }

        [TestMethod]
        public void TestStaticSentenceWordResolution()
        {
            bool exactMatchesFound = false;
            var shortenWordsFound = FindShortenedWords();
            if (shortenWordsFound.Length == test_sentence_result.Length)
            {
                for (int currentWordPossition = 0; currentWordPossition < test_sentence_result.Length; currentWordPossition++)
                {
                    exactMatchesFound = test_sentence_result[currentWordPossition].Equals(shortenWordsFound[currentWordPossition]);
                    if (!exactMatchesFound)
                    {
                        break;
                    }
                }
            }
            Assert.IsTrue(exactMatchesFound);
        }

        [TestMethod]
        public void TestStaticSentenceWordBreaks()
        {
            bool exactMatchesFound = false;
            var wordPossitionsFound = ResolveWordPossitions();
            if (wordPossitionsFound != null && test_sentence_word_possitions.Keys.Count == wordPossitionsFound.Keys.Count)
            {
                foreach (var possition in wordPossitionsFound)
                {
                    exactMatchesFound = false;
                    if (test_sentence_word_possitions.ContainsKey(possition.Key))
                    {
                        exactMatchesFound = test_sentence_word_possitions[possition.Key] == possition.Value;
                    }
                    if (!exactMatchesFound)
                    {
                        break;
                    }
                }
            }
            Assert.IsTrue(exactMatchesFound);
        }

        private static Dictionary<int,int> getchStaticWordPossitions()
        {
            var staticWordPossitions = new Dictionary<int, int>();
            staticWordPossitions.Add(0, 0);//I
            staticWordPossitions.Add(2, 6);//think
            staticWordPossitions.Add(8, 12);//words
            staticWordPossitions.Add(14, 16);//are
            staticWordPossitions.Add(18, 21);//Cool
            staticWordPossitions.Add(24, 25);//do
            staticWordPossitions.Add(27, 27);//U
            return staticWordPossitions;
        }
    }
}
