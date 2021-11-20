using Autodesk.AutoCAD.ApplicationServices;
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
            Application.ShowModelessWindow(new MainWindow());
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
