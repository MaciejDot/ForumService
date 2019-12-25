using ForumService.Domain.Command;
using ForumService.Domain.DTO;
using ForumService.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ForumService.Data.Entities;

namespace ForumService.Domain.CommandHandler
{
    public class CreateForumSubjectCommandHandler : IRequestHandler<CreateForumSubjectCommand , CreateForumSubjectResponseDTO>
    {
        private readonly ForumServiceContext _context;
        public CreateForumSubjectCommandHandler(ForumServiceContext context)
        {
            _context = context;
        }
        public async Task<CreateForumSubjectResponseDTO> Handle(CreateForumSubjectCommand command, CancellationToken token)
        {
            var thumbnail = new Thumbnail { Content = command.Content };
            await _context.Thumbnail.AddAsync(thumbnail, token);
            var subject = new Subject { Thumbnail = thumbnail, Descriprion = command.Description, Title = command.Title };
            await _context.Subjects.AddAsync(subject, token);
            await _context.SaveChangesAsync(token);
            return new CreateForumSubjectResponseDTO
            {
                Id = subject.Id,
                Title = subject.Title
            };
        }
    }
}
