using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unity2d_web_scorerank_api.Models;

namespace unity2d_web_scorerank_api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ScoreRankController : ControllerBase
    {
        private readonly ILogger<ScoreRankController> _logger;

        private readonly IHighScoreRepository _highScoreRepository;

        public ScoreRankController(
            ILogger<ScoreRankController> logger,
            IHighScoreRepository highScoreRepository
            )
        {
            _logger = logger;
            _highScoreRepository = highScoreRepository;
        }

        [HttpPost]
        public async Task UploadScore([FromBody]HighScore info)
        {
            var insertedOrUpdated = await _highScoreRepository.InsertOrUpdateHighScore(info);

            if (!insertedOrUpdated)
            {
                throw new Exception("Insert or Update Failed!");
            }
        }

        [HttpGet]
        public async Task<HighScoreList> GetScores(int rows, int pages)
        {
            return await _highScoreRepository.QueryHighScores(rows, pages);
        }
    }
}
