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
        public GearParameters GearParameters { get; } = new GearParameters();

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
        public RelayCommand<GearParameters> BuildModelCommand { get; }

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
            BuildModelCommand = new RelayCommand<GearParameters>(_builder.BuildGear);
            SetDefaultCommand = new RelayCommand(GearParameters.SetDefault);
            GearParameters.ValidDataChanged += OnValidDataChanged;

            foreach (var parameter in GearParameters.ParametersList)
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
            if (GearParameters.ParametersList.Any(
                parameter => parameter.IsValidData == false))
            {
                IsValidData = false;
                return;
            }

            IsValidData = true;
        }

        #endregion
    }
}
