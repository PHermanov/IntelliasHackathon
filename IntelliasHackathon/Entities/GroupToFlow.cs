namespace IntelliasHackathon.Entities
{
    public class GroupToFlow
    {
        public int Id { get; init; }
        public int GroupId { get; set; }
        public int FlowId { get; set; }

        public Priority Priority { get; set; }
    }
}
