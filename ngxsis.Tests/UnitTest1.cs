using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ngxsis.DataModel;
using System.Linq;

namespace ngxsis.Tests
{
    [TestClass] 
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            using (var db = new ngxsisContext())
            {
                object Listrole = db.x_role.ToList();
            }

        }
    }
}
