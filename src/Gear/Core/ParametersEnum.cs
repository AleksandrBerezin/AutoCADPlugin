using System.ComponentModel;

namespace Core
{
    /// <summary>
    /// Параметры шестерни
    /// </summary>
    public enum ParametersEnum
    {
        [Description("Gear Diameter \"D\":")]
        GearDiameter,
        [Description("Hole Diameter \"d\":")]
        HoleDiameter,
        [Description("Height \"H\":")]
        Height,
        [Description("Tooth Length \"A\":")]
        ToothLength,
        [Description("Tooth Width \"B\":")]
        ToothWidth
    }
}