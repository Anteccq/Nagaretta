using CoreTweet;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using TweRiver.Models;

namespace TweRiver.ViewModels
{
    public class AlcedoWindowViewModel : BindableBase
    {
        public AlcedoModel Model { get; set; }
        public static double FontSize { get; private set; } = 50;

        public AlcedoWindowViewModel(Status status)
        {
            Model = new AlcedoModel(status);
        }
    }
}