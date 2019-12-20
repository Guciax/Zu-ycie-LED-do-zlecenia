using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Zużycie_LED_do_zlecenia.Forms
{
    public partial class LoadOrderFromHistory : Form
    {
        private Dictionary<string, MST.MES.OrderStructureByOrderNo.Kitting> ordersKitting = new Dictionary<string, MST.MES.OrderStructureByOrderNo.Kitting>();
        private Dictionary<string, MST.MES.OrderStructureByOrderNo.SMT> ordersSmt = new Dictionary<string, MST.MES.OrderStructureByOrderNo.SMT>();
        private Dictionary<string, List<MST.MES.OrderStructureByOrderNo.BoxingInfo>> ordersBox = new Dictionary<string, List<MST.MES.OrderStructureByOrderNo.BoxingInfo>>();
        public string selectedOrderNumber = "";

        public LoadOrderFromHistory()
        {
            InitializeComponent();
        }

        private void LoadOrderFromHistory_Load(object sender, EventArgs e)
        {
            ordersKitting = MST.MES.SqlDataReaderMethods.Kitting.GetOrdersInfoByDataReader(30);
            var orders = ordersKitting.Where(o => o.Value.odredGroup == "MST").Select(o => o.Key).ToArray();
            ordersSmt = MST.MES.SqlDataReaderMethods.SMT.GetOrders(orders);
            ordersBox = MST.MES.SqlDataReaderMethods.Boxing.GetMstBoxingForOrders(orders);
            FillOutGrid(orders);
        }

        private void FillOutGrid(string[] mstOrders)
        {
            int finishedOrdersIndex = 0;
            foreach (var orderNumber in mstOrders)
            {
                int smtQty = 0;
                if (ordersSmt.ContainsKey(orderNumber))
                {
                    smtQty = ordersSmt[orderNumber].totalManufacturedQty;
                }
                int boxedQty = 0;
                if (ordersBox.ContainsKey(orderNumber))
                {
                    boxedQty = ordersBox[orderNumber].Count();
                }

                if (ordersKitting[orderNumber].endDate > ordersKitting[orderNumber].kittingDate)
                {
                    dataGridView1.Rows.Add("<- Wczytaj", ordersKitting[orderNumber].orderNo, ordersKitting[orderNumber].modelId_12NCFormat, ordersKitting[orderNumber].ModelName, ordersKitting[orderNumber].orderedQty, ordersKitting[orderNumber].kittingDate, smtQty, boxedQty);
                    foreach (DataGridViewCell cell in dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells)
                    {
                        cell.Style.BackColor = Color.Gray;
                    }
                }
                else
                {
                    dataGridView1.Rows.Insert(finishedOrdersIndex, "<- Wczytaj", ordersKitting[orderNumber].orderNo, ordersKitting[orderNumber].modelId_12NCFormat, ordersKitting[orderNumber].ModelName, ordersKitting[orderNumber].orderedQty, ordersKitting[orderNumber].kittingDate, smtQty, boxedQty);
                    finishedOrdersIndex++;
                }
            }
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 & e.ColumnIndex == 0)
            {
                selectedOrderNumber = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}