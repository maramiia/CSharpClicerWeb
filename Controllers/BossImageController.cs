using CSharpClicker.Web.Infrastructure.Abstractions;
using CSharpClicker.Web.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Web.Controllers
{
    [ApiController]
    [Route("api/bossimage")]
    public class BossImageController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public BossImageController(AppDbContext appDbContext, ICurrentUserAccessor currentUserAccessor)
        {
            _appDbContext = appDbContext;
            _currentUserAccessor = currentUserAccessor;
        }

        [HttpGet]
        [Route("user-boss-images")]
        public async Task<IActionResult> GetUserBossImages()
        {
            var userId = _currentUserAccessor.GetCurrentUserId();
            var bossImages = await _appDbContext.UserBossImages
                .Where(x => x.UserId == userId)
                .Select(x => new
                {
                    BossId = x.BossId,
                    ImageBase64 = Convert.ToBase64String(x.Image)
                })
                .ToListAsync();

            return Ok(bossImages);
        }
    }

}
