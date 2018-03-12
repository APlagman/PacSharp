using System;
using System.Collections.Generic;
using System.Linq;

namespace PacSharpApp
{
    [Serializable]
    public class Highscores
    {
        private const int NumberToSave = 10;

        private List<(int score, string initials)> scores = new List<(int, string)>(NumberToSave);

        public int Highscore => scores.Max(entry => entry.score);
        public IReadOnlyList<(int score, string initials)> ToDisplay => scores;

        public void AddScore(int newScore, string initials)
        {
            int minScore = scores.Min(entry => entry.score);
            scores.Remove(scores.Where(entry => entry.score == minScore).First());
            scores.Add((newScore, initials));
            scores.Sort((entry1, entry2) => entry1.score.CompareTo(entry2.score));
        }
    }
}
