using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
		public enum VerticalAlignments : byte
		{
			// Token: 0x0400019B RID: 411
			Default = 2,
			// Token: 0x0400019C RID: 412
			Top = 0,
			// Token: 0x0400019D RID: 413
			Centered,
			// Token: 0x0400019E RID: 414
			Bottom,
			// Token: 0x0400019F RID: 415
			Justified,
			// Token: 0x040001A0 RID: 416
			Distributed
		}

		[TestMethod]
        public void TestMethod1()
        {
			var a = (VerticalAlignments)16;

			var b = (byte)a;

			Assert.IsTrue(true);
		}
    }
}
