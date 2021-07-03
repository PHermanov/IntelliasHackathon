namespace IntelliasHackathon.Entities
{
    public class UserToVideo
    {
        public int Id { get; init; }
        public int UserId { get; set; }
        public int VideoId { get; set; }

        public Priority Priority { get; set; }
    }
}
