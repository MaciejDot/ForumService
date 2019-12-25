using System;
using System.Collections.Generic;
using System.Text;

namespace ForumService.Data.Entities
{
    public class Thread
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Question { get; set; }
        public string AuthorId { get; set; }
        public DateTime Created { get; set; }
        public int SubjectId { get; set; }

        public User Author { get; set; }
        public Subject Subject { get; set; }
        public ICollection<Post> Post { get; set; }
    }
}
