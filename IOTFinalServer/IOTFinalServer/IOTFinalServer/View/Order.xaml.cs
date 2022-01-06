using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using IOTFinalServer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IOTFinalServer.View
{
    /// <summary>
    /// Order.xaml 的互動邏輯
    /// </summary>
    public partial class Order : Window
    {
        public Order()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
            Messenger.Default.Register<String>(this, "openService", list => {
                var position = SimpleIoc.Default.GetInstance<MainViewModel>();
                var modalWindow = new MainWindow()
                {
                    DataContext = position
                };

                
                modalWindow.Closed += (s, args) => this.Close();
                modalWindow.Show();
                this.Close();
            });
        }
    }
}
