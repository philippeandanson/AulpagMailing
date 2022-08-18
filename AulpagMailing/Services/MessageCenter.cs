using AulpagMailing.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulpagMailing.Services
{

    public interface IListen { }
    public interface IListen<T> : IListen
    {
        void Handle(T obj);
    }

    public class EventAggregator
    {
        private List<IListen> subscribers = new List<IListen>();

        public void Subscribe(IListen model)
        {
            this.subscribers.Add(model);
        }

        public void Unsubscribe(IListen model)
        {
            this.subscribers.Remove(model);
        }

        public void Publish<T>(T message)
        {
            foreach (var item in this.subscribers.OfType<IListen<T>>())
            {
                item.Handle(message);
            }
        }
    }

    public class Guard
    {
        private EventAggregator eventAggregator;

        public Guard(EventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public void SignalCars()
        {
            this.eventAggregator.Publish(new SignalMessage { Message = "Stop" });
        }
    }

    public class SignalMessage
    {
        public string Message { get; set; }
    }

}
