using System;

namespace Logic.Services
{
    public class Randomizer : IRandomizer
    {
        private static Random _random;

        public Randomizer()
        {
            _random ??= new Random();
        }
        
        public bool IsProc(int chance) => _random.Next(0, 100) > 100 - chance;
        
        public int Range(int minValue, int maxValue) => _random.Next(minValue, maxValue);
    }
}
