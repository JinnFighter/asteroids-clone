namespace Logic.Containers
{
    public class ScoreContainer
    {
        public int Score { get; private set; }

        public delegate void ScoreChanged(int score);

        public event ScoreChanged ScoreChangedEvent;

        public ScoreContainer()
        {
            Score = 0;
        }

        public void UpdateScore(int nextScore)
        {
            Score += nextScore;
            ScoreChangedEvent?.Invoke(Score);
        }
    }
}
