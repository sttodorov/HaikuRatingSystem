namespace HaikuRatingSystem.Services.Controllers
{
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http;
    using System.Collections.Generic;

    using HaikuRaitingSystem.Common;
    using HaikuRatingSystem.Models;
    using HaikuRatingSystem.Services.Enums;
    using HaikuRatingSystem.Services.Models;

    [RoutePrefix("api/users")]
    public class UsersController : BaseApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get([FromUri]UsersSortBy sortby = UsersSortBy.UserName, [FromUri]SortingType sortType = SortingType.Ascending, [FromUri]int page = 0, [FromUri]int take = 10)
        {
            var allUsers = this.data.Users.All();
            IQueryable<User> vipDataUsers = allUsers.Where(u => u.IsVip);
            IQueryable<User> normalDataUsers = allUsers.Except(vipDataUsers);

            IEnumerable<UserViewModel> vipUsers = vipDataUsers.Select(UserViewModel.FromUser);
            IEnumerable<UserViewModel> normalUsers = normalDataUsers.Select(UserViewModel.FromUser);

            IEnumerable<UserViewModel> sortedVip = null;
            IEnumerable<UserViewModel> sortedNormal = null;

            if (sortType == SortingType.Ascending)
            {
                if (sortby == UsersSortBy.UserName)
                {
                    sortedVip = vipUsers.OrderBy(u => u.UserName);
                    sortedNormal = normalUsers.OrderBy(u => u.UserName);
                }
                if (sortby == UsersSortBy.Rating)
                {
                    sortedVip = vipUsers.OrderBy(u => u.Rating);
                    sortedNormal = normalUsers.OrderBy(u => u.Rating);
                }
            }
            else
            {
                if (sortby == UsersSortBy.UserName)
                {
                    sortedVip = vipUsers.OrderByDescending(u => u.UserName);
                    sortedNormal = normalUsers.OrderByDescending(u => u.UserName);
                }
                if (sortby == UsersSortBy.Rating)
                {
                    sortedVip = vipUsers.OrderByDescending(u => u.Rating);
                    sortedNormal = normalUsers.OrderByDescending(u => u.Rating);
                }
            }

            var result = sortedVip.Concat(sortedNormal);
            return Ok(result.Skip((page-1)*take).Take(take).ToList());
        }

        [HttpGet]
        [Route("{username}")]
        public IHttpActionResult Get(string username)
        {
            UserViewModel user = UserViewModel.FromUserModel(GetUser(username));
            if(user == null)
            {
                return BadRequest("Seected username was not found");
            }
            return Ok(user);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]UserCreationViewModel user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.PublishCode))
            {
                return BadRequest("Provided user info is not corrrect");
            }

            this.data.Users.Add(new User()
            {
                UserName = user.UserName,
                PasswordHash = Encryptor.GenerateHash(user.PublishCode)
            });
            this.data.SaveChanges();

            return Ok();
        }


        [HttpDelete]
        [Route("{username}")]
        public HttpResponseMessage Delete(string username)
        {
            if (!Request.Headers.Contains("PublishCode"))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest) { Content = new StringContent("Authentication header not found!") };
            }

            string auth = Request.Headers.GetValues("PublishCode").FirstOrDefault();
            if (!IsAdminAuth(auth))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { Content = new StringContent("You don't have permissions to delete accounts") };
            }

            var selectedUser = this.data.Users.All().FirstOrDefault(u => u.UserName == username);
            if (selectedUser == null)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest) { Content = new StringContent("User with selected username was not found!") };
            }

            var haikus = selectedUser.Haikus.ToList();
            foreach (var haiku in haikus)
            {
                var ratings = haiku.Ratings.ToList();
                foreach (var rating in ratings)
                {
                    this.data.Ratings.Delete(rating);
                }
                this.data.SaveChanges();

                this.data.Haikus.Delete(haiku);
            }
            this.data.SaveChanges();

            this.data.Users.Delete(selectedUser);
            this.data.SaveChanges();
            return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
        }

        [HttpPut]
        [Route("promote/{username}")]
        public HttpResponseMessage Put(string username)
        {
            if(!Request.Headers.Contains("PublishCode"))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest) { Content = new StringContent("Authentication header not found!") };
            }

            string auth = Request.Headers.GetValues("PublishCode").FirstOrDefault();
            if (!IsAdminAuth(auth))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { Content = new StringContent("You don't have permissions to delete accounts") };
            }

            var selectedUser = this.data.Users.All().FirstOrDefault(u => u.UserName == username);
            if (selectedUser == null)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest) { Content = new StringContent("User with selected username was not found!") };
            }

            selectedUser.IsVip = true;
            this.data.SaveChanges();

            return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
        }
    }
}