using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
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

namespace Question1_WPF
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : Window
    {
        public User()
        {
            InitializeComponent();
        }

        string ConnectionString = "Data Source=DESKTOP-6CLP6M9\\SQLEXPRESS;Initial Catalog=wpfFinal;Integrated Security=True;Trust Server Certificate=True";


        public void LoadData(string level)
        {
            string query = $"SELECT id, QuestionText FROM Questions WHERE level = '{level}'";

            try
            {
                SqlConnection con = new SqlConnection(ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);

                QuestionDataGrid.ItemsSource = table.DefaultView;


            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}");
            }
        }

        private void SaveQuestion_Click(object sender, RoutedEventArgs e)
        {
            int id=int.Parse(qIdTextBox.Text);
            string answer=answerTextBox.Text;
            string query = $"Select QuestionText from Questions where id='{id}' and CorrectAnswer='{answer}'";
            if (id == null || answer == null )
            {
                MessageBox.Show("All fields are required");
                return;

            }


            try
            {
                SqlConnection con = new SqlConnection(ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                var rows = cmd.ExecuteScalar();
                if (rows!=null) {

                    int m = int.Parse(Score.Text);
                    m = m + 1;
                    Score.Text =m.ToString();
                    MessageBox.Show("Your answer is correct","Success",MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Your answer is incorrect","Warning", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}");
            }


        }

        private void QuitQuiz_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newForm= new MainWindow();  
            newForm.Show();
            this.Close();
        }



        private void difficultyComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            string selectedDifficulty = (difficultyComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            LoadData(selectedDifficulty);
        }
    }
}
