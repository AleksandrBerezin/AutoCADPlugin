using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;

namespace Core
{
    /// <summary>
    /// Класс для хранения списка параметров шестерни
    /// </summary>
    public class GearParameters : ObservableObject
    {
        #region PrivateFields

        /// <summary>
        /// Форма зуба
        /// </summary>
        private ToothShapeEnum _toothShape;

        #endregion

        #region PublicProperties

        /// <summary>
        /// Список параметров
        /// </summary>
        public ObservableCollection<Parameter> ParametersList { get; } = 
            new ObservableCollection<Parameter>();

        /// <summary>
        /// Форма зуба
        /// </summary>
        public ToothShapeEnum ToothShape
        {
            get => _toothShape;
            set => Set(ref _toothShape, value);
        }

        #endregion

        #region Events

        /// <summary>
        /// Событие, уведомляющее об изменении корректности введенных данных
        /// </summary>
        public event EventHandler ValidDataChanged;

        #endregion

        #region Constructors

        /// <summary>
        /// Создает экземпляр класса <see cref="GearParameters"/>
        /// </summary>
        public GearParameters()
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
        public Parameter this[ParametersEnum index]
        {
            get => ParametersList.First(parameter => parameter.Name == index);
            set
            {
                var oldParameter = ParametersList.First(parameter => parameter.Name == index);
                var i = ParametersList.IndexOf(oldParameter);
                ParametersList[i] = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Задает список параметров со значениями по умолчанию
        /// </summary>
        public void SetDefault()
        {
            ParametersList.Clear();
            ParametersList.Add(new Parameter(ParametersEnum.GearDiameter, 24, 60, 40));
            ParametersList.Add(new Parameter(ParametersEnum.HoleDiameter, 4, 10, 6));
            ParametersList.Add(new Parameter(ParametersEnum.Height, 10, 20, 15));
            ParametersList.Add(new Parameter(ParametersEnum.ToothLength, 8, 20, 12));
            ParametersList.Add(new Parameter(ParametersEnum.ToothWidth, 5, 10, 8));
            ParametersList.Add(new Parameter(ParametersEnum.TeethCount, 6, 10, 8));

            this[ParametersEnum.GearDiameter].ValueChanged += OnGearDiameterChanged;
            this[ParametersEnum.ToothWidth].ValueChanged += OnToothWidthChanged;

            foreach (var parameter in ParametersList)
            {
                parameter.ValidDataChanged += ValidDataChanged;
            }

            ToothShape = ToothShapeEnum.Trapezoid;
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
            this[ParametersEnum.HoleDiameter] = new Parameter(holeDiameter.Name,
                holeDiameter.Min, currentGearDiameter / 4, holeDiameter.Value);
            this[ParametersEnum.HoleDiameter].ValidDataChanged += ValidDataChanged;

            var toothLength = this[ParametersEnum.ToothLength];
            this[ParametersEnum.ToothLength] = new Parameter(toothLength.Name,
                currentGearDiameter / 5, currentGearDiameter / 2, toothLength.Value);
            this[ParametersEnum.ToothLength].ValidDataChanged += ValidDataChanged;

            var toothWidth = this[ParametersEnum.ToothWidth];
            this[ParametersEnum.ToothWidth] = new Parameter(toothWidth.Name,
                toothWidth.Min, currentGearDiameter / 4, toothWidth.Value);
            this[ParametersEnum.ToothWidth].ValidDataChanged += ValidDataChanged;
            this[ParametersEnum.ToothWidth].ValueChanged += OnToothWidthChanged;

            OnToothWidthChanged(sender, e);
        }

        /// <summary>
        /// Обработчик события изменения значения ширины зубца
        /// Для зависимых параметров устанавливаются новые ограничения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnToothWidthChanged(object sender, EventArgs e)
        {
            var currentGearDiameter = this[ParametersEnum.GearDiameter].Value;
            var toothWidth = this[ParametersEnum.ToothWidth];

            if (toothWidth.Value != 0)
            {
                var teethCount = this[ParametersEnum.TeethCount];
                this[ParametersEnum.TeethCount] = new Parameter(teethCount.Name,
                    teethCount.Min, currentGearDiameter * 2 / toothWidth.Value,
                    teethCount.Value);
            }
            
            this[ParametersEnum.TeethCount].ValidDataChanged += ValidDataChanged;
        }

        #endregion
    }
}
