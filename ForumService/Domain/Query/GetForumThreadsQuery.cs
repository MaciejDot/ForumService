using ForumService.Domain.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.Domain.Query
{
    public class GetForumThreadsQuery :IRequest<ForumThreadsDTO>
    {
        public int TakeThreads { get; set; }
        public int SkipThreads { get; set; }
        public string SubjectName { get; set; }
    }
}
