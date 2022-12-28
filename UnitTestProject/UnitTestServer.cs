using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Server.DBCRUD;
using System;
using System.Collections.Generic;
using Assert = NUnit.Framework.Assert;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestServer 
    {
        private Server.MerenjeService serverService;
        private Mock<IDBCRUD> mockCrud;

        public UnitTestServer()
        {
            SetUp();
        }

        public void SetUp()
        {
            mockCrud = new Mock<IDBCRUD>();
            serverService = new Server.MerenjeService(mockCrud.Object);
        }

        [TestMethod]
        public void getTestById()
        {
            int id = 1;
            mockCrud.Setup(x => x.GetMerenjeByDbId(id)).Returns(new Server.Merenje()
            {
                idDb = id,
                idDevice = 2,
                idMerenja = 3,
                timestamp = 12345,
                tip = 0,
                vrednost = 12.25
            });

            var res = serverService.GetMerenjeByDbId(id);

            Assert.NotNull(res);
            Assert.AreEqual(res.IdDb, id);
            Assert.AreEqual(res.IdDevice, 2);
            Assert.AreEqual(res.IdMerenja, 3);
            Assert.AreEqual(res.Timestamp, 12345);
            Assert.AreEqual(res.Tip, (Models.MerenjeTip)0);
            Assert.AreEqual(res.Vrednost, 12.25);

            mockCrud.Setup(x => x.GetMerenjeByDbId(id)).Returns((Server.Merenje)null);
            Assert.Throws<Exception>(() => { serverService.GetMerenjeByDbId(1);  }, "Merenje not found");
        }
    }
}
