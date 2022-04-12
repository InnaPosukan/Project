using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace MyApplication
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "database1DataSet.Table". При необходимости она может быть перемещена или удалена.
            this.tableTableAdapter.Fill(this.database1DataSet.Table);
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase1"].ConnectionString);
            sqlConnection.Open();   

            if (sqlConnection.State == ConnectionState.Open)
            {
                MessageBox.Show("Подключено");
            }
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand($"INSERT INTO [Table] (Name,Ingredients,Steps, Time, Feature) VALUES (@Name,@Ingredients,@Steps, @Time,@Feature)" , sqlConnection);

            command.Parameters.AddWithValue("Name", textBox1.Text);
            command.Parameters.AddWithValue("Ingredients", textBox2.Text);
            command.Parameters.AddWithValue("Steps", textBox3.Text);
            command.Parameters.AddWithValue("Time", textBox4.Text);
            command.Parameters.AddWithValue("Feature", comboBox1.Text.ToString());





            MessageBox.Show (command.ExecuteNonQuery().ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM [Table]",sqlConnection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];


        }
    }
}
