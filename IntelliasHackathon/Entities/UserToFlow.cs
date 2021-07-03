namespace IntelliasHackathon.Entities
{
    public class UserToFlow
    {
        public int Id { get; init; }
        public int UserId { get; set; }
        public int FlowId { get; set; }

        public Priority Priority { get; set; }
    }
}
