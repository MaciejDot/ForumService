
using System.Threading;
using System.Threading.Tasks;
using ForumService.Domain.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ForumService.Controllers
{
    [Route("Search/Forum")]
    [ApiController]
    [EnableCors]
    public class SearchForumController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SearchForumController(IMediator mediator) {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<JsonResult> Get(string phrase, int page, CancellationToken token)
        {
            return new JsonResult(new { Threads = await _mediator.Send(new SearchForumThreadQuery { Phrase = phrase, Skip = (page * 20) - 20, Take = 20 },token) });
        }
    }
}