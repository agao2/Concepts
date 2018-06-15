using System;

namespace ReferenceSemantics
{
 class Program
    {
        static void Main(string[] args)
        {

            /*
            Below is an example of passing a value type parameter 
            by value and reference
             */
            int passByValue = -1; 
            int passByReference = -1; 

            SetToTen(passByValue);
            Console.WriteLine(passByValue); // note that it is not 10

            SetToTen(ref passByReference);
            Console.WriteLine(passByReference); // note that -1 changed to 10

            /*
            Below is an example of passing a reference type parameter
            by value and by reference
             */
            Car carByValue = new Car(){ wheels = -1};
            Car carByRefernce = new Car(){wheels =-1 };
            Car newCar = new Car() {wheels = -1};

            SetWheels(carByValue);
            Console.WriteLine(carByValue.wheels); 

            SetWheels(ref carByRefernce);
            Console.WriteLine(carByRefernce.wheels);
            /*
            Notice how they both print the same 4
            This is because class objects are of type reference type , 
            when you pass reference types by value, it is possible to change the 
            data that the reference is pointing to
            You may not however change the value of the reference itself though so using the same
            reference to allocated a new class is not possible.
             */

            NewCar(newCar);
            Console.WriteLine(newCar.wheels); 
            // notice how the above is still printing -1, this is because the value of the 
            // reference cannot be changed!

            
        }

        public static void SetToTen(int x)
        {
            x = 10;
        }

        public static void SetToTen(ref int x)
        {
            x= 10;
        }

        public static void SetWheels(Car car)
        {
            car.wheels = 4;
        }

        public static void SetWheels(ref Car car)
        {
            car.wheels = 4;
        }

        public static void NewCar(Car car)
        {
            car = new Car(){wheels = 2};
        }
        public class Car
        {
            public int wheels {get;set;}
        }

    }
}
