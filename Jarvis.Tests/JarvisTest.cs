using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Jarvis.Tests
{
    [TestClass]
    public class JarvisTest
    {
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
            Jarvis.Service.Events.Instance.NewResponseReceived += (e, o) =>
            {
                commandReceived = o.Response.Message;
            };
            await Jarvis.Service.JarvisService.TranslateResponse(new Jarvis.Service.Impl.JarvisResponse()
            {
                Message = expectedResponse
            });
            Assert.AreEqual(commandReceived, expectedResponse);
        }
    }
}
