using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextConverter.WPF.Views
{
    /// <summary>
    /// RegexItemsView.xaml の相互作用ロジック
    /// </summary>
    public partial class RegexItemsView : UserControl
    {
        public RegexItemsView()
        {
            InitializeComponent();
        }

        protected void ListBoxItem_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            (sender as ListBoxItem).IsSelected = true;
        }
    }
}
