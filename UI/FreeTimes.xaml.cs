using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    /// Interaction logic for FreeTimes.xaml
    /// </summary>
    public partial class FreeTimes : Window
    {
        public static DateTime Start { get; set; } = DateTime.Now;
        public static Schedule x = new Schedule();
        public static MeetTest Test;
        public static List<DateTime> GoodTimes = new List<DateTime>();
        public class DataObject
        {

            public bool A
            {
                get
                {
                    Test.Time = new DateTime(Start.Year, Start.Month, Start.Day, hour, 0, 0);
                    try {
                        WCFServiceWebRole1.BL.Bl_imp.Instance.getTeacherForTest(Test);
                        GoodTimes.Add(new DateTime(Start.Year, Start.Month, Start.Day, hour, 0, 0));
                    }
                    catch { return false; }
                    return true;
                }
                //set { x.SetS(1, hour, value); }
            }

            
            
            public string ADate { get {
                    DateTime x = new DateTime(Start.Year, Start.Month, Start.Day ,hour,0,0);
                    return x.ToString();
                } }
            public string BDate
            {
                get
                {
                    DateTime x = new DateTime(Start.Year, Start.Month, Start.Day, hour, 0, 0).AddDays(1);
                    return x.ToString();
                }
            }
            public string CDate
            {
                get
                {
                    DateTime x = new DateTime(Start.Year, Start.Month, Start.Day, hour, 0, 0).AddDays(2);
                    return x.ToString();
                }
            }
            public string DDate
            {
                get
                {
                    DateTime x = new DateTime(Start.Year, Start.Month, Start.Day, hour, 0, 0).AddDays(3);
                    return x.ToString();
                }
            }
            public string EDate
            {
                get
                {
                    DateTime x = new DateTime(Start.Year, Start.Month, Start.Day, hour, 0, 0).AddDays(4);
                    return x.ToString();
                }
            }
            public bool B
            {
                get
                {
                    Test.Time = new DateTime(Start.Year, Start.Month, Start.Day, hour, 0, 0).AddDays(1);
                    try
                    {
                        WCFServiceWebRole1.BL.Bl_imp.Instance.getTeacherForTest(Test);
                        GoodTimes.Add(new DateTime(Start.Year, Start.Month, Start.Day, hour, 0, 0).AddDays(1));
                    }
                    catch { return false; }
                    return true;
                }
                //set { x.SetS(2, hour, value); }
            }
            public bool C
            {
                get
                {
                    Test.Time = new DateTime(Start.Year, Start.Month, Start.Day, hour, 0, 0).AddDays(2);
                    try
                    {
                        WCFServiceWebRole1.BL.Bl_imp.Instance.getTeacherForTest(Test);
                        GoodTimes.Add(new DateTime(Start.Year, Start.Month, Start.Day, hour, 0, 0).AddDays(2));
                    }
                    catch { return false; }
                    return true;
                }
                //set { x.SetS(3, hour, value); }
            }
            public bool D
            {
                get
                {
                    Test.Time = new DateTime(Start.Year, Start.Month, Start.Day, hour, 0, 0).AddDays(3);
                    try
                    {
                        WCFServiceWebRole1.BL.Bl_imp.Instance.getTeacherForTest(Test);
                        GoodTimes.Add(new DateTime(Start.Year, Start.Month, Start.Day, hour, 0, 0).AddDays(3));
                    }
                    catch { return false; }
                    return true;
                }
                //set { x.SetS(4, hour, value); }
            }
            public bool E
            {
                get
                {
                    Test.Time = new DateTime(Start.Year, Start.Month, Start.Day, hour, 0, 0).AddDays(4);
                    try
                    {
                        WCFServiceWebRole1.BL.Bl_imp.Instance.getTeacherForTest(Test);
                        GoodTimes.Add(new DateTime(Start.Year, Start.Month, Start.Day, hour, 0, 0).AddDays(4));
                    }
                    catch { return false; }
                    return true;
                }
                //set { x.SetS(5, hour, value); }
            }
            public string HH
            {
                get
                {
                    return hour.ToString() + ":00 - "+ (hour+1).ToString() + ":00";
                }
                //set { x.SetS(5, hour, value); }
            }

            public int hour;
            public int Hour { get { return hour; } }
        }
        public FreeTimes(MeetTest test)
        {
            Test = test;
            Start = Test.Time;
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
            Sun.Header = Start.ToString("dd/MM/yy");
            Man.Header = Start.AddDays(1).ToString("dd/MM/yy"); 
            Tus.Header = Start.AddDays(2).ToString("dd/MM/yy"); 
            wed.Header = Start.AddDays(3).ToString("dd/MM/yy"); 
            thu.Header = Start.AddDays(4).ToString("dd/MM/yy"); 
            Selectime.SelectedDate = Start;
            this.dataGrid1.ItemsSource = list;
            //this.dataGrid1.SelectedCellsChanged += selectedCellsChanged;
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            Test.Time = Convert.ToDateTime((sender as Button).Tag.ToString());
            Close();
        }

        private void Selectime_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Start = Convert.ToDateTime((sender as DatePicker).SelectedDate);
            this.dataGrid1.Items.Refresh();
        }
    }
}
