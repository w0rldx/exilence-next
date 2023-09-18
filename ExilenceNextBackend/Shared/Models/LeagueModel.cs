namespace Shared.Models
{
    using System.Text.Json.Serialization;
    using MessagePack;

    [MessagePackObject]
    public class LeagueModel
    {
        [JsonIgnore] [IgnoreMember] public int? Id { get; set; }

        [JsonPropertyName("uuid")] [Key("uuid")] public int? ClientId { get; set; }

        [Key("name")] public string Name { get; set; }
    }
}
