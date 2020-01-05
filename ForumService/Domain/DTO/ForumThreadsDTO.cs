using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.Domain.DTO
{
    public class ForumThreadsDTO
    {
        public string Title { get; set; }
        public IEnumerable<GetForumThreadsDTO> Threads { get; set; }
        public int allThreadsCount { get; set; }
    }
}
