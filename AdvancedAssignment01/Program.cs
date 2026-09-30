using System.Diagnostics.Metrics;
using System.Reflection.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdvancedAssignment01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Question01
            // A generic class allows you to write classes interfaces and methods with type parameters
            // The actual type is specified when the code is used not when it is written

            // Type-safe: catch type mismatches during compilation instead of runtime
            // Code Reusability: write once use with many types
            // Better Performance: No boxing/ unboxing for value types
            #endregion
            #region Question03
            // A class with multiple type parameters is a generic class that has more than one type parameter

            #endregion
        }
    }
    #region Question02
    internal class MyStack<T>
    {
        public T[] _items { get; set; }
        private int _top;
        public int Count => _top;

        public MyStack(int Capacity)
        {
            _items = new T[Capacity];
            _top = 0;
        }

        // Add = Push
        public void Push(T item)
        {
            if (_top < _items.Length)
                _items[_top++] = item;
        }

        // Get = Pop / Peek
        public T? POP()
        {
            if (_top > 0)
                return _items[--_top];
            return default;
        }

        public T? Peek()
        {
            if (_top > 0)
                return _items[_top - 1];
            return default;
        }
    }
    #endregion
    #region Question03
    internal class Pair<T1, T2>
    {
        public Pair(T1 first, T2 second)
        {
            First = first;
            Second = second;
        }
        public T1 First { get; set; }
        public T2 Second { get; set; }
    }
    #endregion
}
