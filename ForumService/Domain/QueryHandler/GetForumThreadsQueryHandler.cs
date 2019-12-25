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
    public class GetForumThreadsQueryHandler :IRequestHandler<GetForumThreadsQuery,List<GetForumThreadsDTO>>
    {
        private readonly ForumServiceContext _context;
        public GetForumThreadsQueryHandler(ForumServiceContext context)
        {
            _context = context;
        }
        public Task<List<GetForumThreadsDTO>> Handle(GetForumThreadsQuery request, CancellationToken token)
        {
            return Task.FromResult(_context.Threads.Where(t => t.Subject.Title == request.SubjectName)
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
                .ToList());
        }
    }
}
