using FAZN6M_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        TextBox Name = new TextBox();
        TextBox Age = new TextBox();
        TextBox DateOfBirth = new TextBox();
        TextBox HomeTown = new TextBox();
        TextBox Country = new TextBox();
        TextBox Gender = new TextBox();
        TextBox RecordLabelId = new TextBox();

        public MainWindow()
        {
            InitializeComponent();

            Selector.Items.Add("Musicians");
            Selector.Items.Add("Tracks");
            Selector.Items.Add("Albums");
            Selector.Items.Add("Record Labels");
            Selector.Items.Add("Non CRUD methods");

            Age.PreviewTextInput += TextBox_PreviewTextInput;
            RecordLabelId.PreviewTextInput += TextBox_PreviewTextInput;

        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            
            if (!IsNumericInput(e.Text))
            {
                e.Handled = true; 
            }
        }

        private bool IsNumericInput(string text)
        {
            return int.TryParse(text, out _);
        }

        private void Selector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Selector.SelectedItem.ToString() == "Musicians")
            {
                ((MainWindowViewModel)DataContext).Selected = new Musician();
                DetailStackLabel.Children.Clear();
                DetailStackText.Children.Clear();
                TableList.ItemsSource = ((MainWindowViewModel)DataContext).Musicians;

                var properties = typeof(Musician).GetProperties().Where(p => !Attribute.IsDefined(p, typeof(RequiredAttribute)) && !p.GetGetMethod().IsVirtual);
                var reqproperties = typeof(Musician).GetProperties().Where(p => Attribute.IsDefined(p, typeof(RequiredAttribute))).Where(p => !Attribute.IsDefined(p, typeof(KeyAttribute)));


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

                DetailStackText.Children.Add(Name);
                DetailStackText.Children.Add(Age);
                DetailStackText.Children.Add(DateOfBirth);
                DetailStackText.Children.Add(HomeTown);
                DetailStackText.Children.Add(Country);
                DetailStackText.Children.Add(Gender);
                DetailStackText.Children.Add(RecordLabelId);
            }
            else if (Selector.SelectedItem.ToString() == "Albums")
            {
                ((MainWindowViewModel)DataContext).Selected = new Album();
                DetailStackLabel.Children.Clear();
                TableList.ItemsSource = ((MainWindowViewModel)DataContext).Albums;

                var properties = typeof(Album).GetProperties().Where(p => !Attribute.IsDefined(p, typeof(RequiredAttribute)) && !p.GetGetMethod().IsVirtual);
                var reqproperties = typeof(Album).GetProperties().Where(p => Attribute.IsDefined(p, typeof(RequiredAttribute))).Where(p => !Attribute.IsDefined(p, typeof(KeyAttribute)));

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
                ((MainWindowViewModel)DataContext).Selected = new Track();
                DetailStackLabel.Children.Clear();
                TableList.ItemsSource = ((MainWindowViewModel)DataContext).Tracks;

                var properties = typeof(Track).GetProperties().Where(p => !Attribute.IsDefined(p, typeof(RequiredAttribute)) && !p.GetGetMethod().IsVirtual);
                var reqproperties = typeof(Track).GetProperties().Where(p => Attribute.IsDefined(p, typeof(RequiredAttribute))).Where(p => !Attribute.IsDefined(p, typeof(KeyAttribute)));

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
                ((MainWindowViewModel)DataContext).Selected = new RecordLabel();
                DetailStackLabel.Children.Clear();
                TableList.ItemsSource = ((MainWindowViewModel)DataContext).RecordLabels;

                var properties = typeof(RecordLabel).GetProperties().Where(p => !Attribute.IsDefined(p, typeof(RequiredAttribute)) && !p.GetGetMethod().IsVirtual);
                var reqproperties = typeof(RecordLabel).GetProperties().Where(p => Attribute.IsDefined(p, typeof(RequiredAttribute))).Where(p => !Attribute.IsDefined(p, typeof(KeyAttribute)));

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

        private void TableList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (Selector.SelectedItem.ToString() == "Musicians")
            {
                Name.Text = (((MainWindowViewModel)DataContext).Selected as Musician).Name;
                Age.Text = (((MainWindowViewModel)DataContext).Selected as Musician).Age.ToString();
                DateOfBirth.Text = $"{(((MainWindowViewModel)DataContext).Selected as Musician).DateOfBirth.Year}-{(((MainWindowViewModel)DataContext).Selected as Musician).DateOfBirth.Month}-{(((MainWindowViewModel)DataContext).Selected as Musician).DateOfBirth.Day}";
                HomeTown.Text = (((MainWindowViewModel)DataContext).Selected as Musician).HomeTown;
                Country.Text = (((MainWindowViewModel)DataContext).Selected as Musician).Country;
                Gender.Text = (((MainWindowViewModel)DataContext).Selected as Musician).Gender;
                RecordLabelId.Text = (((MainWindowViewModel)DataContext).Selected as Musician).RecordLabelId.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Selector.SelectedItem.ToString() == "Musicians")
            {
                ((MainWindowViewModel)DataContext).MusicianConvert(Selector.SelectedItem.ToString(), Name.Text, Age.Text, HomeTown.Text, Country.Text, DateOfBirth.Text, Gender.Text, RecordLabelId.Text);
            }
        }
    }
}
