namespace Helpers
{
    public class GameplayTimer
    {
        private float _startTime;

        public float StartTime
        {
            get => _startTime;
            set
            {
                StartTime = value;
                StartTimeChangedEvent?.Invoke(_startTime);
            }
        }

        private float _currentTime;

        public float CurrentTime
        {
            get => _currentTime;
            set
            {
                
                CurrentTime = value > 0f ? value : 0f;
                CurrentTimeChangedEvent?.Invoke(_currentTime);
            }
        }

        public delegate void StartTimeChanged(float time);

        public event StartTimeChanged StartTimeChangedEvent;
        
        public delegate void CurrentTimeChanged(float time);

        public event CurrentTimeChanged CurrentTimeChangedEvent;
    }
}
