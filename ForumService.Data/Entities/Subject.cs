using System;
using System.Collections.Generic;
using System.Text;

namespace ForumService.Data.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descriprion { get; set; }
        public int ThumbnailId { get; set; }

        public Thumbnail Thumbnail { get; set; }
        public ICollection<Thread> Thread { get; set; }
    }
}
