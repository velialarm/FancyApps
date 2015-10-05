namespace FancyApps.Services.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using Model;
    using Model.Entities;
    using Models;
    using Models.Request;
    using Models.Response;

    public class FriendController : AbstractApiController
    {
        [Route("api/add-friend")]
        [HttpPost]
        public AddFriendResponse AddFriend(AddFriendRequest input)
        {
            if (!this.ModelState.IsValid)
            {
                return new AddFriendResponse
                {
                    Status = new Status
                    {
                        Code = Status.INTERNAL_MESSAGE_CODE,
                        Message = this.ModelState.Values.First().Errors.First().ErrorMessage
                    }
                };
            }

            if (input == null)
            {
                return new AddFriendResponse(Status.MISSING_PARRAMETERS);
            }

            ////check for register user by Token
            User currentUser = null;
            try
            {
                currentUser = this.Users.SingleOrDefault(a => a.Token == input.Token); ////TOKEN must be unique 
                if (currentUser == null)
                {
                    return new AddFriendResponse(Status.INVALID_TOKEN);
                }
            }
            catch (Exception)
            {
                return new AddFriendResponse(Status.INTERNAL_ERROR);
            }

            ////check for exist friend
            var friends = currentUser.Friends;
            if (friends.Count > 0)
            {
                var friend = friends.FirstOrDefault(a => a.Email == input.Email);
                if (friend != null)
                {
                    return new AddFriendResponse(Status.ADDFRIEND_EXIST);
                }
            }

            friends.Add(new Friend
            {
                Nickname = input.Nickname,
                Email = input.Email
            });

            this.Users.Update(currentUser);

            return new AddFriendResponse(Status.ADDFRIEND_SUCCESSFULL_ADDED);
        }

        [Route("api/remove-friend")]
        [HttpPost]
        public RemoveFriendResponse RemoveFriend(RemoveFriendRequest input)
        {
            if (!this.ModelState.IsValid)
            {
                return new RemoveFriendResponse
                {
                    Status = new Status
                    {
                        Code = 601,
                        Message = this.ModelState.Values.First().Errors.First().ErrorMessage
                    }
                };
            }

            if (input == null)
            {
                return new RemoveFriendResponse(Status.MISSING_PARRAMETERS);
            }

            ////check for register user by Token
            User currentUser = null;
            try
            {
                currentUser = this.Users.SingleOrDefault(a => a.Token == input.Token); ////TOKEN must be unique 
                if (currentUser == null)
                {
                    return new RemoveFriendResponse(Status.INVALID_TOKEN);
                }
            }
            catch (Exception)
            {
                return new RemoveFriendResponse(Status.INTERNAL_ERROR);
            }

            ////check for exist friend
            var friends = currentUser.Friends;
            if (friends.Count > 0)
            {
                var friend = friends.FirstOrDefault(a => a.Email == input.Email);
                if (friend != null)
                {
                    friends.Remove(friend);
                    this.Users.Update(currentUser);

                    return new RemoveFriendResponse(Status.REMOVEFRIEND_SUCCESS);
                }
            }

            return new RemoveFriendResponse(Status.REMOVEFRIEND_NOT_EXIST_IN_LIST);
        }
    }
}