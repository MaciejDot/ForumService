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
    public class SearchForumThreadQueryHandler : IRequestHandler<SearchForumThreadQuery,IEnumerable<SearchThreadsDTO>>
    {
        private readonly ForumServiceContext _context;
        public SearchForumThreadQueryHandler(ForumServiceContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SearchThreadsDTO>> Handle(SearchForumThreadQuery request, CancellationToken token)
        {
            return await _context.Threads.Where(t => t.Title.Contains(request.Phrase) || t.Question.Contains(request.Phrase) || t.Author.Username.Contains(request.Phrase) || t.Post.Any(p => p.Author.Username.Contains(request.Phrase) || p.Answear.Contains(request.Phrase)))
                .Select(x => new SearchThreadsDTO { Name = x.Title, Author = x.Author.Username, Created = x.Created, LastActivity = x.Post.Any() ? x.Post.Max(y => y.Created) : x.Created })
                .OrderByDescending(x => x.LastActivity)
                .Skip(request.Skip)
                .Take(request.Take)
                .ToListAsync(token);

        }
    }
}
