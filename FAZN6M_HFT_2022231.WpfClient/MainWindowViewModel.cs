using FAZN6M_HFT_2022231.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.DirectoryServices;
using System.Linq;
using System.Reflection;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace FAZN6M_HFT_2022231.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient, INotifyPropertyChanged
    {
        public RestCollection<Musician> Musicians { get; set; }
        public RestCollection<Album> Albums { get; set; }
        public RestCollection<Track> Tracks { get; set; }
        public RestCollection<RecordLabel> RecordLabels { get; set; }

        public bool canProceed = true;

        private object selected;

        public object Selected
        {
            get { return selected; }
            set 
            {
                if (value != null && value is Musician)
                {
                    Musician help = (value as Musician);
                    selected = new Musician()
                    {
                        Name=help.Name,
                        MusicianId=help.MusicianId,
                        Age=help.Age,
                        Country = help.Country,
                        Tracks = help.Tracks,
                        Albums = help.Albums,
                        HomeTown = help.HomeTown,
                        RecordLabelId = help.RecordLabelId,
                        RecordLabel = help.RecordLabel,
                        DateOfBirth = help.DateOfBirth,
                        Gender = help.Gender
                    };

                    OnPropertyChanged(nameof(Selected));
                    (DeleteCommand as RelayCommand).NotifyCanExecuteChanged();
                }
                else if (value != null && value is Track)
                {
                    Track help = (value as Track);
                    selected = new Track()
                    {
                        Name = help.Name,
                        TrackId = help.TrackId,
                        MusicianId = help.MusicianId,
                        Length = help.Length,
                        AlbumId = help.AlbumId,
                        Musician=help.Musician
                    };

                    OnPropertyChanged(nameof(Selected));
                    (DeleteCommand as RelayCommand).NotifyCanExecuteChanged();
                }
                else if (value != null && value is Album)
                {
                    Album help = (value as Album);
                    selected = new Album()
                    {
                        Name = help.Name,
                        AlbumId = help.AlbumId,
                        MusicianId = help.MusicianId,
                        YearOfRelease = help.YearOfRelease,
                        NumberOfTracks = help.NumberOfTracks,
                        Musician = help.Musician
                    };

                    OnPropertyChanged(nameof(Selected));
                    (DeleteCommand as RelayCommand).NotifyCanExecuteChanged();
                }
                else if (value != null && value is RecordLabel)
                {
                    RecordLabel help = (value as RecordLabel);
                    selected = new RecordLabel()
                    {
                        Name = help.Name,
                        RecordLabelId = help.RecordLabelId,
                        YearOfFoundation  = help.YearOfFoundation,
                        Country = help.Country,
                        Headquarters = help.Headquarters,
                        Musicians = help.Musicians

                    };

                    OnPropertyChanged(nameof(Selected));
                    (DeleteCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public void MusicianConvert(string selectedTable, string name, string age, string homeTown, string country, string dateOfBirth, string gender, string recordLabelId)
        {
            Musician origanal = (Selected as Musician);
            Selected = new Musician();
            (Selected as Musician).MusicianId = origanal.MusicianId;
            if(name!="")
            {
                (Selected as Musician).Name = name;
            }
            else
            {
                MessageBox.Show("Every Musician has a name, please fill out the name field!");
                Selected = origanal;
                canProceed = false;
                return;
            }
            if(age!="")
            {
                try
                {
                    (Selected as Musician).Age = int.Parse(age);
                }
                catch (ArgumentException e)
                {
                    MessageBox.Show(e.Message);
                    Selected = origanal;
                    canProceed = false;
                    return;
                }
            }
            else
            {
                MessageBox.Show("Every Musician has an Age, please fill out the name field!");
            }
            ;
            (Selected as Musician).HomeTown = homeTown;
            (Selected as Musician).Country = country;
            if(dateOfBirth!="")
            {
                try
                {
                    if((Selected as Musician).Age == DateTime.Today.Year - DateTime.Parse(dateOfBirth).Year)
                    {
                        (Selected as Musician).DateOfBirth = DateTime.Parse(dateOfBirth);
                    }
                    else
                    {
                        MessageBox.Show("The given birth is not inline with the age of the Musician, please add the correct date!");
                        Selected = origanal;
                        canProceed = false;
                        return;
                    }
                    
                }
                catch (FormatException e)
                {
                    MessageBox.Show("The given date format is incorrect, please use this template and try again! (YYYY-MM-DD)");
                    Selected = origanal;
                    canProceed = false;
                    return;
                }
            }
            (Selected as Musician).Gender = gender;
            if(recordLabelId!="")
            {
                var recordLabelIds = RecordLabels.Select(recordLabel => recordLabel.RecordLabelId).ToList();
                if (recordLabelIds.Contains((int.Parse(recordLabelId))))
                {
                    (Selected as Musician).RecordLabelId = int.Parse(recordLabelId);
                }
                else
                {
                    MessageBox.Show("The given RecordLabelId doesn't exist, please try again with a different number!");
                    Selected = origanal;
                    canProceed = false;
                    return;
                }
            }
            canProceed = true;
        }
        public ICommand CreateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public MainWindowViewModel()
        {
            
            if(!IsInDesignMode)
            {
                Musicians = new RestCollection<Musician>("http://localhost:34694/", "musician");
                Albums = new RestCollection<Album>("http://localhost:34694/", "album");
                Tracks = new RestCollection<Track>("http://localhost:34694/", "track");
                RecordLabels = new RestCollection<RecordLabel>("http://localhost:34694/", "recordlabel");

                CreateCommand = new RelayCommand(() =>
                {
                    if (Selected is Musician)
                    {
                            Musicians.Add(new Musician()
                            {
                                Name = (Selected as Musician).Name,
                                Age = (Selected as Musician).Age,
                                DateOfBirth = (Selected as Musician).DateOfBirth,
                                HomeTown = (Selected as Musician).HomeTown,
                                Country = (Selected as Musician).Country,
                                Gender = (Selected as Musician).Gender,
                                RecordLabelId = (Selected as Musician).RecordLabelId
                            });
                    }
                    else if (Selected is Track)
                    {
                        Tracks.Add(new Track()
                        {
                            Name = (Selected as Track).Name,
                            Length = 120,
                            MusicianId=1
                        });
                    }
                    else if (Selected is Album)
                    {
                        Albums.Add(new Album()
                        {
                            Name = (Selected as Album).Name,
                            MusicianId = 1
                        });
                    }
                    else if (Selected is RecordLabel)
                    {
                        RecordLabels.Add(new RecordLabel()
                        {
                            Name = (Selected as RecordLabel).Name,
                        });
                    }

                },
                () => canProceed);

                UpdateCommand = new RelayCommand(() =>
                {
                    if(Selected is Musician)
                    {

                        Musicians.Update(Selected as Musician);
 
                    }
                    else if (Selected is Track)
                    {
                        Tracks.Update(Selected as Track);
                    }
                    else if (Selected is Album)
                    {
                        Albums.Update(Selected as Album);
                    }
                    else if (Selected is RecordLabel)
                    {
                        RecordLabels.Update(Selected as RecordLabel);
                    }
                },
                () => canProceed);

                DeleteCommand = new RelayCommand(() =>
                {
                    if(Selected is Musician)
                    {
                        Musicians.Delete((Selected as Musician).MusicianId);
                    }
                    else if (Selected is Track)
                    {
                        Tracks.Delete((Selected as Track).TrackId);
                    }
                    else if (Selected is Album)
                    {
                        Albums.Delete((Selected as Album).AlbumId);
                    }
                    else if (Selected is RecordLabel)
                    {
                        RecordLabels.Delete((Selected as RecordLabel).RecordLabelId);
                    }
                },
                () => Selected !=null);
            }
        }
    }
}
