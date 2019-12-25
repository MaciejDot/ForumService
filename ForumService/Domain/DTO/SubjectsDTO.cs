using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.Domain.DTO
{
    public class SubjectsDTO
    {
        public IEnumerable<GetForumSubjectDTO> Subjects { get; set; }
    }
}
