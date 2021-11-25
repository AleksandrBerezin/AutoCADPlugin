using System.ComponentModel;
using System.Linq;

namespace Core
{
    /// <summary>
    /// Класс, расширяющий возможности перечисление <see cref="ParametersEnum"/>
    /// </summary>
    public static class ParametersEnumExtension
    {
        /// <summary>
        /// Возвращает текстовое описание параметра
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static string GetDescription(this ParametersEnum parameter)
        {
            //TODO: Description к значениям перечисления
            var fieldInfo = parameter.GetType().GetField(parameter.ToString());
            var description = parameter.ToString();

            if (fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) is 
                DescriptionAttribute[] attributes && attributes.Any())
            {
                description = attributes.First().Description;
            }

            return description;
        }
    }
}
