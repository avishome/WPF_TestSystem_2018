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
    /// Interaction logic for NewMeetTest.xaml
    /// </summary>
    public partial class NewMeetTest : Window
    {
        MeetTest x;
        string token;
        string idupDate = "-1";
        public event EventHandler deligEven = delegate { };
        public Dictionary<string, object> dicNumber = new Dictionary<string, object>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
        }
        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            valudation(e, false);
        }
        public NewMeetTest()
        {
            InitializeComponent();

        }
        public NewMeetTest(String Token, String id = "-1")
        {
            
            token = Token;

            WCFServiceWebRole1.BL.Bl_imp Bl = WCFServiceWebRole1.BL.Bl_imp.Instance;

            if (id != "-1") {
                x = Bl.ShowTest(Token, id).Clone();
                idupDate = id;
            } else
                x = def.defMeetTest.Clone();

            if (id == "-1") x.Time = DateTime.Now.AddDays(3);

            InitializeComponent();
            
            //student LIst init
            StudentList.ItemsSource = from x in Bl.GetListStudents(Token) select x.Id+" "+x.FirstName+" "+x.LastName;

            stackPanel.DataContext = x;

            //select old object Id student
            try
            {
                StudentList.SelectedItem = (from y in Bl.GetListStudents(Token) where x.StudentId == y.Id select y.Id + " " + y.FirstName + " " + y.LastName).ToList<String>().FirstOrDefault();
            }
            catch { StudentList.SelectedIndex = 0;}

            Hour.Text = x.Time.Hour.ToString();
            IntefaceAC.Text = x.TestAddress.city;
            IntefaceAS.Text = x.TestAddress.street;
            IntefaceAN.Text = x.TestAddress.num.ToString();

            //init to dictionary problems
            dicNumber.Add("Time", console);
            dicNumber.Add("TestId", console1);
            dicNumber.Add("TeacherId", console2);
            dicNumber.Add("StudentId", console3);
            dicNumber.Add("TestAddress.city", console4);
            dicNumber.Add("TestAddress.street", console5);
            dicNumber.Add("TestAddress.num", console6);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            valudation(e);
        }

        private void valudation(RoutedEventArgs e, bool sendForm = true)
        {
            MeetTest GetCheck = x.Clone();
            EventHandler handler = deligEven;
            bool send = sendForm;

            try {
            x.TestAddress = new address(IntefaceAS.Text, Convert.ToInt32(IntefaceAN.Text), IntefaceAC.Text);
            }
            catch { IntefaceAN.Text = ""; }

            try { Convert.ToInt32(Hour.Text); } catch { Hour.Text = "0"; }
            try
            {
                x.Time = new DateTime(Date.SelectedDate.Value.Year, Date.SelectedDate.Value.Month, Date.SelectedDate.Value.Day, Convert.ToInt32(Hour.Text), 0, 0);
            }
            catch { MessageBox.Show("your hour is wrong"); }

            foreach (var di in dicNumber) { ((Label)di.Value).Content = ""; }

            try
            {
                if (idupDate == "-1")
                {
                    if (WCFServiceWebRole1.BL.Bl_imp.Instance.AddMeetTest(token, GetCheck, send)) { handler(this, e); Close(); }
                }
                else
                { if (WCFServiceWebRole1.BL.Bl_imp.Instance.EditTest(token, idupDate, GetCheck, send)) { handler(this, e); Close(); } }
            }
            catch (UI.Service.defineds.ex ex)
            {
                //show windows select time if the time is uncorrect
                if (send && ex.Problems.Where(x => x.Value == "Time" ).Select(x=>x.Key).ToList().Count>0) { new FreeTimes(x).ShowDialog(); Hour.Text = x.Time.Hour.ToString(); Date.SelectedDate = x.Time; }
                //show the errors on lables
                foreach (KeyValuePair<string, object> iterator in dicNumber)
                {
                    List<Exception> eror = ex.Problems.Where(x => x.Value == iterator.Key).Select(x => x.Key).ToList();
                    ((Label)iterator.Value).Content = "";
                    foreach (Exception er in eror)
                    {
                        ((Label)iterator.Value).Content += er.Message + "\n";
                    }
                }
            }
        }

        private void StudentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            x.StudentId = (from x in WCFServiceWebRole1.BL.Bl_imp.Instance.GetListStudents(token) where (x.Id + " " + x.FirstName + " " + x.LastName) == (string)e.AddedItems[0] select x.Id).ToList<String>()[0];
        }
    }
}
