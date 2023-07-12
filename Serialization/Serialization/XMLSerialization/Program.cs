using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace XMLSerialization
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

            XmlSerializer serializer = new XmlSerializer(typeof(Department));
            string filePath = "../../department.xml";

            // Serialize
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(stream, department);
                Console.WriteLine("Department object serialized.");
            }

            // Deserialize
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                Department deserializedDepartment = (Department)serializer.Deserialize(stream);
                Console.WriteLine("Department object deserialized.");
            }
        }
    }
}