using System;
using System.Collections.Generic;
using System.Text;

namespace ForumService.Data.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }

        public ICollection<Thread> Threads { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
