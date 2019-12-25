using ForumService.Domain.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.Domain.Query
{
    public class GetForumPostsQuery : IRequest<PostPageDTO>
    {
        public int ThreadId { get; set; }
        public int SkipPosts { get; set; }
        public int TakePosts { get; set; }
    }
}
