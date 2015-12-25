using HaikuRatingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace HaikuRatingSystem.Services.Models
{
    public class UserSimpleViewModel
    {
        public string UserName { get; set; }
        public string PublishCode { get; set; }

        public Expression<Func<User, UserSimpleViewModel>> FromUser
        {
            get
            {
                return u => new UserSimpleViewModel
                {
                    UserName = u.UserName,
                    PublishCode = u.PasswordHash
                };
            }
        }
    }
}