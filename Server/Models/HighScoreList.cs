using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace unity2d_web_scorerank_api.Models
{
    public class HighScoreList
    {
        public IEnumerable<HighScore> Scores { get; set; }
    }
}
