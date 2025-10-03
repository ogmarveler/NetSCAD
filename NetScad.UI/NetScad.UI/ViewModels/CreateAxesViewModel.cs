using NetScad.Axis.SCAD.Modules;
using NetScad.Core.Measurements;
using NetScad.Core.Utility;
using ReactiveUI;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static NetScad.Axis.SCAD.Utility.AxisConfig;
using static NetScad.Core.Measurements.Conversion;
using static NetScad.Core.Measurements.Selector;

namespace NetScad.UI.ViewModels
{
    public class CreateAxesViewModel : ReactiveObject, INotifyDataErrorInfo
    {
        private UnitSystem _selectedUnit;         // Pass these back to backend functions
        private BackgroundType _selectedBackground;
        private double _minX;
        private double _maxX;
        private double _minY;
        private double _maxY;
        private double _minZ;
        private double _maxZ;
        private bool _unitHasChanged;
        private bool _isMetric;
        private bool _isImperial;
        private CustomAxis _customAxis;
        private bool _axisDetailsShown;
        private string _moduleName;
        private string _callingMethod;
        private string _includeFile;
        private double _totalCubicVolume;
        private double _totalCubicVolumeScale;
        public int _decimalPlaces;
        public int _callingMethodLength;
        private string _inputText;
        private readonly ConcurrentDictionary<string, List<string>> _errors = new();

        public CreateAxesViewModel()
        {
            UnitSystemValues = Enum.GetValues(typeof(UnitSystem)).Cast<UnitSystem>().ToList();
            BackgroundTypeValues = Enum.GetValues(typeof(BackgroundType)).Cast<BackgroundType>().ToList();
            SelectedBackgroundValue = BackgroundType.Light;
            SelectedUnitValue = UnitSystem.Metric;
            MinXValue = 0;
            MaxXValue = SelectedUnitValue == UnitSystem.Metric ? 300 : 12; // Set based on defaults
            MinYValue = 0;
            MaxYValue = SelectedUnitValue == UnitSystem.Metric ? 300 : 12;
            MinZValue = 0;
            MaxZValue = SelectedUnitValue == UnitSystem.Metric ? 300 : 12;
            _unitHasChanged = false;
            UnitHasChanged = false;
            _isImperial = SelectedUnitValue == UnitSystem.Metric ? false : true; // Set based on SelectedUnit
            _isMetric = SelectedUnitValue == UnitSystem.Metric ? true : false;
            IsImperial = SelectedUnitValue == UnitSystem.Metric ? false : true;
            IsMetric = SelectedUnitValue == UnitSystem.Metric ? true : false;
            _axisDetailsShown = true; // Module Details
            AxisDetailsShown = false;
            ModuleName = string.Empty;
            CallingMethod = string.Empty;
            IncludeFile = string.Empty;
            TotalCubicVolume = 0;
            TotalCubicVolumeScale = 0;
            _decimalPlaces = 12; // Rounding for conversions
            _callingMethodLength = 0; // For selectable text for module to be called in SCAD file

            //this.WhenAnyValue( // Observe changes to the group of input variables
            //    vm => vm.SelectedUnitValue,
            //    vm => vm._minX,
            //    vm => vm._maxX,
            //    vm => vm._minY,
            //    vm => vm._maxY,
            //    vm => vm._minZ,
            //    vm => vm._maxZ
            //)
            //.Skip(1) // Skip initial values to avoid clearing on ViewModel creation
            //.Subscribe(_ =>
            //{
            //    Dispatcher.UIThread.Post(() =>
            //    { // When changes made to any inputs after generating axis, clear out details
            //        ModuleName = string.Empty;
            //        CallingMethod = string.Empty;
            //        IncludeFile = string.Empty;
            //        AxisDetailsShown = false;
            //    });
            //});
        }
        public double MinXValue { get => _minX; set => this.RaiseAndSetIfChanged(ref _minX, value); }
        public double MaxXValue { get => _maxX; set => this.RaiseAndSetIfChanged(ref _maxX, value); }
        public double MinYValue { get => _minY; set => this.RaiseAndSetIfChanged(ref _minY, value); }
        public double MaxYValue { get => _maxY; set => this.RaiseAndSetIfChanged(ref _maxY, value); }
        public double MinZValue { get => _minZ; set => this.RaiseAndSetIfChanged(ref _minZ, value); }
        public double MaxZValue { get => _maxZ; set => this.RaiseAndSetIfChanged(ref _maxZ, value); }
        public List<UnitSystem> UnitSystemValues { get; set; }
        public UnitSystem SelectedUnitValue
        {
            get => _selectedUnit;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedUnit, value);
                UnitHasChanged = true; // For use in conversions when _selectedUnit has changed
                ConvertInputs(_decimalPlaces);
            }
        }
        public List<BackgroundType> BackgroundTypeValues { get; set; }
        public BackgroundType SelectedBackgroundValue { get => _selectedBackground; set => this.RaiseAndSetIfChanged(ref _selectedBackground, value); }
        public bool UnitHasChanged { get => _unitHasChanged; set => this.RaiseAndSetIfChanged(ref _unitHasChanged, value); }
        public bool IsMetric { get => _isMetric; set => this.RaiseAndSetIfChanged(ref _isMetric, value); }
        public bool IsImperial { get => _isImperial; set => this.RaiseAndSetIfChanged(ref _isImperial, value); }
        public bool AxisDetailsShown { get => _axisDetailsShown; set => this.RaiseAndSetIfChanged(ref _axisDetailsShown, value); }
        public string ModuleName { get => _moduleName; set => this.RaiseAndSetIfChanged(ref _moduleName, value); }
        public string CallingMethod { get => _callingMethod; set => this.RaiseAndSetIfChanged(ref _callingMethod, value); }
        public string IncludeFile { get => _includeFile; set => this.RaiseAndSetIfChanged(ref _includeFile, value); }
        public double TotalCubicVolume { get => _totalCubicVolume; set => this.RaiseAndSetIfChanged(ref _totalCubicVolume, value); }
        public double TotalCubicVolumeScale { get => _totalCubicVolumeScale; set => this.RaiseAndSetIfChanged(ref _totalCubicVolumeScale, value); }
        public int CallingMethodLength { get => _callingMethodLength; set => this.RaiseAndSetIfChanged(ref _callingMethodLength, value); }

        public Task ConvertInputs(int decimalPlaces) // Convert from unit system to another
        {
            if (_selectedUnit == UnitSystem.Imperial && UnitHasChanged) { ConvertInputsImperial(decimalPlaces); }
            else if (_selectedUnit == UnitSystem.Metric && UnitHasChanged) { ConvertInputsMetric(decimalPlaces); }
            IsImperial = SelectedUnitValue == UnitSystem.Metric ? false : true;
            IsMetric = SelectedUnitValue == UnitSystem.Metric ? true : false;
            return Task.CompletedTask;
        }

        public async Task CreateCustomAxisAsync()
        {
            AxisDetailsShown = false;      // Disables display of previous generated output details
            if (!double.IsNaN(_minX) && !double.IsNaN(_maxX) &&   // Check inputs to ensure they're doubles
                !double.IsNaN(_minY) && !double.IsNaN(_maxY) &&
                !double.IsNaN(_minZ) && !double.IsNaN(_maxZ))
            {
                if (_selectedUnit == UnitSystem.Imperial && !UnitHasChanged)
                {                 
                    // Convert if inputs are inches but function is mm
                    _minX = Math.Round(InchesToMillimeter(_minX), _decimalPlaces);
                    _maxX = Math.Round(InchesToMillimeter(_maxX), _decimalPlaces);
                    _minY = Math.Round(InchesToMillimeter(_minY), _decimalPlaces);
                    _maxY = Math.Round(InchesToMillimeter(_maxY), _decimalPlaces);
                    _minZ = Math.Round(InchesToMillimeter(_minZ), _decimalPlaces);
                    _maxZ = Math.Round(InchesToMillimeter(_maxZ), _decimalPlaces);
                    UnitHasChanged = true; // To prevent converting inches to mm twice
                }
                var axisSettings = new AxisSettings(
                 outputDirectory: PathHelper.GetProjectRoot(), // ..NetScad.UI.<Platform>/Scad/Axes
                 backgroundType: _selectedBackground,
                 measureType: _selectedUnit,
                 minX: _minX,
                 maxX: _maxX,
                 minY: _minY,
                 maxY: _maxY,
                 minZ: _minZ,
                 maxZ: _maxZ
                );

                /**** SetAxis Generation function ****/
                _customAxis = new CustomAxis();             //Clear out previous custom axis data
                _customAxis = await GUI.SetAxis(axisSettings); 

                // Convert values back to inches since backend function uses mm
                if (_selectedUnit == UnitSystem.Imperial && UnitHasChanged)  // Update frontend with adjusted imperial values
                { 
                    ConvertInputsImperial((int)(_decimalPlaces / 1.5));
                }
                else   // Update frontend with adjusted metric values, no conversion needed
                { 
                    MinXValue = Math.Round(_customAxis.Settings.MinX, (int)(_decimalPlaces / 1.5));
                    MaxXValue = Math.Round(_customAxis.Settings.MaxX, (int)(_decimalPlaces / 1.5));
                    MinYValue = Math.Round(_customAxis.Settings.MinY, (int)(_decimalPlaces / 1.5));
                    MaxYValue = Math.Round(_customAxis.Settings.MaxY, (int)(_decimalPlaces / 1.5));
                    MinZValue = Math.Round(_customAxis.Settings.MinZ, (int)(_decimalPlaces / 1.5));
                    MaxZValue = Math.Round(_customAxis.Settings.MaxZ, (int)(_decimalPlaces / 1.5));
                }

                // Set Post-Axis Generation Details
                IsImperial = SelectedUnitValue == UnitSystem.Metric ? false : true;
                IsMetric = SelectedUnitValue == UnitSystem.Metric ? true : false;
                TotalCubicVolume = _customAxis.TotalCubicVolume; // Provides the total volume of the axes
                TotalCubicVolumeScale = _customAxis.TotalCubicVolumeScale; // Provides the total volume of the axes
                ModuleName = _customAxis.ModuleName;          // Update new axis details
                CallingMethod = _customAxis.CallingMethod; // To call the axis module
                CallingMethodLength = _customAxis.CallingMethod.Length - 1; // For copy and paste into main file
                IncludeFile = $"include <{_customAxis.CallingMethod.ToLower().Replace("();", "")}.scad>";
                AxisDetailsShown = true;
            }
        }

        public Task ClearInputs()
        {
            AxisDetailsShown = false; // Post-gen of axis details - static resources disabled in XAML
            TotalCubicVolume = 0;
            TotalCubicVolumeScale = 0;
            ModuleName = string.Empty;
            CallingMethod = string.Empty;
            IncludeFile = string.Empty;
            MinXValue = 0;  // Set to 0 for coordinates
            MaxXValue = 0;
            MinYValue = 0;
            MaxYValue = 0;
            MinZValue = 0;
            MaxZValue = 0;
            SelectedUnitValue = UnitSystem.Metric; // Defaults for enums
            SelectedBackgroundValue = BackgroundType.Light;
            return Task.CompletedTask;
        }

        // ViewModel helper functions for conversions - stateful
        private Task ConvertInputsImperial(int decimalPlaces)
        {   
            // Convert from metric unit system to imperial
            MinXValue = Math.Round(MillimeterToInches(_minX), decimalPlaces);  // mm to inches
            MaxXValue = Math.Round(MillimeterToInches(_maxX), decimalPlaces);
            MinYValue = Math.Round(MillimeterToInches(_minY), decimalPlaces);
            MaxYValue = Math.Round(MillimeterToInches(_maxY), decimalPlaces);
            MinZValue = Math.Round(MillimeterToInches(_minZ), decimalPlaces);
            MaxZValue = Math.Round(MillimeterToInches(_maxZ), decimalPlaces);
            TotalCubicVolume = Math.Round(VolumeConverter.ConvertCm3ToIn3(_totalCubicVolume), decimalPlaces);  // cm3 to in3
            TotalCubicVolumeScale = Math.Round(VolumeConverter.ConvertM3ToFt3(_totalCubicVolumeScale), decimalPlaces);  // m to feet 
            UnitHasChanged = false;
            return Task.CompletedTask;
        }

        private Task ConvertInputsMetric(int decimalPlaces)
        {   
            // Convert from imperial unit system to metric
            MinXValue = Math.Round(InchesToMillimeter(_minX), decimalPlaces);  // inches to mm
            MaxXValue = Math.Round(InchesToMillimeter(_maxX), decimalPlaces);
            MinYValue = Math.Round(InchesToMillimeter(_minY), decimalPlaces);
            MaxYValue = Math.Round(InchesToMillimeter(_maxY), decimalPlaces);
            MinZValue = Math.Round(InchesToMillimeter(_minZ), decimalPlaces);
            MaxZValue = Math.Round(InchesToMillimeter(_maxZ), decimalPlaces);
            TotalCubicVolume = Math.Round(VolumeConverter.ConvertIn3ToCm3(_totalCubicVolume), decimalPlaces);  // inches to cm
            TotalCubicVolumeScale = Math.Round(VolumeConverter.ConvertFt3ToM3(_totalCubicVolumeScale), decimalPlaces);  // feet to m
            UnitHasChanged = false;
            return Task.CompletedTask;
        }

        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                OnPropertyChanged();
                ValidateInputText(value);
            }
        }

        private void ValidateInputText(string value)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(value))
            {
                errors.Add("Input cannot be empty.");
            }
            else if (value.Length < 3)
            {
                errors.Add("Input must be at least 3 characters long.");
            }

            if (errors.Count > 0)
            {
                _errors["InputText"] = errors;
            }
            else
            {
                _errors.TryRemove("InputText", out _);
            }
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(InputText)));
        }

        public bool HasErrors => _errors.Count > 0;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !_errors.ContainsKey(propertyName))
                return null;
            return _errors[propertyName];
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}