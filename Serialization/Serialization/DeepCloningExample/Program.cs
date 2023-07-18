using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DeepCloningExample
{
    internal class Program
    {
        public static void Main(string[] args)
        {Department originalDepartment = new Department
            {
                DepartmentName = "HR",
                Employees = new List<Employee>
                {
                    new Employee { EmployeeName = "John Doe" },
                    new Employee { EmployeeName = "Jane Smith" }
                }
            };

            Department clonedDepartment = DeepClone(originalDepartment);

            clonedDepartment.DepartmentName = "IT";
            clonedDepartment.Employees[0].EmployeeName = "Mike Johnson";

            Console.WriteLine("Original Department:");
            Console.WriteLine($"Department Name: {originalDepartment.DepartmentName}");
            foreach (Employee emp in originalDepartment.Employees)
            {
                Console.WriteLine($"Employee Name: {emp.EmployeeName}");
            }

            Console.WriteLine("\nCloned Department:");
            Console.WriteLine($"Department Name: {clonedDepartment.DepartmentName}");
            foreach (Employee emp in clonedDepartment.Employees)
            {
                Console.WriteLine($"Employee Name: {emp.EmployeeName}");
            }
        }
        public static T DeepClone<T>(T obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}