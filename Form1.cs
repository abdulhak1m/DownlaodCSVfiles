using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadCSVfiles
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //private void volunteerBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        //{
        //    this.Validate();
        //    //this.volunteerBindingSource.EndEdit();
        //    //this.tableAdapterManager.UpdateAll(this.dbMaraphoneDataSet);

        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbMaraphoneDataSet.Volunteer". При необходимости она может быть перемещена или удалена.
            this.volunteerTableAdapter.Fill(this.dbMaraphoneDataSet.Volunteer);
            label4.Text = this.volunteerBindingSource.Count.ToString();
        }

        private void btn_Open_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog file = new OpenFileDialog())
            {
                if (file.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = file.FileName;
                }
            }
        }

        private void btn_Download_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader sr = new StreamReader(textBox1.Text);
                bool flag = false;
                while (!sr.EndOfStream)
                {
                    if (flag == true)
                    {
                        string line = sr.ReadLine();
                        if (line == "")
                            break;
                        string[] temp = line.Split(',');
                        this.volunteerTableAdapter.Insert(temp[1].Trim(), temp[2].Trim(), temp[3].Trim(), temp[4].Trim());
                    }
                    flag = true;
                }
                sr.Close();
                MessageBox.Show("!");
                textBox1.Text = "Статус загрузки: успешно загружено";
                MessageBox.Show(this, "Информация о волонтерах успешно загружена!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source);
            }
        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            panel2.BringToFront();
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            this.Update();
            this.volunteerTableAdapter.Fill(this.dbMaraphoneDataSet.Volunteer);
            label4.Text = this.volunteerBindingSource.Count.ToString();
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            volunteerBindingSource.RemoveFilter();
            switch (comboBox1.Text)
            {
                case "Имя":
                    dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Ascending);
                    break;
                case "Фамилия":
                    dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Ascending);
                    break;
                case "Страна":
                    dataGridView1.Sort(dataGridView1.Columns[3], ListSortDirection.Ascending);
                    break;
                case "Пол":
                    dataGridView1.Sort(dataGridView1.Columns[4], ListSortDirection.Ascending);
                    break;
            }
            label4.Text = this.volunteerBindingSource.Count.ToString();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            panel1.BringToFront();
        }
    }
}
