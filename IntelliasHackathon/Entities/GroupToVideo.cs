namespace IntelliasHackathon.Entities
{
    public class GroupToVideo
    {
        public int Id { get; init; }
        public int GroupId { get; set; }
        public int VideoId { get; set; }

        public Priority Priority { get; set; }
    }
}
