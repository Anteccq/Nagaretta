using Prism.Ioc;
using TweRiver.Views;
using System.Windows;
using System.ComponentModel;
using System.IO;
using TweRiver.Models;
using System;

namespace TweRiver
{
    /// <summary>   
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static Random random = new Random((int)DateTime.Now.ToBinary());

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        private void PrismApplication_Startup(object sender, StartupEventArgs e)
        {
            if (!Directory.Exists(TweetModel.PATH)) Directory.CreateDirectory(TweetModel.PATH);
        }
    }
}