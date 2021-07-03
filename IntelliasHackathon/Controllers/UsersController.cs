using IntelliasHackathon.Entities;
using IntelliasHackathon.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelliasHackathon.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public UsersController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("{userId}/videos")]
        public async Task<IEnumerable<VideoViewModel>> GetAsync(int userId)
        {
            var filteredVideos = new Dictionary<string, Priority>();

            var videos = await _appDbContext.Users
                .Where(u => u.Id == userId)
                .Join(_appDbContext.UsersToVideos,
                    user => user.Id,
                    video => video.UserId,
                    (user, userVideo) => new { VideoId = userVideo.VideoId, Prio = userVideo.Priority })
                .Join(_appDbContext.Videos,
                    userVideo => userVideo.VideoId,
                    video => video.Id,
                    (userVideo, video) => new VideoViewModel { Video = video.Name, Priority = userVideo.Prio })
                .ToArrayAsync();

            foreach (var video in videos)
            {
                this.TryAddToDict(filteredVideos, video);
            }

            var videosFromGroups = await _appDbContext.Users
                .Where(u => u.Id == userId)
                .Join(_appDbContext.UsersToGroups,
                    user => user.Id,
                    userGroup => userGroup.UserId,
                    (user, userGroup) => new { GroupID = userGroup.GroupId })
                .Join(_appDbContext.GroupsToVideos,
                    userGroup => userGroup.GroupID,
                    groupVideo => groupVideo.Id,
                    (userGroup, groupVideo) => new { VideoId = groupVideo.VideoId, Prio = groupVideo.Priority })
                .Join(_appDbContext.Videos,
                    groupVideo => groupVideo.VideoId,
                    video => video.Id,
                    (groupVideo, video) => new VideoViewModel { Video = video.Name, Priority = groupVideo.Prio })
                .ToArrayAsync();

            foreach (var video in videos)
            {
                this.TryAddToDict(filteredVideos, video);
            }

            return filteredVideos.Select(d => new VideoViewModel { Video = d.Key, Priority = d.Value });
        }

        private void TryAddToDict(Dictionary<string, Priority> dict, VideoViewModel videoViewModel)
        {
            if (dict.Keys.Contains(videoViewModel.Video))
            {
                if (dict[videoViewModel.Video] > videoViewModel.Priority)
                {
                    dict.Add(videoViewModel.Video, videoViewModel.Priority);
                }
            }
            else
            {
                dict.Add(videoViewModel.Video, videoViewModel.Priority);
            }
        }
    }
}
