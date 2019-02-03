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
    /// Interaction logic for GroupsManage.xaml
    /// </summary>
    public partial class GroupsManage : Window
    {
        private static List<InterfaceAcsess> rightlist;
        private static List<InterfaceAcsess> lstMyObject = new List<InterfaceAcsess>();
        private static List<InterfaceAcsess> lstMyObjectRight = new List<InterfaceAcsess>();
        public static List<string> listgruops
        {
            get
            {
                return WCFServiceWebRole1.BL.Bl_imp.Instance.FindGroups(MainWindow.Token);
            }
        }
        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (object item in e.RemovedItems)
            {
                lstMyObject.Remove(item as InterfaceAcsess);
            }

            foreach (object item in e.AddedItems)
            {
                lstMyObject.Add(item as InterfaceAcsess);
            }
        }
        private void listView2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (object item in e.RemovedItems)
            {
                rightlist.Remove(item as InterfaceAcsess);
            }

            foreach (object item in e.AddedItems)
            {
                rightlist.Add(item as InterfaceAcsess);
            }
        }
        public GroupsManage()
        {
            InitializeComponent();
            leftlist.ItemsSource = listgruops;
            groupList.ItemsSource = listgruops;
            rightlist = new List<InterfaceAcsess>();
        }

        private void insertToGroup(object sender, RoutedEventArgs e)
        {
            rightlist.AddRange(lstMyObject);
            right.ItemsSource = rightlist;
            right.Items.Refresh();
        }
        private void outFromGroup(object sender, RoutedEventArgs e)
        {
            foreach(InterfaceAcsess x in lstMyObjectRight)
                rightlist.Remove(x);

            right.ItemsSource = rightlist;
            right.Items.Refresh();
        }

        private void groupList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (sender as ComboBox).SelectedItem.ToString();

        }
    }
}
