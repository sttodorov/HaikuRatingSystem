using HaikuRaitingSystem.Common;
using HaikuRatingSystem.Data;
using HaikuRatingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HaikuRatingSystem.Services.Controllers
{
    public class BaseApiController : ApiController
    {
        protected IHaikuData data;

        public BaseApiController()
            : this(new HaikuData())
        {
        }

        public BaseApiController(IHaikuData data)
        {
            this.data = data;
        }

        protected User GetUser(string userName)
        {
            var user = this.data.Users.All().FirstOrDefault(u => u.UserName == userName);
            if (user == null)
            {
                return user;
                //throw new ArgumentException("No user with provided publish code.");
            }
            return user;
        }

        protected bool IsAuthValud(int userId, string password)
        {
            var selectedUser = this.data.Users.GetById(userId);
            if(selectedUser == null)
            {
                return false;
            }

            return selectedUser.PasswordHash == Encryptor.GenerateHash(password);

        }
        protected bool IsAdminAuth(string password)
        {
            // TODO: Only for demo purpossees. Ckeck from DB
            return password == "Adm1n!";
        }
    }
}
