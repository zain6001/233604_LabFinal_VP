using System.Data;
using System.Data.SqlTypes;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;

namespace Question1_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            string query = $"select * from Questions";
            try
            {
                SqlConnection con = new SqlConnection(ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);
                QuestionsDetails.ItemsSource = table.DefaultView;
                

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}");
            }
        }

        string ConnectionString = "Data Source=DESKTOP-6CLP6M9\\SQLEXPRESS;Initial Catalog=wpfFinal;Integrated Security=True;Trust Server Certificate=True";
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            if (questionNameTextBox.Text == null || MarksTextBox.Text == null || correctAnswerTextBox.Text == null)
            {
                MessageBox.Show("All fields are required");
                return;

            }
            string qn=questionNameTextBox.Text;
            string ca=correctAnswerTextBox.Text;
           int m = int.Parse(MarksTextBox.Text);
           
         
            string query = $"insert into Questions(questionText,CorrectAnswer,assignedMarks) values ('{qn}','{ca}','{m}')";
            try
            {
                SqlConnection con = new SqlConnection(ConnectionString);
                con.Open();
                SqlCommand cmd =new SqlCommand(query,con);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex) {
                MessageBox.Show($"{ex}");
            }

            MessageBox.Show("Question Saved Successfully","Success",MessageBoxButton.OK);
            LoadData();
        }

        private void SaveQuestion_Click(object sender, RoutedEventArgs e)
        {

            User newForm = new User();
            newForm.Show();
            this.Close();

        }
    }
}