using System;

namespace PCL
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