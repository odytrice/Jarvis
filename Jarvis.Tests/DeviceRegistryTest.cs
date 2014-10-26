using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jarvis.Core.Device;
using System.Linq;


namespace Jarvis.Tests
{
    [TestClass]
    public class DeviceRegistryTest
    {
        [TestMethod]
        public void TestStore()
        {
            var t = Jarvis.Core.Device.DeviceRegistry.Instance.Store("1", new Jarvis.Core.Device.Device[] {
                new Jarvis.Core.Device.Device() {
                    IdTags = new Tag[] {"t1"}
                }
            });
            Assert.AreEqual(t, 1);
        }

        [TestMethod]
        public void TestInstance()
        {
            var id = Jarvis.Core.Device.DeviceRegistry.Instance;
            Assert.AreSame(id, Jarvis.Core.Device.DeviceRegistry.Instance);
        }

        [TestMethod]
        public void TestFetch()
        {
            Jarvis.Core.Device.DeviceRegistry.Instance.Store("1", new Jarvis.Core.Device.Device[] {
                new Jarvis.Core.Device.Device() {
                    IdTags = new Tag[] {"t1"}
                }
            });
            Assert.AreEqual(Jarvis.Core.Device.DeviceRegistry.Instance.Fetch("id").Count(), 1);
        }
    }
}
