namespace UnityScripts.Presentation.Models
{
    public class ScoreModel
    {
        private int _score;

        public delegate void ScoreChanged(int score);

        public event ScoreChanged ScoreChangedEvent;
        
        public ScoreModel(int score = 0)
        {
            _score = score;
        }

        public void UpdateScore(int score)
        {
            _score = score;
            ScoreChangedEvent?.Invoke(_score);
        }
    }
}
