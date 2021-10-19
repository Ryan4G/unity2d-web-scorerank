using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace unity2d_web_scorerank_api.Models
{
    public class HighScore
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
    }
}
