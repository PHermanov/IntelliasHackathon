using IntelliasHackathon.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntelliasHackathon.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Flow> Flows { get; set; }
        public DbSet<FlowToVideo> FlowsToVideos { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupToVideo> GroupsToVideos { get; set; }
        public DbSet<GroupToFlow> GroupsToFlows { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserToFlow> UsersToFlows { get; set; }
        public DbSet<UserToGroup> UsersToGroups { get; set; }
        public DbSet<UserToVideo> UsersToVideos { get; set; }
        public DbSet<Video> Videos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}

