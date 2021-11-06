using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Gear;

namespace AutoCADConnector
{
    /// <summary>
    /// Класс для связи AutoCAD с плагином
    /// </summary>
    public class AutoCADConnector : IExtensionApplication
    {
        [CommandMethod("StartGearPlugin")]
        public static void StartGearPlugin()
        {
            // Get the current document and database, and start a transaction
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                var window = new MainWindow();
                //window.Show();
                Application.ShowModalWindow(window);
                // Save the new objects to the database
                acTrans.Commit();
            }
        }

        /// <inheritdoc/>
        public void Initialize()
        {
        }

        /// <inheritdoc/>
        public void Terminate()
        {
        }
    }
}
