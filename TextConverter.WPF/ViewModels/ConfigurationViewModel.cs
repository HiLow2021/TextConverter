using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;
using Unity;
using TextConverter.WPF.Models;

namespace TextConverter.WPF.ViewModels
{
    class ConfigurationViewModel : BindableBase
    {
        [Dependency]
        public MyAppSettings Settings { get; set; }
        public Configuration Configuration => Settings.Configuration;
        public ActionInformation ActionInformation => Settings.ActionInformation;

        public DelegateCommand InitializeSizeCommand { get; }

        public ConfigurationViewModel()
        {
            InitializeSizeCommand = new DelegateCommand(() =>
            {
                Application.Current.MainWindow.Width = MyAppSettings.DefaultWindowWidth;
                Application.Current.MainWindow.Height = MyAppSettings.DefaultWindowHeight;
            });
        }
    }
}
