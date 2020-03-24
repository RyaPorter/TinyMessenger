using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TinyMessenger.Tests
{
    [TestClass]
    public class TinyMessengerTests
    {
        [TestMethod]
        public void ListenAndReceiveMessage()
        {

            TinyMessenger messenger = new TinyMessenger();

            string state = string.Empty;

            messenger.Subscribe<StatusMessage>("global", (m) =>
            {
                state = m.State;
            });

            messenger.Send("global", new StatusMessage() { State = "sent" });


            Assert.AreEqual("sent", state);


        }

        [TestMethod]
        public void ListenAndReceiveMessageTwoChannels()

        {

            TinyMessenger messenger = new TinyMessenger();

            string state = string.Empty;

            messenger.Subscribe<StatusMessage>("global", (m) =>
            {
                state = m.State;
            });

            messenger.Subscribe<StatusMessage>("other", (m) =>
            {
                state = m.State;
            });

            messenger.Send("global", new StatusMessage() { State = "sent" });
            Assert.AreEqual("sent", state);

            messenger.Send("global", new StatusMessage() { State = "sent_other" });
            Assert.AreEqual("sent_other", state);
        }
    }
}
