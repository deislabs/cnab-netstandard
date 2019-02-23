using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cnab;

namespace Cnab.Tests
{
    [TestClass()]
    public class BundleTests
    {
        [TestMethod]
        public async Task TestLoadUnsigned()
        {
            var bundle = await Bundle.LoadUnsignedAsync("../../../../examples/bundles/thin-bundle.json");

            Assert.AreEqual("helloworld", bundle.Name);
            Assert.AreEqual("0.1.2", bundle.Version);
            Assert.AreEqual("An example 'thin' helloworld Cloud-Native Application Bundle", bundle.Description);

            Assert.AreEqual("Matt Butcher", bundle.Maintainers[0].Name);
            Assert.AreEqual("technosophos@gmail.com", bundle.Maintainers[0].Email);
            Assert.AreEqual("https://example.com", bundle.Maintainers[0].Url);

            Assert.AreEqual("docker", bundle.InvocationImages[0].ImageType);
            Assert.AreEqual("technosophos/helloworld:0.1.0", bundle.InvocationImages[0].Image);
            Assert.AreEqual("sha256:aaaaaaa...", bundle.InvocationImages[0].Digest);

            Assert.AreEqual("technosophos/microservice:1.2.3", bundle.Images["my-microservice"].Image);
            Assert.AreEqual("my microservice", bundle.Images["my-microservice"].Description);
            Assert.AreEqual("sha256:aaaaaaaaaaaa...", bundle.Images["my-microservice"].Digest);
            // TODO - see https://github.com/deislabs/cnab-spec/issues/89
            // Assert.AreEqual("urn:image1uri", bundle.Images["my-microservice"].Uri);
            Assert.AreEqual("image1path", bundle.Images["my-microservice"].LocationReferences[0].Path);
            Assert.AreEqual("image.1.field", bundle.Images["my-microservice"].LocationReferences[0].Field);

            Assert.AreEqual("int", bundle.Parameters["backend_port"].DataType);
            Assert.AreEqual("The port that the back-end will listen on", bundle.Parameters["backend_port"].Metadata.Description);
            Assert.AreEqual(80, (bundle.Parameters["backend_port"] as IntParameter).DefaultValue);
            Assert.AreEqual(10, (bundle.Parameters["backend_port"] as IntParameter).MinimumValue);
            Assert.AreEqual(10240, (bundle.Parameters["backend_port"] as IntParameter).MaximumValue);

            Assert.AreEqual("/home/.kube/config", bundle.Credentials["kubeconfig"].Path);
            Assert.AreEqual("AZ_IMAGE_TOKEN", bundle.Credentials["image_token"].EnvironmentVariable);
            Assert.AreEqual("/etc/hostkey.txt", bundle.Credentials["hostkey"].Path);
            Assert.AreEqual("HOST_KEY", bundle.Credentials["hostkey"].EnvironmentVariable);
        }
    }
}