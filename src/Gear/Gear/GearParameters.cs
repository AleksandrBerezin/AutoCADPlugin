using System;

namespace Gear
{
    public class GearParameters
    {
        private double _gearDiameter;

        public double GearDiameter
        {
            get => _gearDiameter;
            set
            {
                const double min = 24;
                const double max = 60;

                if (value >= min && value <= max)
                {
                    _gearDiameter = value;
                }
                else
                {
                    throw new ArgumentException($"Parameter {nameof(GearDiameter) }" +
                                                $"should be more then {min} " +
                                                $"and less then {max}.");
                }
            }
        }

        public double HoleDiameter
        {
            get;
            set;
        }

        public double Height
        {
            get;
            set;
        }

        public double ToothLength
        {
            get;
            set;
        }

        public double ToothWidth
        {
            get;
            set;
        }
    }
}
