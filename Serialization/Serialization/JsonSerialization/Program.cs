using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace JsonSerialization
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

            string filePath = "../../department.json";

            // Serialize
            string serializedDepartment = JsonConvert.SerializeObject(department, Formatting.Indented);
            File.WriteAllText(filePath, serializedDepartment);
            Console.WriteLine("Department object serialized.");

            // Deserialize
            string jsonContent = File.ReadAllText(filePath);
            Department deserializedDepartment = JsonConvert.DeserializeObject<Department>(jsonContent);
            Console.WriteLine("Department object deserialized.");
        }
    }
}