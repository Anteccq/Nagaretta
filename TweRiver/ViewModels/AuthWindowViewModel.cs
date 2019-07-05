using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoreTweet;
using Reactive;
using Prism.Mvvm;
using Reactive.Bindings;
using TweRiver.Interfaces;
using TweRiver.Models;

namespace TweRiver.ViewModels
{
    public class AuthWindowViewModel : BindableBase
    {
        public IAuthModel Model { get; private set; }

        public AuthWindowViewModel(IAuthModel model) => this.Model = model;
    }
}
