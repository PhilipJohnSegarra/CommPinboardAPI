using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommPinboardAPI.Data;
using CommPinboardAPI.Entities;
using CommPinboardAPI.Repositories;
using BCrypt.Net;
using CommPinboardAPI.Helpers.Interfaces;
using AutoMapper;
using CommPinboardAPI.Dtos;

namespace CommPinboardAPI.Helpers
{
    public class UserHelper : RepositoryBase<User>, IUserHelper
    {
        private readonly HashHelper _hashHelper;
        private readonly IMapper _mapper;
        public UserHelper(DataContext db, HashHelper hashHelper, IMapper mapper) : base(db)
        {
            _hashHelper = hashHelper;
            _mapper = mapper;
        }

        public async Task<List<User>> GetAll()
        {
            List<User> users = await GetAllAsync(user => user.IsDeleted == false);

            return users;
        }
        public async Task<User> Get(Guid externalId)
        {
            User user = await GetAsync(user => user.ExternalId == externalId && user.IsDeleted.Equals(false));
            return user;
        }

        public async Task<User> Add(User payload)
        {
            if(payload == null){
                throw new BadHttpRequestException("No user received");
            }

            User newUser = payload;
            newUser.PasswordHash = await _hashHelper.Encrypt(payload.PasswordHash);

            await AddAsync(newUser);
            return payload;
        }

        public async Task<User> Update(Guid externalId, User payload){
            if(payload == null){
                throw new BadHttpRequestException("No update received");
            }

            var oldUser = await GetAsync(user => user.ExternalId.Equals(externalId) && user.IsDeleted.Equals(false));
            //Hash the updated password
            payload.PasswordHash = await _hashHelper.Encrypt(payload.PasswordHash);
            payload.DateCreated = oldUser.DateCreated;
            await UpdateAsync(oldUser, payload);

            return await Get(externalId);
        }

        public async Task Delete(Guid externalId)
        {
            var user = await GetAsync(user => user.ExternalId.Equals(externalId) && user.IsDeleted.Equals(false));

            User deletedUser = user;
            deletedUser.IsDeleted = true;

            await UpdateAsync(user, deletedUser);
        }

        public async Task<UsersDto> AuthenticateUser(string userName, string password)
        {
            string hashedPassword = await _hashHelper.ComputeSHA256(password);

            var user = await GetAsync(user => user.UserName.Equals(userName));
            var userDto = _mapper.Map<UsersDto>(user);
            bool isValid = await _hashHelper.Verify(password, user.PasswordHash);

            if(!isValid){
                throw new BadHttpRequestException("Invalid credentials");
            }
            return userDto;
        }
    }
}