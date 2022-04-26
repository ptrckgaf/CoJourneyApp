using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoJourney.App.Messages;

namespace CoJourney.App.Services
{
    public interface IMediator
    {
        public void Register<TMessage>(Action<TMessage> action)
            where TMessage : IMessage;
        public void UnRegister<TMessage>(Action<TMessage> action)
            where TMessage : IMessage;
        public void Send<TMessage>(TMessage message)
            where TMessage : IMessage;
    }
}
