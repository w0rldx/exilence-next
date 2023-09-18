namespace Shared.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using MessagePack;

    [MessagePackObject]
    public class SnapshotModel
    {
        [JsonIgnore] [IgnoreMember] public int? Id { get; set; }

        [JsonPropertyName("uuid")] [Key("uuid")] public string ClientId { get; set; }

        [Key("created")] public DateTime? Created { get; set; }

        [Key("stashTabs")] public List<StashtabModel> StashTabs { get; set; }
    }
}
