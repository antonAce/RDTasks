using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RDTask2
{
    public abstract class ListComponent<T> : IEnumerable<T>
    {
        protected T element;
        protected ListComponent<T> next;

        public T Value {
            get { return element; }
            set { element = value; }
        }

        public ListComponent<T> Next { get { return next; } }

        public ListComponent() { }

        public virtual void PushNext(ListComponent<T> component) { }
        public virtual void PushEnd(ListComponent<T> component) { }
        public virtual void PopNext() { }
        public virtual void PopEnd() { }

        public virtual IEnumerator<T> GetEnumerator()
        {
            yield return element;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return GetEnumerator();
        }
    }

    public class ListComposite<T>: ListComponent<T>
    {
        public ListComposite() : base() { next = new NullElement<T>(); }

        public override void PushNext(ListComponent<T> component) { next = component; }

        public override void PushEnd(ListComponent<T> component)
        {
            ListComponent<T> pointer = this;

            while (!(pointer.Next is NullElement<T>))
                pointer = pointer.Next;

            pointer.PushNext(component);
        }

        public override void PopNext() { next = new NullElement<T>(); }

        public override void PopEnd()
        {
            ListComponent<T> pointer = this;

            while (!(pointer.Next is NullElement<T>))
                pointer = pointer.Next;

            pointer.PopNext();
        }

        public override IEnumerator<T> GetEnumerator()
        {
            yield return element;

            foreach (T nelement in next)
                yield return nelement;
        }
    }

    public class NullElement<T> : ListComponent<T>
    {
        public NullElement() : base() { }

        public override IEnumerator<T> GetEnumerator() { yield break; }
    }

    /// <summary>
    /// This is just a usual list, but with a dank behaviour.
    /// Caution: Not recommended to use in real projects!
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DankList<T> : IList<T>
    {
        protected ListComponent<T> core = new NullElement<T>();

        public DankList() {}

        public DankList(params T[] values)
        {
            foreach (var item in values)
                Add(item);
        }

        /// <summary>
        /// You may think this is the traditional way to seek element via index... But...
        /// Let's have a f*cking Python-like code in C# and make possible to get element via negative index.
        /// If you've never coded on Python before: -i index = element with i steps position from end.
        /// </summary>
        /// <param name="index">Any integer number</param>
        /// <returns>Element, stored by this index</returns>
        public T this[int index] {
            get
            {
                if (index >= 0)
                {
                    int i = 0;
                    ListComponent<T> pointer = core;

                    while (!(pointer is NullElement<T>))
                    {
                        if (i == index)
                            return pointer.Value;

                        pointer = pointer.Next;
                        i++;
                    }

                    throw new IndexOutOfRangeException();
                }
                else
                {
                    return this[Count - (Math.Abs(index) % (Count - 1))];
                }
            }
            set
            {
                if (index >= 0)
                {
                    int i = 0;
                    ListComponent<T> pointer = core;

                    if (pointer is NullElement<T>)
                        throw new IndexOutOfRangeException();

                    while (!(pointer.Next is NullElement<T>))
                    {
                        if (i == index)
                        {
                            pointer.Value = value;
                            return;
                        }

                        pointer = pointer.Next;
                        i++;
                    }

                    throw new IndexOutOfRangeException();
                }
                else
                {
                    this[Count - (Math.Abs(index) % (Count - 1))] = value;
                }
            }
        }

        /// <summary>
        /// Okey, now getting the part of Python-like code into C# is not enough, huh...
        /// Let's have a f*cking Javascript-like code in C# and make possible to get element via string index by convertion into.
        /// I mean: <danklistname>["0"] = <danklistname>[0]
        /// </summary>
        /// <param name="index">String form of int</param>
        /// <returns>Element, stored by this index</returns>
        public T this[string index] {
            get
            {
                return this[int.Parse(index)];
            }
            set
            {
                this[int.Parse(index)] = value;
            }
        }

        /// <summary>
        /// You may ask yourseft, how in the hell is it possible to operate this double index in list
        /// Well, it returns an array of elements where start index is value before comma in index and end index - after comma
        /// For instance, <danklistname>[3.5] = [element[3], element[4], element[5]]. And yes, I'm not idiot, I'm just drunk :)
        /// </summary>
        /// <param name="index">Any double number</param>
        /// <returns>Elements, stored by this index</returns>
        public T[] this[double index]
        {
            get
            {
                int min_pos = Convert.ToInt32(Math.Floor(index));
                double index_tail = index - Math.Floor(index);
                int max_pos = Convert.ToInt32((index - Math.Floor(index)) * Math.Pow(10, index_tail.ToString().Length - 2));
                int result_size = max_pos - min_pos + 1;

                if (min_pos > max_pos)
                    throw new IndexOutOfRangeException();

                T[] result = new T[result_size];

                for (int i = 0; i < result_size; i++)
                    result[i] = this[min_pos + i];

                return result;
            }
        }

        public int Count {
            get
            {
                int i = 0;
                ListComponent<T> pointer = core;

                while (!(pointer is NullElement<T>))
                {
                    pointer = pointer.Next;
                    i++;
                }

                return i;
            }
        }

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            if (core is NullElement<T>)
                core = new ListComposite<T>() { Value = item };
            else
                core.PushEnd(new ListComposite<T>() { Value = item });
        }

        public void Clear() { core = new NullElement<T>(); }

        public bool Contains(T item)
        {
            ListComponent<T> pointer = core;

            while (!(pointer is NullElement<T>))
            {
                if (pointer.Value.Equals(item))
                    return true;

                pointer = pointer.Next;
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return core.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            int i = 0;
            ListComponent<T> pointer = core;

            while (!(pointer is NullElement<T>))
            {
                if (pointer.Value.Equals(item))
                    return i;

                pointer = pointer.Next;
                i++;
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            ListComponent<T> pointer = core;

            for (int i = 0; i < index; i++)
            {
                if (pointer is NullElement<T>)
                    throw new IndexOutOfRangeException();

                pointer = pointer.Next;
            }

            ListComponent<T> afterCompontent = pointer.Next;
            ListComponent<T> intermediateComp = new ListComposite<T>() { Value = item };

            intermediateComp.PushNext(afterCompontent);
            pointer.PushNext(intermediateComp);
        }

        public bool Remove(T item)
        {
            ListComponent<T> pointer = core;

            for (int i = 0; i < this.Count; i++)
            {
                if (pointer.Value.Equals(item))
                {
                    this.RemoveAt(i);
                    return true;
                }

                pointer = pointer.Next;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (index == 0)
                core = core.Next;
            else
            {
                if (index >= this.Count)
                    throw new IndexOutOfRangeException();

                ListComponent<T> pointer = core;

                for (int i = 0; i < index - 1; i++)
                {
                    if (pointer is NullElement<T>)
                        throw new IndexOutOfRangeException();

                    pointer = pointer.Next;
                }

                ListComponent<T> afterCompontent = pointer.Next.Next;
                pointer.PushNext(afterCompontent);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
