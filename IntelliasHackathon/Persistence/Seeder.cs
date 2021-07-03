using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using IntelliasHackathon.Entities;

namespace IntelliasHackathon.Persistence
{
    public static class Seeder
    {
        public static void Seed(AppDbContext appDbContext)
        {
            appDbContext.Database.EnsureCreated();

            if (appDbContext.Users.Any())
            {
                return; // DB has been seeded
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            options.Converters.Add(new JsonStringEnumConverter());

            var seedData = JsonSerializer.Deserialize<SeedData>(File.ReadAllText("Data.json"), options);

            foreach (var user in seedData.Users)
            {
                appDbContext.Users.Add(user);
            }
            foreach (var video in seedData.Videos)
            {
                appDbContext.Videos.Add(video);
            }
            foreach (var group in seedData.Groups)
            {
                appDbContext.Groups.Add(group);
            }
            foreach (var flow in seedData.Flows)
            {
                appDbContext.Flows.Add(flow);
            }
            foreach (var userToVideo in seedData.UsersToVideos)
            {
                appDbContext.UsersToVideos.Add(userToVideo);
            }
            foreach (var userToGroup in seedData.UsersToGroups)
            {
                appDbContext.UsersToGroups.Add(userToGroup);
            }
            foreach (var userToFlow in seedData.UsersToFlows)
            {
                appDbContext.UsersToFlows.Add(userToFlow);
            }
            foreach (var groupToVideo in seedData.GroupsToVideos)
            {
                appDbContext.GroupsToVideos.Add(groupToVideo);
            }
            foreach (var groupToFlow in seedData.GroupsToFlows)
            {
                appDbContext.GroupsToFlows.Add(groupToFlow);
            }
            foreach (var flowToVideo in seedData.FlowsToVideos)
            {
                appDbContext.FlowsToVideos.Add(flowToVideo);
            }

            appDbContext.SaveChanges();
        }

        public class SeedData
        {
            public List<User> Users { get; set; }
            public List<Video> Videos { get; set; }
            public List<Group> Groups { get; set; }
            public List<Flow> Flows { get; set; }
            public List<UserToVideo> UsersToVideos { get; set; }
            public List<UserToGroup> UsersToGroups { get; set; }
            public List<UserToFlow> UsersToFlows { get; set; }
            public List<GroupToVideo> GroupsToVideos { get; set; }
            public List<GroupToFlow> GroupsToFlows { get; set; }
            public List<FlowToVideo> FlowsToVideos { get; set; }
        }
    }
}