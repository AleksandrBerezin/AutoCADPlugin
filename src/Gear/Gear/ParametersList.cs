using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Gear
{
    public class ParametersList : ObservableCollection<Parameter>
    {
        public ParametersList()
        {
            SetDefault();
        }

        public double this[ParametersEnum index]
        {
            get => this.First(parameter => parameter.Name.Equals(index)).Value;
            set => this.First(parameter => parameter.Name.Equals(index)).Value = value;
        }

        public void SetDefault()
        {
            Clear();
            Add(new Parameter(ParametersEnum.GearDiameter, 24, 60, 40));
            Add(new Parameter(ParametersEnum.HoleDiameter, 4, 10, 6));
            Add(new Parameter(ParametersEnum.Height, 10, 20, 15));
            Add(new Parameter(ParametersEnum.ToothLength, 8, 20, 12));
            Add(new Parameter(ParametersEnum.ToothWidth, 5, 10, 8));
        }

        /// <summary>
        /// Обработчик для изменения коллекции параметров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    //(e.NewItems[0] as Parameter).ChangeModeCommand = new RelayCommand<Filter>(ChangeFilterMode);
                    //(e.NewItems[0] as Filter).RemoveFilterCommand = new RelayCommand<Filter>(RemoveFilter);
                    break;
                }
            }
        }
    }
}
