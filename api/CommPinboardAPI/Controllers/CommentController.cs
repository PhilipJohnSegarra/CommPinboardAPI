using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommPinboardAPI.Dtos;
using CommPinboardAPI.Entities;
using CommPinboardAPI.Helpers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CommPinboardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        ICommentHelper _helper;
        IPostHelper _postHelper;
        IMapper _mapper;
        public CommentController(ICommentHelper helper, IMapper mapper, IPostHelper postHelper){
            _helper = helper;
            _mapper = mapper;
            _postHelper = postHelper;
        }
        [HttpGet]
        public async Task<IActionResult> Get(){
            var comments = await _helper.GetAll();
            if(comments == null) return NotFound();
            return Ok(comments);
        }

        [HttpGet("{externalId}/postComments")]
        public async Task<IActionResult> GetPostComments(Guid externalId){
            var result = await _helper.GetPostComments(externalId);
            return Ok(new ResponseDto{
                Message = "Successfully fetched",
                Data = result
            });
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(CommentDto payload){
            var toEntity = _mapper.Map<Comment>(payload);
            var result = await _helper.Add(toEntity);

            return Ok(new ResponseDto{
                Data = result,
                Message = "Successfully created"
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update(CommentDto oldComment)
        {
            var toEntity = _mapper.Map<Comment>(oldComment);
            var result = await _helper.Update(oldComment.ExternalId, toEntity);

            return Ok(new ResponseDto{
                Data = result,
                Message = "Successfully Updated"
            });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid externalId){
            await _helper.Delete(externalId);
            if(_helper.Get(externalId) != null){
                return BadRequest("Deletion unsuccessful");
            }

            return Ok(new ResponseDto{Message = "Deletion successful"});
        }
    }
}