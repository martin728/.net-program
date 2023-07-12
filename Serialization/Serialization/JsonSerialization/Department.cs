using System.Collections.Generic;
using Newtonsoft.Json;

namespace JsonSerialization
{
    public class Department
    {
        public string DepartmentName { get; set; }

        [JsonProperty("Employees")]
        public List<Employee> Employees { get; set; }
    }
}