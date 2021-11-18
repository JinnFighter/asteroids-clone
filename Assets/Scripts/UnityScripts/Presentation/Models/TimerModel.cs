namespace UnityScripts.Presentation.Models
{
    public class TimerModel
    {

        public float StartTime { get; private set; }

        public float CurrentTime { get; private set; }

        public TimerModel(float startTime)
        {
            StartTime = startTime;
        }
        
        public void UpdateStartTime(float time) => StartTime = time;

        public void UpdateTime(float time)
        {
            CurrentTime = time;
            CurrentTimeChangedEvent?.Invoke(CurrentTime);
        }

        public delegate void CurrentTimeChanged(float time);

        public event CurrentTimeChanged CurrentTimeChangedEvent;
    }
}
