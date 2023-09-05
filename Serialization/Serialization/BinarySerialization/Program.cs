using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BinarySerialization
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Department department = new Department
            {
                DepartmentName = "IT",
                Employees = new List<Employee>
                {
                    new Employee { EmployeeName = "John" },
                    new Employee { EmployeeName = "Jane" }
                }
            };

            BinaryFormatter formatter = new BinaryFormatter();
            string filePath = "../../department.bin";

            // Serialize
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                formatter.Serialize(stream, department);
                Console.WriteLine("Department object serialized.");
            }

            // Deserialize
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                Department deserializedDepartment = (Department)formatter.Deserialize(stream);
                Console.WriteLine("Department object deserialized.");
            }
        }
    }
}