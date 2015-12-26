using HaikuRatingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace HaikuRatingSystem.Services.Models
{
    public class UserCreationViewModel
    {
        public string UserName { get; set; }
        public string PublishCode { get; set; }

        public Expression<Func<User, UserCreationViewModel>> FromUser
        {
            get
            {
                return u => new UserCreationViewModel
                {
                    UserName = u.UserName,
                    PublishCode = u.PasswordHash
                };
            }
        }
    }
}