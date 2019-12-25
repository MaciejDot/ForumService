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
    public class GetForumPostsQueryHandler : IRequestHandler<GetForumPostsQuery, PostPageDTO>
    {
        private readonly ForumServiceContext _context;
        public GetForumPostsQueryHandler(ForumServiceContext context)
        {
            _context = context;
        }
        public async Task<PostPageDTO> Handle(GetForumPostsQuery request, CancellationToken token)
        {
            var posts = await _context
                .Posts
                .Where(p => p.ThreadId == request.ThreadId)
                .OrderBy(post => post.Created)
                .Skip(request.SkipPosts)
                .Take(request.TakePosts)
                .Select(post => new GetForumPostsDTO
                {
                    Author = post.Author.Username,
                    Content = post.Answear,
                    Created = post.Created,
                    Id = post.Id
                }
                )
                .ToListAsync(token)
                ;
            var allPostsCount = await _context.Posts.CountAsync(x => x.Thread.Id == request.ThreadId, token);
            var thread = await _context.Threads
                .Select(x => 
                    new GetForumThreadDTO { Id = x.Id, Author = x.Author.Username, Content = x.Question, Title = x.Title, Created = x.Created, SubjectName = x.Subject.Title })
                .SingleAsync(x => x.Id == request.ThreadId, token);
            return new PostPageDTO
            {
                Posts = posts,
                AllPostsCount = allPostsCount,
                Thread = thread
            };
        }
    }
}
