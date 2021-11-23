using System;
using System.Linq;
using Builder;
using Core;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Gear
{
    /// <summary>
    /// Модель представления главного окна
    /// </summary>
    public class MainVM : ObservableObject
    {
        #region PrivateFields

        /// <summary>
        /// Построитель модели
        /// </summary>
        private readonly GearBuilder _builder = new GearBuilder();

        /// <summary>
        /// Хранит значение, показывающее, корректны ли введенные данные
        /// </summary>
        private bool _isValidData = true;

        #endregion

        #region PublicProperties

        /// <summary>
        /// Список параметров
        /// </summary>
        public GearParametersList GearParameters { get; } = new GearParametersList();

        /// <summary>
        /// Проверяет, корректны ли введенные данные
        /// </summary>
        public bool IsValidData
        {
            get => _isValidData;
            set => Set(ref _isValidData, value);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Команда построения модели
        /// </summary>
        public RelayCommand<GearParametersList> BuildModelCommand { get; }

        /// <summary>
        /// Команда установки значений параметров по умолчанию
        /// </summary>
        public RelayCommand SetDefaultCommand { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Создает экземпляр класса <see cref="MainVM"/>
        /// </summary>
        public MainVM()
        {
            BuildModelCommand = new RelayCommand<GearParametersList>(_builder.BuildGear);
            SetDefaultCommand = new RelayCommand(GearParameters.SetDefault);

            GearParameters[ParametersEnum.GearDiameter].ValueChanged += OnGearDiameterChanged;
            GearParameters[ParametersEnum.ToothWidth].ValueChanged += OnToothWidthChanged;

            foreach (var parameter in GearParameters)
            {
                parameter.ValidDataChanged += OnValidDataChanged;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Обработчик события изменения корректности введенных данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnValidDataChanged(object sender, EventArgs e)
        {
            if (GearParameters.Any(parameter => parameter.IsValidData == false))
            {
                IsValidData = false;
                return;
            }

            IsValidData = true;
        }

        /// <summary>
        /// Обработчик события изменения значения диаметра шестернни
        /// Для зависимых параметров устанавливаются новые ограничения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGearDiameterChanged(object sender, EventArgs e)
        {
            var currentGearDiameter = GearParameters[ParametersEnum.GearDiameter].Value;

            var holeDiameter = GearParameters[ParametersEnum.HoleDiameter];
            GearParameters[ParametersEnum.HoleDiameter] = new GearParameter(holeDiameter.Name, holeDiameter.Min,
                currentGearDiameter / 4, holeDiameter.Value);
            GearParameters[ParametersEnum.HoleDiameter].ValidDataChanged += OnValidDataChanged;

            var toothLength = GearParameters[ParametersEnum.ToothLength];
            GearParameters[ParametersEnum.ToothLength] = new GearParameter(toothLength.Name, currentGearDiameter / 5,
                currentGearDiameter / 2, toothLength.Value);
            GearParameters[ParametersEnum.ToothLength].ValidDataChanged += OnValidDataChanged;

            var toothWidth = GearParameters[ParametersEnum.ToothWidth];
            GearParameters[ParametersEnum.ToothWidth] = new GearParameter(toothWidth.Name, toothWidth.Min,
                currentGearDiameter / 4, toothWidth.Value);
            GearParameters[ParametersEnum.ToothWidth].ValidDataChanged += OnValidDataChanged;
            GearParameters[ParametersEnum.ToothWidth].ValueChanged += OnToothWidthChanged;

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
            var currentGearDiameter = GearParameters[ParametersEnum.GearDiameter].Value;
            var toothWidth = GearParameters[ParametersEnum.ToothWidth];

            var teethCount = GearParameters[ParametersEnum.TeethCount];
            GearParameters[ParametersEnum.TeethCount] = new GearParameter(teethCount.Name, teethCount.Min,
                currentGearDiameter * 2 / toothWidth.Value, teethCount.Value);
            GearParameters[ParametersEnum.TeethCount].ValidDataChanged += OnValidDataChanged;
        }

        #endregion
    }
}
