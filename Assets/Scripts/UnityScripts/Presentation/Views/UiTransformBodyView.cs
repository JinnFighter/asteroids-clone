using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityScripts.Presentation.Views
{
    public class UiTransformBodyView : MonoBehaviour, ITransformBodyView
    {
        [SerializeField] private Text _positionText;
        [SerializeField] private Text _rotationText;

        private void Start()
        {
            UpdatePosition(0f, 0f);
            UpdateRotation(0f);
        }

        public void UpdatePosition(float x, float y) => _positionText.text = $"X: {Math.Round(x, 2)} Y: {Math.Round(y, 2)}";

        public void UpdateRotation(float rotationAngle) => _rotationText.text = $"Angle: {Math.Round(rotationAngle)}";
        public void Destroy()
        {
            _positionText.gameObject.SetActive(false);
            _rotationText.gameObject.SetActive(false);
        }
    }
}
