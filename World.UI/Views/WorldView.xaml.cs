using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Musiqual.Parameter.Views;
using YDock.Interface;

namespace World.UI.Views
{
    /// <summary>
    /// WorldView.xaml 的交互逻辑
    /// </summary>
    public partial class WorldView : UserControl, IDockSource, INotifyPropertyChanged
    {

        public WorldView()
        {

            InitializeComponent();

            DataContext = this;

        }

        #region DockSource

        public IDockControl DockControl { get; set; }

        public string Header => "WORLD";

        public ImageSource Icon => null;

        #endregion

        #region Current

        public static WorldView Current = new WorldView();

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
            }
        }

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

        #endregion

        #region Load/Save

        private void LoadF0()
        {

        }

        private void UnloadF0()
        {

        }

        private void SaveF0ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        #endregion

    }
}
