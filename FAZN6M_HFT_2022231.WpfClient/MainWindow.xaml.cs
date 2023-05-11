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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FAZN6M_HFT_2022231.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel asd = new MainWindowViewModel();
        public MainWindow()
        {
            InitializeComponent();

            Selector.Items.Add("Musicians");
            Selector.Items.Add("Tracks");
            Selector.Items.Add("Albums");
            Selector.Items.Add("Record Labels");
            Selector.Items.Add("Non CRUD methods");
        }

        private void Selector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Selector.SelectedItem.ToString()=="Musicians")
            {
               TableList.ItemsSource=asd.Musicians;

            }
            else if (Selector.SelectedItem.ToString() == "Albums")
            {

                TableList.ItemsSource = asd.Albums;

            }
            else if (Selector.SelectedItem.ToString() == "Tracks")
            {

                TableList.ItemsSource = asd.Tracks;

            }
            else if (Selector.SelectedItem.ToString() == "Record Labels")
            {

                TableList.ItemsSource = asd.RecordLabels;

            }
        }
    }
}
