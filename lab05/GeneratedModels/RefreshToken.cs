using System;
using System.Collections.Generic;

namespace lab05.GeneratedModels
{
    public partial class RefreshToken
    {
        public string Token { get; set; }
        public string StudentId { get; set; }
        public DateTime ValidTo { get; set; }

        public virtual Student Student { get; set; }
    }
}
