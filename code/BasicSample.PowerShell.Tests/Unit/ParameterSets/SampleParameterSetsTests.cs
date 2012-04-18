namespace BasicSample.PowerShell.Tests.Unit.ParameterSets
{
    using System.Collections.ObjectModel;
    using System.Management.Automation;
    using BasicSample.DomainServices;
    using BasicSample.DomainServices.Interfaces;
    using BasicSample.PowerShell;
    using BasicSample.PowerShell.Domain;
    using BasicSample.PowerShell.Infrastructure;
    using BasicSample.PowerShell.Tests.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class SampleParameterSetsTests
    {
        [TestMethod]
        public void RunningGetAttributeWithFilePathShouldResolveTo_defaultParameterSet_()
        {
            var entFactory = new Mock<ServicesAbstractFactory>();
            ServicesAbstractFactory.Instance = entFactory.Object;

            var attribReader = new Mock<IAttributeReader>();

            entFactory.Setup(f => f.GetAttributeReader()).Returns(attribReader.Object);
            attribReader.Setup(fs => fs.GetAttributeDataByFilePath(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new AttributeData())
                .Verifiable();

            var helpersFactory = new Mock<HelpersAbstractFactory>();
            HelpersAbstractFactory.Instance = helpersFactory.Object;

            var logger = new Mock<ICmdletLogger>();

            helpersFactory.Setup(f => f.GetLogger()).Returns(logger.Object);

            logger.Setup(l => l.LogParameterSetResolvedEvent("defaultParameterSet", It.IsAny<PSCmdlet>())).Verifiable();

            Collection<PSObject> result = CmdletTestingHelper.RunCmdlet("Get-Attribute", "-File c:\\test.txt -Attribute Size", typeof(GetAttributeCmdlet));

            Assert.IsNotNull(result);
            logger.Verify();            
        }
    }
}
