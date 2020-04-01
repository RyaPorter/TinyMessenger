using System.Diagnostics;
using System.Threading.Tasks;
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
        public void RecieveImplcitCallback()
        {

            TinyMessenger messenger = new TinyMessenger();

            string state = string.Empty;

            bool callbackCalled = false;

            messenger.Subscribe<StatusMessage>("global", (m) =>
                {
                    state = m.State;
                });

            messenger.Send("global",
            new StatusMessage() { State = "sent" },
            (h, b) =>
            {
                callbackCalled = true;
                Assert.AreEqual(true, b);
            }
            );

            if (!callbackCalled)
            {
                Assert.Fail();
            }
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

            messenger.Send("other", new StatusMessage() { State = "sent_other" });
            Assert.AreEqual("sent_other", state);
        }

        [TestMethod]
        public void RecieveCallback()
        {

            TinyMessenger messenger = new TinyMessenger();

            string state = string.Empty;
            bool callbackCalled = false;

            messenger.Subscribe<StatusMessage>("global", async (c, m) =>
                {
                    m.State = "Sent from callback";
                    await c.RaiseCallbackAsync(m, true);

                    if (!callbackCalled)
                    {
                        Assert.Fail();
                    }
                });

            Task.Run(async () =>
            {
                await messenger.SendAsync("global",
                new StatusMessage() { State = "sent" },
                (a, b) =>
                {
                    callbackCalled = true;
                    Assert.AreEqual("Sent from callback", a.State);

                });
            });
        }
    }
}
