using ForumService.Domain.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.Domain.Query
{
    public class GetForumSubjectQuery :IRequest<SubjectsDTO>
    {
    }
}
