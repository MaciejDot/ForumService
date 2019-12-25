using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ForumService.Domain.Query;
using ForumService.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ForumService.Forum
{
    [EnableCors]
    [Route("SubjectThumbnail")]
    [ApiController]
    public class SubjectThumbnailController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SubjectThumbnailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{thumbnailId}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetThumbnailPicture(int thumbnailId, CancellationToken token)
        {
            var thumbnail = await _mediator.Send(new GetForumThumbnailPictureQuery { Id = thumbnailId },token);
            if (thumbnail.Content != null)
            {
                return base.File(thumbnail.Content, "application/octet-stream");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
