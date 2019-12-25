using ForumService.Domain.Command;
using ForumService.Domain.DTO;
using ForumService.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using ForumService.Data.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Thread = ForumService.Data.Entities.Thread;
namespace ForumService.Domain.CommandHandler
{
    public class CreateThreadCommandHandler : IRequestHandler<CreateThreadCommand, CreateThreadResponseDTO>
    {
        private readonly ForumServiceContext _context;
        public CreateThreadCommandHandler(ForumServiceContext applicationDatabaseContext)
        {
            _context = applicationDatabaseContext;
        }
        public async Task<CreateThreadResponseDTO> Handle(CreateThreadCommand command, CancellationToken token)
        {
            var thread = new Thread
            {
                Author = await _context.Users.FindAsync(new object[] { command.UserId }, token),
                Title = command.Title,
                Created = DateTime.Now.ToUniversalTime(),
                Question = command.Content,
                Subject = await _context.Subjects
                .FirstAsync(s => s.Title == command.SubjectName, token)

            };
            await _context.Threads.AddAsync(thread, token);
            await _context.SaveChangesAsync(token);
            return new CreateThreadResponseDTO { ThreadId = thread.Id, SubjectName = command.SubjectName };
        }
    }
}
