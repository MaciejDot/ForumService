﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ForumService.Domain.Command;
using ForumService.Domain.DTO;
using ForumService.Domain.Query;
using ForumService.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ForumService.Forum
{
    [EnableCors]
    [Route("Subject")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SubjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<SubjectsDTO>> Get(CancellationToken token)
        {
            return await _mediator.Send(new GetForumSubjectQuery(), token);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Post([FromForm] CreateSubject command, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid model" });
            }
            var fileStreamResult = Request.Form.Files[0].OpenReadStream();
            var fileBytes = new byte[fileStreamResult.Length];
            fileStreamResult.Read(fileBytes, 0, (int)fileStreamResult.Length);
            if (!fileBytes.Any())
            {

                return BadRequest(new { message = "Photo was not provided" });
            }
            return new JsonResult(new
            {
                Forum = await _mediator.Send(new CreateForumSubjectCommand
                {
                    Content = fileBytes,
                    Description = command.Description,
                    Title = command.Title
                },token)
            });
        }
    }
}