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
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, CreatePostResponseDTO>
    {
        private readonly ForumServiceContext _context;
        public CreatePostCommandHandler(ForumServiceContext context)
        {
            _context = context;
        }
        public async Task<CreatePostResponseDTO> Handle(CreatePostCommand command, CancellationToken token)
        {
            var post = new Post
            {
                Author = await _context.Users.FindAsync(new object[] { command.UserId },token),
                Created = DateTime.Now,
                Answear = command.Content
            };
            var thread = await _context.Threads.FindAsync(new object[] { command.ThreadId }, token);
            thread.Post.Add(post);
            await _context.SaveChangesAsync(token);
            return new CreatePostResponseDTO { 
                PostPlace = _context.Threads.Find(command.ThreadId).Post.Count(x => x.Created < post.Created) + 1 
            
            };
        }
    }
}
