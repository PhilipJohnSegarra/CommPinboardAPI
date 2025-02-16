using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommPinboardAPI.Dtos;
using CommPinboardAPI.Entities;

namespace CommPinboardAPI.Services
{
    public class ObjectMapper : Profile
    {
        public ObjectMapper(){
            CreateMap<UsersDto, User>();
            CreateMap<PostDto, Post>();
        }
    }
}