using System;
using System.Collections.Generic;
using System.Linq;

namespace PacSharpApp
{
    [Serializable]
    public class Highscores
    {
        [NonSerialized]
        private const int NumberToSave = 10;
        
        private List<(int score, string initials)> scores = new List<(int, string)>(NumberToSave);

        public Highscores()
        {
            scores.Add((15000, "ZYL"));
            scores.Add((10000, "ZYL"));
            scores.Add((9000, "ZYL"));
            scores.Add((8000, "ZYL"));
            scores.Add((7000, "ZYL"));
            scores.Add((6000, "ZYL"));
            scores.Add((5000, "ZYL"));
            scores.Add((3000, "ZYL"));
            scores.Add((2500, "ZYL"));
            scores.Add((1000, "ZYL"));
        }

        public int Highscore => scores.Max(entry => entry.score);
        public IReadOnlyList<(int score, string initials)> ToDisplay
        {
            get
            {
                scores.Sort((entry1, entry2) => entry2.score.CompareTo(entry1.score));
                return scores;
            }
        }

        public int Minimum => scores.Min(entry => entry.score);

        public int AddScore(int newScore, string initials)
        {
            int minScore = Minimum;
            scores.Remove(scores.Where(entry => entry.score == minScore).First());
            scores.Add((newScore, initials));
            scores.Sort((entry1, entry2) => entry2.score.CompareTo(entry1.score));
            return scores.FindIndex(entry => entry.score == newScore && entry.initials == initials);
        }

        internal void Update(int index, string newInitials)
        {
            scores[index] = (scores[index].score, newInitials);
        }
    }
}
