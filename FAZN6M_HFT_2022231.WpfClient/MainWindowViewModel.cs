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
using System.Windows.Input;

namespace FAZN6M_HFT_2022231.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<Musician> Musicians { get; set; }
        public RestCollection<Album> Albums { get; set; }
        public RestCollection<Track> Tracks { get; set; }
        public RestCollection<RecordLabel> RecordLabels { get; set; }

        private Musician selectedMusician;

        public Musician SelectedMusician
        {
            get { return selectedMusician; }
            set 
            {
                if (value != null)
                {
                    selectedMusician = new Musician()
                    {
                        Name=value.Name,
                        MusicianId=value.MusicianId,
                        Age=value.Age,
                        Country = value.Country,
                        Tracks = value.Tracks,
                        Albums = value.Albums,
                        HomeTown = value.HomeTown,
                        RecordLabelId = value.RecordLabelId,
                        RecordLabel = value.RecordLabel,
                        DateOfBirth = value.DateOfBirth,
                        Gender = value.Gender
                    };
                    OnPropertyChanged();
                    (DeleteMusicianCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateMusicianCommand { get; set; }
        public ICommand DeleteMusicianCommand { get; set; }
        public ICommand UpdateMusicianCommand { get; set; }

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

                CreateMusicianCommand = new RelayCommand(() =>
                {
                    Musicians.Add(new Musician()
                    {
                        Name = SelectedMusician.Name,
                        Age = 20
                    }); ;
                });

                UpdateMusicianCommand = new RelayCommand(() =>
                {
                    
                    Musicians.Update(SelectedMusician);
                    
                });

                DeleteMusicianCommand = new RelayCommand(() =>
                {
                    Musicians.Delete(SelectedMusician.MusicianId);
                },
                () =>
                {
                    return SelectedMusician != null;
                });
                SelectedMusician = new Musician();
            }
        }
    }
}
