using HaikuRaitingSystem.Common;
using HaikuRatingSystem.Data;
using HaikuRatingSystem.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HaikuRatingSystem.Services.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
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
            return this.data.Users.All().FirstOrDefault(u => u.Username == userName);
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
