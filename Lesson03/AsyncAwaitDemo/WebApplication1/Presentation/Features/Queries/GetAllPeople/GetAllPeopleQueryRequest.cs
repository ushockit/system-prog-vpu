using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Features.Queries.GetAllPeople
{
    public class GetAllPeopleQueryRequest : IRequest<List<GetAllPeopleQueryResponse>>
    {
        
    }
}
