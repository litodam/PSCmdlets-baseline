namespace BasicSample.PowerShell
{
    using System.ComponentModel;
    using System.Management.Automation;

    [RunInstaller(true)]
    public class BasicSampleSnapIn : PSSnapIn
    {
        /// <summary>
        /// Gets the snap-in description.
        /// </summary>
        public override string Description
        {
            get { return "Basic PowerShell Cmdlets Sample"; }
        }

        /// <summary>
        /// Gets the snap-in name.
        /// </summary>
        public override string Name
        {
            get { return "BasicSample"; }
        }

        /// <summary>
        /// Gets the snap-in vendor.
        /// </summary>
        public override string Vendor
        {
            get { return "litodam"; }
        }
    }
}