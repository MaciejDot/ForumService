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
    public class SubjectNameExistsInDatabaseQueryHandler : IRequestHandler<SubjectNameExistsInDatabaseQuery,string>
    {
        private readonly ForumServiceContext _context;
        public SubjectNameExistsInDatabaseQueryHandler(ForumServiceContext context)
        {
            _context = context;
        }
        public async Task<string> Handle(SubjectNameExistsInDatabaseQuery request, CancellationToken token)
        {
            return (await _context.Subjects.FirstOrDefaultAsync(x => x.Title == request.SubjectName, token))?.Title;
        }
    }
}
