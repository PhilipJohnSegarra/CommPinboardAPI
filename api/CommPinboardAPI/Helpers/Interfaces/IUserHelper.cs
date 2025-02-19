using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommPinboardAPI.Dtos;
using CommPinboardAPI.Entities;

namespace CommPinboardAPI.Helpers.Interfaces
{
    public interface IUserHelper
    {
        Task<List<User>> GetAll();
        Task<User> Get(Guid externalId);
        Task<User> Add(User payload);
        Task<User> Update(Guid externalId, User payload);
        Task<UsersDto> AuthenticateUser(string userName, string password);
        Task Delete(Guid externalId);
    }
}