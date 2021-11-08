namespace Gear
{
    public static class ParametersEnumExtension
    {
        public static string GetDescription(this ParametersEnum parameter)
        {
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
                default:
                {
                    return "Parameter";
                }
            }
        }
    }
}
