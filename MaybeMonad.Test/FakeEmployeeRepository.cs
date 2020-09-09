using System.Collections.Generic;

namespace MaybeMonad.Test
{
    class Employee
    {
        public int Id { get; }
        public string Name { get; }
        public Maybe<int> SupervisorId { get; }

        public Employee(int id, string name, int? supervisorId = null)
        {
            Id = id;
            Name = name;
            SupervisorId = Maybe<int>.FromValue(supervisorId ?? default /* 0 */);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public override bool Equals(object? obj) => obj is Employee e && e.Id == Id;
    }

    interface IEmployeeRepository
    {
        Maybe<Employee> Get(int employeeId);
        Maybe<Employee> GetSupervisorOf(int employeeId);
    }

    class FakeEmployeeRepository : IEmployeeRepository
    {
        readonly IDictionary<int, Employee> _employees;

        public FakeEmployeeRepository()
        {
            _employees = new Dictionary<int, Employee>
            {
                { 1, new Employee(1, "Big Boss") },
                { 2, new Employee(2, "Alfonso", 1) },
                { 3, new Employee(3, "Dmitry", 1) }
            };
        }

        public Maybe<Employee> Get(int employeeId) => _employees.TryGetValue(employeeId, out var employee) ? 
            Maybe<Employee>.Some(employee) : 
            Maybe<Employee>.None();
 
        public Maybe<Employee> GetSupervisorOf(int employeeId)
        {
            return Get(employeeId)
                .FlatMap(employee => Get(employee.SupervisorId.GetOrElse(-1)));
        }
    }
}
