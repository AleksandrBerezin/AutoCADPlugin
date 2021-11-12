namespace Gear
{
    /// <summary>
    /// Интерфейс работы с главным окном
    /// </summary>
    public interface IWindowService
    {
        /// <summary>
        /// Открыть главное окно
        /// </summary>
        /// <returns></returns>
        bool? ShowDialog();
    }
}