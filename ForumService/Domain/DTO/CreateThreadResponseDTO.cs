using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.Domain.DTO
{
    public class CreateThreadResponseDTO
    {
        public int ThreadId { get; set; }
        public string SubjectName { get; set; }
            
    }
}
