using System.Collections.Generic;
using System.Xml.Serialization;

namespace XMLSerialization
{
    [XmlRoot("Department")]
    public class Department
    {
        public string DepartmentName { get; set; }

        [XmlElement("Employee")]
        public List<Employee> Employees { get; set; }
    }

}