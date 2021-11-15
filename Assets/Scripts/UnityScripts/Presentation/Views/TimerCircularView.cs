using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityScripts.Presentation.Views
{
    [RequireComponent(typeof(Image))]
    public class TimerCircularView : MonoBehaviour, ITimerView
    {
        private Image _image;

        void Start()
        {
            _image = GetComponent<Image>();
        }

        public void UpdateCurrentTime(float time) => _image.fillAmount = time;
    }
}
