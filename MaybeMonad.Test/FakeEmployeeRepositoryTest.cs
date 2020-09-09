using FluentAssertions;
using Xunit;

namespace MaybeMonad.Test
{
    public class FakeEmployeeRepositoryTest
    {
        private readonly IEmployeeRepository _repo = new FakeEmployeeRepository();

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public void Supervisor_of_Everyone_Is_BigBoss(int employeeId) => _repo.GetSupervisorOf(employeeId).GetValue().Name.Should().Be("Big Boss");

        [Fact]
        public void BigBoss_Has_No_Supervisor() => _repo.GetSupervisorOf(1).GetOrElse(null).Should().BeNull();
    }
}
