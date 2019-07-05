using CoreTweet;
using System;
using System.Collections.Generic;
using System.Text;

namespace TweRiver.Models
{
    public class AlcedoModel
    {
        public string Name { get; set; }

        public string Tweet { get; set; }

        public AlcedoModel(Status status)
        {
            if (status.RetweetedStatus != null)
            {
                this.Name = $"{status.User.Name} RT. => {status.RetweetedStatus.User.Name}";
                this.Tweet = status.RetweetedStatus.FullText.Replace(@"\r", "").Replace(@"\n", "");
            }
            else
            {
                this.Name = $"{status.User.Name}";
                this.Tweet = status.FullText.Replace(@"\r", "").Replace(@"\n", "");
            }
        }
    }
}
