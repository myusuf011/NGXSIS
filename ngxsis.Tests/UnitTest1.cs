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
                x_role role = new x_role();
                for (int i = 0; i < 103; i++)
                {
                    role.code = "ROLE_KODE_" + i.ToString();
                    role.name = "NAMA " + i.ToString();
                    role.created_by = 1;
                    role.created_on = DateTime.Now;
                    role.is_deleted = false;
                    db.x_role.Add(role);
                    db.SaveChanges();
                }
            }
        }
    }
}
