using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CoreTweet;
using TweRiver.Interfaces;
using System.Linq;
using MessagePack;
using TweRiver.Views;
using TweRiver.ViewModels;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading;

namespace TweRiver.Models
{
    public class TweetModel : ITweetToken
    {
        public static string PATH = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ProjectATR\TweRiver";
        public static readonly string KEY_FILEPATH = PATH + @"\key.mpac";

        public string ConsumerKey { get; } = "{consumerKey}";
        public string ConsumerSecret { get; } = "{consumerSecretKey}";

        public Tokens Token { get; set; }

        private readonly Dictionary<string, object> fparam = new Dictionary<string, object>
        {
            ["count"] = 30,
            ["tweet_mode"] = TweetMode.Extended
        };

        private readonly Dictionary<string, object> param = new Dictionary<string, object>
        {
            ["since_id"] = 0,
            ["tweet_mode"] = TweetMode.Extended
        };

        public TweetModel()
        {
        }
        public async Task InitializeAsync()
        {
            for (var i = 0; i < 10; i++) Debug.WriteLine("わああ");
            if (File.Exists(KEY_FILEPATH))
            {
                using var fs = File.OpenRead(KEY_FILEPATH);
                var keys = await MessagePackSerializer.DeserializeAsync<KeyModel>(fs);
                this.Token = Tokens.Create(ConsumerKey, ConsumerSecret, keys.AccessToken, keys.AccessSecret);
                this.Publish();
            }
            else
            {
                var model = new AuthModel(this);
                await model.InitializeAsync();
                var vm = new AuthWindowViewModel(model);
                var authWin = new AuthWindow(vm);
                authWin.ShowDialog();
            }
        }

        public async Task SetTokensAsync()
        {
            if (Token != null)
            {
                using var bs = File.OpenWrite(KEY_FILEPATH);
                await MessagePackSerializer.SerializeAsync(bs, new KeyModel(Token.AccessToken, Token.AccessTokenSecret));
                this.Publish();
            }
        }

        private void Publish()
        {
            long lastId = -1;
            Observable.Timer(TimeSpan.FromMilliseconds(0),TimeSpan.FromSeconds(70))
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(async _ =>
                {
                    var now = DateTime.Now;
                    try
                    {
                        using (var sww = new StreamWriter($@"{PATH}/log_{now.Year}_{now.Month}_{now.Day}.log", true, Encoding.UTF8))
                        {
                            await sww.WriteLineAsync($"{now.Hour}:{now.Minute}:{now.Second}.{now.Millisecond} => lastId = {lastId} : lastId!=-1 = {lastId != -1}");
                        }
                        var status = (await Token.Statuses.HomeTimelineAsync(lastId != -1 ? param : fparam))?.Reverse().ToArray();
                        var id = status?.Where(status => status.RetweetedStatus == null)?.Select(x => x.Id).Max();
                        if (id != null && lastId != id)
                        {
                            lastId = (long)id;
                            Observable.Timer(TimeSpan.FromMilliseconds(0), TimeSpan.FromSeconds((int)65 / status.Length))
                            .TakeWhile(x => x < status.Length)
                            .ObserveOn(SynchronizationContext.Current)
                            .Subscribe(x =>
                            {
                                var al = new AlcedoWindowViewModel(status[x]);
                                var window = new AlcedoWindow(al);
                                window.Show();
                            });
                            param["since_id"] = lastId;
                        }
                        using var sw = new StreamWriter($@"{PATH}/log_{now.Year}_{now.Month}_{now.Day}.log", true, Encoding.UTF8);
                        foreach (var wId in status.Select(x => x.Id).ToArray()) await sw.WriteLineAsync(wId.ToString());
                    }
                    catch (Exception ex)
                    {
                        using var sw = new StreamWriter($@"{PATH}/log_{now.Year}_{now.Month}_{now.Day}.log", true, Encoding.UTF8);
                        await sw.WriteLineAsync($"{now.Hour}:{now.Minute}:{now.Second}.{now.Millisecond} => {ex.Message}");
                    }
                });
        }

        [MessagePackObject]
        public class KeyModel
        {
            [Key(0)]
            public string AccessToken { get; set; }

            [Key(1)]
            public string AccessSecret { get; set; }

            public KeyModel() { }
            public KeyModel(string accessToken, string accessSecret)
            {
                AccessToken = accessToken;
                AccessSecret = accessSecret;
            }
        }
    }
}