using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Webapp.Models;
using Webapp.Models.Repository;
using Webapp.ViewModel;

namespace Webapp.BusinessLayer
{
    public interface IAuthentication
    {
        UserModel GetUserDetails(string username, string password);
    }
    public class Authentication : IAuthentication
    {
        dynamic config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserDetail, UserModel>();
            cfg.CreateMap<UserModel, UserDetail>();
        });
             
             
        dynamic mapper;
        private readonly IUserDetailRepository _IUserDetailRepository;
        public Authentication(IUserDetailRepository userDetailRepository)
        {
            _IUserDetailRepository = userDetailRepository;
            mapper = new Mapper(config);
        }

        public UserModel GetUserDetails(string username, string password)
        {
            return mapper.Map<UserModel>(_IUserDetailRepository.GetUserDetail(username, password));
        }
    }
}