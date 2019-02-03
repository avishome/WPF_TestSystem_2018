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
    /// Interaction logic for NewTeacher.xaml
    /// </summary>
    public partial class NewTeacher : Window
    {
        Teacher x;
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
        public NewTeacher()
        {
            InitializeComponent();
            Gear.ItemsSource = Enum.GetValues(typeof(gearbox)).Cast<gearbox>();
            GenderSelect.ItemsSource = Enum.GetValues(typeof(Gender)).Cast<Gender>();
            Car.ItemsSource = Enum.GetValues(typeof(typecar)).Cast<typecar>();
        }
        public NewTeacher(String Token, String id = "-1")
        {
            token = Token;
            WCFServiceWebRole1.BL.Bl_imp Bl = WCFServiceWebRole1.BL.Bl_imp.Instance;
            if (id != "-1") { x = Bl.ShowTeacher(Token, id).Clone(); idupDate = id; } else x = def.defTeacher.Clone();
            //x.adress = new address("ass", 5, "sd");
            InitializeComponent();
            stackPanel.DataContext = x;

            IntefaceAC.Text = x.Adress.city;
            IntefaceAS.Text = x.Adress.street;
            IntefaceAN.Text = x.Adress.num.ToString();
            IntefaceBD.Text = x.BirthDay.ToString();
            Gear.ItemsSource = Enum.GetValues(typeof(gearbox)).Cast<gearbox>();
            GenderSelect.ItemsSource = Enum.GetValues(typeof(Gender)).Cast<Gender>();
            Gear.SelectedValue = x.Gear;
            Car.ItemsSource = Enum.GetValues(typeof(typecar)).Cast<typecar>();
            Car.SelectedValue = x.carTypeSpecialization;
            //GenderSelect.SelectedValue = x.g


            dicNumber.Add("Id", console);
            dicNumber.Add("FirstName", console1);
            dicNumber.Add("LastName", console2);
            dicNumber.Add("Adress.city", console4);
            dicNumber.Add("Adress.street", console5);
            dicNumber.Add("Adress.num", console6);
            dicNumber.Add("PhoneNumber", console7);
            dicNumber.Add("BirthDay", console8);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            valudation(e);

        }

        private void valudation(RoutedEventArgs e,bool sendForm=true)
        {
            x.Gear = (gearbox)Gear.SelectedItem;
            x.carTypeSpecialization = (typecar)Car.SelectedItem;
            try { x.Adress = new address(IntefaceAS.Text, Convert.ToInt32(IntefaceAN.Text), IntefaceAC.Text); } catch { }
            Teacher GetCheck = x.Clone();
            EventHandler handler = deligEven;
            bool send = sendForm;
            try
            {
                if (idupDate == "-1")
                {
                    if (WCFServiceWebRole1.BL.Bl_imp.Instance.AddTeacher(token, GetCheck, send)) { handler(this, e); Close(); } else refill(GetCheck);
                }
                else
                { if (WCFServiceWebRole1.BL.Bl_imp.Instance.EditTeacher(token, idupDate, GetCheck, send)) { handler(this, e); Close(); } else refill(GetCheck); }
            }
            catch (UI.Service.defineds.ex ex)
            {
                foreach (KeyValuePair<string, object> iterator in dicNumber)
                {
                    List<Exception> eror = ex.Problems.Where(x => x.Value == iterator.Key).Select(x => x.Key).ToList();
                    ((Label)iterator.Value).Content = "";
                    foreach (Exception er in eror)
                    {
                        ((Label)iterator.Value).Content += er.Message + "\n";
                    }
                }
                //if (ex.Problems.Where(x => x.Value == "id").Select(x => x.Key).ToList().Count()>0) ((Label)dicNumber["id"]).Content = "problem"; else ((Label)dicNumber["id"]).Content = "";
            }
        }

        private void refill(Teacher GetCheck)
        {
            if (GetCheck.Id != x.Id) console.Content = "problem"; else console.Content = "";
            if (GetCheck.FirstName != x.FirstName) console1.Content = "problem"; else console1.Content = "";
            if (GetCheck.LastName != x.LastName) console2.Content = "problem"; else console2.Content = "";
            //if (GetCheck. != x.TeacherId) console3.Content = "problem"; else console3.Content = "";
            if (GetCheck.Adress.city != x.Adress.city) console4.Content = "problem"; else console4.Content = "";
            if (GetCheck.Adress.street != x.Adress.street) console5.Content = "problem"; else console5.Content = "";
            if (GetCheck.Adress.num != x.Adress.num) console6.Content = "problem"; else console6.Content = "";
            if (GetCheck.PhoneNumber != x.PhoneNumber) console7.Content = "problem"; else console7.Content = "";
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            lusToTeacher win9 = new lusToTeacher(x.Hours);
            win9.Show();
        }
    }
}
