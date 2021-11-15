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
        ///// <summary>
        ///// Поле для сервиса главного окна
        ///// </summary>
        //private IWindowService _windowService;

        ///// <summary>
        ///// Создает экземпляр класса <see cref="AutoCADConnector"/>
        ///// </summary>
        ///// <param name="windowService"></param>
        //public AutoCADConnector(IWindowService windowService)
        //{
        //    _windowService = new WindowService();
        //}

        [CommandMethod("StartGearPlugin")]
        public static async void StartGearPlugin()
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
