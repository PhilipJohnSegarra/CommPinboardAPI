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
    public class PostController : ControllerBase
    {
        IPostHelper _helper;
        IMapper _mapper;
        public PostController(IPostHelper postHelper, IMapper mapper){
            _helper = postHelper;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(){
            var posts = await _helper.GetAll();
            if(posts == null) return NotFound();
            return Ok(posts);
        }

        [HttpGet("withUsers")]
        public async Task<IActionResult> GetWithUsers(){
            var result = await _helper.GetPostsWithUsers();
            if(result == null) return NotFound();
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(PostDto payload){
            var toEntity = _mapper.Map<Post>(payload);
            var result = await _helper.Add(toEntity);

            return Ok(new ResponseDto{
                Data = result,
                Message = "Successfully created"
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update(PostDto oldPost)
        {
            var toEntity = _mapper.Map<Post>(oldPost);
            var result = await _helper.Update(oldPost.ExternalId, toEntity);

            return Ok(new ResponseDto{
                Data = result,
                Message = "Successfully Updated"
            });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid externalId){
            await _helper.Delete(externalId);
            if(_helper.Get(externalId) != null){
                return BadRequest(new ResponseDto{Message = "Deletion Unsuccessful"});
            }

            return Ok(new ResponseDto{Message = "Deletion successful"});
        }
    }
}