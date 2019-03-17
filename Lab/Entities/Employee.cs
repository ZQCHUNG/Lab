using System.Collections.Generic;

namespace Lab.Entities
{
    public class Employee
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public Role Role { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public decimal Saving { get; set; }
    }

    public class JoeyEmployeeWithPhoneEqualityComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.FirstName == y.FirstName
                  && x.LastName == y.LastName
                  && x.Phone == y.Phone;
        }

        public int GetHashCode(Employee obj)
        {
            return (obj.FirstName + obj.LastName + obj.Phone).GetHashCode();
        }
    }

    public class EmployeeEqualityComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.FirstName == y.FirstName
                   && x.LastName == y.LastName;
        }

        public int GetHashCode(Employee obj)
        {
            return (obj.FirstName + obj.LastName).GetHashCode();
        }
    }
}