using TinyMessenger;
using System;

namespace DriverApp
{

    public class StatusMessage
    {
        public int status { get; set; }
    }

    public class AppClass
    {
        public AppClass(TinyMessenger.TinyMessenger messenger)
        {
            this.Messenger = messenger;
        }

        public TinyMessenger.TinyMessenger Messenger { get; set; }


        public void SendMessage()
        {
            Messenger.Send<StatusMessage>(new StatusMessage() { status = 1 });
        }

    }

    public class App
    {


        public void main()
        {

            TinyMessenger.TinyMessenger messenger = new TinyMessenger.TinyMessenger();

            messenger.Listen<StatusMessage>((m) =>
            {
                Console.WriteLine(m.status);
            });


            AppClass cls = new AppClass(messenger);

            cls.SendMessage();
        }
    }
}