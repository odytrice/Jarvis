using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Jarvis.Core.Device;

namespace Jarvis.Tests
{
    [TestClass]
    public class CommandPipelineTest
    {
        public TestContext TestContext { get; set; }
        private class DummyDevice : Device 
        {
            public DummyDevice()
            {
                this.Id = "xxxx";
                this.IdTags = new Tag[] {"dummy", "test"};
                this.Properties = new DeviceProperty[] {
                    new DeviceProperty() {
                        Id = "x",
                        IdTags = new Tag[] {
                            "turn", "go", "check"
                        },
                        MutatorTags = new Tag[] {
                           "check" ,"state"
                        }
                    },

                    new DeviceProperty() {
                        Id = "y",
                        IdTags = new Tag[] {
                            "take", "fish", "check"
                        },
                        MutatorTags = new Tag[] {
                           "check", "go", "come", "see", "state"
                        }
                    }
                };
            }

            public object Perform(string attribute, object arg)
            {
                return arg;
            }
        }
        [TestMethod]
        public async Task TestProcess()
        {
            string attributeCalled = null;

            DummyDevice device = new DummyDevice();
            Jarvis.Service.Events.Instance.NewCommandReceived += (o, e) =>
            {
                Array.ForEach(e.Command.Properties, p =>
                {
                    TestContext.WriteLine("Property called: {0}={1}", p.Name, p.Value);
                    device.Perform(p.Name, p.Value);
                });
            };

            Jarvis.Service.Events.Instance.NewResponseReceived += (o, e) =>
            {
                   
            };
            await CommandPipeline.Instance.Process("check dummy state check go", new Device[] { device });
        }
    }
}
