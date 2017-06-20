using System;

namespace NetStandard
{
    public class GenericEventArgs<T> : EventArgs
    {
        public T Value { get; set; }

        public GenericEventArgs(T value)
        {
            Value = value;
        }
    }
}