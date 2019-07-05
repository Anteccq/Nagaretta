using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TweRiver.Models;
using TweRiver.ViewModels;

namespace TweRiver.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += async (a,e) => await (this.DataContext as MainWindowViewModel).Model.InitializeAsync();
            this.Closing += (a, e) =>{ foreach (var al in AlcedoWindow.WindowList)
                {
                    al.Close();
                }
                AlcedoWindow.WindowList.Clear();
            };
        }
    }
}
