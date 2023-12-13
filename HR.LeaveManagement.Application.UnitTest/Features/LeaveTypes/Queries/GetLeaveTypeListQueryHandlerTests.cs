using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTest.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTest.Features.LeaveTypes.Queries
{
	public class GetLeaveTypeListQueryHandlerTests
	{
		private readonly Mock<ILeaveTypeRepository> _listMockRepo;
		private readonly Mock<ILeaveTypeRepository> _detailMockRepo;
		private IMapper _mapper;
		private Mock<IAppLogger<GetLeaveTypesQueryHandler>> _mockAppLogger;

		public GetLeaveTypeListQueryHandlerTests()
		{
			_listMockRepo = MockLeaveTypeRepository.GetLeaveTypeMockLeaveTypeRepository();
			_detailMockRepo = MockLeaveTypeRepository.GetLeaveTypeDetailMockLeaveTypeRepository();
			var mapperConfig = new MapperConfiguration(c =>
			{
				c.AddProfile<LeaveTypeProfile>();
			});

			_mapper = mapperConfig.CreateMapper();
			_mockAppLogger = new Mock<IAppLogger<GetLeaveTypesQueryHandler>>();
		}

        [Fact]
		public async Task GetLeaveTypeListTest()
		{
			var handler = new GetLeaveTypesQueryHandler(_mapper, _listMockRepo.Object, _mockAppLogger.Object);

			var result = await handler.Handle(new GetLeaveTypesQuery(), CancellationToken.None);

			result.ShouldBeOfType<List<LeaveTypeDto>>();
			result.Count.ShouldBe(3);
		}

		[Fact]
		public async Task GetLeaveTypeDetailTest()
		{
			var handler = new GetLeaveTypeDetailsQueryHandler(_mapper, _detailMockRepo.Object);

			var result = await handler.Handle(new GetLeaveTypeDetailsQuery(1), CancellationToken.None);

			result.ShouldBeOfType<LeaveTypeDetailsDto>();
		}
	}
}
