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

        TextBox MusicianName = new TextBox();
        TextBox Age = new TextBox();
        TextBox DateOfBirth = new TextBox();
        TextBox HomeTown = new TextBox();
        TextBox MusicianCountry = new TextBox();
        TextBox Gender = new TextBox();
        TextBox MusicianRecordLabelId = new TextBox();

        TextBox TrackName = new TextBox();
        TextBox Length = new TextBox();
        TextBox TrackMusicianId = new TextBox();
        TextBox TrackAlbumId = new TextBox();

        TextBox AlbumName = new TextBox();
        TextBox AlbumMusicianId = new TextBox();
        TextBox YearOfRelease = new TextBox();
        TextBox NumberOfTracks = new TextBox();

        TextBox RecordLabelName = new TextBox();
        TextBox YearOfFoundation = new TextBox();
        TextBox RecordLabelCountry = new TextBox();
        TextBox Headquarters = new TextBox();

        public MainWindow()
        {
            InitializeComponent();

            Selector.Items.Add("Musicians");
            Selector.Items.Add("Tracks");
            Selector.Items.Add("Albums");
            Selector.Items.Add("Record Labels");
            Selector.Items.Add("Non CRUD methods");

            Age.PreviewTextInput += TextBox_PreviewTextInput;
            MusicianRecordLabelId.PreviewTextInput += TextBox_PreviewTextInput;

            Length.PreviewTextInput += TextBox_PreviewTextInput;
            TrackMusicianId.PreviewTextInput += TextBox_PreviewTextInput;
            TrackAlbumId.PreviewTextInput += TextBox_PreviewTextInput;

            AlbumMusicianId.PreviewTextInput += TextBox_PreviewTextInput;
            YearOfRelease.PreviewTextInput += TextBox_PreviewTextInput;
            NumberOfTracks.PreviewTextInput += TextBox_PreviewTextInput;

            YearOfFoundation.PreviewTextInput += TextBox_PreviewTextInput;

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

                DetailStackText.Children.Add(MusicianName);
                DetailStackText.Children.Add(Age);
                DetailStackText.Children.Add(DateOfBirth);
                DetailStackText.Children.Add(HomeTown);
                DetailStackText.Children.Add(MusicianCountry);
                DetailStackText.Children.Add(Gender);
                DetailStackText.Children.Add(MusicianRecordLabelId);
            }
            else if (Selector.SelectedItem.ToString() == "Albums")
            {
                ((MainWindowViewModel)DataContext).Selected = new Album();
                DetailStackLabel.Children.Clear();
                DetailStackText.Children.Clear();
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


                DetailStackText.Children.Add(AlbumName);
                DetailStackText.Children.Add(AlbumMusicianId);
                DetailStackText.Children.Add(YearOfRelease);
                DetailStackText.Children.Add(NumberOfTracks);
            }
            else if (Selector.SelectedItem.ToString() == "Tracks")
            {
                ((MainWindowViewModel)DataContext).Selected = new Track();
                DetailStackLabel.Children.Clear();
                DetailStackText.Children.Clear();
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

                DetailStackText.Children.Add(TrackName);
                DetailStackText.Children.Add(Length);
                DetailStackText.Children.Add(TrackMusicianId);
                DetailStackText.Children.Add(TrackAlbumId);

            }
            else if (Selector.SelectedItem.ToString() == "Record Labels")
            {
                ((MainWindowViewModel)DataContext).Selected = new RecordLabel();
                DetailStackLabel.Children.Clear();
                DetailStackText.Children.Clear();
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

                DetailStackText.Children.Add(RecordLabelName);
                DetailStackText.Children.Add(YearOfFoundation);
                DetailStackText.Children.Add(RecordLabelCountry);
                DetailStackText.Children.Add(Headquarters);

            }
        }

        private void TableList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (Selector.SelectedItem.ToString() == "Musicians")
            {
                
                MusicianName.Text = (((MainWindowViewModel)DataContext).Selected as Musician).Name;
                Age.Text = (((MainWindowViewModel)DataContext).Selected as Musician).Age.ToString();
                DateOfBirth.Text = $"{(((MainWindowViewModel)DataContext).Selected as Musician).DateOfBirth.Year}-{(((MainWindowViewModel)DataContext).Selected as Musician).DateOfBirth.Month}-{(((MainWindowViewModel)DataContext).Selected as Musician).DateOfBirth.Day}";
                HomeTown.Text = (((MainWindowViewModel)DataContext).Selected as Musician).HomeTown;
                MusicianCountry.Text = (((MainWindowViewModel)DataContext).Selected as Musician).Country;
                Gender.Text = (((MainWindowViewModel)DataContext).Selected as Musician).Gender;
                MusicianRecordLabelId.Text = (((MainWindowViewModel)DataContext).Selected as Musician).RecordLabelId.ToString();
            }
            else if (Selector.SelectedItem.ToString() == "Albums")
            {
                AlbumName.Text= (((MainWindowViewModel)DataContext).Selected as Album).Name;
                AlbumMusicianId.Text = (((MainWindowViewModel)DataContext).Selected as Album).MusicianId.ToString();
                YearOfRelease.Text = (((MainWindowViewModel)DataContext).Selected as Album).YearOfRelease.ToString();
                NumberOfTracks.Text = (((MainWindowViewModel)DataContext).Selected as Album).NumberOfTracks.ToString();
            }
            else if (Selector.SelectedItem.ToString() == "Tracks")
            {
                TrackName.Text = (((MainWindowViewModel)DataContext).Selected as Track).Name;
                Length.Text = (((MainWindowViewModel)DataContext).Selected as Track).Length.ToString();
                TrackMusicianId.Text = (((MainWindowViewModel)DataContext).Selected as Track).MusicianId.ToString();
                TrackAlbumId.Text = (((MainWindowViewModel)DataContext).Selected as Track).AlbumId.ToString();
            }
            else if (Selector.SelectedItem.ToString() == "Record Labels")
            {
                RecordLabelName.Text = (((MainWindowViewModel)DataContext).Selected as RecordLabel).Name;
                YearOfFoundation.Text = (((MainWindowViewModel)DataContext).Selected as RecordLabel).YearOfFoundation.ToString();
                RecordLabelCountry.Text = (((MainWindowViewModel)DataContext).Selected as RecordLabel).Country;
                Headquarters.Text = (((MainWindowViewModel)DataContext).Selected as RecordLabel).Headquarters;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Selector.SelectedItem.ToString() == "Musicians")
            {
                ((MainWindowViewModel)DataContext).MusicianConvert(MusicianName.Text, Age.Text, HomeTown.Text, MusicianCountry.Text, DateOfBirth.Text, Gender.Text, MusicianRecordLabelId.Text);
            }
            else if(Selector.SelectedItem.ToString() == "Albums")
            {
                ((MainWindowViewModel)DataContext).AlbumConvert(AlbumName.Text, AlbumMusicianId.Text, YearOfRelease.Text, NumberOfTracks.Text);
            }
            else if (Selector.SelectedItem.ToString() == "Tracks")
            {
                ((MainWindowViewModel)DataContext).TrackConvert(TrackName.Text, Length.Text, TrackMusicianId.Text, TrackAlbumId.Text);
            }
            else if (Selector.SelectedItem.ToString() == "Record Labels")
            {
                ((MainWindowViewModel)DataContext).RecordLabelConvert(RecordLabelName.Text, YearOfFoundation.Text, RecordLabelCountry.Text, Headquarters.Text);
            }
        }
    }
}
