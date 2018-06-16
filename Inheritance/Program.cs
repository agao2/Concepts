using System;

namespace Inheritance
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Notice the order in which constructors get called:
            first the static constructor gets called, then the base constructor gets called
            and then finally the instance constructor gets called
             */
            Cat cat = new Cat("Zoey" ,"2013");

            // Notice how creating a second new instance below does not call the static constructor!
            // this is because the static constructor is only called before the first instance
            // is created
            Cat cat2 = new Cat("Simbah" , "2015");


        }
    }

    public class Animal
    {
        public Animal(string yearBorn)
        {
            Console.WriteLine("Was born in the year " + yearBorn);
        }
    }

    public class Cat : Animal
    {
        // notice how the base  class constructor is called inside the method declaration
        public Cat(string name , string yearBorn) : base(yearBorn)
        {
            Console.WriteLine("My name is " + name);
        } 

        // this is a static constructor, it is called before the first instance is created
        // or when any static members are referenced
        static Cat()
        {
            Console.WriteLine("Thats a cat!");
        }
    }
}
