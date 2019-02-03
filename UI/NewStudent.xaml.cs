using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WCFServiceWebRole1.BE;

namespace UI
{
    /// <summary>
    /// Interaction logic for NewUser.xaml
    /// </summary>
    public partial class NewStudent : Window
    {
        Student x;
        string token;
        string idupDate = "-1";
        public event EventHandler deligEven = delegate { };
        public Dictionary<string, object> dicNumber = new Dictionary<string, object>();
        public NewStudent()
        {
            InitializeComponent();
            Gear.ItemsSource = Enum.GetValues(typeof(gearbox)).Cast<gearbox>();
            GenderSelect.ItemsSource = Enum.GetValues(typeof(Gender)).Cast<Gender>();
            Car.ItemsSource = Enum.GetValues(typeof(typecar)).Cast<typecar>();
        }
        public NewStudent(String Token, String id = "-1")
        {
            token = Token;
            WCFServiceWebRole1.BL.Bl_imp Bl = WCFServiceWebRole1.BL.Bl_imp.Instance;
            if (id != "-1") { x = Bl.ShowStudent(Token, id).Clone(); idupDate = id; } else x = def.defstudent.Clone();
            //x.adress = new address("ass", 5, "sd");
            InitializeComponent();
            stackPanel.DataContext = x;

            IntefaceAC.Text = x.Adress.city;
            IntefaceAS.Text = x.Adress.street;
            IntefaceAN.Text = x.Adress.num.ToString();
            //IntefaceBD.Text = x.BirthDay.ToString();
            Gear.ItemsSource = Enum.GetValues(typeof(gearbox)).Cast<gearbox>();
            Gear.SelectedIndex = (int)x.Gear;
            GenderSelect.ItemsSource = Enum.GetValues(typeof(Gender)).Cast<Gender>();
            GenderSelect.SelectedIndex = (int)x.gender;
            Car.ItemsSource = Enum.GetValues(typeof(typecar)).Cast<typecar>();
            Car.SelectedIndex = (int)x.CarType;
            //GenderSelect.SelectedValue = x.g

            dicNumber.Add("Id", console);
            dicNumber.Add("FirstName", console1);
            dicNumber.Add("LastName", console2);
            dicNumber.Add("Adress.city", console4);
            dicNumber.Add("Adress.street", console5);
            dicNumber.Add("Adress.num", console6);
            dicNumber.Add("PhoneNumber", console7);
            dicNumber.Add("BirthDay", console8);
            dicNumber.Add("HoursLearned", console11);
            dicNumber.Add("TeacherName", console12);
            dicNumber.Add("SchoolName", console13);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            x.Adress = new address(IntefaceAS.Text, Convert.ToInt32(IntefaceAN.Text), IntefaceAC.Text);
            x.gender = (Gender)GenderSelect.SelectedIndex;
            x.Gear = (gearbox)Gear.SelectedIndex;
            x.CarType = (typecar)Car.SelectedIndex;
            Student GetCheck = x.Clone();
            EventHandler handler = deligEven;
            bool send = true;
            try
            {
                if (idupDate == "-1")
                {
                    if (WCFServiceWebRole1.BL.Bl_imp.Instance.AddStudent(token, GetCheck, send)) { handler(this, e); Close(); } else refill(GetCheck);
                }
                else
                { if (WCFServiceWebRole1.BL.Bl_imp.Instance.EditStudent(token, idupDate, GetCheck, send)) { handler(this, e); Close(); } else refill(GetCheck); }
            }
            catch (UI.Service.defineds.ex ex) {
                foreach (KeyValuePair<string, object> iterator in dicNumber) {
                    List<Exception> eror=ex.Problems.Where(x => x.Value == iterator.Key).Select(x => x.Key).ToList();
                    ((Label)iterator.Value).Content = "";
                    foreach (Exception er in eror) {
                        ((Label)iterator.Value).Content += er.Message + "\n";
                    }
                }
                //if (ex.Problems.Where(x => x.Value == "id").Select(x => x.Key).ToList().Count()>0) ((Label)dicNumber["id"]).Content = "problem"; else ((Label)dicNumber["id"]).Content = "";
            }
        }

        private void refill(Student GetCheck)
        {
        }
        private void GenderSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void GenderSelect_Selected(object sender, RoutedEventArgs e)
        {
        }
    }
}
