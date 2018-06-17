using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            //LinqIntro();
            //Joining();
            Grouping();
        }

        public static void LinqIntro()
        {       
            /*
            Below is an example of a linq query on a collection of values
            Note that you can use linq queries against SQL Servers, Datasets , and Xml documents
            In general any collection of objects that implement IEnumerable or a third party data source 
            that implements IQueryable
             */
            int[] set1 = new int[]{1,2,3,4,5,6,7,8,9,10};

            // sorting using orderby
            IEnumerable<int> descending = from s in set1
                                     orderby s descending
                                     select s;

            IEnumerable<int> ascending = from s in set1
                            orderby s ascending
                            select s;
            
            /*
            Note that the above two queries follows deferred execution meaning that they are not
            executed when they are created. Instead they are executed when they are iterated over. 

            In general queries that result in a sequence of value follows deferred execution where as
            queries that return a single value are executed immediately. 
            Methods such as count, average, max  return a single value.

            You can force a query that returns a sequence of values to execute immediately by calling
            ToList() method or ToArray() method or in general To anything method.
             */

            foreach(int s in ascending)
            {
                Console.Write(s + " ");  // query is executed here
            }
            Console.WriteLine("");

            List<int> immediateList = ascending.ToList();  // query is executed here when it gets converted to a list!
            foreach (int item in immediateList)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine("");
        }

        public static void Joining()
        {
            /*
            Below is an example showing how join operations work in Linq
            The methodology behind joins are pretty standard and consistent in reference to SQL joins though
            The Join operator in C# implements an inner join 
                So say you have two collection of objects. Preforming an inner join between these two collections
                would return a new collection of objects in which the objects from the two seperate groups
                have a similar property
             */

             // notice that Category is the generic type that we have to put into List
             List<Category> categories = new List<Category>() {
                new Category() {Name = "Dog" , Id = 0} , 
                new Category() {Name = "Cat" , Id = 1}, 
                new Category() {Name = "Fish" , Id = 2},
                new Category() {Name = "Snake" , Id = 3}
             };

             List<Pet> pets = new List<Pet>(){
                new Pet() {Name = "Zoey" , CategoryId = 1},
                new Pet() {Name = "Scratchy" , CategoryId = 1},
                new Pet() {Name = "Hissy" , CategoryId = 3},
                new Pet() {Name = "Bobble" , CategoryId = 2},
                new Pet() {Name = "RyanPaige" , CategoryId = 0}
             };

             // Below is an example of an inner join
             Console.WriteLine("INNER JOIN:");
             var innerJoin  = from category in categories 
                              join pet in pets on category.Id equals pet.CategoryId
                              select category.Name;

             Console.WriteLine(string.Join(" " , innerJoin.ToList())); 
             /*
             Notice how the result above returns 5 values
             Inner join does a cross product between all the rows in the sets and returns
             only the rows satisfying the equal conditions
             Duplicates are not removed!
              */

            // To remove duplicates from a single set do 
            var distinct = innerJoin.Distinct();
            Console.WriteLine(string.Join(" " , distinct.ToList()));


        }


        public static void Grouping()
        {
            /*
            Grouping in LINQ essentially lets you section off the data returned by your query into groups 
            All the items in that specific group would share a common attribute that you specified
             */

            List<int> numbers = new List<int>() { 35, 44, 200, 84, 3987, 4, 199, 329, 446, 208 };  

            var query = from number in numbers
                        group number by number == 446;
            
            // notice that there are 2 groups, {true,false}. because the result of number == 446 can only be true and false
            foreach(var group in query)
            {
                Console.WriteLine(group.Key); 
            }

            var query2 = from number in numbers
                         group number by number % 3;
            
            // notice that there are 3 groups,  {0,1,2}. This is because there are 3 possible outcomes of number % 3
            // int mod 3 can only be 0 , 1, or 2 so hence 3 groups
            foreach(var group in query2)
            {
                Console.WriteLine(group.Key); 
            }
            
        }


        class Category 
        {
            public string Name{get;set;}
            public int Id {get;set;}
        }

        class Pet 
        {
            public string Name{get;set;}
            public int CategoryId {get;set;}
        }
    }


}
