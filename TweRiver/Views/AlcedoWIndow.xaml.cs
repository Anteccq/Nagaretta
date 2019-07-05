using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reactive.Linq;
using System.Reactive;
using System.Threading;
using TweRiver.ViewModels;

namespace TweRiver.Views
{
    /// <summary>
    /// AlcedoWIndow.xaml の相互作用ロジック
    /// </summary>
    public partial class AlcedoWindow : Window
    {
        public static List<AlcedoWindow> WindowList { get; } = new List<AlcedoWindow>();

        public AlcedoWindow(AlcedoWindowViewModel vm)
        {
            WindowList.Add(this);
            this.DataContext = vm;
            InitializeComponent();
            this.Left = SystemParameters.VirtualScreenWidth;
            var vsh = (int)SystemParameters.WorkArea.Height - 100;
            this.Top = App.random.Next(vsh);
            Observable.Interval(TimeSpan.FromMilliseconds(1))
                .ObserveOn(SynchronizationContext.Current)
                .TakeUntil(_ => this.Left < -this.Width)
                .Subscribe(_ => this.Left -= 2.5, () => this.Close());
        }
    }
}
