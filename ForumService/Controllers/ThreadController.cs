
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ForumService.Domain.Command;
using ForumService.Domain.DTO;
using ForumService.Domain.Query;
using ForumService.Helpers;
using ForumService.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ForumService.Controllers
{
    [EnableCors]
    [Route("Thread")]
    [ApiController]
    public class ThreadController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStringToHtmlHelper _stringToHtmlHelper;
        public ThreadController(IMediator mediator, IStringToHtmlHelper stringToHtmlHelper)
        {
            _mediator = mediator;
            _stringToHtmlHelper = stringToHtmlHelper;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody]CreateThread command, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return NotFound("not found");
            }

            return new JsonResult(new
            {
                Thread = await _mediator.Send(new CreateThreadCommand
                {
                    Content = _stringToHtmlHelper.GetHtml(command.Content),
                    SubjectName = command.SubjectName,
                    Title = command.Title,
                    UserId = User.Claims.Single(x => x.Type == "Id").Value
                }, token)
            });
        }

        [HttpGet("{subjectName}/{page}")]
        [AllowAnonymous]
        public async Task<ActionResult<ForumThreadsDTO>> Get(string subjectName, int page,CancellationToken token)
        {
            if (page < 1)
            {
                return BadRequest($"parameter page can't be less than 1");
            }
            try
            {
                return await _mediator.Send(new GetForumThreadsQuery { SkipThreads = page * 20 - 20, SubjectName = subjectName, TakeThreads = 20 },token);      
            }
            catch
            {
                return NotFound();
            }
        }
    }
}