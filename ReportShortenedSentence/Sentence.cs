using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ReportShortenedSentence
{
    public class Sentence
    {
        private const string regex_to_match_only_alphabetic_letters = "^[a-zA-Z]+$";

        private string _short_sentence;

        protected string sentance
        {
            get
            {
                return sentance_char != null ? new string(sentance_char) : String.Empty;
            }
            set
            {
                sentance_char = String.IsNullOrEmpty(value) ? new char[0] : value.ToCharArray();
            }
        }

        protected char[] sentance_char { get; set; }

        public string short_sentence
        {
            get
            {
                if (String.IsNullOrEmpty(_short_sentence) && !String.IsNullOrEmpty(sentance))
                {
                    FindShortenedWords();
                }
                return _short_sentence;
            }
        }

        public Sentence(string sentance)
        {
            this.sentance = sentance;
        }

        public string[] FindShortenedWords()
        {
            //TODO:consider implementing a lock on sentance_char so that this method wont be broken by thread access
            var itemsMapArea = new StringBuilder();
            var textToReturn = new List<string>();
            var wordBreaks = ResolveWordPossitions();
            for (int currentLetter = 0; currentLetter < sentance_char.Length; currentLetter++)
            {
                if (wordBreaks.ContainsKey(currentLetter))
                {
                    int wordCountSpace = wordBreaks[currentLetter] - currentLetter;
                    if (wordCountSpace > 0)
                    {
                        wordCountSpace -= 1;
                        var shortenedWord = sentance_char[currentLetter] + wordCountSpace.ToString() + sentance_char[wordBreaks[currentLetter]];
                        if (wordCountSpace == 0)
                        {
                            shortenedWord = sentance_char[currentLetter].ToString() + sentance_char[wordBreaks[currentLetter]].ToString();
                        }
                        textToReturn.Add(shortenedWord);
                        itemsMapArea.Append(shortenedWord);
                        currentLetter = wordBreaks[currentLetter];
                    }
                    else
                    {
                        textToReturn.Add(sentance_char[currentLetter].ToString());
                        itemsMapArea.Append(sentance_char[currentLetter]);
                    }
                }
                else
                {
                    itemsMapArea.Append(sentance_char[currentLetter]);
                }
            }
            _short_sentence = itemsMapArea.ToString();
            return textToReturn.ToArray();
        }

        protected Dictionary<int, int> ResolveWordPossitions()
        {
            var wordBreaks = new Dictionary<int, int>();
            bool inWord = false;
            int? startOfWord = null;
            for (int currentLetter = 0; currentLetter < sentance_char.Length; currentLetter++)
            {
                bool wordBreakChar = IsAWordBreakCharacter(sentance_char[currentLetter]);
                if (wordBreakChar && inWord)
                {
                    wordBreaks.Add((int)startOfWord,currentLetter-1);
                    inWord = false;
                }
                else if (!wordBreakChar && !inWord)
                {
                    startOfWord = currentLetter;
                    inWord = true;
                }
            }
            if (inWord)
            {
                wordBreaks.Add((int)startOfWord, sentance_char.Length-1);
            }
            return wordBreaks;
        }

        private static bool IsAWordBreakCharacter(char givenWord)
        {
            return !Char.IsLetter(givenWord);
        }

        public static bool IsStringASetence(string stringToReview)
        {
            return !String.IsNullOrEmpty(stringToReview);
        }
    }
}
