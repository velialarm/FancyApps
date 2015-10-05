using FancyApps.Model.Entities;

namespace FancyApps.Services.Controllers
{

    using System;
    using System.Linq;
    using System.Web.Http;
    using Model;
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
                        Code = 601,
                        Message = this.ModelState.Values.First().Errors.First().ErrorMessage
                    }
                };
            }
            if (input == null)
            {
                return new AddFriendResponse
                {
                    Status = new Status(900, "Missing parameters")
                };
            }

            //check for register user by Token
            User currentUser = null;
            try
            {
                currentUser = this.Users.SingleOrDefault(a => a.Token == input.Token); //TODO TOKEN must be unique 
                if (currentUser == null)
                {
                    return new AddFriendResponse(new Status(103, "Invalid Token"));
                }
            }
            catch (Exception)
            {
                return new AddFriendResponse(new Status(105, "Internal Error"));
            }


            //check for exist friend
            var friends = currentUser.Friends;
            if (friends.Count > 0)
            {
                var friend = friends.FirstOrDefault(a => a.Email == input.Email);
                if (friend != null)
                {
                    return new AddFriendResponse(new Status(603, "Friend is exist in your list"));
                }
            }

            friends.Add(new Friend
            {
                Nickname = input.Nickname,
                Email = input.Email
            });

            this.Users.Update(currentUser);

            return new AddFriendResponse(new Status(0, "Friend was successfully added"));
            
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
                return new RemoveFriendResponse
                {
                    Status = new Status(900, "Missing parameters")
                };
            }

            //check for register user by Token
            User currentUser = null;
            try
            {
                currentUser = this.Users.SingleOrDefault(a => a.Token == input.Token); //TODO TOKEN must be unique 
                if (currentUser == null)
                {
                    return new RemoveFriendResponse(new Status(103, "Invalid Token"));
                }
            }
            catch (Exception)
            {
                return new RemoveFriendResponse(new Status(105, "Internal Error"));
            }

            //check for exist friend
            var friends = currentUser.Friends;
            if (friends.Count > 0)
            {
                var friend = friends.FirstOrDefault(a => a.Email == input.Email);
                if (friend != null)
                {
                    friends.Remove(friend);
                    this.Users.Update(currentUser);

                    return new RemoveFriendResponse(new Status(603, "Success remove friend from your list"));
                }
            }

            return new RemoveFriendResponse(new Status(603, "Friend is not exist in your list."));
        }
    }
}