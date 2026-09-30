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
            #region Question04
            // A generic method is a method that can work with different data types using a type parameter
            // instead of specifying one fixed type.
            #endregion
            #region Question06
            // A generic interface is an interface that uses a type parameter so it can define behavior for different data types
            #endregion

            #region Question07
            // A struct constraint allows a generic type to be only a value type
            // and doesn't allow it to be a reference type

            var b1 = new Box<int>(10);
            // var b2 = new Box<string>("Hi"); (gives an error)
            #endregion

            #region Question08
            // A class constraint allows a generic type to be only a reference type
            // and doesn't allow it to be a value type

            var c1 = new Container<string>("Hello");
            // Container<int> c2 = new(10); (gives an error)
            #endregion

            #region Question09
            // The new constraint requires that the type has a public parameterless constructor
            // it allows you to create new instances of T using new T()

            var factory = new Factory<Product>();
            Product p = factory.Create();
            //var bad = new Factory<string>(); (gives an error)
            #endregion

        }
        #region Question04
        public static void Swap<T>(ref T a, ref T b)
        {
            T Temp = a;
            a = b;
            b = Temp;
        }
        #endregion
        #region Question05
        public static T FindMax<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) > 0 ? a : b;
        }

        #endregion
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
    #region Question06
    internal interface IReporitory<TEntity>
    {
        void Add(TEntity item);
        List<TEntity> GetAll();
        TEntity? GetById(int id);
        void Delete(int Id);
    }
    #endregion
    #region Question07
    public class Box<T> where T : struct
    {
        public T Value { get; set; }
        public Box(T value) { Value = value; }
    }
    #endregion
    #region Question08
    internal class Container<T> where T : class
    {
        public Container(T value) { Value = value; }
        public T Value { get; set; }
    }
    #endregion
    #region Question09
    public class Factory<T> where T : new()
    {
        public T Create()
        {
            return new T();
        }
    }

    class Product
    {
        public int Id { get; set; }
        public Product() { Id = 0; }
    }
    #endregion
}
