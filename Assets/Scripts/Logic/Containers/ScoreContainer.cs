namespace Logic.Containers
{
    public class ScoreContainer
    {
        private int _score;

        public delegate void ScoreChanged(int score);

        public event ScoreChanged ScoreChangedEvent;

        public ScoreContainer()
        {
            _score = 0;
        }

        public void UpdateScore(int nextScore)
        {
            _score += nextScore;
            ScoreChangedEvent?.Invoke(_score);
        }
    }
}
