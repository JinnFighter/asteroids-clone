using Logic.Services;
using UnityEngine;

namespace UnityScripts.Services
{
    public class UnityDeltaTimeCounter : IDeltaTimeCounter
    {
        public void Reset()
        {
        }

        public float GetDeltaTime() => Time.deltaTime;
    }
}
