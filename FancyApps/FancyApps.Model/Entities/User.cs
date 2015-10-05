namespace FancyApps.Model
{
    using System;
    using System.Collections.Generic;
    using Data;
    using Entities;
    using MongoDB.Bson.Serialization.Attributes;

    [CollectionName("User")]
    public class User : Entity
    {
        private ICollection<Friend> friends;

        public User()
        {
            this.friends = new HashSet<Friend>();
        }

        public User(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        [BsonElement("fname")]
        public string FirstName { get; set; }

        [BsonElement("lname")]
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }
        
        [BsonIgnoreIfNull]
        public string Phone { get; set; }

        public DateTime CreateDatetime { get; set; }

        [BsonElement("fanapp")]
        public string FanApps { get; set; }

        [BsonElement("gender")]
        public string Gender { get; set; }

        [BsonElement("city")]
        public string City { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }

        public ICollection<Friend> Friends {
            get { return this.friends; }
            set { this.friends = value; }
        }
    }
}
