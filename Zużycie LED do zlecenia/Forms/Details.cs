using Graffiti.MST;
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
        private readonly IEnumerable<ComponentsTools.ComponentStruct> inputData;
        List<Color> colorList = new List<Color>();

        public Details(IEnumerable<ComponentsTools.ComponentStruct> inputData)
        {
            InitializeComponent();
            this.inputData = inputData.OrderBy(x => x.operationDate);
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
            DataTable sourceTable = new DataTable();
            sourceTable.Columns.Add("Data");
            sourceTable.Columns.Add("Zlecenie");
            sourceTable.Columns.Add("Ilość");
            sourceTable.Columns.Add("Zużycie");
            sourceTable.Columns.Add("Lokalizacja");
            double previousQty = inputData.First().Quantity;
            foreach (var record in inputData)
            {
                sourceTable.Rows.Add(
                    record.operationDate,
                    record.ConnectedToOrder,
                    record.Quantity,
                    previousQty - record.Quantity,
                    record.Location
                    );
                previousQty = record.Quantity;
            }

            dataGridView1.DataSource = sourceTable;

        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            List<string> ordersList = new List<string>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string order = row.Cells["Zlecenie"].Value.ToString();
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
