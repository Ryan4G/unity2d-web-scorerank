using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace unity2d_web_scorerank_api.Models
{
    public class HighScoreRepository : RepositoryBase, IHighScoreRepository
    {
        /// <summary>
        /// Insert or Update Records
        /// </summary>
        /// <param name="highScore"></param>
        /// <returns></returns>
        public async Task<bool> InsertOrUpdateHighScore(HighScore highScore)
        {
            using (var conn = GetMySqlConnection())
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }

                bool insert = false;

                if (highScore.ID == null)
                {
                    insert = true;
                }
                else
                {
                    var queryCmd = @"SELECT id, name, score FROM highscore WHERE id = @ID";

                    var matchedRecord = conn.Query<HighScore>(queryCmd, new { ID = highScore.ID }).FirstOrDefault();

                    if (matchedRecord == null)
                    {
                        insert = true;
                    }
                }

                if (insert)
                {
                    var insertCmd = @"INSERT INTO highscore VALUES(NULL, @NAME, @SCORE)";

                    var rowAffected = await conn.ExecuteAsync(insertCmd, new { NAME = highScore.Name, SCORE = highScore.Score });

                    return rowAffected > 0;
                }
                else
                {
                    var insertCmd = @"UPDATE highscore SET name = @NAME, score = @SCORE WHERE id = @ID";

                    var rowAffected = await conn.ExecuteAsync(insertCmd, new { ID = highScore.ID, NAME = highScore.Name, SCORE = highScore.Score });

                    return rowAffected > 0;
                }
            }
        }

        /// <summary>
        /// Query 
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="pages"></param>
        /// <returns></returns>
        public async Task<HighScoreList> QueryHighScores(int rows = 10, int pages = 0)
        {
            using (var conn = GetMySqlConnection())
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }

                int takes = Math.Max(1, rows);
                int skips = Math.Max(0, pages) * takes;

                var queryCmd = @$"SELECT id, name, score FROM highscore ORDER BY score DESC LIMIT {skips},{takes}";

                return new HighScoreList
                {
                    Scores = conn.Query<HighScore>(queryCmd)
                };
            }
        }


    }
}
