using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TweRiver.Interfaces
{
    public interface IAuthModel
    {
        public ReactiveProperty<string> Text { get; }

        public string AuthURI { get;}

        Task<bool> AuthorizeAsync();
    }
}
