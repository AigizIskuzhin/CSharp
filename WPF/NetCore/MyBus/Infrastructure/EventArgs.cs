using System;

namespace MyBus.Infrastructure
{
    public class EventArgs<T>:EventArgs
    {
        public T Arg { get; set; }
        public EventArgs(T arg) => Arg = arg;
        public static implicit operator EventArgs<T>(T arg) => new(arg);
        public static implicit operator T(EventArgs<T> arg) => arg.Arg;
    }
}
