

// Console.Write("Enter first number: ");
// int a = int.Parse(Console.ReadLine());

// Console.Write("Enter second number: ");
// int b = int.Parse(Console.ReadLine());

// Console.WriteLine("Sum = " + (a + b));



// using System;
// namespace sampleprogram
// {
//     class Program
//     {
//         static void Main()
//         {
//             Console.WriteLine("lets Learn coding");
//         }
//     }
// }

// using System;
// using System.Collections.Generic;
// using System.Linq;  // Important: include LINQ namespace

// class Program
// {
//     static void Main()
//     {
//         // Sample Data
//         List<Student> students = new List<Student>()
//         {
//             new Student { Id = 1, Name = "Ranjana", Age = 22 },
//             new Student { Id = 2, Name = "Akhil", Age = 24 },
//             new Student { Id = 3, Name = "Neha", Age = 21 },
//             new Student { Id = 4, Name = "Rahul", Age = 22 },
//             new Student { Id = 5, Name = "Anita", Age = 24 }
//         };

//         // ===== 1. WHERE: Filter data =====
//         var adults = students.Where(s => s.Age >= 22);
//         Console.WriteLine("Students Age >= 22:");
//         foreach (var s in adults)
//         {
//             Console.WriteLine($"{s.Name} - {s.Age}");
//         }

//         // ===== 2. SELECT: Project data =====
//         var names = students.Select(s => s.Name);
//         Console.WriteLine("\nAll Student Names:");
//         foreach (var name in names)
//         {
//             Console.WriteLine(name);
//         }

//         // ===== 3. ORDERBY: Sort data =====
//         var sortedByAge = students.OrderBy(s => s.Age);
//         Console.WriteLine("\nStudents Sorted by Age:");
//         foreach (var s in sortedByAge)
//         {
//             Console.WriteLine($"{s.Name} - {s.Age}");
//         }

//         // ===== 4. GROUPBY: Group data =====
//         var groupedByAge = students.GroupBy(s => s.Age);
//         Console.WriteLine("\nStudents Grouped by Age:");
//         foreach (var group in groupedByAge)
//         {
//             Console.WriteLine($"Age: {group.Key}");
//             foreach (var s in group)
//             {
//                 Console.WriteLine($"  {s.Name}");
//             }
//         }
//     }
// }

// // Student class
// // class Student
// // {
// //     public int Id { get; set; }
// //     public string Name { get; set; }
// //     public int Age { get; set; }
// // }



// using System;

// class Program
// {
//     static void Main(string[] args)
//     {
//         // Output a message to the console
//         Console.WriteLine("Hello, World!");
        
//         // Prompt the user for their name
//         Console.WriteLine("What is your name?");

//         // Read user input from the console and store it in a variable
//         string name = Console.ReadLine();

//         // Output a personalized greeting using string interpolation
//         Console.WriteLine($"Hello, {name}! Welcome to C# practice.");
        
//         // Keep the console window open until a key is pressed (for non-IDE execution)
//         Console.ReadKey();
//     }
// }


// using System;
// using System.Linq;
// using System.Collections.Generic;

// class Program
// {
//     static void Main()
//     {
//         List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };

//         // Select even numbers
//         var evenNumbers = numbers.Where(n => n % 2 == 0).Select(n => n);

//         foreach (var n in evenNumbers)
//         {
//             Console.WriteLine(n);
//         }
//     }
// }
List<int> numbers = new List<int> { 5, 2, 8, 1, 9 };

// Ascending order
var ascending = numbers.OrderBy(n => n);

// Descending order
var descending = numbers.OrderByDescending(n => n);

Console.WriteLine("Ascending: " + string.Join(", ", ascending));
Console.WriteLine("Descending: " + string.Join(", ", descending));


// using System;
// class program
// {
//     static void Main()
//     {
//         int age=22;
//         double salary=25000.680;
//         char Grade='S';
//         bool isActive = true;
//         string name ="Sara";
        
//         Console.WriteLine(age);
//         Console.WriteLine(salary);
//         Console.WriteLine(Grade);
//         Console.WriteLine(isActive);
//         Console.WriteLine(name);

//         Console.ReadKey();
//     }
// }