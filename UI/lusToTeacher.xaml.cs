using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace UI
{
    /// <summary>
    /// Interaction logic for lusToTeacher.xaml
    /// </summary>
    public partial class lusToTeacher : Window
    {
        public static Schedule x = new Schedule();
        public class DataObject
        {
            public bool A {
                get { return x.getS(1, hour); }
                set {x.SetS(1, hour, value); }
            }
            public bool B
            {
                get { return x.getS(2, hour); }
                set { x.SetS(2, hour, value); }
            }
            public bool C
            {
                get { return x.getS(3, hour); }
                set { x.SetS(3, hour, value); }
            }
            public bool D
            {
                get { return x.getS(4, hour); }
                set { x.SetS(4, hour, value); }
            }
            public bool E
            {
                get { return x.getS(5, hour); }
                set { x.SetS(5, hour, value); }
            }

            public int hour;
        }
        public lusToTeacher(Schedule t)
        {
            x = t;
            InitializeComponent();
            var list = new ObservableCollection<DataObject>();
            list.Add(new DataObject() { hour = 8 });
            list.Add(new DataObject() { hour = 9 });
            list.Add(new DataObject() { hour = 10 });
            list.Add(new DataObject() { hour = 11 });
            list.Add(new DataObject() { hour = 12 });
            list.Add(new DataObject() { hour = 13 });
            list.Add(new DataObject() { hour = 14 });
            list.Add(new DataObject() { hour = 15 });
            list.Add(new DataObject() { hour = 16 });
           
            this.dataGrid1.ItemsSource = list;
        }
        private void dataGrid1_Loaded(object sender, DataGridRowEventArgs e)
        {
            var id = e.Row.GetIndex();
            switch (id)
            {
                case 0:
                    {
                        e.Row.Header = "8-9";
                        break;
                    }
                case 1:
                    {
                        e.Row.Header = "9-10";
                        break;
                    }
                case 2:
                    {
                        e.Row.Header = "10-11";
                        break;
                    }
                case 3:
                    {
                        e.Row.Header = "11-12";
                        break;
                    }
                case 4:
                    {
                        e.Row.Header = "12-13";
                        break;
                    }
                case 5:
                    {
                        e.Row.Header = "13-14";
                        break;
                    }
                case 6:
                    {
                        e.Row.Header = "14-15";
                        break;
                    }
                case 7:
                    {
                        e.Row.Header = "15-16";
                        break;
                    }
                case 8:
                    {
                        e.Row.Header = "16-17";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
            private void button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void dataGrid1_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
