namespace Shared.Entities
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Snapshot
    {
        [BsonId] [BsonRepresentation(BsonType.ObjectId)] public string Id { get; set; }

        public string ClientId { get; set; }
        public DateTime Created { get; set; }

        [BsonIgnore] public List<StashTab> StashTabs { get; set; }

        public string ProfileClientId { get; set; } //stored in SQL
    }
}
