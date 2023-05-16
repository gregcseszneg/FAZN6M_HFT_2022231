using FAZN6M_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FAZN6M_HFT_2022231.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button create;
        Button delete;
        Button update;

        TextBox musicianName;
        TextBox age;
        TextBox dateOfBirth;
        TextBox homeTown;
        TextBox musicianCountry;
        TextBox gender;
        TextBox musicianRecordLabelId;

        TextBox trackName;
        TextBox length;
        TextBox trackMusicianId;
        TextBox trackAlbumId;

        TextBox albumName;
        TextBox albumMusicianId;
        TextBox yearOfRelease;
        TextBox numberOfTracks;

        TextBox recordLabelName;
        TextBox yearOfFoundation;
        TextBox recordLabelCountry;
        TextBox headquarters;

        TextBox constraintText;
        Label constraintLabel;
        Button nonCrudButton;
        ListBox nonCrudResult;


        List<string> nameOfTheNonCruds;
        public MainWindow()
        {
            InitializeComponent();

            nameOfTheNonCruds = new List<string>()
                {
                    "Musicians From a Record Label",
                    "Musician Average Age In The Record Labels",
                    "Tracks From Musicians Who Born After",
                    "Sum Of Music Length Per Musician",
                    "Musicians Who Has Longer Song Than"
                };

            Selector.Items.Add("Musicians");
            Selector.Items.Add("Tracks");
            Selector.Items.Add("Albums");
            Selector.Items.Add("Record Labels");
            Selector.Items.Add("Non CRUD methods");

            musicianName = new TextBox();
            age = new TextBox();
            dateOfBirth = new TextBox();
            homeTown = new TextBox();
            musicianCountry = new TextBox();
            gender = new TextBox();
            musicianRecordLabelId = new TextBox();

            trackName = new TextBox();
            length = new TextBox();
            trackMusicianId = new TextBox();
            trackAlbumId = new TextBox();

            albumName = new TextBox();
            albumMusicianId = new TextBox();
            yearOfRelease = new TextBox();
            numberOfTracks = new TextBox();

            recordLabelName = new TextBox();
            yearOfFoundation = new TextBox();
            recordLabelCountry = new TextBox();
            headquarters = new TextBox();

            constraintText = new TextBox();
            constraintLabel = new Label();
            constraintText.Margin = new Thickness(10);
            constraintText.Padding = new Thickness(10);
            constraintLabel.Margin = new Thickness(10);
            constraintLabel.Padding = new Thickness(0,10,0,10);
            nonCrudButton = new Button();
            nonCrudButton.Content = "Result";
            nonCrudResult = new ListBox();

            create = new Button();
            delete = new Button();
            update = new Button();
            create.Content = "Create";
            delete.Content = "Delete";
            update.Content = "Update";
            create.Command = ((MainWindowViewModel)DataContext).CreateCommand;
            delete.Command = ((MainWindowViewModel)DataContext).DeleteCommand;
            update.Command = ((MainWindowViewModel)DataContext).UpdateCommand;
            create.Click += Button_Click;
            update.Click += Button_Click;
            nonCrudButton.Click += Button_Click;
            nonCrudButton.Margin = new Thickness(10);
            nonCrudButton.Padding = new Thickness(10);
            create.Margin = new Thickness(10);
            create.Padding = new Thickness(10);
            delete.Margin = new Thickness(10);
            delete.Padding = new Thickness(10);
            update.Margin = new Thickness(10);
            update.Padding = new Thickness(10);

            age.PreviewTextInput += TextBox_PreviewTextInput;
            musicianRecordLabelId.PreviewTextInput += TextBox_PreviewTextInput;

            length.PreviewTextInput += TextBox_PreviewTextInput;
            trackMusicianId.PreviewTextInput += TextBox_PreviewTextInput;
            trackAlbumId.PreviewTextInput += TextBox_PreviewTextInput;

            albumMusicianId.PreviewTextInput += TextBox_PreviewTextInput;
            yearOfRelease.PreviewTextInput += TextBox_PreviewTextInput;
            numberOfTracks.PreviewTextInput += TextBox_PreviewTextInput;

            yearOfFoundation.PreviewTextInput += TextBox_PreviewTextInput;
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
            if (Selector.SelectedItem.ToString() == "Non CRUD methods")
            {
                CommandStack.Children.Clear();
                DetailStackLabel.Children.Clear();
                DetailStackText.Children.Clear();
                TableList.ItemsSource = nameOfTheNonCruds;
                TableList.ItemTemplate = null;
                TableList.SelectedItem = null;

                CommandStack.Children.Add(nonCrudButton);
                DetailStackLabel.Children.Add(constraintLabel);
            }
            else
            {
                CommandStack.Children.Clear();
                TableList.ItemTemplate = (DataTemplate)Resources["TableTemplate"];
                CommandStack.Children.Add(create);
                CommandStack.Children.Add(delete);
                CommandStack.Children.Add(update);

                if (Selector.SelectedItem.ToString() == "Musicians")
                {
                    ((MainWindowViewModel)DataContext).Selected = new Musician();
                    DetailStackLabel.Children.Clear();
                    DetailStackText.Children.Clear();
                    TableList.ItemsSource = ((MainWindowViewModel)DataContext).Musicians;

                    var properties = typeof(Musician).GetProperties().Where(p => !Attribute.IsDefined(p, typeof(RequiredAttribute)) && !p.GetGetMethod().IsVirtual);
                    var reqProperties = typeof(Musician).GetProperties().Where(p => Attribute.IsDefined(p, typeof(RequiredAttribute))).Where(p => !Attribute.IsDefined(p, typeof(KeyAttribute)));


                    foreach (var prop in reqProperties)
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

                    DetailStackText.Children.Add(musicianName);
                    DetailStackText.Children.Add(age);
                    DetailStackText.Children.Add(dateOfBirth);
                    DetailStackText.Children.Add(homeTown);
                    DetailStackText.Children.Add(musicianCountry);
                    DetailStackText.Children.Add(gender);
                    DetailStackText.Children.Add(musicianRecordLabelId);
                }
                else if (Selector.SelectedItem.ToString() == "Albums")
                {
                    ((MainWindowViewModel)DataContext).Selected = new Album();
                    DetailStackLabel.Children.Clear();
                    DetailStackText.Children.Clear();
                    TableList.ItemsSource = ((MainWindowViewModel)DataContext).Albums;

                    var properties = typeof(Album).GetProperties().Where(p => !Attribute.IsDefined(p, typeof(RequiredAttribute)) && !p.GetGetMethod().IsVirtual);
                    var reqProperties = typeof(Album).GetProperties().Where(p => Attribute.IsDefined(p, typeof(RequiredAttribute))).Where(p => !Attribute.IsDefined(p, typeof(KeyAttribute)));

                    foreach (var prop in reqProperties)
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

                    DetailStackText.Children.Add(albumName);
                    DetailStackText.Children.Add(albumMusicianId);
                    DetailStackText.Children.Add(yearOfRelease);
                    DetailStackText.Children.Add(numberOfTracks);
                }
                else if (Selector.SelectedItem.ToString() == "Tracks")
                {
                    ((MainWindowViewModel)DataContext).Selected = new Track();
                    DetailStackLabel.Children.Clear();
                    DetailStackText.Children.Clear();
                    TableList.ItemsSource = ((MainWindowViewModel)DataContext).Tracks;

                    var properties = typeof(Track).GetProperties().Where(p => !Attribute.IsDefined(p, typeof(RequiredAttribute)) && !p.GetGetMethod().IsVirtual);
                    var reqProperties = typeof(Track).GetProperties().Where(p => Attribute.IsDefined(p, typeof(RequiredAttribute))).Where(p => !Attribute.IsDefined(p, typeof(KeyAttribute)));

                    foreach (var prop in reqProperties)
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

                    DetailStackText.Children.Add(trackName);
                    DetailStackText.Children.Add(length);
                    DetailStackText.Children.Add(trackMusicianId);
                    DetailStackText.Children.Add(trackAlbumId);

                }
                else if (Selector.SelectedItem.ToString() == "Record Labels")
                {
                    ((MainWindowViewModel)DataContext).Selected = new RecordLabel();
                    DetailStackLabel.Children.Clear();
                    DetailStackText.Children.Clear();
                    TableList.ItemsSource = ((MainWindowViewModel)DataContext).RecordLabels;

                    var properties = typeof(RecordLabel).GetProperties().Where(p => !Attribute.IsDefined(p, typeof(RequiredAttribute)) && !p.GetGetMethod().IsVirtual);
                    var reqProperties = typeof(RecordLabel).GetProperties().Where(p => Attribute.IsDefined(p, typeof(RequiredAttribute))).Where(p => !Attribute.IsDefined(p, typeof(KeyAttribute)));

                    foreach (var prop in reqProperties)
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
                    DetailStackText.Children.Add(recordLabelName);
                    DetailStackText.Children.Add(yearOfFoundation);
                    DetailStackText.Children.Add(recordLabelCountry);
                    DetailStackText.Children.Add(headquarters);
                }
            }
        }

        private void TableList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (Selector.SelectedItem.ToString() == "Musicians")
            {
                musicianName.Text = (((MainWindowViewModel)DataContext).Selected as Musician).Name;
                age.Text = (((MainWindowViewModel)DataContext).Selected as Musician).Age.ToString();
                dateOfBirth.Text = $"{(((MainWindowViewModel)DataContext).Selected as Musician).DateOfBirth.Year}-{(((MainWindowViewModel)DataContext).Selected as Musician).DateOfBirth.Month}-{(((MainWindowViewModel)DataContext).Selected as Musician).DateOfBirth.Day}";
                homeTown.Text = (((MainWindowViewModel)DataContext).Selected as Musician).HomeTown;
                musicianCountry.Text = (((MainWindowViewModel)DataContext).Selected as Musician).Country;
                gender.Text = (((MainWindowViewModel)DataContext).Selected as Musician).Gender;
                musicianRecordLabelId.Text = (((MainWindowViewModel)DataContext).Selected as Musician).RecordLabelId.ToString();
            }
            else if (Selector.SelectedItem.ToString() == "Albums")
            {
                albumName.Text = (((MainWindowViewModel)DataContext).Selected as Album).Name;
                albumMusicianId.Text = (((MainWindowViewModel)DataContext).Selected as Album).MusicianId.ToString();
                yearOfRelease.Text = (((MainWindowViewModel)DataContext).Selected as Album).YearOfRelease.ToString();
                numberOfTracks.Text = (((MainWindowViewModel)DataContext).Selected as Album).NumberOfTracks.ToString();
            }
            else if (Selector.SelectedItem.ToString() == "Tracks")
            {
                trackName.Text = (((MainWindowViewModel)DataContext).Selected as Track).Name;
                length.Text = (((MainWindowViewModel)DataContext).Selected as Track).Length.ToString();
                trackMusicianId.Text = (((MainWindowViewModel)DataContext).Selected as Track).MusicianId.ToString();
                trackAlbumId.Text = (((MainWindowViewModel)DataContext).Selected as Track).AlbumId.ToString();
            }
            else if (Selector.SelectedItem.ToString() == "Record Labels")
            {
                recordLabelName.Text = (((MainWindowViewModel)DataContext).Selected as RecordLabel).Name;
                yearOfFoundation.Text = (((MainWindowViewModel)DataContext).Selected as RecordLabel).YearOfFoundation.ToString();
                recordLabelCountry.Text = (((MainWindowViewModel)DataContext).Selected as RecordLabel).Country;
                headquarters.Text = (((MainWindowViewModel)DataContext).Selected as RecordLabel).Headquarters;
            }
            else if (Selector.SelectedItem.ToString() == "Non CRUD methods")
            {
                DetailStackText.PreviewTextInput -= TextBox_PreviewTextInput;
                if (TableList.SelectedItem.ToString() == "Musicians From a Record Label")
                {
                    if (!DetailStackLabel.Children.Contains(constraintLabel))
                    {
                        DetailStackLabel.Children.Add(constraintLabel);
                    }
                    DetailStackText.Children.Clear();
                    DetailStackText.Children.Add(constraintText);

                    constraintLabel.Content = "Record Label Name:";
                    constraintText.Text = "";
                    nonCrudResult.ItemsSource = null;
                }
                else if (TableList.SelectedItem.ToString() == "Musician Average Age In The Record Labels")
                {
                    DetailStackText.Children.Clear();
                    DetailStackLabel.Children.Clear();
                    nonCrudResult.ItemsSource = null;
                }
                else if (TableList.SelectedItem.ToString() == "Tracks From Musicians Who Born After")
                {
                    DetailStackText.PreviewTextInput += TextBox_PreviewTextInput;
                    if (!DetailStackLabel.Children.Contains(constraintLabel))
                    {
                        DetailStackLabel.Children.Add(constraintLabel);
                    }
                    DetailStackText.Children.Clear();
                    DetailStackText.Children.Add(constraintText);

                    constraintLabel.Content = "Year:";
                    constraintText.Text = "";
                    nonCrudResult.ItemsSource = null;
                }
                else if (TableList.SelectedItem.ToString() == "Sum Of Music Length Per Musician")
                {
                    DetailStackText.Children.Clear();
                    DetailStackLabel.Children.Clear();
                    nonCrudResult.ItemsSource = null;
                }
                else if (TableList.SelectedItem.ToString() == "Musicians Who Has Longer Song Than")
                {
                    DetailStackText.PreviewTextInput += TextBox_PreviewTextInput;
                    if (!DetailStackLabel.Children.Contains(constraintLabel))
                    {
                        DetailStackLabel.Children.Add(constraintLabel);
                    }
                    DetailStackText.Children.Clear();
                    DetailStackText.Children.Add(constraintText);

                    constraintLabel.Content = "Length in sec:";
                    constraintText.Text = "";
                    nonCrudResult.ItemsSource = null;
                }
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Selector.SelectedItem.ToString() == "Musicians")
            {
                ((MainWindowViewModel)DataContext).MusicianConvert(musicianName.Text, age.Text, homeTown.Text, musicianCountry.Text, dateOfBirth.Text, gender.Text, musicianRecordLabelId.Text);
            }
            else if (Selector.SelectedItem.ToString() == "Albums")
            {
                ((MainWindowViewModel)DataContext).AlbumConvert(albumName.Text, albumMusicianId.Text, yearOfRelease.Text, numberOfTracks.Text);
            }
            else if (Selector.SelectedItem.ToString() == "Tracks")
            {
                ((MainWindowViewModel)DataContext).TrackConvert(trackName.Text, length.Text, trackMusicianId.Text, trackAlbumId.Text);
            }
            else if (Selector.SelectedItem.ToString() == "Record Labels")
            {
                ((MainWindowViewModel)DataContext).RecordLabelConvert(recordLabelName.Text, yearOfFoundation.Text, recordLabelCountry.Text, headquarters.Text);
            }
            else if (Selector.SelectedItem.ToString() == "Non CRUD methods")
            {
                DetailStackText.Children.Remove(nonCrudResult);
                if (TableList.SelectedItem.ToString() == "Musicians From a Record Label")
                {
                    List<Musician> result =(((MainWindowViewModel)DataContext).MusiciansFromRecordLabel(constraintText.Text));
                    if(result.Count!=0)
                    {
                        nonCrudResult.ItemsSource = result;
                        DetailStackText.Children.Add(nonCrudResult);
                    }

                }
                else if (TableList.SelectedItem.ToString() == "Musician Average Age In The Record Labels")
                {
                    nonCrudResult.ItemsSource = (((MainWindowViewModel)DataContext).MusicianAverageAgeInTheRecordLabels);
                    DetailStackText.Children.Add(nonCrudResult);
                }
                else if (TableList.SelectedItem.ToString() == "Tracks From Musicians Who Born After")
                {
                    List<Track> result= (((MainWindowViewModel)DataContext).TracksFromMusicianBornAfter(constraintText.Text));
                    if (result.Count != 0)
                    {
                        nonCrudResult.ItemsSource = result;
                        DetailStackText.Children.Add(nonCrudResult);
                    }
                }
                else if (TableList.SelectedItem.ToString() == "Sum Of Music Length Per Musician")
                {
                    nonCrudResult.ItemsSource = (((MainWindowViewModel)DataContext).SumOfMusicLengthPerMusician);
                    DetailStackText.Children.Add(nonCrudResult);

                }
                else if (TableList.SelectedItem.ToString() == "Musicians Who Has Longer Song Than")
                {
                    List<Musician> result = (((MainWindowViewModel)DataContext).MusiciansWHoHasLongerSongThan(constraintText.Text));
                    if (result.Count != 0)
                    {
                        nonCrudResult.ItemsSource = result;
                        DetailStackText.Children.Add(nonCrudResult);
                    }
                }
            }
        }
    }
}
