using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Primitives;

namespace Trees
{
    public class BTree<TElement>: BNode<TElement>, ICloneable
    {
        public delegate void BTreeStateLogger(string message);

        public event BTreeStateLogger AddNodeLogging;
        public event BTreeStateLogger BranchNodeLogging;

        public BTree(TElement element) : base(element) { }

        /// <summary>
        /// Push element to the last left side ('till BTermainal<TElement> element)
        /// </summary>
        public void BranchLeft(TElement element)
        {
            BPrimitive<TElement> parentLeft = this;
            BPrimitive<TElement> nextLeft = new BNode<TElement>(element);

            if (parentLeft.LeftLink is BTerminal<TElement>)
                parentLeft.PushLeft(nextLeft);
            else
            {
                while (!(parentLeft.LeftLink is BTerminal<TElement>))
                    parentLeft = parentLeft.LeftLink;

                nextLeft = new BNode<TElement>(element);
                parentLeft.PushLeft(nextLeft);
            }

            BranchNodeLogging?.Invoke("Element brached left");
        }

        /// <summary>
        /// Push element to the last right side ('till BTermainal<TElement> element)
        /// </summary>
        public void BranchRight(TElement element)
        {
            BPrimitive<TElement> parentRight = this;
            BPrimitive<TElement> nextRight = new BNode<TElement>(element);

            if (parentRight.RightLink is BTerminal<TElement>)
                parentRight.PushRight(nextRight);
            else
            {
                while (!(parentRight.LeftLink is BTerminal<TElement>))
                    parentRight = parentRight.RightLink;

                nextRight = new BNode<TElement>(element);
                parentRight.PushLeft(nextRight);
            }

            BranchNodeLogging?.Invoke("Element brached right");
        }

        public override void PushLeft(BPrimitive<TElement> primitive)
        {
            base.PushLeft(primitive);
            BranchNodeLogging?.Invoke("Element brached left");
        }

        public override void PushRight(BPrimitive<TElement> primitive)
        {
            base.PushRight(primitive);
            BranchNodeLogging?.Invoke("Element brached right");
        }

        public BPrimitive<TElement> this[int index]
        {
            get
            {
                BPrimitive<TElement> pointer = this;

                for (int i = 0; i < index; i++)
                {
                    if (pointer.LeftLink is BTerminal<TElement>)
                        return default(BPrimitive<TElement>);

                    pointer = pointer.LeftLink;
                }

                return pointer;
            }
            set
            {
                BPrimitive<TElement> pointer = this;

                for (int i = 0; i < index; i++)
                {
                    if (pointer.LeftLink is BTerminal<TElement>) return;
                    pointer = pointer.LeftLink;
                }

                pointer.PushLeft(value.LeftLink);
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
