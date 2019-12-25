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
    public class GetForumThumbnailPictureQueryHandler : IRequestHandler<GetForumThumbnailPictureQuery,GetForumThumbnailPictureQueryDTO>
    {
        private readonly ForumServiceContext _context;
        public GetForumThumbnailPictureQueryHandler(ForumServiceContext context)
        {
            _context = context;
        }
        public async Task<GetForumThumbnailPictureQueryDTO> Handle(GetForumThumbnailPictureQuery request, CancellationToken token)
        {
            return new GetForumThumbnailPictureQueryDTO
            {
                Content = (await _context.Thumbnail.FirstOrDefaultAsync(x=>x.Id == request.Id, token))?.Content
            };
        }
    }
}
