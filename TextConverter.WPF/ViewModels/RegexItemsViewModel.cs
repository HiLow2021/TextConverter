using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Unity;
using TextConverter.WPF.Models;
using My.Extensions;

namespace TextConverter.WPF.ViewModels
{
    class RegexItemsViewModel : BindableBase
    {
        [Dependency]
        public MyAppSettings Settings { get; set; }
        public Configuration Configuration => Settings.Configuration;
        public ActionInformation ActionInformation => Settings.ActionInformation;
        public ObservableCollection<ReplaceItem> Patterns => ActionInformation.Patterns;
        public int SelectedIndex => ActionInformation.SelectedPatternIndex;

        public DelegateCommand AddCommand { get; }
        public DelegateCommand RemoveCommand { get; }
        public DelegateCommand ClearCommand { get; }

        public RegexItemsViewModel() { }

        public RegexItemsViewModel(IUnityContainer container)
        {
            Settings = (MyAppSettings)container.Resolve(typeof(MyAppSettings));

            AddCommand = new DelegateCommand(() =>
            {
                var selectedIndex = ActionInformation.SelectedPatternIndex;

                if (selectedIndex >= 0)
                {
                    Patterns.Insert(selectedIndex + 1, new ReplaceItem());
                    ActionInformation.SelectedPatternIndex = selectedIndex;
                }
                else
                {
                    Patterns.Add(new ReplaceItem());
                }
            });
            RemoveCommand = new DelegateCommand(() =>
            {
                var selectedIndex = ActionInformation.SelectedPatternIndex;

                if (selectedIndex >= 0)
                {
                    Patterns.RemoveAt(selectedIndex);
                    ActionInformation.SelectedPatternIndex = selectedIndex == Patterns.Count ? selectedIndex - 1 : selectedIndex;
                }
                else if (Patterns.Count > 0)
                {
                    Patterns.RemoveAt(Patterns.Count - 1);
                }
            });
            ClearCommand = new DelegateCommand(() => Patterns.Clear());
        }
    }
}
