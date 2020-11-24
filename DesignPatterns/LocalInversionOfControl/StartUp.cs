using System;
using System.Collections.Generic;
using System.Linq;

namespace LocalInversionOfControl
{
    public static class ExtensionMethods
    {
        public struct BoolMarker<T>
        {
            public bool Result;
            public T Self;

            public enum Operations
            {
                None,
                And,
                Or
            };

            internal Operations PendingOp;

            internal BoolMarker(bool result, T self, Operations pendingOp)
            {
                Result = result;
                Self = self;
                PendingOp = pendingOp;
            }

            public BoolMarker(bool result, T self)
                : this(result, self, Operations.None)
            { }

            public BoolMarker<T> And => new BoolMarker<T>(Result, Self, Operations.And);

            public static implicit operator bool(BoolMarker<T> marker)
            {
                return marker.Result;
            }
        }

        public static T AddTo<T>(this T self, params ICollection<T>[] collections)
        {
            foreach (var collection in collections)
                collection.Add(self);
            return self;
        }

        public static bool IsOnOf<T>(this T self, params T[] values)
        {
            return values.Contains(self);
        }

        public static BoolMarker<TSubject> HasNo<TSubject, T>(this TSubject self, Func<TSubject, IEnumerable<T>> props)
        {
            return new BoolMarker<TSubject>(!props(self).Any(), self);
        }

        public static BoolMarker<TSubject> HasSome<TSubject, T>(this TSubject self, Func<TSubject, IEnumerable<T>> props)
        {
            return new BoolMarker<TSubject>(props(self).Any(), self);
        }

        public static BoolMarker<T> HasNo<T, U>(this BoolMarker<T> marker, Func<T, IEnumerable<U>> props)
        {
            if (marker.PendingOp == BoolMarker<T>.Operations.And && !marker.Result)
                return marker;
            return new BoolMarker<T>(!props(marker.Self).Any(), marker.Self);
        }
    }

    public class Person
    {
        public List<string> Names = new List<string>();
        public List<Person> Children = new List<Person>();
    }

    public class Inverted
    {
        public void AddingNumbers()
        {
            // list.Add(24); instead of list being in cotroll we make the integer in control
            var list1 = new List<int>();
            var list2 = new List<int>();

            24.AddTo(list1, list2);
        }

        public void ProcessCommand(string opcode)
        {
            // instead of using it like this:
            // if (opcode == "AND" || opcode == "OR" || opcode == "XOR") { }
            // if (new[] { "AND", "OR", "XOR"}.Contains(opcode)) { }
            // if ("AND OR XOR".Split(' ').Contains(opcode)) { }

            // inverted approach

            if (opcode.IsOnOf("AND", "OR", "XOR"))
            {
                // Logic goes here ..
            }
        }

        public void Process (Person person)
        {
            // if (person.Names.Count == 0) { }
            // if (!person.Names.Any()) { }

            if (person.HasNo(p => p.Names).And.HasNo(p => p.Children)) 
            {
                // Process
            }

            if (person.HasSome(p => p.Names))
            {
                // Process
            }
        }
    }


    class StartUp
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
