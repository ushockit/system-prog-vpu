using AutoMapper;
using MediatR;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Features.Queries.GetAllPeople
{
    public class GetAllPeopleQueryHandler : IRequestHandler<GetAllPeopleQueryRequest, List<GetAllPeopleQueryResponse>>
    {
        readonly IServiceManager serviceManager;
        readonly IMapper mapper;

        public GetAllPeopleQueryHandler(IServiceManager serviceManager, IMapper mapper)
        {
            this.serviceManager = serviceManager;
            this.mapper = mapper;
        }
        public async Task<List<GetAllPeopleQueryResponse>> Handle(GetAllPeopleQueryRequest request, CancellationToken cancellationToken)
        {
            var people = await serviceManager.PeopleService.GetAllPeopleAsync(cancellationToken);
            return mapper.Map<List<GetAllPeopleQueryResponse>>(people);
        }
    }
}
