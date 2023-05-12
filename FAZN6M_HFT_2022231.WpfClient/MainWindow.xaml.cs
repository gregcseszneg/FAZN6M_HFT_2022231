using FAZN6M_HFT_2022231.Models;
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
        MainWindowViewModel asd;

        public MainWindow()
        {
            InitializeComponent();

            Selector.Items.Add("Musicians");
            Selector.Items.Add("Tracks");
            Selector.Items.Add("Albums");
            Selector.Items.Add("Record Labels");
            Selector.Items.Add("Non CRUD methods");
            asd = new MainWindowViewModel();
            
        }

        private void Selector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataContext = new MainWindowViewModel();
            if (Selector.SelectedItem.ToString() == "Musicians")
            {
                asd.Selected = new Musician();
                TableList.ItemsSource = ((MainWindowViewModel)DataContext).Musicians;


            }
            else if (Selector.SelectedItem.ToString() == "Albums")
            {
                asd.Selected = new Album();
                TableList.ItemsSource = ((MainWindowViewModel)DataContext).Albums;

            }
            else if (Selector.SelectedItem.ToString() == "Tracks")
            {
                asd.Selected = new Track();
                TableList.ItemsSource = ((MainWindowViewModel)DataContext).Tracks;

            }
            else if (Selector.SelectedItem.ToString() == "Record Labels")
            {
                asd.Selected = new RecordLabel();
                TableList.ItemsSource = ((MainWindowViewModel)DataContext).RecordLabels;
            }
        }

        private void TableList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (Selector.SelectedItem is Musician)
            //{
            //    tb.Text = (asd.Selected as Musician).Name;
            //    TableList.ItemsSource = asd.Musicians;

            //}
            //else if (Selector.SelectedItem is Album)
            //{
            //    tb.Text = (asd.Selected as Album).Name;
            //    TableList.ItemsSource = asd.Albums;

            //}
            //else if (Selector.SelectedItem is Track)
            //{

            //    tb.Text = (asd.Selected as Track).Name;
            //    TableList.ItemsSource = asd.Tracks;

            //}
            //else if (Selector.SelectedItem is RecordLabel)
            //{

            //    tb.Text = (asd.Selected as RecordLabel).Name;
            //    TableList.ItemsSource = asd.RecordLabels;
            //}
        }
    }
}
