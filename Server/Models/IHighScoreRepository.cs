using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace unity2d_web_scorerank_api.Models
{
    public interface IHighScoreRepository
    {
        Task<bool> InsertOrUpdateHighScore(HighScore highScore);

        Task<HighScoreList> QueryHighScores(int rows = 10, int pages = 0);
    }
}
