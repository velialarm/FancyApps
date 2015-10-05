namespace FancyApps.Model.Entities
{
    using Data;
    using MongoDB.Bson.Serialization.Attributes;

    [CollectionName("Friend")]
    public class Friend : Entity
    {
        [BsonIgnoreIfNull]
        public User User { get; set; }

        public string Nickname { get; set; }

        public string Email { get; set; }
    }
}