using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace pract12
{
    public partial class Form1 :Form
    {
        DataTable cheak;
        public Form1 ()
        {
            InitializeComponent( );
            
        }
        public void prepod ()
        {
            SqlConnection connect = new SqlConnection(@"Data Source=PC3810;Initial Catalog=deconat_Oberuhtina;Integrated Security=True");
            connect.Open( );
            SqlDataAdapter adptr = new SqlDataAdapter("select * from Преподователи", connect);
            DataTable table = new DataTable( );
            adptr.Fill(table);
            dataGridView1.DataSource = table;
            connect.Close( );
        }
        public void discplin ()
        {
            SqlConnection connect = new SqlConnection(@"Data Source=PC3810;Initial Catalog=deconat_Oberuhtina;Integrated Security=True");
            connect.Open( );
            SqlDataAdapter adptr = new SqlDataAdapter("select * from Дисциплины", connect);
            DataTable table = new DataTable( );
            adptr.Fill(table);
            dataGridView2.DataSource = table;
            connect.Close( );
        }
        public void control ()
        {
            SqlConnection connect = new SqlConnection(@"Data Source=PC3810;Initial Catalog=deconat_Oberuhtina;Integrated Security=True");
            connect.Open( );
            SqlDataAdapter adptr = new SqlDataAdapter("select * from Контроль", connect);
            DataTable table = new DataTable( );
            adptr.Fill(table);
            dataGridView3.DataSource = table;
            connect.Close( );
        }
      
       
        private void Form1_Load (object sender, EventArgs e)
        {
            prepod( );
            discplin( );
            control( );
            combobox();
        }
   
        public DataTable Zapros (string zapros)
        {
            //запрос в базу
            SqlConnection connect = new SqlConnection(@"Data Source=PC3810;Initial Catalog=deconat_Oberuhtina;Integrated Security=True");
            connect.Open( );
            SqlDataAdapter adptr = new SqlDataAdapter(zapros, connect);
            DataTable table = new DataTable( );
            adptr.Fill(table);
            connect.Close( );
            return table;
        }
         public void combobox()
         {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            comboBox3.Items.Clear();
            DataTable prep = Zapros("SELECT TOP (1000)[ФИО]\r\n  FROM [deconat_Oberuhtina].[dbo].[Преподователи]");
            for(int i = 0; i < prep.Rows.Count; i++)
            {
                comboBox1.Items.Add(prep.Rows[i][0]);
                comboBox4.Items.Add(prep.Rows[i][0]);
            }
            DataTable dis = Zapros("SELECT TOP (1000) [Название]\r\n  FROM [deconat_Oberuhtina].[dbo].[Дисциплины]");
            for (int i = 0; i < dis.Rows.Count; i++)
            {
                comboBox2.Items.Add(dis.Rows[i][0]);
                comboBox3.Items.Add(dis.Rows[i][0]);
                comboBox5.Items.Add(dis.Rows[i][0]);
            }
            
            
        }
        private void button1_Click (object sender, EventArgs e)
            
        { //на совпадение кода
           
            DataTable cheakcod = new DataTable( );
           cheakcod = Zapros( $"SELECT TOP (1000) [Код]\r\n      \r\n  FROM [deconat_Oberuhtina].[dbo].[Преподователи]where [Код]='{numericUpDown1.Value}'");
            DataTable cheakfio = new DataTable( );
            cheakfio = Zapros($"SELECT TOP (1000) [Код]\r\n      \r\n  FROM [deconat_Oberuhtina].[dbo].[Преподователи]where [ФИО]='{textBox1.Text}'");
            if (textBox1.Text=="" ) MessageBox.Show("Введите ФИО", "Ошибка");          
            else if( cheakcod.Rows.Count > 0 ) MessageBox.Show("Код совпадает", "Ошибка");
            else if( cheakfio.Rows.Count > 0 ) MessageBox.Show("Фио совподает", "Ошибка");
            else
            {
                DataTable prep= new DataTable( );
                prep = Zapros($"INSERT INTO dbo.Преподователи (Код,ФИО) VALUES('{numericUpDown1.Value}','{textBox1.Text}')");
                prepod();
                discplin();
                control();
                combobox();
            } 
           
        }

        private void button2_Click (object sender, EventArgs e)
        {
           
            DataTable cheakcod = new DataTable( );
            cheakcod = Zapros($"SELECT TOP (1000) [Код]\r\n      \r\n  FROM [deconat_Oberuhtina].[dbo].[Дисциплины]where [Код]='{numericUpDown2.Value}'");
            DataTable cheakdis = new DataTable( );
            cheakdis = Zapros($"SELECT TOP (1000) [Код]\r\n      \r\n  FROM [deconat_Oberuhtina].[dbo].[Дисциплины]where [Название]='{textBox2.Text}'");
            if ( textBox2.Text == "" &&textBox3.Text=="" ) MessageBox.Show("Введите название/категорию ", "Ошибка");
            else if ( cheakcod.Rows.Count > 0 ) MessageBox.Show("Код совпадает", "Ошибка");
            else if ( cheakdis.Rows.Count > 0 ) MessageBox.Show("Фио совподает", "Ошибка");
            else
            {
                DataTable discp = new DataTable( );
                discp = Zapros($"INSERT INTO dbo.Дисциплины (Код,Название,Категория,[Объем часов]) VALUES ({numericUpDown2.Value},'{textBox2.Text}','{textBox3.Text}',{numericUpDown3.Value})");
                prepod();
                discplin();
                control();
                combobox();
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            DataTable cheakcod = new DataTable();
            cheakcod = Zapros($"SELECT TOP (1000) [Код_записи]\r\n      \r\n  FROM [deconat_Oberuhtina].[dbo].[Контроль]where [Код_записи]='{numericUpDown4.Value}'");
            //DataTable cheadate1 = new DataTable();
            //cheadate1 = Zapros($"SELECT TOP (1000) [Преподователь],[Дата] FROM [deconat_Oberuhtina].[dbo].[Контроль]where [Преподователь]='{comboBox1.Text}'and [Дата]='{dateTimePicker1.Text}'");
            //DataTable cheadate2 = new DataTable();
            //cheadate2 = Zapros($"SELECT TOP (1000) [Группа],[Дата] FROM [deconat_Oberuhtina].[dbo].[Контроль]where [Группа]='{textBox4.Text}'and [Дата]='{dateTimePicker1.Text}'");
            if (textBox4.Text == "" && textBox5.Text == ""&&comboBox1.Text==""&&comboBox2.Text=="") MessageBox.Show("Пустые поля", "Ошибка");
           
            else if (cheakcod.Rows.Count > 0) MessageBox.Show("Код совпадает", "Ошибка");
            //else if (cheadate1.Rows.Count > 0) MessageBox.Show("В этот день у преподователя уже стоит контроль", "Ошибка");
            //else if (cheadate2.Rows.Count > 0) MessageBox.Show("В этот день у группы уже стоит контроль", "Ошибка");
            else
            {
                DataTable chec1 = Zapros($"SELECT [Код] FROM [deconat_Oberuhtina].[dbo].[Преподователи]where [ФИО]='{comboBox1.Text}'");
                DataTable chec2 = Zapros($"SELECT [Код] FROM [deconat_Oberuhtina].[dbo].[Дисциплины]where [Название]='{comboBox2.Text}'");
                DataTable discp = new DataTable();
                discp = Zapros($"INSERT INTO dbo.Контроль(Код_записи,Группа,Преподователь,Дисциплинна,Вид_контроля,Дата) VALUES ('{numericUpDown4.Value}','{textBox4.Text}','{chec1.Rows[0][0]}','{chec2.Rows[0][0]}','{textBox5.Text}','{dateTimePicker1.Value}')");
                prepod();
                discplin();
                control();
                combobox();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int stroca = dataGridView1.CurrentCell.RowIndex;
            string number = Convert.ToString(dataGridView1[0, stroca].Value);
            Zapros($"DELETE FROM [Преподователи]WHERE [Код]='{number}'");
            Zapros($"DELETE FROM [Контроль]WHERE [Преподователь]='{number}'");
            prepod();
            discplin();
            control();
            combobox();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int stroca = dataGridView2.CurrentCell.RowIndex;
            string number = Convert.ToString(dataGridView2[0, stroca].Value);
            Zapros($"DELETE FROM [Дисциплины]WHERE [Код]='{number}'");
            Zapros($"DELETE FROM [Контроль]WHERE [Дисциплинна]='{number}'");
            prepod();
            discplin();
            control();
            combobox();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int stroca = dataGridView3.CurrentCell.RowIndex;
            string number = Convert.ToString(dataGridView3[0, stroca].Value);
            Zapros($"DELETE FROM [Контроль]WHERE [Код_записи]='{number}'");
            prepod();
            discplin();
            control();
            combobox();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text=="") MessageBox.Show("Выберите из списка", "Ошибка");
            else
            {
                combobox();
            DataTable chec1 = Zapros($"SELECT [Код] FROM [deconat_Oberuhtina].[dbo].[Дисциплины]where [Название]='{comboBox3.Text}'");
            DataTable zap=Zapros($"SELECT*\r\nFROM  Дисциплины INNER JOIN Контроль ON Контроль.Дисциплинна=Дисциплины.Код\r\nWhere Дисциплины.Код={chec1.Rows[0][0]}");
            dataGridView4.DataSource = zap;

            }
            
            
           
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (comboBox4.Text == "") MessageBox.Show("Выберите из списка", "Ошибка");
            else
            { combobox();
            DataTable chec1 = Zapros($"SELECT [Код] FROM [deconat_Oberuhtina].[dbo].[Преподователи]where [ФИО]='{comboBox4.Text}'");
            DataTable zap = Zapros($"SELECT*\r\nFROM  Преподователи INNER JOIN Контроль ON Контроль.Преподователь=Преподователи.Код\r\nWhere Преподователи.Код={chec1.Rows[0][0]}");
            dataGridView4.DataSource = zap;}
                
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (comboBox5.Text == "") MessageBox.Show("Выберите из списка", "Ошибка");
            else
            {combobox();
            DataTable chec1 = Zapros($"SELECT [Код] FROM [deconat_Oberuhtina].[dbo].[Дисциплины]where [Название]='{comboBox5.Text}'");
            DataTable zap = Zapros($"select Контроль.Группа,Контроль.Дисциплинна\r\nFrom deconat_Oberuhtina.dbo.Контроль Where Дисциплинна='{chec1.Rows[0][0]}'");
            dataGridView4.DataSource = zap;

            }
                
        }
    }
}
