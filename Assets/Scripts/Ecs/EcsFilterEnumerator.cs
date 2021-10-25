using System.Collections;
using System.Collections.Generic;

namespace Ecs
{
    public class EcsFilterEnumerator : IEnumerator<int>
    {
        private readonly int _count;
        private int _position;

        public EcsFilterEnumerator(int count)
        {
            _count = count;
            
            _position = -1;
        }

        public bool MoveNext()
        {
            _position++;
            return _position < _count;
        }

        public void Reset() => _position = -1;

        public int Current => _position;
        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}
