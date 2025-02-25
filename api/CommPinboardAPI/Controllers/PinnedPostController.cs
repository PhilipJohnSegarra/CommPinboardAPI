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
    public class PinnedPostController : ControllerBase
    {
        IPinnedPostHelper _helper;
        IMapper _mapper;
        public PinnedPostController(IPinnedPostHelper helper, IMapper mapper){
            _helper = helper;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get(){
            var comments = await _helper.GetAll();
            if(comments == null) return NotFound();
            return Ok(comments);
        }
        [HttpGet("userPinnedPost")]
        public async Task<IActionResult> GetUserPinnedPost(Guid externalId){
            var result = await _helper.GetUserPinnedPosts(externalId);
            return Ok(new ResponseDto{Data = result, Message = "Successfully retrieved"});
        }
        //ADD GET USER PINNEDPOSTS
        
        [HttpPost]
        public async Task<IActionResult> Add(PinnedPostDto payload){
            var toEntity = _mapper.Map<PinnedPost>(payload);
            var result = await _helper.Add(toEntity);

            return Ok(new ResponseDto{
                Data = result,
                Message = "Successfully created"
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update(PinnedPostDto oldComment)
        {
            var toEntity = _mapper.Map<PinnedPost>(oldComment);
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