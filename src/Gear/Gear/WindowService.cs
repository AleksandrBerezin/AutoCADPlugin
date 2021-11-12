namespace Gear
{
    /// <summary>
    /// Сервис для работы с главным окном
    /// </summary>
    public class WindowService : IWindowService
    {
        /// <inheritdoc/>
        public bool? ShowDialog()
        {
            return new MainWindow().ShowDialog();
        }
    }
}