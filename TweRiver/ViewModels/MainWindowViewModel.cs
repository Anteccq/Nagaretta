using Prism.Mvvm;
using TweRiver.Models;

namespace TweRiver.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "tween";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public TweetModel Model { get; } = new TweetModel();
    }
}
