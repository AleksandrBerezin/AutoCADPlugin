using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Core
{
    /// <summary>
    /// Класс для хранения списка параметров шестерни
    /// </summary>
    public class GearParametersList : ObservableCollection<GearParameter>
    {
        #region Events

        /// <summary>
        /// Событие, уведомляющее об изменении корректности введенных данных
        /// </summary>
        public event EventHandler ValidDataChanged;

        #endregion

        #region Constructors

        /// <summary>
        /// Создает экземпляр класса <see cref="GearParametersList"/>
        /// </summary>
        public GearParametersList()
        {
            SetDefault();
        }

        #endregion

        #region Indexers

        /// <summary>
        /// Индексатор, позволяющий получать элементы списка по значению
        /// <see cref="ParametersEnum"/>
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

        #endregion

        #region Methods

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

            this[ParametersEnum.GearDiameter].ValueChanged += OnGearDiameterChanged;

            foreach (var parameter in this)
            {
                parameter.ValidDataChanged += ValidDataChanged;
            }
        }

        /// <summary>
        /// Обработчик события изменения значения диаметра шестернни
        /// Для зависимых параметров устанавливаются новые ограничения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGearDiameterChanged(object sender, EventArgs e)
        {
            var currentGearDiameter = this[ParametersEnum.GearDiameter].Value;

            var holeDiameter = this[ParametersEnum.HoleDiameter];
            this[ParametersEnum.HoleDiameter] = new GearParameter(holeDiameter.Name,
                holeDiameter.Min, currentGearDiameter / 4, holeDiameter.Value);
            this[ParametersEnum.HoleDiameter].ValidDataChanged += ValidDataChanged;

            var toothLength = this[ParametersEnum.ToothLength];
            this[ParametersEnum.ToothLength] = new GearParameter(toothLength.Name,
                currentGearDiameter / 5, currentGearDiameter / 2, toothLength.Value);
            this[ParametersEnum.ToothLength].ValidDataChanged += ValidDataChanged;

            var toothWidth = this[ParametersEnum.ToothWidth];
            this[ParametersEnum.ToothWidth] = new GearParameter(toothWidth.Name,
                toothWidth.Min, currentGearDiameter / 4, toothWidth.Value);
            this[ParametersEnum.ToothWidth].ValidDataChanged += ValidDataChanged;
        }

        #endregion
    }
}
