using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;
using Unity;
using TextConverter.WPF.Models;

namespace TextConverter.WPF.ViewModels
{
    class RegexViewModel : BindableBase
    {
        [Dependency]
        public MyAppSettings Settings { get; set; }
        public Configuration Configuration => Settings.Configuration;
        public ActionInformation ActionInformation => Settings.ActionInformation;

        public DelegateCommand<RoutedPropertyChangedEventArgs<object>> SelectedItemChangedCommand { get; }

        public RegexViewModel(IUnityContainer container)
        {
            Settings = (MyAppSettings)container.Resolve(typeof(MyAppSettings));

            SelectedItemChangedCommand = new DelegateCommand<RoutedPropertyChangedEventArgs<object>>(e =>
            {
                if (e.NewValue is FileItem item)
                {
                    ActionInformation.InputPath = item.Path;
                }
            });
        }
    }
}
