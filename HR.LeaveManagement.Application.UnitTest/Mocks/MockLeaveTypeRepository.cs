using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTest.Mocks
{
	public class MockLeaveTypeRepository
	{
		private static readonly Mock<ILeaveTypeRepository> mockRepo = new();
		public static Mock<ILeaveTypeRepository> GetLeaveTypeMockLeaveTypeRepository()
		{
			var leaveTypes = new List<LeaveType>
			{
				new LeaveType
				{
					Id = 1,
					DefaultDays = 10,
					Name = "Test Vacation",
				},
				new LeaveType
				{
					Id = 2,
					DefaultDays = 15,
					Name = "Test Sick",
				},
				new LeaveType
				{
					Id = 3,
					DefaultDays = 15,
					Name = "Test Maternity",
				},
			};

			mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(leaveTypes);

			mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>()))
				.Returns((LeaveType leaveType) =>
				{
					leaveTypes.Add(leaveType);
					return Task.CompletedTask;
				});

			return mockRepo;
		}

		public static Mock<ILeaveTypeRepository> GetLeaveTypeDetailMockLeaveTypeRepository()
		{
			var leaveType = new LeaveType
			{
				Id = 1,
				DefaultDays = 10,
				Name = "Test Vacation",
			};

			mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(leaveType);

			return mockRepo;
		}
	}
}
