using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Prism.Commands;
using Prism.Mvvm;
using Unity;
using TextConverter.Models;
using My.Collections;
using My.Extensions;

namespace TextConverter.ViewModels
{
    public class RegexControlViewModel : BindableBase
    {
        [Dependency]
        public MyAppSettings Settings { get; set; }
        public Configuration Configuration => Settings.Configuration;
        public ActionInformation ActionInformation => Settings.ActionInformation;

        public DelegateCommand OpenFileCommand { get; }
        public DelegateCommand ReloadCommand { get; }
        public DelegateCommand SaveFileCommand { get; }
        public DelegateCommand SaveFileWithEditCommand { get; }
        public DelegateCommand ConvertCommand { get; }

        public RegexControlViewModel() { }

        public RegexControlViewModel(IUnityContainer container)
        {
            Settings = (MyAppSettings)container.Resolve(typeof(MyAppSettings));

            OpenFileCommand = new DelegateCommand(() =>
            {
                if (Configuration.IsFolderMode)
                {
                    using (var folderBrowserDialog = new FolderBrowserDialog())
                    {
                        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        {
                            var root = new DirectoryItem(folderBrowserDialog.SelectedPath);

                            Recursive(root);

                            ActionInformation.RootDirectoryItems.Clear();
                            ActionInformation.RootDirectoryItems.Add(root);
                            ActionInformation.InputPath = string.Empty;
                        }
                    }

                    void Recursive(DirectoryItem directoryItem)
                    {
                        var directories = Directory.GetDirectories(directoryItem.Path).OrderBy(x => x, new NaturalSortComparer())
                                                                                      .Select(x => new DirectoryItem(x))
                                                                                      .ToArray();
                        var files = Directory.GetFiles(directoryItem.Path).Where(x => IsValidExtension(x))
                                                                          .OrderBy(x => x, new NaturalSortComparer())
                                                                          .Select(x => new FileItem(x)).ToArray();

                        foreach (var item in directories)
                        {
                            Recursive(item);
                            directoryItem.Directories.Add(item);
                        }

                        directoryItem.Files.AddRange(files);
                    }
                }
                else
                {
                    using (var openFileDialog = new OpenFileDialog
                    {
                        Title = "ファイルを開く",
                        Filter = GetFilter()
                    })
                    {
                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            ActionInformation.InputPath = openFileDialog.FileName;
                            ReloadCommand.Execute();
                        }
                    }
                }
            });
            ReloadCommand = new DelegateCommand(() =>
            {
                if (File.Exists(ActionInformation.InputPath))
                {
                    ActionInformation.InputDocument.Text = File.ReadAllText(ActionInformation.InputPath);
                }
            });
            SaveFileCommand = new DelegateCommand(() =>
            {
                File.WriteAllText(ActionInformation.OutputPath, ActionInformation.OutputDocument.Text);
            });
            SaveFileWithEditCommand = new DelegateCommand(() =>
            {
                using (var saveFileDialog = new SaveFileDialog
                {
                    Title = "ファイルを開く",
                    Filter = GetFilter()
                })
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(saveFileDialog.FileName, ActionInformation.OutputDocument.Text);
                    }
                }
            });
            ConvertCommand = new DelegateCommand(() =>
            {
                var textConverter = new Models.TextConverter()
                {
                    Source = ActionInformation.InputDocument.Text,
                    Items = ActionInformation.Patterns.Cast<ConvertItem>().ToArray(),
                };

                textConverter.Convert();
                ActionInformation.OutputDocument.Text = textConverter.Result;
            });
        }

        private string GetFilter()
        {
            var extensions = string.Join(";", Configuration.ValidExtensions.Select(x => @"*" + x));

            return $"テキスト形式({extensions})|{extensions}";
        }

        private bool IsValidExtension(string path)
        {
            var searchPattern = string.Join("|", Configuration.ValidExtensions.Select(x => @"\" + x));

            return Regex.IsMatch(path, $@"{searchPattern}$", RegexOptions.IgnoreCase);
        }
    }
}
