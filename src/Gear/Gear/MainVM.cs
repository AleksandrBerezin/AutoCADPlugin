using System;
using System.Linq;
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
        private GearBuilder _builder = new GearBuilder();

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

            GearParameters[ParametersEnum.GearDiameter].ValueChanged += OnValueChanged;
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
        /// Обработчик события изменения значения параметра
        /// Для зависимых параметров устанавливаются новые ограничения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnValueChanged(object sender, EventArgs e)
        {
            if (sender is GearParameter newParameter && newParameter.Name == ParametersEnum.GearDiameter)
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
            }
        }

        #endregion
    }
}
