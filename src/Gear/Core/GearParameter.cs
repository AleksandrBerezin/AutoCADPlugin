using System;
using System.ComponentModel;
using GalaSoft.MvvmLight;

namespace Core
{
    /// <summary>
    /// Класс <see cref="GearParameter"/> хранит информацию о параметре шестерни
    /// </summary>
    public class GearParameter : ObservableObject, IDataErrorInfo, ICloneable
    {
        #region PrivateFields
        
        /// <summary>
        /// Текущее значение параметра
        /// </summary>
        private int _value;

        /// <summary>
        /// Ограничения для значения параметра
        /// </summary>
        private string _limits;

        /// <summary>
        /// Хранит значение, показывающее, корректны ли введенные данные
        /// </summary>
        private bool _isValidData = true;

        #endregion

        #region PublicProperties

        /// <summary>
        /// Название параметра
        /// </summary>
        public ParametersEnum Name { get; }

        /// <summary>
        /// Минимальное значение параметра
        /// </summary>
        public int Min { get; }

        /// <summary>
        /// Максимальное значение параметра
        /// </summary>
        public int Max { get; }

        /// <summary>
        /// Текущее значение параметра
        /// </summary>
        public int Value
        {
            get => _value;
            set
            {
                Set(ref _value, value);
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Текстовое описание параметра
        /// </summary>
        public string Description => Name.GetDescription();

        /// <summary>
        /// Ограничения для значения параметра
        /// </summary>
        public string Limits
        {
            get => _limits;
            private set => Set(ref _limits, value);
        }

        /// <summary>
        /// Проверяет, корректны ли введенные данные
        /// </summary>
        public bool IsValidData
        {
            get => _isValidData;
            private set
            {
                Set(ref _isValidData, value);
                ValidDataChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <inheritdoc/>
        public string Error => string.Empty;

        #endregion

        #region Events

        /// <summary>
        /// Событие, уведомляющее об изменении корректности введенных данных
        /// </summary>
        public event EventHandler ValidDataChanged;

        /// <summary>
        /// Событие изменения текущего значения параметра
        /// </summary>
        public event EventHandler ValueChanged;

        #endregion

        #region Constructors

        /// <summary>
        /// Создает экземпляр класса <see cref="GearParameter"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="value"></param>
        public GearParameter(ParametersEnum name, int min, int max, int value)
        {
            Name = name;
            Min = min;
            Max = max;
            Value = value;

            Limits = Name == ParametersEnum.TeethCount ? $"({Min}-{Max})" : $"({Min}-{Max} mm)";
        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            var parameter = obj as GearParameter;

            if (parameter == null)
            {
                return false;
            }

            if (parameter.Name == Name && parameter.Min == Min && parameter.Max == Max && parameter.Value == Value)
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion

        #region Validators

        /// <inheritdoc/>
        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case nameof(Value):
                    {
                        if (Value < Min || Value > Max)
                        {
                            error = $"Parameter {Name} " +
                                    $"should be more then {Min} " +
                                    $"and less then {Max} mm.";
                        }
                        break;
                    }
                }

                IsValidData = error == string.Empty;
                return error;
            }
        }

        #endregion
    }
}
