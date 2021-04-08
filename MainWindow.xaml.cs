using System.IO;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;

namespace JewelryApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            //if (((MainWindowViewModel)DataContext).CheckCredentials())
            //{
            //    Close();
            //}


            XmlDocument doc = new XmlDocument();
            doc.Load("Users.xml");
            XmlNode root = doc.DocumentElement;

            XmlSerializer serializer = new XmlSerializer(typeof(Users));//, new XmlRootAttribute("Users"));
            StringReader stringReader = new StringReader(doc.InnerXml);
            var users = (Users)serializer.Deserialize(stringReader);
            if (!validateCredentials(users, txt_UserName.Text))
            {
                MessageBox.Show("UserName or Password is incorrect!", "Incorrect Credentials", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {                
                EstimationScreen estimationScreen = new EstimationScreen();
                estimationScreen.lbl_UserName.Content = txt_UserName.Text;
                if (txt_UserName.Text == "Normal")
                {
                    estimationScreen.lbl_Discount.Visibility = Visibility.Hidden;
                    estimationScreen.txt_Discount.Visibility = Visibility.Hidden;
                    estimationScreen.txt_Discount.Text = "0";
                }
                else
                {
                    estimationScreen.txt_Discount.Text = "2";
                }
                this.Visibility = Visibility.Hidden;
                estimationScreen.Show();
            }
        }

        private bool validateCredentials(Users users, string UserName)
        {
            if (users.Credential.Any(x => x.UserName == UserName)
                && (users.Credential.Single(x => x.UserName == UserName).Password == txt_Password.Password))
            {
                return true;
            }
            return false;
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
