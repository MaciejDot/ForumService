using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumService.Domain.DTO;
using MediatR;
namespace ForumService.Domain.Command
{
    public class CreateThreadCommand : IRequest<CreateThreadResponseDTO>
    {
        public string SubjectName { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
