using System;

namespace Logic.Services
{
    public class DefaultDeltaTimeCounter : IDeltaTimeCounter
    {
        private DateTime _lastSavedTime;

        public void Reset() => _lastSavedTime = DateTime.Now;

        public float GetDeltaTime()
        {
            var currentTime = DateTime.Now;
            return (currentTime.Ticks - _lastSavedTime.Ticks) / 10000000f;
        }
    }
}
