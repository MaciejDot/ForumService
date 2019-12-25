using System;
using System.Collections.Generic;
using System.Text;

namespace ForumService.Data.Entities
{
    public class Thumbnail
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }

        public ICollection<Subject> Subject { get; set; }
    }
}
