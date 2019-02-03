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
using System.Windows.Shapes;
using WCFServiceWebRole1.BE;

namespace UI
{

    /// <summary>
    /// Interaction logic for UserPermissen.xaml
    /// </summary>
    public partial class UserPermissen : Window
    {
        public List<permission> Permisses;
        public static List<string> listgruops { get {
                return WCFServiceWebRole1.BL.Bl_imp.Instance.FindGroups(MainWindow.Token);
            } }
        public string Id;
        public event EventHandler deligEven = delegate { };
        public UserPermissen(string token,string id)
        {
            Id = id;
            WCFServiceWebRole1.BL.Bl_imp.Instance.ShowPermission("",id);
            Permisses = new List<permission>(WCFServiceWebRole1.BL.Bl_imp.Instance.ShowPermission("", id).permisses);
            InitializeComponent();
            title.Content = id.ToString();
            dataGrid1.ItemsSource = Permisses;
            groupList.ItemsSource = listgruops;
        }
        public void close() {
            EventHandler handler = deligEven;
            handler(new object(),new EventArgs());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Permisses.Remove((from x in Permisses where x.Name == (sender as Button).Tag.ToString() select x).FirstOrDefault());
            WCFServiceWebRole1.BL.Bl_imp.Instance.ReplacePermission("", Permisses, Id);
            dataGrid1.Items.Refresh();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string select = (groupList.SelectedValue as string);
            if (select == null) { MessageBox.Show("not selected"); return; }
            if (Permisses.Exists(x => x.Name == select)) { MessageBox.Show("the value is exists"); return; }
            Permisses.Add(new permission(select, def.NewIdGroup.Clone() as bool[]));
            WCFServiceWebRole1.BL.Bl_imp.Instance.ReplacePermission("", Permisses, Id);
            dataGrid1.Items.Refresh();
        }
    }
}
