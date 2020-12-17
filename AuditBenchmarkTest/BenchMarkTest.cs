using AuditBenchmark.Controllers;
using AuditBenchmark.Models;
using AuditBenchmark.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;

namespace AuditBenchmarkTesting
{
    [TestFixture]
    public class Tests
    {
        private Mock<IAuditBenchmarkProvider> _provider;
        private AuditBenchmarkController controllerObj;
        [SetUp]
        public void Setup()
        {

            _provider = new Mock<IAuditBenchmarkProvider>();
            controllerObj = new AuditBenchmarkController(_provider.Object);
        }
        [Test]
        public void ReturnForInternalAuditTypeTest()
        {
            
            
            _provider.Setup(p => p.GetAll()).Returns(new List<AuditBenchmark.Models.AuditBenchmark>{new AuditBenchmark.Models.AuditBenchmark(){AuditType = "Internal", BenchmarkNoAnswers = 3}});
            var result = controllerObj.Get() as ObjectResult;
            Assert.AreEqual(200, result.StatusCode);
            
        }

        [Test]
        public void ReturnForSOXAuditTypeTest()
        {
            _provider.Setup(p => p.GetAll()).Returns(new List<AuditBenchmark.Models.AuditBenchmark>{new AuditBenchmark.Models.AuditBenchmark()
            {
                    AuditType = "SOX", BenchmarkNoAnswers = 1
            }
            });
            var result = controllerObj.Get();

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }
        [Test]
        public void ReturnNullTest()
        {
            _provider.Setup(x => x.GetAll()).Returns((List<AuditBenchmark.Models.AuditBenchmark>)null);
            var result = controllerObj.Get() as ObjectResult;
            Assert.AreEqual(404, result.StatusCode);
            Assert.AreEqual("BenchMark does not exist", result.Value);
        }
        [Test]
        public void ExceptionTest()
        {
            _provider.Setup(x => x.GetAll()).Throws(new Exception());
            var result = controllerObj.Get() as ObjectResult;
            Assert.AreEqual(500, result.StatusCode);
            Assert.AreEqual("Unexpected error occured", result.Value);
        }
    }
}