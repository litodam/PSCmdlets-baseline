namespace BasicSample.PowerShell.Tests.Unit.Interaction
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Management.Automation;
    using BasicSample.DomainServices;
    using BasicSample.DomainServices.Interfaces;
    using BasicSample.PowerShell;
    using BasicSample.PowerShell.Domain;
    using BasicSample.PowerShell.Tests.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class SampleInteractionTests
    {
        [TestMethod]
        public void RunningGetFileWithFilePathShouldCallGetFileDataByFilePath()
        {
            var factory = new Mock<ServicesAbstractFactory>();

            ServicesAbstractFactory.Instance = factory.Object;

            var fileFinder = new Mock<IFileFinder>();

            factory.Setup(f => f.GetFileFinder()).Returns(fileFinder.Object);
            fileFinder.Setup(fs => fs.GetFileDataByFilePath("c:\\test.txt"))
                .Returns(new List<IFileData> { new FileData { FileNfo = new FileInfo("c:\\test.txt") } })
                .Verifiable();

            Collection<PSObject> result = CmdletTestingHelper.RunCmdlet("Get-File", "-File c:\\test.txt", typeof(GetFileCmdlet));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
            Assert.IsInstanceOfType(result[0].BaseObject, typeof(GetFileResult));
            Assert.AreEqual(((GetFileResult)result[0].BaseObject).FileNfo.FullName, "c:\\test.txt");

            fileFinder.Verify();
        }
    }
}
