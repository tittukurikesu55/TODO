using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webapp.Models.Repository
{
    public interface IUserDetailRepository
    {
        UserDetail GetUserDetail(string username, string password);
    }

    public class UserDetailRepository : IUserDetailRepository
    {
        public UserDetail GetUserDetail(string username, string password)
        {
            using (codedemoEntities dc = new codedemoEntities())
            {
                var userdetail = dc.UserDetails.Where(i => i.UserID == username && i.Password == password).FirstOrDefault(); ;
                return userdetail;
            }
        }
    }
}