using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;

namespace Gear
{
    public class Parameter : ObservableObject
    {
        private double _min;
        private double _max;
        private double _value;







        ///// <summary>
        ///// Хранит значение, показывающее, есть ли ошибка в
        ///// одном из значений времени сепарирования
        ///// </summary>
        //private bool _hasError;

        ///// <summary>
        ///// Проверяет есть ошибка в формате даты
        ///// </summary>
        //public bool HasError
        //{
        //    get => _hasError;
        //    set
        //    {
        //        _hasError = value;

        //        if (_hasError == false)
        //        {
        //            RaisePropertyChanged(nameof(HasError));
        //        }
        //    }
        //}





        public ParametersEnum Name { get; set; }

        public double Min
        {
            get => _min;
            set
            {
                var comparerResultMin = Comparer<double>.Default.Compare(value, _min);

                //if (comparerResultMin >= 0)
                //{
                //    throw new ArgumentException("Minimum should be less or equal to maximum.");
                //}

                _min = value;
            }
        }

        public double Max
        {
            get => _max;
            set
            {
                var comparerResultMin = Comparer<double>.Default.Compare(value, _max);

                //if (comparerResultMin <= 0)
                //{
                //    throw new ArgumentException("Maximum should be more or equal to minimum.");
                //}

                _max = value;
            }
        }

        public double Value
        {
            get => _value;
            set
            {
                var comparerResultMin = Comparer<double>.Default.Compare(value, _min);
                //if (comparerResultMin <= 0)
                //{
                //    throw new ArgumentException("");
                //}

                //var comparerResultMax = Comparer<double>.Default.Compare(value, _max);
                //if (comparerResultMax >= 0)
                //{
                //    throw new ArgumentException("");
                //}

                Set(ref _value, value);
            }
        }

        public string Description => Name.GetDescription();

        public string Limits => $"({Min}-{Max} mm)";

        public Parameter(ParametersEnum name, double min, double max, double value)
        {
            Name = name;
            Min = min;
            Max = max;
            Value = value;



            //HasError = false;
        }
















        #region Validators

        /// <inheritdoc/>
        //public string this[string columnName]
        //{
        //    get
        //    {
        //        string error = String.Empty;
        //        switch (columnName)
        //        {
        //            case nameof(StartTimeString):
        //                error = CheckDateTimeFormat(StartTimeString);
        //                _startTimeHasError = error != String.Empty;
        //                break;
        //            case nameof(EndTimeString):
        //                error = CheckDateTimeFormat(EndTimeString);
        //                _endTimeHasError = error != String.Empty;
        //                break;
        //        }

        //        HasError = (_endTimeHasError || _startTimeHasError);

        //        return error;
        //    }
        //}

        /// <inheritdoc/>
        //public string Error => string.Empty;

        #endregion
    }
}
