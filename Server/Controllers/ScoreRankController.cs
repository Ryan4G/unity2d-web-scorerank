using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            if (!VerifyDataMD5(info))
            {
                throw new Exception("MD5 Error!");
            }

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

        private bool VerifyDataMD5(HighScore highScore)
        {
            var md5Str = $"{highScore.Name}#{highScore.Score}#MD5";

            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

            var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(md5Str));

            StringBuilder sb = new StringBuilder();

            foreach (var b in bytes)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString() == highScore.MD5;
        }
    }
}
