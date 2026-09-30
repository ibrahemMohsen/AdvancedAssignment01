using System.Diagnostics.Metrics;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
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

            #region Question10
            // The interface constraint requires that the type implements the specified interface
            var p1 = new Printer<Document>();
            p1.PrintItem(new Document());
            var p2 = new Printer<Report>();
            p2.PrintItem(new Report());
            // var p3 = new Printer<string>(); (gives an error)
            #endregion

            #region Question11
            //  T or its derived types must inherit from BaseClass
            #endregion

            #region Question12
            // we can combine multiple constraints
            // where T : BaseClass, IInterface1, IInterface2, new()

            Repository<Order> repo = new Repository<Order>();
            Order o = repo.Create();
            #endregion

            #region Question13
            // When T is unknown at compile time, you cannot return null or 0 directly
            // default(T) / default returns the default value of the type (0, null, etc)
            #endregion

            #region Question14
            SafeList<int> list = new SafeList<int>();
            list.Add(10);
            list.Add(20);
            Console.WriteLine(list.Get(0)); // 10
            Console.WriteLine(list.Get(5)); // 0 (default of int, no exception)
            SafeList<string> s = new();
            Console.WriteLine(s.Get(0) == null); // True (default of string)
            #endregion

            #region Question15
            // Covariance allows a generic type to use a more derived type where a base type is expected
            // the out keyword marks a generic type parameter as covariant meaning it can only be used for output/return values not as method parameters
            #endregion

            #region Question16
            // Contravariance allows a generic type to use a less derived(base) type where a more derived type is expected
            // the in keyword marks a generic type parameter as contravariant meaning it can only be used for input/method parameters not as return values
            #endregion

            #region Question17
            // Covariance(out) lets you use a more derived type where a base type is expected
            // while contravariance(in) lets you use a base type where a more derived type is expected
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

    #region Question10
    public interface IPrintable
    {
        void Print();
    }

    public class Document : IPrintable
    {
        public void Print() => Console.WriteLine("Document printed");
    }
    public class Report : IPrintable
    {
        public void Print() => Console.WriteLine("Report printed");
    }

    public class Printer<T> where T : IPrintable
    {
        public void PrintItem(T item)
        {
            item.Print(); // We can call Print because of constraint
        }
    }
    #endregion

    #region Question11
    internal class AnimalShelter<T> where T : Animal
    {
        private readonly List<T> _animals = [];
        public void Add(T animal) { _animals.Add(animal); }
    }
    class Animal { }
    #endregion

    #region Question12
    public interface IAuditable { void Audit(); }

    public class BaseEntity { public int Id { get; set; } }

    public class Order : BaseEntity, IAuditable
    {
        public void Audit() => Console.WriteLine("Order audited");
        public Order() { } // public parameterless ctor
    }

    public class Repository<T> where T : BaseEntity, IAuditable, new()
    {
        public T Create()
        {
            return new T(); // Allowed: has public parameterless ctor
        }
    }
    #endregion

    #region Question14
    public class SafeList<T>
    {
        private List<T> _items = new List<T>();
        public void Add(T item) => _items.Add(item);

        public T? Get(int index)
        {
            if (index < 0 || index >= _items.Count)
                return default;
            return _items[index];
        }
    }
    #endregion
}
