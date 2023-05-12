using FAZN6M_HFT_2022231.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FAZN6M_HFT_2022231.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient, INotifyPropertyChanged
    {
        public RestCollection<Musician> Musicians { get; set; }
        public RestCollection<Album> Albums { get; set; }
        public RestCollection<Track> Tracks { get; set; }
        public RestCollection<RecordLabel> RecordLabels { get; set; }

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

        public ICommand CreateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public bool CanDelete()
        {
            return Selected != null;
        }


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
                            Age = 20
                        }); ;
                    }
                    else if (Selected is Track)
                    {
                        Tracks.Add(new Track()
                        {
                            Name = (Selected as Track).Name,
                            Length = 120,
                            MusicianId=1
                        }); ;
                    }
                    else if (Selected is Album)
                    {
                        Albums.Add(new Album()
                        {
                            Name = (Selected as Album).Name,
                            MusicianId = 1
                        }); ;
                    }
                    else if (Selected is RecordLabel)
                    {
                        RecordLabels.Add(new RecordLabel()
                        {
                            Name = (Selected as RecordLabel).Name,
                        }); ;
                    }

                });

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
                });

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
                },CanDelete);
            }
        }
    }
}
