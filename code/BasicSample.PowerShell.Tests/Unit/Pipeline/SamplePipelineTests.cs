namespace BasicSample.PowerShell.Tests.Unit.Pipeline
{
    using System;
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
    public class SamplePipelineTests
    {
        [TestMethod]
        public void OutputOfGetFileShouldPassToGetAttributeThroughPipeline()
        {
            var factory = new Mock<ServicesAbstractFactory>();

            ServicesAbstractFactory.Instance = factory.Object;

            var fileFinder = new Mock<IFileFinder>();
            var attribReader = new Mock<IAttributeReader>();

            factory.Setup(f => f.GetFileFinder()).Returns(fileFinder.Object);
            factory.Setup(f => f.GetAttributeReader()).Returns(attribReader.Object);

            var getFileOutput = new List<IFileData> { new FileData { FileNfo = new FileInfo("c:\\test.txt") } };

            fileFinder.Setup(fs => fs.GetFileDataByFolderAndPattern(It.IsAny<string>(), "*"))
                .Returns(getFileOutput)
                .Verifiable();

            attribReader.Setup(ar => ar.GetAttributeDataByFileInfo(getFileOutput[0].FileNfo, "Size"))
                .Returns(new AttributeData())
                .Verifiable();

            const string Command = "Get-File -Folder c:\\ -Pattern * | Get-Attribute -Attribute Size";

            var cmdlets = new Dictionary<string, Type>()
            {
                { "Get-File", typeof(GetFileCmdlet) },
                { "Get-Attribute", typeof(GetAttributeCmdlet) }
            };

            Collection<PSObject> result = CmdletTestingHelper.RunCmdlet(cmdlets, Command);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);

            fileFinder.Verify();
            attribReader.Verify();
        }
    }
}
