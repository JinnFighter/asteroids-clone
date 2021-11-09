using UnityEngine;
using UnityEngine.UI;

namespace UnityScripts.Presentation.Views
{
    public class UiTransformBodyView : MonoBehaviour, ITransformBodyView
    {
        [SerializeField] private Text _positionText;
        [SerializeField] private Text _rotationText;
        public void UpdatePosition(float x, float y) => _positionText.text = $"X: {x} Y: {y}";

        public void UpdateRotation(float rotationAngle) => _rotationText.text = $"Angle: {rotationAngle}";
        public void Destroy()
        {
            _positionText.gameObject.SetActive(false);
            _rotationText.gameObject.SetActive(false);
        }
    }
}
