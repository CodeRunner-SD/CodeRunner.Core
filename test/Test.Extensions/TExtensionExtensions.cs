using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Extensions
{
    [TestClass]
    public class TExtensionExtensions
    {
        [TestMethod]
        public void FullName()
        {
            (string a, string b, string c) = CodeRunner.Extensions.ExtensionExtensions.SplitFullName("pub.ext::sub");
            Assert.AreEqual("pub", a);
            Assert.AreEqual("ext", b);
            Assert.AreEqual("sub", c);
        }
    }
}
