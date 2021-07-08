using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
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
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace tryWpfass
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        XmlSerializer xs;
        List<Person1> ls;
        public MainWindow()
        {
            InitializeComponent();

            ls = new List<Person1>();
            xs = new XmlSerializer(typeof(List<Person1>));
        }

        private bool Save_Clicked = false;
        private bool Delete_Clicked = false;
       

        private void Button1_Click(object sender, RoutedEventArgs e)         //Save Button.
        {
            Save_Clicked = true;

            FileStream fs = new FileStream(@"C:\Users\quest\source\repos\tryWpfass\tryWpfass\Person.xml", FileMode.Create, FileAccess.Write);
            Person1 p = new Person1();
            
            p.FName = firstNameTB.Text;
            p.LName = lastNameTB.Text;
            p.Compny = cmpnyTB.Text;
            p.Saveas = savasTB.Text;
            p.Position = postnTB.Text;
            p.Phone = pnTB.Text;
            p.Mobile = mobTB.Text;
            p.Fax = faxTB.Text;
            p.Email = mailTB.Text;
            p.Twitter = twtTB.Text;
            p.IM = imTB.Text;

            ls.Add(p);

            xs.Serialize(fs, ls);
            fs.Close();

            XmlDocument doc = new XmlDocument();

            XmlAttribute fname = doc.CreateAttribute("Person1");
            fname.Value = firstNameTB.Text;
          

            MessageBox.Show("File saved successfully.");
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)           //New Button to clear the fields
        {
            try
            {
                using (Stream str = new FileStream("Person.xml", FileMode.Open))
                {
                    if (Save_Clicked == false)
                    {
                        MessageBox.Show("File is not saved.");        //pop up to show when file not saved
                    }
                }
            }
            catch
            {
                if (Save_Clicked == false)
                {
                    MessageBox.Show("File is not saved.");
                }
                else
                {
                    firstNameTB.Text = "";
                    lastNameTB.Text = "";
                    pnTB.Text = "";
                    cmpnyTB.Text = "";
                    savasTB.Text = "";
                    mobTB.Text = "";
                    faxTB.Text = "";
                    mailTB.Text = "";
                    twtTB.Text = "";
                    imTB.Text = "";
                    postnTB.Text = "";
                }
            }


        }

       



        private void DeleteButton_Click(object sender, RoutedEventArgs e)          //Delete Button
        {

            Delete_Clicked = true;


            firstNameTB.Text = "";
            lastNameTB.Text = "";
            pnTB.Text = "";
            cmpnyTB.Text = "";
            savasTB.Text = "";
            mobTB.Text = "";
            faxTB.Text = "";
            mailTB.Text = "";
            twtTB.Text = "";
            imTB.Text = "";
            postnTB.Text = "";

            string filepath = @"C:\Users\quest\source\repos\tryWpfass\tryWpfass\Person.xml";

          

            XmlSerializer serializer = new XmlSerializer(typeof(Person1));

            StreamReader reader = new StreamReader(filepath);
            object obj = serializer.Deserialize(reader) as Person1;

            Person1 name = (Person1)obj;
            reader.Close();
          
                int t = name.personList.Count();


                for (int i = 0; i < t; i++)
                {

                    if (firstNameTB.Text == name.personList[i].FName)
                    {
                    name.personList.Clear();
                    }

                }



                IEditableCollectionView iecv = CollectionViewSource.GetDefaultView(dataGridView.ItemsSource) as IEditableCollectionView;
            while (dataGridView.SelectedIndex >= 0)
            {
                int selectedIndex = dataGridView.SelectedIndex;
                DataGridRow dgr = dataGridView.ItemContainerGenerator.ContainerFromIndex(selectedIndex) as DataGridRow;
                dgr.IsSelected = false;

                if (iecv.IsEditingItem)
                {
                    iecv.CommitEdit();
                    iecv.RemoveAt(selectedIndex);
                }
                else
                {
                    iecv.RemoveAt(selectedIndex);
                }
            }

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)              //Close the UI
        {
            Close();
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)            //View on grid
        {

            if (Delete_Clicked)
            {

                MessageBox.Show("The file was Deleted.");

            }
            else
            {

                DataSet dataset = new DataSet();
                dataset.ReadXml(@"C:\Users\quest\source\repos\tryWpfass\tryWpfass\Person.xml");
                dataGridView.ItemsSource = dataset.Tables[0].DefaultView;
            }

        }
      
        private void Load_Click(object sender, RoutedEventArgs e)             //Load on text boxes
        {
           
            string path = @"C:\Users\quest\source\repos\tryWpfass\tryWpfass\Person.xml";

            XmlSerializer serializer = new XmlSerializer(typeof(Person1));

            StreamReader reader = new StreamReader(path);
           object obj = serializer.Deserialize(reader) as Person1;

            Person1 name = (Person1)obj;
            reader.Close();
            try
            {
                int t = name.personList.Count();
               


                for (int i = 0; i < t; i++) {

                    if (firstNameTB.Text == name.personList[i].FName)
                    {                                            

                        firstNameTB.Text = name.personList[i].FName;
                        lastNameTB.Text = name.personList[i].LName;
                        cmpnyTB.Text = name.personList[i].Compny;
                        postnTB.Text = name.personList[i].Position;
                        savasTB.Text = name.personList[i].Saveas;
                        pnTB.Text = name.personList[i].Phone;
                        mobTB.Text = name.personList[i].Mobile;
                        faxTB.Text = name.personList[i].Fax;
                        mailTB.Text = name.personList[i].Email;
                        twtTB.Text = name.personList[i].Twitter;
                        imTB.Text = name.personList[i].IM;
                    }
                }

            }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
           

        }

    }
}
