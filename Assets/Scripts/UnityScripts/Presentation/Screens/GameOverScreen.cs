using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UnityScripts.Presentation.Screens
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private Text _scoreText;

        public void UpdateScore(int score) => _scoreText.text = $"Score: {score}";
        
        public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        public void Quit() => Application.Quit();
    }
}
