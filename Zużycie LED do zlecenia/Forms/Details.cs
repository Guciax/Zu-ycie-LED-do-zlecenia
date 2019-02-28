using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zużycie_LED_do_zlecenia
{
    public partial class Details : Form
    {
        private readonly DataTable sourceTable;
        List<Color> colorList = new List<Color>();

        public Details(DataTable sourceTable)
        {
            InitializeComponent();
            this.sourceTable = sourceTable;
            colorList.Add(Color.FromArgb(255,255, 215, 119));
                colorList.Add(Color.FromArgb(255, 17, 215, 54));
            colorList.Add(Color.FromArgb(255, 41, 255, 179));
            colorList.Add(Color.FromArgb(255, 41, 239, 255));
            colorList.Add(Color.FromArgb(255, 41, 155, 255));
            colorList.Add(Color.FromArgb(255, 72, 103, 255));
            colorList.Add(Color.FromArgb(255, 137, 72, 255));
            colorList.Add(Color.FromArgb(255, 228, 72, 255));
            colorList.Add(Color.FromArgb(255, 255, 72, 173));
            colorList.Add(Color.FromArgb(255, 255, 255, 255));


        }

        private void Details_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = sourceTable;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            int orderCount = 0;
            List<string> ordersList = new List<string>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string order = row.Cells["zlecenieString"].Value.ToString();
                if (!ordersList.Contains(order)) ordersList.Add(order);

                ColorRow(row, colorList[ordersList.IndexOf(order)]);
            }
        }

        private void ColorRow(DataGridViewRow row, Color clr)
        {
            foreach (DataGridViewCell cell in row.Cells)
            {
                cell.Style.BackColor = clr;
            }
        }
    }
}
