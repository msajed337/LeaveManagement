using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
	public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
	{

        public GetLeaveTypesQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
			_mapper = mapper;
			_leaveTypeRepository = leaveTypeRepository;
		}

		public IMapper _mapper { get; }
		public ILeaveTypeRepository _leaveTypeRepository { get; }

		public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
		{
			var leaveTypes = await _leaveTypeRepository.GetAsync();

			var data = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

			return data;
		}
	}
}
