using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cnab.Runtime;
namespace Cnab.Runtime.Tests
{
    [TestClass()]
    public class RuntimeTests
    {
        [TestMethod]
        public void TestNewClaim()
        {
            var c = new Claim("myClaim");

            Assert.AreEqual("myClaim", c.Name);
            Assert.AreEqual("unknown", c.Result.Action);
            Assert.AreEqual("unknown", c.Result.Status);
        }

        [TestMethod]
        public void TestUpdateClaim()
        {
            var c = new Claim("myClaim");
            var m = c.Modified;
            var r = c.Revision;

            c.Update(Action.Install, Status.Success);

            Assert.AreNotEqual(m, c.Modified);
            Assert.AreNotEqual(r, c.Revision);
            Assert.AreEqual(Action.Install, c.Result.Action);
            Assert.AreEqual(Status.Success, c.Result.Status);
        }
    }
}