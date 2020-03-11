using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;
using Musiqual.Editor.Models;
using Musiqual.Parameter;
using Musiqual.Parameter.Views;
using Scrosser.Models;
using World.UI.Models.Parameter;
using YDock;
using YDock.Interface;

namespace World.UI.Views
{
    /// <summary>
    /// WorldView.xaml 的交互逻辑
    /// </summary>
    public partial class WorldView : UserControl, IDockSource, INotifyPropertyChanged
    {

        public WorldView(DockManager dockManager, IDockControl navigateDockControl, Scross scross, EditMode editMode)
        {

            _scross = scross;
            _editMode = editMode;
            _dockManager = dockManager;
            NavigateDockControl = navigateDockControl;

            InitializeComponent();

            DataContext = this;

        }

        #region DockSource

        public IDockControl DockControl { get; set; }

        public string Header => "WORLD";

        public ImageSource Icon => null;

        #endregion

        #region DataContext

        private bool _isF0Loaded;

        public bool IsF0Loaded
        {
            get => _isF0Loaded;
            set
            {
                if (!_isF0Loaded && value) LoadF0();
                else if (!value && _isF0Loaded) UnloadF0();
                _isF0Loaded = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsOpenAsDeltaDisabled));
            }
        }

        public bool IsOpenAsDeltaDisabled => !_isF0Loaded;

        private string _f0Path = "";

        public string F0Path
        {
            get => _f0Path;
            set
            {
                _f0Path = value;
                OnPropertyChanged();
            }
        }

        private bool _openAsDelta;

        public bool OpenAsDelta
        {
            get => _openAsDelta;
            set
            {
                _openAsDelta = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region ParameterView

        public ParameterView ParameterView;

        public F0DeltaParameterData F0DeltaParameterData;

        public F0ParameterData F0ParameterData;

        private Scross _scross;

        private EditMode _editMode;

        private DockManager _dockManager;

        public IDockControl NavigateDockControl;

        #endregion

        #region Load/Save

        private void LoadF0()
        {

            CommonOpenFileDialog fileDialog = new CommonOpenFileDialog
            {
                Title = "Choose F0 Parameter Files",
                DefaultDirectory = Environment.CurrentDirectory,
                IsFolderPicker = false,
                AllowNonFileSystemItems = true,
                EnsurePathExists = true,
                Multiselect = false,
                Filters = { new CommonFileDialogFilter("F0 Parameter", ".f0") },
                EnsureFileExists = true
            };

            if (fileDialog.ShowDialog() != CommonFileDialogResult.Ok)
            {
                IsF0Loaded = false;
                return;
            }
            F0Path = fileDialog.FileName;

            #region Process F0 Data

            byte[] f = File.ReadAllBytes(F0Path);
            List<double> f1 = new List<double>();
            int cnt = f.Length / 8;
            for (int i = 0; i < cnt; i++) f1.Add(BitConverter.ToDouble(f, i * 8));
            if (OpenAsDelta)
                F0DeltaParameterData = F0DeltaParameterData.CreateF0DeltaParameterData(f1);
            else
                F0ParameterData = F0ParameterData.CreateF0ParameterData(f1);

            #endregion

            _scross = new Scross()
            {
                IsEnabled = true,
                Total = f1.Count,
                Position = 0
            };

            ParameterView = OpenAsDelta
                ? new ParameterView(F0DeltaParameterData, _scross, _editMode)
                : new ParameterView(F0ParameterData, _scross, _editMode);

            _dockManager.RegisterDocument(ParameterView);
            NavigateDockControl.Show();
            ParameterView.DockControl.Show();

        }

        private void UnloadF0()
        {
            if (ParameterView is null) return;
            MessageBoxResult result = MessageBox.Show(
                "Save before unload?",
                "Unload",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question,
                MessageBoxResult.Yes);
            if (result == MessageBoxResult.Cancel) return;
            if (result == MessageBoxResult.Yes) SaveF0ButtonBase_OnClick(this, null);

            F0Path = "";

            _scross = new Scross()
            {
                IsEnabled = false
            };

            NavigateDockControl.Hide();
            ParameterView.DockControl.Hide();
            ParameterView.DockControl.Dispose();
            ParameterView = null;

        }

        private void SaveF0ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        #endregion

    }
}
