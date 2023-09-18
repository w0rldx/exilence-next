namespace API.Models
{
    using MessagePack;

    [MessagePackObject]
    public class AnouncementMessageModel
    {
        [Key("title")] public string Title { get; set; }

        [Key("message")] public string Message { get; set; }
    }
}
