using Microsoft.VisualStudio.TestTools.UnitTesting;
using TinyMessenger;

namespace TinyMessenger.Tests
{
    [TestClass]
    public class TinyMessengerTests
    {
        [TestMethod]
        public void ListenAndRecieveMessage()
        {

                TinyMessenger messenger = new TinyMessenger();

                string state = string.Empty;

                messenger.Listen<StatusMessage>((m) => {
                    state = m.State;
                });


                messenger.Send(new StatusMessage(){State = "sent"});


                Assert.AreEqual("sent", state);


        }
    }
}
