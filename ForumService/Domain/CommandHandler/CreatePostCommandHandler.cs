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
                AuthorId = command.UserId,
                ThreadId = command.ThreadId,
                Created = DateTime.Now,
                Answear = command.Content
            };
            await _context.Posts.AddAsync(post, token);
            await _context.SaveChangesAsync(token);
            return new CreatePostResponseDTO { 
                PostPlace = _context.Threads.Find(command.ThreadId).Post.Count(x => x.Created < post.Created) + 1 
            
            };
        }
    }
}
