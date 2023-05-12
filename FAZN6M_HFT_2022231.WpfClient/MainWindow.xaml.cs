using FAZN6M_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            if (Selector.SelectedItem.ToString() == "Musicians")
            {
                DetailStackLabel.Children.Clear();
                asd.Selected = new Musician();
                TableList.ItemsSource = ((MainWindowViewModel)DataContext).Musicians;

                var properties = typeof(Musician).GetProperties().Where(p => !Attribute.IsDefined(p, typeof(RequiredAttribute)));
                var reqproperties = typeof(Musician).GetProperties().Where(p => Attribute.IsDefined(p, typeof(RequiredAttribute)));

                foreach (var prop in reqproperties)
                {
                    Label lb = new Label();
                    lb.Content = prop.Name + " (Required):";
                    DetailStackLabel.Children.Add(lb);

                }

                foreach (var prop in properties)
                {
                    Label lb = new Label();
                    lb.Content = prop.Name + ":";
                    DetailStackLabel.Children.Add(lb);

                }

            }
            else if (Selector.SelectedItem.ToString() == "Albums")
            {
                DetailStackLabel.Children.Clear();
                asd.Selected = new Album();
                TableList.ItemsSource = ((MainWindowViewModel)DataContext).Albums;

                var properties = typeof(Album).GetProperties().Where(p => !Attribute.IsDefined(p, typeof(RequiredAttribute)));
                var reqproperties = typeof(Album).GetProperties().Where(p => Attribute.IsDefined(p, typeof(RequiredAttribute)));

                foreach (var prop in reqproperties)
                {
                    Label lb = new Label();
                    lb.Content = prop.Name + " (Required):";
                    DetailStackLabel.Children.Add(lb);

                }

                foreach (var prop in properties)
                {
                    Label lb = new Label();
                    lb.Content = prop.Name + ":";
                    DetailStackLabel.Children.Add(lb);

                }

            }
            else if (Selector.SelectedItem.ToString() == "Tracks")
            {
                DetailStackLabel.Children.Clear();
                asd.Selected = new Track();
                TableList.ItemsSource = ((MainWindowViewModel)DataContext).Tracks;

                var properties = typeof(Track).GetProperties().Where(p => !Attribute.IsDefined(p, typeof(RequiredAttribute)));
                var reqproperties = typeof(Track).GetProperties().Where(p => Attribute.IsDefined(p, typeof(RequiredAttribute)));

                foreach (var prop in reqproperties)
                {
                    Label lb = new Label();
                    lb.Content = prop.Name + " (Required):";
                    DetailStackLabel.Children.Add(lb);

                }

                foreach (var prop in properties)
                {
                    Label lb = new Label();
                    lb.Content = prop.Name + ":";
                    DetailStackLabel.Children.Add(lb);

                }

            }
            else if (Selector.SelectedItem.ToString() == "Record Labels")
            {
                DetailStackLabel.Children.Clear();
                asd.Selected = new RecordLabel();
                TableList.ItemsSource = ((MainWindowViewModel)DataContext).RecordLabels;

                var properties = typeof(RecordLabel).GetProperties().Where(p => !Attribute.IsDefined(p, typeof(RequiredAttribute)));
                var reqproperties = typeof(RecordLabel).GetProperties().Where(p => Attribute.IsDefined(p, typeof(RequiredAttribute)));

                foreach (var prop in reqproperties)
                {
                    Label lb = new Label();
                    lb.Content = prop.Name + " (Required):";
                    DetailStackLabel.Children.Add(lb);

                }

                foreach (var prop in properties)
                {
                    Label lb = new Label();
                    lb.Content = prop.Name + ":";
                    DetailStackLabel.Children.Add(lb);

                }
            }
        }

    }
}
