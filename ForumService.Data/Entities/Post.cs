using System;
using System.Collections.Generic;
using System.Text;

namespace ForumService.Data.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Answear { get; set; }
        public string AuthorId { get; set; }
        public DateTime Created { get; set; }
        public int ThreadId { get; set; }

        public User Author { get; set; }
        public Thread Thread { get; set; }
    }
}
