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
    public class GetForumSubjectQueryHandler : IRequestHandler<GetForumSubjectQuery, SubjectsDTO>
    {
        private readonly ForumServiceContext _context;
        public GetForumSubjectQueryHandler(ForumServiceContext context)
        {
            _context = context;
        }
        public async Task<SubjectsDTO> Handle(GetForumSubjectQuery request, CancellationToken token)
        {
            var subjects = await _context.Subjects
                .Select(subject => new GetForumSubjectDTO
                {
                    Id = subject.Id,
                    ThumbnailId = subject.ThumbnailId,
                    Description = subject.Descriprion,
                    Title = subject.Title
                }).ToListAsync(token);
            subjects.ForEach(subject => {
                subject.PostCount = _context.Threads.Count(x => x.SubjectId == subject.Id) + _context.Posts.Count(x => x.Thread.SubjectId == subject.Id);
                subject.LastActivity = new DateTime(Math.Max(
                    _context.Threads.Any(x => x.SubjectId == subject.Id)?
                    _context.Threads.Where(x => x.SubjectId == subject.Id).Max(x => x.Created).Ticks:
                    DateTime.MinValue.Ticks,
                    _context.Posts.Any(x => x.Thread.SubjectId == subject.Id)?
                    _context.Posts.Where(x => x.Thread.SubjectId == subject.Id).Max(x => x.Created).Ticks:
                    DateTime.MinValue.Ticks));
             });
            return new SubjectsDTO
            {
                Subjects = subjects
            };
        }
    }
}
