using System.Collections.ObjectModel;
using System.Linq;

namespace Core
{
    /// <summary>
    /// Класс для хранения списка параметров шестерни
    /// </summary>
    public class GearParametersList : ObservableCollection<GearParameter>
    {
        /// <summary>
        /// Создает экземпляр класса <see cref="GearParametersList"/>
        /// </summary>
        public GearParametersList()
        {
            SetDefault();
        }

        /// <summary>
        /// Индексатор, позволяющий получать элементы списка по значению <see cref="ParametersEnum"/>
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public GearParameter this[ParametersEnum index]
        {
            get => this.First(parameter => parameter.Name == index);
            set
            {
                var oldParameter = this.First(parameter => parameter.Name == index);
                var i = this.IndexOf(oldParameter);
                this[i] = value;
            }
        }

        /// <summary>
        /// Задает список параметров со значениями по умолчанию
        /// </summary>
        public void SetDefault()
        {
            Clear();
            Add(new GearParameter(ParametersEnum.GearDiameter, 24, 60, 40));
            Add(new GearParameter(ParametersEnum.HoleDiameter, 4, 10, 6));
            Add(new GearParameter(ParametersEnum.Height, 10, 20, 15));
            Add(new GearParameter(ParametersEnum.ToothLength, 8, 20, 12));
            Add(new GearParameter(ParametersEnum.ToothWidth, 5, 10, 8));
            Add(new GearParameter(ParametersEnum.TeethCount, 6, 8, 8));
        }
    }
}
