using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Jarvis.Tests
{
    [TestClass]
    public class JarvisTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public async Task TestToCommand()
        {
            string commandReceived = null;
            Jarvis.Service.Events.Instance.NewCommandReceived += (e, o) =>
            {
                commandReceived = o.Command.Action;
            };
            await Jarvis.Service.JarvisService.ToCommand("go", "");
            Assert.AreEqual(commandReceived, "go");
        }

        [TestMethod]
        public async Task TestToTranslateResponse()
        {
            string commandReceived = null, expectedResponse = "go to yaba";
            Jarvis.Service.Events.Instance.NewMessageAvailable += (e, o) =>
            {
                commandReceived = o.Content;
            };
            await Jarvis.Service.JarvisService.TranslateResponse(new Jarvis.Service.Impl.JarvisResponse()
            {
                Message = expectedResponse,
                StatusCode = Jarvis.Service.ResponseCodes.SUCCESS
            });
            Assert.IsNotNull(commandReceived);
            TestContext.WriteLine(commandReceived);
        }

        [TestMethod]
        public void TestStringifyArray()
        {
            String str = Jarvis.Service.JarvisService.StringifyArray(new string[] { "A", "B", "C" });
            Assert.AreEqual(str, "A, B and C");
        }
    }
}
