using ForumService.Domain.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.Domain.Query
{
    public class GetForumThumbnailPictureQuery :IRequest<GetForumThumbnailPictureQueryDTO>
    {
        public int Id { get; set; }
    }
}
