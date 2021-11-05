using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace Gear
{
    public class AutoCADConnector : IExtensionApplication
    {
        [CommandMethod("WindowCommand")]
        public static void OpenWindow()
        {
            // Get the current document and database, and start a transaction
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                var window = new MainWindow();
                window.Show();
                // Save the new objects to the database
                acTrans.Commit();
            }
        }

        public void Initialize()
        {
        }

        public void Terminate()
        {
        }
    }
}
