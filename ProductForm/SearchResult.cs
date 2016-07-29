using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductForm
{
    public partial class SearchResult : Form
    {

        public SearchResult()
        {
            InitializeComponent();
        }

        public void ProductInfo(string code, string name, string price, string quantity, string manufacturer) //Or params string[] values
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dataGridView1);
            r.SetValues(code, name, price, quantity, manufacturer);
            /*
             * for (int i = 0; i < r.Cells.Count; i++) 
             * {
             *      r.Cells[i].Value = values[i];
             * }
             */
            dataGridView1.Rows.Add(r);
        }
    }
}
