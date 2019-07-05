using CoreTweet;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using TweRiver.Interfaces;

namespace TweRiver.Models
{
    public class AuthModel : IAuthModel
    {
        private ITweetToken tokens;

        public ReactiveProperty<string> Text { get; } = new ReactiveProperty<string>();

        public string AuthURI { get; private set; }

        private OAuth.OAuthSession session;

        public AuthModel(ITweetToken token)
        {
            this.tokens = token;
        }

        public async Task InitializeAsync()
        {
            session = await OAuth.AuthorizeAsync(tokens.ConsumerKey, tokens.ConsumerSecret);
            AuthURI = session.AuthorizeUri.ToString();
        }
        public async Task<bool> AuthorizeAsync()
        {

            //ここまで実装した。
            //エラー処理とか、トークンつかった処理とかかこう。
            try
            {
                tokens.Token = await session.GetTokensAsync(Text.Value);
                await tokens.SetTokensAsync();
                return true;
            }
            catch
            {
                tokens.Token = null;
                return false;
            }
        }
    }
}
