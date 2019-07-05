using CoreTweet;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TweRiver.Interfaces
{
    public interface ITweetToken
    {
        public string ConsumerKey { get; }
        public string ConsumerSecret { get; }
        public Tokens Token { get; set; }

        public Task SetTokensAsync();
    }
}
