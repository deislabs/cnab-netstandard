using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Cnab.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Cnab.Bundle.Tests
{
    [TestClass()]
    public class ParameterTests
    {
        private string _paramsJson = "../../../testdata/params.json";

        [TestMethod]
        public async Task TestBundleContainsParameters()
        {
            var b = await Bundle.LoadUnsignedAsync(_paramsJson);

            Assert.AreEqual(true, b.Parameters.ContainsKey("backend_port"));
            Assert.AreEqual(true, b.Parameters.ContainsKey("greeting"));
            Assert.AreEqual(true, b.Parameters.ContainsKey("isAdmin"));
        }

        [TestMethod]
        public async Task TestIntParameter()
        {
            var b = await Bundle.LoadUnsignedAsync(_paramsJson);
            var backendPort = b.Parameters["backend_port"];

            Assert.AreEqual("int", backendPort.DataType);
            Assert.AreEqual("this will be $CNAB_P_PORT", backendPort.Metadata.Description);
            Assert.AreEqual("BACKEND_PORT", backendPort.Destination.EnvironmentVariable);
            Assert.IsInstanceOfType(backendPort, typeof(IntParameter));
            Assert.AreEqual(8080, (backendPort as IntParameter).DefaultValue);
            Assert.AreEqual(10, (backendPort as IntParameter).MinimumValue);
            Assert.AreEqual(10240, (backendPort as IntParameter).MaximumValue);
            Assert.AreEqual(true, TestUtil.EqualsAll((backendPort as IntParameter).AllowedValues, new List<int>(){80, 8000, 8080}));
        }

        [TestMethod]
        public async Task TestStringParameter()
        {
            var b = await Bundle.LoadUnsignedAsync(_paramsJson);
            var greeting = b.Parameters["greeting"];

            Assert.AreEqual("string", greeting.DataType);
            Assert.AreEqual("this will be in $GREETING", greeting.Metadata.Description);
            Assert.AreEqual("GREETING", greeting.Destination.EnvironmentVariable);
            Assert.IsInstanceOfType(greeting, typeof(StringParameter));
            Assert.AreEqual("hello", (greeting as StringParameter).DefaultValue);
            Assert.AreEqual(1, (greeting as StringParameter).MinimumLength);
            Assert.AreEqual(46, (greeting as StringParameter).MaximumLength);
            Assert.AreEqual(true, TestUtil.EqualsAll((greeting as StringParameter).AllowedValues, new List<string>(){"hello", "goodbye", "gday"}));
        }

        [TestMethod]
        public async Task TestBoolParameter()
        {
            var b = await Bundle.LoadUnsignedAsync(_paramsJson);
            var isAdmin = b.Parameters["isAdmin"];

            Assert.AreEqual("bool", isAdmin.DataType);
            Assert.AreEqual("this will be located in a file", isAdmin.Metadata.Description);
            Assert.AreEqual("/opt/example-parameters/config.txt", isAdmin.Destination.Path);
            Assert.IsInstanceOfType(isAdmin, typeof(BoolParameter));
            Assert.AreEqual(false, (isAdmin as BoolParameter).DefaultValue);
        }
    }
}