using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Unity;
using TextConverter.Models;
using TextConverter.Views;

namespace TextConverter.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        [Dependency]
        public MyAppSettings Settings { get; set; }
        public Configuration Configuration => Settings.Configuration;

        public DelegateCommand TopMostCommand { get; }
        public DelegateCommand FileModeCommand { get; }
        public DelegateCommand FolderModeCommand { get; }
        public DelegateCommand ConfigurationCommand { get; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            TopMostCommand = new DelegateCommand(() =>
            {
                Configuration.TopMost = !Application.Current.MainWindow.Topmost;
                Application.Current.MainWindow.Topmost = Configuration.TopMost;
            });
            FileModeCommand = new DelegateCommand(() =>
            {
                Configuration.IsFolderMode = false;
                regionManager.RequestNavigate("MainWindowContentRegion", nameof(RegexView));
            });
            FolderModeCommand = new DelegateCommand(() =>
            {
                Configuration.IsFolderMode = true;
                regionManager.RequestNavigate("MainWindowContentRegion", nameof(RegexView));
            });
            ConfigurationCommand = new DelegateCommand(() => regionManager.RequestNavigate("MainWindowContentRegion", nameof(ConfigurationView)));
        }
    }
}
