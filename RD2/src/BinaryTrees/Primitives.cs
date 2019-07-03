using System.Collections;
using System.Collections.Generic;

namespace Primitives
{
    /// <summary>
    /// Simple tree element template. Implements 'Composite' pattern (as 'Component' element)
    /// </summary>
    /// <typeparam name="TElement">Any given typeparam</typeparam>
    public abstract class BPrimitive<TElement> : IEnumerable<TElement>
    {
        protected TElement _element;

        public TElement Value {
            get
            {
                return _element;
            }
            set
            {
                _element = value;
            }

        }

        public BPrimitive(TElement element) { _element = element; }

        public abstract BPrimitive<TElement> LeftLink { get; }
        public abstract BPrimitive<TElement> RightLink { get; }

        public virtual void PushLeft(BPrimitive<TElement> primitive) { }
        public virtual void PushRight(BPrimitive<TElement> primitive) { }
        public virtual void PopLeft() { }
        public virtual void PopRight() { }

        public virtual IEnumerator<TElement> GetEnumerator()
        {
            yield return _element;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return GetEnumerator();
        }
    }

    /// <summary>
    /// Building block for binary tree. Implements 'Composite' pattern (as 'Composite' element)
    /// Is built recurently:
    ///             [CurrentNode (BNode)]
    ///             /                   \
    /// [LowerNode (BNode)]            [Leaf (BTerminal)]
    /// </summary>
    /// <typeparam name="TElement">Any given typeparam</typeparam>
    public class BNode<TElement> : BPrimitive<TElement>
    {
        public BNode(TElement element) : base(element) {
            _leftElement = new BTerminal<TElement>(_element);
            _rightElement = new BTerminal<TElement>(_element);
        }

        protected BPrimitive<TElement> _leftElement, _rightElement;

        public override BPrimitive<TElement> LeftLink { get { return _leftElement; } }

        public override BPrimitive<TElement> RightLink { get { return _rightElement; } }

        public override void PushLeft(BPrimitive<TElement> primitive)
        { _leftElement = primitive; }

        public override void PushRight(BPrimitive<TElement> primitive)
        { _rightElement = primitive; }

        public override void PopLeft()
        { _leftElement = new BTerminal<TElement>(_element); }

        public override void PopRight()
        { _rightElement = new BTerminal<TElement>(_element); }

        public override IEnumerator<TElement> GetEnumerator()
        {
            yield return _element;

            foreach (TElement leftNodeElement in _leftElement)
                yield return leftNodeElement;

            foreach (TElement rightNodeElement in _rightElement)
                yield return rightNodeElement;
        }

        public override string ToString()
        {
            string result = $"{_element.ToString()}";

            if (!(_leftElement is BTerminal<TElement>))
                result = $"{_leftElement.ToString()} <- {result}";

            if (!(_rightElement is BTerminal<TElement>))
                result = $"{result} -> {_rightElement.ToString()}";

            return $"({result})";
        }
    }

    /// <summary>
    /// Represents the terminal element for node, doesn't return value back. Implements 'Composite' pattern (as 'Leaf' element)
    /// </summary>
    /// <typeparam name="TElement">Any given typeparam. Object doesn't return value back!</typeparam>
    public class BTerminal<TElement> : BPrimitive<TElement>
    {
        public BTerminal(TElement element) : base(element) { }

        public override BPrimitive<TElement> LeftLink { get { return default(BPrimitive<TElement>); } }

        public override BPrimitive<TElement> RightLink { get  { return default(BPrimitive<TElement>); } }

        public override IEnumerator<TElement> GetEnumerator()
        {
            yield break;
        }

        public override string ToString()
        {
            return "null";
        }
    }
}
