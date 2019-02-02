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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private DataTable CheckLot(string orderNo)
        {
            var kittingData = MST.MES.SqlDataReaderMethods.Kitting.GetOneOrderByDataReader(orderNo);
            DataTable result = new DataTable();
            if (kittingData.orderNo != null)
            {
                if (kittingData.endDate < DateTime.Now.AddYears(-50))
                {
                    labelStatus.Text = $"Status: zlecenie nie jest zakończone!";
                    buttonEndOrder.Visible = true;
                }
                else
                {
                    labelStatus.Text = $"Status: zlecenie zakończone: {kittingData.endDate.ToString("dd-MM-yyyy HH:mm:ss")}";
                    buttonEndOrder.Visible = false;
                }


                DataTable sqlLedsForOrder = MST.MES.SqlOperations.SparingLedInfo.GetReelsForLot(orderNo);
                Dictionary<string, Dictionary<string, DataTable>> detailedLedInfo = FullLedInfo(sqlLedsForOrder);
                
                result.Columns.Add("LED_12NC");
                result.Columns.Add("ID");
                result.Columns.Add("Partia");
                result.Columns.Add("Ilosc zużyta");
                result.Columns.Add("Ilosc aktualna");

                foreach (var nc12Entry in detailedLedInfo)
                {
                    foreach (var idEntry in nc12Entry.Value)
                    {
                        double startQty = 0;
                        double totalQty = 0;
                        string partia = "";
                        double currentQty = 0;
                        for (int r = 0; r < idEntry.Value.Rows.Count; r++)
                        {
                            partia = idEntry.Value.Rows[r]["Partia"].ToString();
                            if (idEntry.Value.Rows[r]["ZlecenieString"].ToString() != orderNo) continue;
                            startQty = double.Parse(idEntry.Value.Rows[r]["qty"].ToString());

                            if (r < idEntry.Value.Rows.Count - 1)
                            {
                                for (int rr = r + 1; rr < idEntry.Value.Rows.Count; rr++)
                                {
                                    double endQty = double.Parse(idEntry.Value.Rows[rr - 1]["qty"].ToString());
                                    if (idEntry.Value.Rows[rr]["ZlecenieString"].ToString() != orderNo || endQty == 0)
                                    {
                                        totalQty += startQty - endQty;
                                        currentQty = endQty;
                                        r = rr;
                                        break;
                                    }

                                    if (rr == idEntry.Value.Rows.Count - 1)
                                    {
                                        endQty = double.Parse(idEntry.Value.Rows[rr]["qty"].ToString());
                                        totalQty += startQty - endQty;
                                        currentQty = endQty;
                                        r = rr;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                double endQty = double.Parse(idEntry.Value.Rows[r]["qty"].ToString());
                                totalQty += startQty - endQty;
                                currentQty = endQty;
                            }
                        }
                        result.Rows.Add(nc12Entry.Key, idEntry.Key, partia, totalQty, currentQty);
                    }
                }
            }
            return result;
        }

        private Dictionary<string,Dictionary<string, DataTable>> FullLedInfo (DataTable lotSqlTable)
        {
            Dictionary<string, Dictionary<string, DataTable>> result = new Dictionary<string, Dictionary<string, DataTable>>();
            List<MST.MES.Data_structures.LedInfo> ledsInLot = new List<MST.MES.Data_structures.LedInfo>();
            foreach (DataRow row in lotSqlTable.Rows)
            {
                string nc12 = row["NC12"].ToString();
                string id = row["ID"].ToString();
                //string partia = row["Partia"].ToString();
                if (ledsInLot.Select(l => l.Nc12).Contains(nc12) & ledsInLot.Select(l => l.Id).Contains(id)) continue;
                ledsInLot.Add(new MST.MES.Data_structures.LedInfo(nc12, id));
            }

            DataTable detailedLedTable = MST.MES.SqlOperations.SparingLedInfo.GetInfoForMultiple12NC_ID(ledsInLot.ToArray());

            foreach (DataRow row in detailedLedTable.Rows)
            {
                DataTable templateTable = new DataTable();
                templateTable.Columns.Add("nc12");
                templateTable.Columns.Add("id");
                templateTable.Columns.Add("Partia");
                templateTable.Columns.Add("qty");
                templateTable.Columns.Add("zlecenieString");

                
                string nc12 = row["NC12"].ToString();
                string id = row["ID"].ToString();
                string partia = row["Partia"].ToString();
                string zlecenieString = row["zlecenieString"].ToString();
                string qty = row["Ilosc"].ToString();

                if (!result.ContainsKey(nc12)) {
                    result.Add(nc12, new Dictionary<string, DataTable>()); }

                if (!result[nc12].ContainsKey(id)) {
                    result[nc12].Add(id, templateTable.Clone()); }

                result[nc12][id].Rows.Add(nc12, id,partia, qty, zlecenieString);
            }
            return result;
        }

        private void textBoxOrderNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                
                dataGridView1.DataSource = CheckLot(textBoxOrderNo.Text);
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
        }

        private void buttonEndOrder_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Możesz zakończyć zlecenie, jeżeli masz pewność, że wszystkie końcowki LED pozostałe po tym zleceniu zostały policzone i zaktualizowane w systemie." + Environment.NewLine + Environment.NewLine + "Czy chcesz zakończyć zlecenie?", "UWAGA", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                MST.MES.SqlOperations.Kitting.UpdateOrderEndDate(textBoxOrderNo.Text, DateTime.Now);
                //zapisz końcówki!!!!!!!!!!!
            }
        }
    }
}
