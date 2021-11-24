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
            switch (parameter)
            {
                case ParametersEnum.GearDiameter:
                {
                    return "Gear Diameter \"D\":";
                }
                case ParametersEnum.HoleDiameter:
                {
                    return "Hole Diameter \"d\":";
                }
                case ParametersEnum.Height:
                {
                    return "Height \"H\":";
                }
                case ParametersEnum.ToothLength:
                {
                    return "Tooth Length \"A\":";
                }
                case ParametersEnum.ToothWidth:
                {
                    return "Tooth Width \"B\":";
                }
                case ParametersEnum.TeethCount:
                {
                    return "Teeth Count:";
                }
                default:
                {
                    return "Parameter";
                }
            }
        }
    }
}
