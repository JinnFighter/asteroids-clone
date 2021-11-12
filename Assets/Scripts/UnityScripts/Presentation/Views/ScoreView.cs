using UnityEngine;
using UnityEngine.UI;

namespace UnityScripts.Presentation.Views
{
    [RequireComponent(typeof(Text))]
    public class ScoreView : MonoBehaviour, IScoreView
    {
        private Text _text;
    
        // Start is called before the first frame update
        void Start()
        {
            _text = GetComponent<Text>();
        }

        public void UpdateScore(int score) => _text.text = $"Score: {score}";
    }
}
