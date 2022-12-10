using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using TextConverter.Models;

namespace TextConverter.Models
{
    public class MyAppSettings
    {
        public static readonly IList<Type> KnownTypes = new[] { typeof(DirectoryItem), typeof(FileItem) };
        public static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string ConfigPath = BaseDirectory + "config.dat";
        public static readonly string DefaultDataDirectory = BaseDirectory + "data" + Path.DirectorySeparatorChar;
        public static readonly double DefaultWindowWidth = 1000;
        public static readonly double DefaultWindowHeight = 600;

        public Configuration Configuration { get; set; } = new Configuration();
        public ActionInformation ActionInformation { get; set; } = new ActionInformation();
    }
}
