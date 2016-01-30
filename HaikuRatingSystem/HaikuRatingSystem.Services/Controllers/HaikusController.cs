using HaikuRatingSystem.Models;
using HaikuRatingSystem.Services.Enums;
using HaikuRatingSystem.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace HaikuRatingSystem.Services.Controllers
{
    [RoutePrefix("api/haikus")]
    public class HaikusController : BaseApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get([FromUri]HaikusSrotBy sortby = HaikusSrotBy.DateCreated, [FromUri]SortingType sorttype = SortingType.Ascending, [FromUri]int page = 0, [FromUri]int take = 10)
        {
            var allDataHaikus = this.data.Haikus.All();
            IEnumerable<HaikuViewModel> allHaikus = allDataHaikus.Select(HaikuViewModel.FromHaiku);
            IEnumerable<HaikuViewModel> sortedHaikus = null;

            if (sorttype == SortingType.Ascending)
            {
                if (sortby == HaikusSrotBy.DateCreated)
                {
                    sortedHaikus = allHaikus.OrderBy(h => h.DatePublished);
                }
                if (sortby == HaikusSrotBy.Rating)
                {
                    sortedHaikus = allHaikus.OrderBy(h => h.Rating);
                }
            }
            else
            {
                if (sortby == HaikusSrotBy.DateCreated)
                {
                    sortedHaikus = allHaikus.OrderByDescending(h => h.DatePublished);
                }
                if (sortby == HaikusSrotBy.Rating)
                {
                    sortedHaikus = allHaikus.OrderByDescending(h => h.Rating);
                }
            }

            return Ok(sortedHaikus.Skip((page - 1) * take).Take(take).ToList());
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var selectedHaiku = this.data.Haikus.GetById(id);
            if (selectedHaiku == null)
            {
                return NotFound();
            }

            return Ok(HaikuViewModel.FromHaikuModel(selectedHaiku));
        }

        [HttpGet]
        [Route("reported")]
        public IHttpActionResult Get([FromUri]int page = 0, [FromUri]int take = 10)
        {
            if (!Request.Headers.Contains("PublishCode"))
            {
                return BadRequest("Authentication header not found!");
            }

            string auth = Request.Headers.GetValues("PublishCode").FirstOrDefault();
            if (!IsAdminAuth(auth))
            {
                return Unauthorized();
            }

            var reported = this.data.Haikus.All().Where(h => h.IsReported).OrderBy(h => h.DateReported).Select(HaikuViewModel.FromHaiku);

            return Ok(reported.Skip((page - 1) * take).Take(take).ToList());
        }

        [HttpPost]
        [Route("~/api/{username}/haikus")]
        public IHttpActionResult Post(string username, [FromBody]HaikuSimpleViewModel haiku)
        {
            if (!Request.Headers.Contains("PublishCode"))
            {
                return BadRequest("Authentication header not found!");
            }

            string auth = Request.Headers.GetValues("PublishCode").FirstOrDefault();
            if (!HaikuSimpleViewModel.IsValid(haiku))
            {
                return BadRequest("Haiku is not valid!");
            }

            var currentUser = GetUser(username);

            if (currentUser == null || !IsAuthValud(currentUser.UserId, auth))
            {
                return BadRequest("Username or password are not correct");
            }

            var haikuToAdd = new Haiku() { Author = currentUser, Text = haiku.Text };

            this.data.Haikus.Add(haikuToAdd);
            this.data.SaveChanges();


            var created = currentUser.Haikus.AsQueryable().Where(h => h.Text == haiku.Text).Select(HaikuCreationViewModel.FromHaiku).FirstOrDefault();

            return Created<HaikuCreationViewModel>("", created);
        }

        [HttpPost]
        [Route("{id}")]
        public IHttpActionResult Post(int id, [FromBody]RatingViewModel rating)
        {
            if (!RatingViewModel.IsValid(rating))
            {
                return BadRequest("Rating is not valid");
            }

            var selectedHaiku = this.data.Haikus.GetById(id);
            if (selectedHaiku == null)
            {
                return BadRequest("Hakiu with selected id was not found!");
            }

            var ratingToCreate = new Rating() { Haiku = selectedHaiku, RatingValue = rating.RatingValue };

            selectedHaiku.Ratings.Add(ratingToCreate);

            this.data.SaveChanges();
            return Created<RatingViewModel>("", RatingViewModel.FromRatingModel(ratingToCreate));
        }

        [HttpDelete]
        [Route("~/api/{username}/haikus/{id}")]
        public HttpResponseMessage Delete(string username, int id)
        {
            if (!Request.Headers.Contains("PublishCode"))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { Content = new StringContent("Authentication header was not found!") };
            }

            string auth = Request.Headers.GetValues("PublishCode").FirstOrDefault();
            var currentUser = GetUser(username);

            if (currentUser == null)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest) { Content = new StringContent("Username is not correct!") };
            }

            if (!IsAdminAuth(auth) && !IsAuthValud(currentUser.UserId, auth))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { Content = new StringContent("Publish code is not correct!") };
            }
            if (!currentUser.Haikus.Any(h => h.HaikuId == id))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest) { Content = new StringContent("Selected Haiku Id is not real or not owned by selected user!") };
            }

            var toDelete = this.data.Haikus.GetById(id);
            if (toDelete == null)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest) { Content = new StringContent("Haiku with selected ID was not found!") };
            }

            var haikuRatings = toDelete.Ratings.ToList();
            foreach (var rating in haikuRatings)
            {
                this.data.Ratings.Delete(rating);
            }
            this.data.SaveChanges();

            this.data.Haikus.Delete(toDelete);
            this.data.SaveChanges();

            return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("~/api/{username}/haikus")]
        public HttpResponseMessage Delete(string username)
        {
            if (!Request.Headers.Contains("PublishCode"))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest) { Content = new StringContent("Authentication header not found!") };
            }

            string auth = Request.Headers.GetValues("PublishCode").FirstOrDefault();
            var currentUser = GetUser(username);

            if (currentUser == null)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest) { Content = new StringContent("User is not correct!") };
            }
            if (!IsAdminAuth(auth) && !IsAuthValud(currentUser.UserId, auth))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { Content = new StringContent("Publish code is not correct!") };
            }

            var haikus = currentUser.Haikus.ToList();
            foreach (var hauku in haikus)
            {
                var haikuRatings = hauku.Ratings.ToList();
                foreach (var rating in haikuRatings)
                {
                    this.data.Ratings.Delete(rating);
                }
                this.data.SaveChanges();

                this.data.Haikus.Delete(hauku);
                this.data.SaveChanges();
            }

            return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
        }

        [HttpPut]
        [Route("~/api/{username}/haikus/{id}")]
        public HttpResponseMessage Put(string username, int id, [FromBody]HaikuSimpleViewModel haiku)
        {
            if (!Request.Headers.Contains("PublishCode"))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest) { Content = new StringContent("Authentication header not found!") };
            }

            string auth = Request.Headers.GetValues("PublishCode").FirstOrDefault();
            var currentUser = GetUser(username);

            if (currentUser == null || !IsAuthValud(currentUser.UserId, auth))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest) { Content = new StringContent("User or publish code are not correct!") };
            }
            if (!HaikuSimpleViewModel.IsValid(haiku))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest) { Content = new StringContent("Haiku is not valid!") };
            }
            var selectedHaiku = this.data.Haikus.GetById(id);
            if (selectedHaiku == null)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest) { Content = new StringContent("Haiku with selected ID was not found!") };
            }
            selectedHaiku.Text = haiku.Text;
            this.data.SaveChanges();

            var currentRatings = selectedHaiku.Ratings.ToList();
            foreach (var rating in currentRatings)
            {
                this.data.Ratings.Delete(rating);
            }
            this.data.SaveChanges();

            return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
        }

        [HttpPut]
        [Route("{id}")]
        public HttpResponseMessage Put(int id)
        {
            var selectedHaiku = this.data.Haikus.GetById(id);
            if (selectedHaiku == null)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest) { Content = new StringContent("Haiku with selected ID was not found!") };
            }
            if (selectedHaiku.IsReported)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
            }
            var author = selectedHaiku.Author;
            selectedHaiku.IsReported = true;
            selectedHaiku.DateReported = DateTime.Now;
            selectedHaiku.Author = author;

            this.data.SaveChanges();

            return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
        }
    }
}
