using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TweRiver.Interfaces;
using TweRiver.ViewModels;
using System.Diagnostics;
using TweRiver.Utilities;
using System.Reactive.Concurrency;
using System.Threading;

namespace TweRiver.Views
{
    /// <summary>
    /// AuthWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow(AuthWindowViewModel vm)
        {
            this.DataContext = vm;
            InitializeComponent();
            ProcessUtil.StartUrl(vm.Model.AuthURI);
            var stream = Observable.FromEventPattern<KeyEventArgs>(Code, "KeyDown")
                .Where(x => x.EventArgs.Key == Key.Enter && Code.Text.Length == 7)
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(async _ =>
                {
                    NotifText.Text = "Connect...";
                    var isAuth = await vm.Model.AuthorizeAsync();
                    if (isAuth) { this.Close(); NotifText.Text = "Success"; }
                    else NotifText.Text = "Failed (´・ω・｀)";
                });
            this.Closing += (a, e) => stream.Dispose();
        }
    }
}