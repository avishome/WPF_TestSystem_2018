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
using WCFServiceWebRole1.BL;

namespace UI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class login : Window
    {
        public event Action<string> Check;
        public login()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, RoutedEventArgs e)
        {
            string h = Bl_imp.Instance.AddToken(textBoxId.Text);
            //WCFServiceWebRole1.DL.Dal_imp.instance.ListpermissById(h.Id).permisses.Add(new WCFServiceWebRole1.BE.permission("ALL", def.ALLL.Clone() as bool[]));
            if (Check != null)
                Check(h);
            this.Close();
        }
    }
}
