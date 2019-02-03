using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
using WCFServiceWebRole1.BE;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int SelectedObj { get; set; } = 0;
        public object theObj { get; set; }
        public static string Token;
        public static string Mode = "Item";
        public static List<Fillter> filters = new List<Fillter>();
        public static string GroupToShow = "ALL";
        private static WCFServiceWebRole1.BL.Bl_imp BL = WCFServiceWebRole1.BL.Bl_imp.Instance;
        public static List<string> FilterGroup { get {
                List<Student> TheStudents = BL.GetListStudents(Token);
                List<Student> TheNewStudents = new List<Student>();
                List<Teacher> TheTeachers = BL.GetListTeachers(Token);
                List<MeetTest> TheMeetTest = BL.GetListMeetTests(Token);
                List<MeetTest> TheNewMeetTest = new List<MeetTest>();
                List<Teacher> TheNewTeachers = new List<Teacher>();
                foreach (Fillter fillter in filters) {
                    if (fillter.FuncName == "StudentTeacherName")
                        TheNewStudents.AddRange((from Item in TheStudents where Item.TeacherName ==  fillter.Prop select Item).ToList<Student>());
                    if (fillter.FuncName == "FindMoreSchool")
                        TheNewStudents.AddRange((from Item in TheStudents where Item.SchoolName == fillter.Prop select Item).ToList<Student>());
                    if (fillter.FuncName == "FindmeetTestGrade")
                        TheNewMeetTest.AddRange((from Item in TheMeetTest where (Item.TestGrade) == Convert.ToInt32( fillter.Prop) select Item).ToList<MeetTest>());
                    if (fillter.FuncName == "MinGrade")
                        TheNewMeetTest.AddRange((from Item in TheMeetTest where (Item.TestGrade) > Convert.ToDouble(fillter.Prop) select Item).ToList<MeetTest>());
                    if (fillter.FuncName == "MaxGrade")
                        TheNewMeetTest.AddRange((from Item in TheMeetTest where (Item.TestGrade) > Convert.ToDouble(fillter.Prop) select Item).ToList<MeetTest>());
                    if (fillter.FuncName == "ByName")
                    {
                        TheNewTeachers.AddRange((from Item in TheTeachers where Item.FirstName == fillter.Prop || Item.LastName == fillter.Prop select Item).ToList<Teacher>());
                        TheNewStudents.AddRange((from Item in TheStudents where Item.FirstName == fillter.Prop || Item.LastName == fillter.Prop select Item).ToList<Student>());
                    }

                }
                if (filters.Count == 0) { TheNewStudents = TheStudents; }
                if (filters.Count == 0) { TheNewMeetTest = TheMeetTest; }
                if (filters.Count == 0) { TheNewTeachers = TheTeachers; }
                List<string> studentId= new List<string>(
                    from x in TheNewStudents
                    where x.GroupName.Exists(s=>s.Equals(GroupToShow))
                    select x.Id
                    );
                List<string> teachersId = new List<string>(
                    from x in TheNewTeachers
                    where x.GroupName.Exists(s => s.Equals(GroupToShow))
                    select x.Id
                    );
                List<string> meettestId = new List<string>(
                    from x in TheNewMeetTest
                    where x.GroupName.Exists(s => s.Equals(GroupToShow))
                    select x.TestId.ToString()
                    );
                teachersId.AddRange(studentId);
                teachersId.AddRange(meettestId);
                return teachersId;
            }
            set { } }

        /// <summary>
        /// for theard of calculate distance filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Backgroundworker_DoWorkcompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result as string != "-1")
            filters.Add(new Fillter("ByName", e.Result.ToString()));
            RefreshBinding();
            Listfillters.Items.Refresh(); ;
        }
        private static void Backgroundworker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<Object> objs = e.Argument as List<Object>;

            string traineeAddress = objs[0] as string;
            string currentTester = objs[1] as string;
            string idTeacher = objs[2] as string;
            int maxKil = Convert.ToInt32( objs[3] as string);
            if(Convert.ToInt32(UI.Service.Data.MenegeDistance.Distance(traineeAddress, currentTester)) > maxKil)
                e.Result = idTeacher;
            else e.Result = "-1";

        }
        /////////////////////////view model////////////////////////////////////////
        public class MyViewModel
        {
            public List<Fillter> Filters { get { return MainWindow.filters; } }
            public string Mytoken { get; set; }
            ////calculate sum of objects to user    
            public string Num { get { return "Students (" + Items.Count().ToString() + ")"; } }
            public string Num2 { get { return "Teachers (" + Items2.Count().ToString() + ")"; } }
            public string Num3 { get { return "Tests (" + Items3.Count().ToString() + ")"; } }
            public string Num4 { get { return "Users (" + Items4.Count().ToString() + ")"; } }

               /// <summary>
               /// get list object to view
               /// </summary>
            public List<Student> Items
            {
                get
                {
                    return BL.GetListStudents(Mytoken);
                }
            }
            public List<Teacher> Items2
            {
                get
                {
                    return BL.GetListTeachers(Mytoken);
                }
            }
            public List<MeetTest> Items3
            {
                get
                {
                    return BL.GetListMeetTests(Mytoken);
                }
            }
            public List<ListPermission> Items4
            {
                get
                {
                    return BL.GetListUsers(Mytoken);
                }
            }
            /// <summary>
            /// list of gruop to view
            /// </summary>
            public List<string> FindGroups
            {
                get
                {
                    List<string> grop = new List<string>();
                    foreach (InterfaceAcsess x in Items)
                        foreach (string p in x.GroupName)
                            if (!grop.Exists(t => t == p)) grop.Add(p);
                    foreach (InterfaceAcsess x in Items2)
                        foreach (string p in x.GroupName)
                            if (!grop.Exists(t => t == p)) grop.Add(p);
                    foreach (InterfaceAcsess x in Items3)
                        foreach (string p in x.GroupName)
                            if (!grop.Exists(t => t == p)) grop.Add(p);
                    return grop;
                }
            }
            /// <summary>
            /// ////////////////////////////////////////////////////////////
            /// function to fill list of filters
            /// </summary>
            public List<string> FindMoreTeacher
            {
                get
                {
                    List<string> grop = new List<string>();
                    foreach (Student x in Items)
                        if (!grop.Exists(t => t == x.TeacherName)) grop.Add(x.TeacherName);
                    return grop;
                }
            }
            public List<string> FindMoreSchool
            {
                get
                {
                    List<string> grop = new List<string>();
                    foreach (Student x in Items)
                        if (!grop.Exists(t => t == x.SchoolName)) grop.Add(x.SchoolName);
                    return grop;
                }
            }
            public List<string> FindmeetTestGrade
            {
                get
                {
                    List<string> grop = new List<string>();
                    foreach (MeetTest x in Items3)
                        if (!grop.Exists(t => t == x.TestGrade.ToString())) grop.Add(x.TestGrade.ToString());
                    return grop;
                }
            }

        }

        public MainWindow()
        {

            InitializeComponent();

            login popup = new login();
            popup.Check += value => { label.Content = value; Token = value; };
            popup.ShowDialog();

            hi.Text = "hi "+WCFServiceWebRole1.DL.Dal_imp.Instance.idByToken(label.Content.ToString())+" (LogOut)";

            DataContext = new MyViewModel() { Mytoken = label.Content.ToString() };

        }

        /// <summary>
        /// log out event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hi_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            hi.Text = "You are disconnected";
            DataContext = new MyViewModel();

            login popup = new login();
            popup.Check += value => { label.Content = value; Token = value; };
            popup.ShowDialog();

            hi.Text = "Hi " + WCFServiceWebRole1.DL.Dal_imp.Instance.idByToken(label.Content.ToString()) + " (LogOut)";

            DataContext = new MyViewModel() { Mytoken = label.Content.ToString() };
        }


            /// <summary>
            /// event click on item
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
        private void ClickAddStudent(object sender, RoutedEventArgs e)
        {
            NewStudent win2 = new NewStudent(label.Content.ToString(), "-1");
            win2.deligEven += (s, t) => refreshMyView();
            win2.Show();
        }

        private void ClickAddTeacher(object sender, RoutedEventArgs e)
        {
            NewTeacher win3 = new NewTeacher(label.Content.ToString(), "-1");
            win3.deligEven += (s, t) => refreshMyView();
            win3.Show();
        }

        private void ClickAddMeetTest(object sender, RoutedEventArgs e)
        {
            NewMeetTest win4 = new NewMeetTest(label.Content.ToString(), "-1");
            win4.deligEven += (s, t) => refreshMyView();
            win4.Show();
        }

        private void ClickDelete(object sender, RoutedEventArgs e)
        {
            switch (SelectedObj)
            {
                case 1:
                        Student x = theObj as Student;
                            BL.DelStudent(label.Content.ToString(), x.Id);
                    break;
                case 2:
                    Teacher y = theObj as Teacher;
                        BL.DelTeacher(label.Content.ToString(), y.Id);
                    break;
                case 3:
                    MeetTest z = theObj as MeetTest;
                        BL.DelTest(label.Content.ToString(), z.TestId.ToString());
                    break;
            }
            refreshMyView();
        }

        private void ClickEdit(object sender, RoutedEventArgs e)
        {
            switch (SelectedObj)
            {
                case 1:
                    Student x = theObj as Student;
                    NewStudent win6 = new NewStudent(label.Content.ToString(), x.Id);
                    win6.deligEven += (s, t) => refreshMyView();
                    win6.Show();
                    break;
                case 2:
                    Teacher y = theObj as Teacher;
                    NewTeacher win7 = new NewTeacher(label.Content.ToString(), y.Id);
                    win7.deligEven += (s, t) => refreshMyView();
                    win7.Show();
                    break;
                case 3:
                    MeetTest z = theObj as MeetTest;
                    NewMeetTest win8 = new NewMeetTest(label.Content.ToString(), z.TestId.ToString());
                    win8.deligEven += (s, t) => refreshMyView();
                    win8.Show();
                    break;
                case 4:
                    ListPermission u = theObj as ListPermission;
                    UserPermissen win9 = new UserPermissen(label.Content.ToString(), u.Id);
                    win9.deligEven += (s, t) => refreshMyView();
                    win9.Show();
                    break;
            }
        }
        ///Refresh view modle
        public void refreshMyView()
        {
            DataContext = new MyViewModel() { Mytoken = label.Content.ToString() };
        }
        private void RefreshBinding()
        {
            lstNames.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
            lstNames2.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
            lstNames3.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
            lstNames10.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
            lstNames11.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
            lstNames12.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
        }
        /// <summary>
        /// detect which object selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                theObj=e.AddedItems[0];
                if (e.AddedItems[0] is Student)
                    SelectedObj = 1;
                if (e.AddedItems[0] is Teacher)
                    SelectedObj = 2;
                if (e.AddedItems[0] is MeetTest)
                    SelectedObj = 3;
                if (e.AddedItems[0] is ListPermission)
                    SelectedObj = 4;
            }
            catch { SelectedObj = 0; }
        }

        /// <summary>
        /// select goups filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groping_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GroupToShow = (sender as ComboBox).SelectedItem as string;
            RefreshBinding();
        }

        
        ///start fillters click-------------------------------------------------------------
        private void MenuItem_ClickTeacherName(object sender, RoutedEventArgs e)
        {
            filters.Add(new Fillter("StudentTeacherName", (sender as MenuItem).Header.ToString()));
            RefreshBinding();
            Listfillters.Items.Refresh();
        }
        private void MenuItem_ClickMoreSchool(object sender, RoutedEventArgs e)
        {
            filters.Add(new Fillter("FindMoreSchool", (sender as MenuItem).Header.ToString()));
            RefreshBinding();
            Listfillters.Items.Refresh();
        }

        private void MenuItem_Click_1FindmeetTestGrade(object sender, RoutedEventArgs e)
        {
            filters.Add(new Fillter("FindmeetTestGrade", (sender as MenuItem).Header.ToString()));
            RefreshBinding();
            Listfillters.Items.Refresh();
        }
        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            filters.Add(new Fillter("MinGrade", (startFrom.Value * 10).ToString()));
            filters.Add(new Fillter("MaxGrade", (endFrom.Value * 10).ToString()));
            RefreshBinding();
            Listfillters.Items.Refresh();
        }
        private void Label_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            filters.Add(new Fillter("ByName", Byname.Text));
            RefreshBinding();
            Listfillters.Items.Refresh();
        }
        private void Label_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            BackgroundWorker backgroundworker;
            foreach (var Item in BL.GetListTeachers(Token))
            {
                try
                {
                    backgroundworker = new BackgroundWorker();
                    backgroundworker.DoWork += Backgroundworker_DoWork;
                    backgroundworker.RunWorkerCompleted += Backgroundworker_DoWorkcompleted;
                    backgroundworker.RunWorkerAsync(new List<Object> { Wheredistance.Text, Item.Adress.ToString(), Item.FirstName, Item.maxKMFromHome });
                }
                catch { }
            }
        }

        ///end fillters click-------------------------------------------------------------

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.Mode == "List") MainWindow.Mode = "Item"; else MainWindow.Mode = "List";
            refreshMyView();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BL.SaveData();

        }
        /// <summary>
        /// remove filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            filters.Remove((from x in filters where x.Id.ToString() == (sender as Button).Tag.ToString() select x).FirstOrDefault());
            Listfillters.Items.Refresh();
            refreshMyView();
        }


        private void DatePicker_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ///optional filter
        }


    }
    /// <summary>
    /// color for test
    /// </summary>
    public class TestToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            
             if(value.ToString()=="0") return "#ffffff";
             if(System.Convert.ToInt32(value.ToString()) > def.minGradeToPassTest) return "#f2f2f2";
            return "red";
        }
    
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "no";
        }
    }
    /// <summary>
    /// show for filter
    /// </summary>
    public class YesNoToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (MainWindow.FilterGroup.Exists(e => e.Equals(value.ToString()))) return "visible"; return "collapsed";
        }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "no";
        }
    }
    /// <summary>
    /// show for mode
    /// </summary>
    public class ModeListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (MainWindow.Mode == "List") return "visible"; return "collapsed";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "no";
        }
    }
    /// <summary>
    /// secend mode show
    /// </summary>
    public class ModeItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (MainWindow.Mode == "Item") return "visible"; return "collapsed";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "no";
        }

    }


}
