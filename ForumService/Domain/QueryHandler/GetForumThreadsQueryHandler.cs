using ForumService.Domain.DTO;
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
    public class GetForumThreadsQueryHandler : IRequestHandler<GetForumThreadsQuery, ForumThreadsDTO>
    {
        private readonly ForumServiceContext _context;
        public GetForumThreadsQueryHandler(ForumServiceContext context)
        {
            _context = context;
        }
        public async Task<ForumThreadsDTO> Handle(GetForumThreadsQuery request, CancellationToken token)
        {
            return new ForumThreadsDTO
            {
                Threads = await _context.Threads.Where(t => t.Subject.Title == request.SubjectName)
                .OrderByDescending(thread => thread.Created)
                .Skip(request.SkipThreads)
                .Take(request.TakeThreads)
                .Select(thread =>
                    new GetForumThreadsDTO
                    {
                        Id = thread.Id,
                        Author = thread.Author.Username,
                        Created = thread.Created,
                        Title = thread.Title,
                        Replies = thread.Post.Count,
                        LastActivity = thread.Post.Any() ? thread.Post.Max(post => post.Created) : thread.Created
                    }
                )
                .ToListAsync(token),
                allThreadsCount = await _context.Threads.CountAsync(t => t.Subject.Title == request.SubjectName, token),
                Title = (await _context.Subjects.SingleAsync(x => x.Title == request.SubjectName)).Title
            };
        }
    }
}
