using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Gear
{
    public class MainVM : ObservableObject
    {
        private bool _isValidData = true;
        
        public ParametersList Parameters { get; } = new ParametersList();

        public bool IsValidData
        {
            get => _isValidData;
            set => Set(ref _isValidData, value);
        }

        public RelayCommand BuildModelCommend { get; private set; }

        public RelayCommand SetDefaultCommand { get; private set; }

        public MainVM()
        {
            BuildModelCommend = new RelayCommand(new GearBuilder().Build);
            SetDefaultCommand = new RelayCommand(Parameters.SetDefault);
        }
    }
}
