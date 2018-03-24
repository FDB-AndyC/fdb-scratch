using Outlook = Microsoft.Office.Interop.Outlook;
namespace RecurringSlotFinder
{
    using OfficeCore = Microsoft.Office.Core;

    public partial class ThisAddIn
    {
        private Outlook.Inspectors inspectors = null;

        protected override OfficeCore.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            return new SlotFinder();
        }

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            inspectors = this.Application.Inspectors;
            inspectors.NewInspector += NewInspectorEventHandler;

        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see https://go.microsoft.com/fwlink/?LinkId=506785
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion

        private void NewInspectorEventHandler(Outlook.Inspector inspector)
        {
        }
    }
}
