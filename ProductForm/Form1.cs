using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ProductForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool checkInput()
        {
            string code = txtCode.Text;
            string name = txtName.Text;
            string price = txtPrice.Text;
            string quantity = txtQuantity.Text;
            string manu = txtManufacturer.Text;

            bool error = false;
            int prices = -1;
            int quantities = -1;
            errorProvider1.Clear();

            if (code.Equals(""))
            {
                errorProvider1.SetError(txtCode, "No empty allow please!");
                error = true;
            }

            if (name.Equals(""))
            {
                errorProvider1.SetError(txtName, "No empty allow please!");
                error = true;
            }

            if (price.Equals(""))
            {
                errorProvider1.SetError(txtPrice, "No empty allow please!");
                error = true;
            }
            else
            {
                try
                {
                    prices = int.Parse(price);
                    if (prices < 0)
                    {
                        int.Parse("nope");
                    }
                }
                catch (Exception)
                {
                    errorProvider1.SetError(txtPrice, "Must be number and larger than 0!");
                    error = true;
                }
            }

            if (quantity.Equals(""))
            {
                errorProvider1.SetError(txtQuantity, "No empty allow please!");
                error = true;
            }
            else
            {
                try
                {
                    quantities = int.Parse(quantity);
                    if (quantities < 0)
                    {
                        int.Parse("nope");
                    }
                }
                catch (Exception)
                {
                    errorProvider1.SetError(txtQuantity, "Must be number and larger than 0!");
                    error = true;
                }
            }

            if (manu.Equals(""))
            {
                errorProvider1.SetError(txtManufacturer, "No empty allow please!");
                error = true;
            }

            if (error) return false;
            else return true;
        }

        private bool addProduct()
        {
            bool added = true;

            try
            {
                DataGridViewRow r = new DataGridViewRow();
                r.CreateCells(dataGridView1);
                r.SetValues(txtCode.Text, txtName.Text, txtPrice.Text, txtQuantity.Text, txtManufacturer.Text);
                dataGridView1.Rows.Add(r);
                cbFilter.Items.Add(txtManufacturer.Text);
            }
            catch (Exception)
            {
                added = false;
            }

            return added;
        }

        private void SearchByName()
        {
            SearchResult form = new SearchResult();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewRow r = dataGridView1.Rows[i];
                if (r.Cells[1].Value.ToString().Contains(txtSearch.Text))
                {
                    form.ProductInfo(r.Cells[0].Value.ToString(), r.Cells[1].Value.ToString(), r.Cells[2].Value.ToString(), r.Cells[3].Value.ToString(), r.Cells[4].Value.ToString());
                }
            }
            form.ShowDialog();
        }

        private void SearchByMaunufacturer()
        {
            SearchResult form = new SearchResult();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewRow r = dataGridView1.Rows[i];
                if (r.Cells[4].Value.ToString().Contains(cbFilter.Text))
                {
                    form.ProductInfo(r.Cells[0].Value.ToString(), r.Cells[1].Value.ToString(), r.Cells[2].Value.ToString(), r.Cells[3].Value.ToString(), r.Cells[4].Value.ToString());
                }
            }
            form.ShowDialog();
        }

        private bool updateProduct()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow r = dataGridView1.SelectedRows[0];
                r.SetValues(txtCode.Text, txtName.Text, txtPrice.Text, txtQuantity.Text, txtManufacturer.Text);
                return true;
            }
            
            
            return false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (checkInput())
            {
                if (addProduct()) MessageBox.Show("Added Successful");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                MessageBox.Show("Removed Successful");
            }
            else
            {
                MessageBox.Show("Choose a row first");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow r = dataGridView1.SelectedRows[0];
                txtCode.Text = r.Cells[0].Value.ToString();
                txtName.Text = r.Cells[1].Value.ToString();
                txtPrice.Text = r.Cells[2].Value.ToString();
                txtQuantity.Text = r.Cells[3].Value.ToString();
                txtManufacturer.Text = r.Cells[4].Value.ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (checkInput())
            {
                if (updateProduct()) MessageBox.Show("Updated Successful");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int rows = dataGridView1.Rows.Count;
            StreamWriter writer = new StreamWriter("products.txt");
            for (int i = 0; i < rows; i++)
            {
                DataGridViewRow r = dataGridView1.Rows[i];
                string row = r.Cells[0].Value.ToString() + ";" + r.Cells[1].Value.ToString() + ";" + r.Cells[2].Value.ToString() + ";" + r.Cells[3].Value.ToString() + ";" + r.Cells[4].Value.ToString();
                writer.WriteLine(row);
            }
            writer.Close();
            MessageBox.Show("Saved Successful");
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            cbFilter.Items.Clear();
            StreamReader sr = new StreamReader("products.txt");
            dataGridView1.Rows.Clear();
            string input = null;
            while ((input = sr.ReadLine()) != null)
            {
                string[] rows = input.Split(';');
                DataGridViewRow r = new DataGridViewRow();
                r.CreateCells(dataGridView1);
                r.SetValues(rows[0], rows[1], rows[2], rows[3], rows[4]);
                dataGridView1.Rows.Add(r);
                cbFilter.Items.Add(rows[4]);
            }
            sr.Close();
            MessageBox.Show("Loaded Successful");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchByName();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            SearchByMaunufacturer();
        }
    }
}
