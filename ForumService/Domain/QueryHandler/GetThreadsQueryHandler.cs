using ForumService.Domain.Query;
using ForumService.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ForumService.Domain.QueryHandler
{
    public class GetThreadsCountQueryHandler :IRequestHandler<GetThreadsCountQuery,int>
    {
        private readonly ForumServiceContext _context;
        public GetThreadsCountQueryHandler(ForumServiceContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(GetThreadsCountQuery request, CancellationToken token)
        {
            return await _context.Threads.CountAsync(t=> t.Subject.Title == request.SubjectName, token);
        }
    }
}
