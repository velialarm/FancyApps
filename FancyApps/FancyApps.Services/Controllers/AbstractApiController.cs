using System.Web.Http;

namespace FancyApps.Services.Controllers
{
    using Data.MongoRepository;
    using Model;
    using Model.Entities;

    public class AbstractApiController : ApiController
    {

        private MongoRepository<User> usersRepository;
        private MongoRepository<Friend> friendRepository;

        public AbstractApiController()
        {
            this.usersRepository = new MongoRepository<User>();
            this.friendRepository = new MongoRepository<Friend>();
        }

        public MongoRepository<User> Users
        {
            get { return this.usersRepository; }
            set { value = this.usersRepository; }
        }

        public MongoRepository<Friend> Friends
        {
            get { return this.friendRepository; }
            set { this.friendRepository = value; }
        }
    }
}