using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SQLManagement
{
    public class SQL
    {
        Form form;
        public SqlConnection dataBase;
        DataGridView dataTable = new DataGridView();

        string dbName = "ProjeDB";
        string server = ".";
        string tableName = "userDB";

        string connStr;
        string masterStr;
        string createDataBaseScript;
        string createTableScript;
        string insertDataScript;
        string showDataScript;

        int gridTableWidth;
        int gridTableHeight;
        int gridTableX;
        int gridTableY;
        public SQL(Form form)
        {
            this.form = form;

            this.initSQL();
        }
        public void initSQL()
        {
            this.connStr = $"Data Source={this.server};Initial Catalog={this.dbName};Integrated Security=True;TrustServerCertificate=True;";
            this.masterStr = $"Data Source={this.server};Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True;";
            this.createDataBaseScript = $"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '{this.dbName}') CREATE DATABASE {this.dbName}";
            this.createTableScript = $@"IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = '{this.tableName}')
                            CREATE TABLE {this.tableName} (id INT PRIMARY KEY IDENTITY(1,1), name NVARCHAR(50) NOT NULL DEFAULT 'noname',score INT)";
            this.insertDataScript = $"INSERT INTO {this.tableName} (name,score) VALUES (@p1,@p2)";
            this.showDataScript = $"SELECT TOP 5 name,score FROM {this.tableName} ORDER BY score DESC";


            this.CreateDataBase();
            this.dataBase = new SqlConnection(this.connStr);

            this.CreateTable();

            this.gridTableWidth = 200;
            this.gridTableHeight = 130;
            this.gridTableX = this.form.Width - this.gridTableWidth - 25;
            this.gridTableY = this.form.Height / 4;

            this.CreateGridTable();
            this.ShowData();

        }
        public void CreateGridTable()
        {
            this.dataTable.Width = this.gridTableWidth;
            this.dataTable.Height = this.gridTableHeight;
            this.dataTable.Left = this.gridTableX;
            this.dataTable.Top = this.gridTableY;

            this.dataTable.AllowUserToAddRows = false;
            this.dataTable.ReadOnly = true;
            this.dataTable.AllowUserToDeleteRows = false;

            this.dataTable.ScrollBars = ScrollBars.Vertical;
            this.dataTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataTable.RowHeadersVisible = false; 
            this.dataTable.ColumnHeadersVisible = false;
            this.dataTable.BackgroundColor = Color.White;
            this.dataTable.BorderStyle = BorderStyle.None;
            this.dataTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Satýrý tam seçer
            this.dataTable.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249); // Bir satýr koyu bir satýr açýk renk

            this.form.Controls.Add(this.dataTable);
        }
        private void BaglantiyiAc()
        {
            // Nesnenin null olup olmadýðýný ve baðlantý metnini kontrol edin
            if (this.dataBase != null && this.dataBase.State == ConnectionState.Closed)
            {
                this.dataBase.Open();
            }
        }

        private void CreateDataBase()
        {
            // Master veri tabanýna baðlanarak yeni DB oluþturma yetkisi alýyoruz
            using (SqlConnection con = new SqlConnection(this.masterStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(this.createDataBaseScript, con);
                cmd.ExecuteNonQuery();
            }
        }

        public void CreateTable()
        {
            BaglantiyiAc(); // Baðlantýnýn açýk olduðundan emin oluyoruz
            using (SqlCommand cmd = new SqlCommand(this.createTableScript, this.dataBase))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertData(string name,float score)
        {
            BaglantiyiAc();
            using (SqlCommand cmd = new SqlCommand(this.insertDataScript, this.dataBase))
            {
                if (name == "") name = "noname";
                cmd.Parameters.AddWithValue("@p1", name);
                cmd.Parameters.AddWithValue("@p2", score);
                cmd.ExecuteNonQuery();
            }
        }
        public void ShowData()
        {
                BaglantiyiAc();
                SqlDataAdapter da = new SqlDataAdapter(this.showDataScript, this.dataBase);
                DataTable dt = new DataTable();
                da.Fill(dt);
                this.dataTable.DataSource = dt;
        }
        public void DeleteTable()
        {
                BaglantiyiAc(); // Baðlantýnýn açýk olduðundan emin olun

                // Tabloyu veritabanýndan tamamen silen script
                string deleteScript = $"IF EXISTS (SELECT * FROM sys.tables WHERE name = '{this.tableName}') DROP TABLE {this.tableName}";

                using (SqlCommand cmd = new SqlCommand(deleteScript, this.dataBase))
                {
                    cmd.ExecuteNonQuery(); // Komutu çalýþtýr
                }

                // DataGridView içeriðini temizle
                this.dataTable.DataSource = null;
            
        }
    }
}