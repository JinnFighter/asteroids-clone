using System.Collections.Generic;
using System.Linq;
using Common;

namespace Physics
{
    public class QuadTree
    {
        private int _maxObjects = 10;
        private int _maxLevels = 5;

        private readonly int _level;
        private readonly List<PhysicsCollider> _objects;
        private readonly Rectangle _bounds;
        private readonly List<QuadTree> _nodes;

        public QuadTree(int level, Rectangle bounds)
        {
            _level = level;
            _objects = new List<PhysicsCollider>();
            _bounds = bounds;
            _nodes = new List<QuadTree>(4);
        }

        public void Clear()
        {
            _objects.Clear();
            foreach (var node in _nodes)
                node.Clear();
            _nodes.Clear();
        }

        private void Split()
        {
            var size = new Vector2(_bounds.Width, _bounds.Height);
            var halfSize = size / 2;
            var quarterSize = halfSize / 2;
            var nextLevel = _level + 1;
            var position = _bounds.Position;
            _nodes.Add(new QuadTree(nextLevel, new Rectangle(new Vector2(position.X + quarterSize.X, position.Y - quarterSize.Y),
                halfSize.X, halfSize.Y)));
            _nodes.Add(new QuadTree(nextLevel, new Rectangle(position - quarterSize , halfSize.X, halfSize.Y)));
            _nodes.Add(new QuadTree(nextLevel, new Rectangle(new Vector2(position.X - quarterSize.X, position.Y + quarterSize.Y), 
                halfSize.X, halfSize.Y)));
            _nodes.Add(new QuadTree(nextLevel,new Rectangle(position + quarterSize, 
                halfSize.X, halfSize.Y)));
        }
        
        private int GetIndex(PhysicsCollider collider) => collider.GetQuadTreeIndex(this);

        public int GetBoxIndex(BoxPhysicsCollider boxCollider)
        {
            var index = -1;
            
            var colliderMin = boxCollider.TopLeft;
            var colliderMax = boxCollider.DownRight;
            
            var isInTop = colliderMin.Y >= _bounds.Min.Y && colliderMax.Y <= _bounds.Position.Y;
            var isInBottom = colliderMin.Y > _bounds.Position.Y && colliderMax.Y <= _bounds.Max.Y;
            var isInLeft = colliderMin.X >= _bounds.Min.X && colliderMax.X <= _bounds.Position.X;
            var isInRight = colliderMin.X > _bounds.Position.X && colliderMax.X <= _bounds.Max.X;

            if (isInTop)
            {
                if (isInLeft)
                    index = 1;
                else if (isInRight)
                    index = 0;
            }
            else if(isInBottom)
            {
                if (isInLeft)
                    index = 2;
                else if (isInRight)
                    index = 3;
            }


            return index;
        }

        public void Insert(PhysicsCollider collider)
        {
            if (_nodes.Any())
            {
                var index = GetIndex(collider);
                if(index > -1)
                    _nodes[index].Insert(collider);
                return;
            }
            
            _objects.Add(collider);
            if (_objects.Count > _maxObjects && _level < _maxLevels)
            {
                if (!_nodes.Any())
                    Split();

                var i = 0;
                while (i < _objects.Count)
                {
                    var index = GetIndex(_objects[i]);
                    if (index > -1)
                    {
                        var colliderObject = _objects[i];
                        _objects.RemoveAt(i);
                        _nodes[index].Insert(colliderObject);
                    }
                    else
                        i++;
                }
            }
        }
    }
}
