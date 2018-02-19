using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClipboardCompanionTest
{
    [TestClass]
    public class DependencyRegistration
    {
        [TestMethod]
        public void Register()
        {
            var container = ClipboardCompanion.DependencyRegistration.Register();
            container.Verify();
        }
    }
}
