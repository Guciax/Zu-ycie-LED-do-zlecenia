using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zużycie_LED_do_zlecenia.Forms;

namespace Zużycie_LED_do_zlecenia
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
        }

        private double ledsUsed = 0;
        DataTable sourceTable = new DataTable();
        MST.MES.OrderStructureByOrderNo.Kitting kittingData = new MST.MES.OrderStructureByOrderNo.Kitting();
        MST.MES.OrderStructureByOrderNo.SMT smtData = new MST.MES.OrderStructureByOrderNo.SMT();
        MST.MES.ModelInfo.ModelSpecification modelSpec = new MST.MES.ModelInfo.ModelSpecification();
        MST.MES.OrderStructureByOrderNo.TestInfo testData = new MST.MES.OrderStructureByOrderNo.TestInfo();
        MST.MES.OrderStructureByOrderNo.VisualInspection viData = new MST.MES.OrderStructureByOrderNo.VisualInspection();
        List<MST.MES.OrderStructureByOrderNo.BoxingInfo> boxingData = new List<MST.MES.OrderStructureByOrderNo.BoxingInfo>();

        string[] userList = new string[] { "piotr.dabrowski", "wojciech.komor", "katarzyna.kustra", "tomasz.jurkin", "grazyna.fabisiak", "mariola.czernis" };
        string currentUser = Environment.UserName;

        private void textBoxOrderNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                
                pictureBox1.Visible = true;
                backgroundWorker1.RunWorkerAsync();
                ledsUsed = 0;   
            }
        }

        Dictionary<string, DataTable> detailedLedInfoDict = new Dictionary<string, DataTable>();
        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            detailedLedInfoDict = new Dictionary<string, DataTable>();
            string orderNo = textBoxOrderNo.Text;
            kittingData = MST.MES.SqlDataReaderMethods.Kitting.GetOneOrderByDataReader(orderNo);
            if (kittingData.modelId != null)
            {
                smtData = MST.MES.SqlDataReaderMethods.SMT.GetOneOrder(orderNo);
                modelSpec = MST.MES.SqlDataReaderMethods.MesModels.mesModel(kittingData.modelId);
                viData = MST.MES.SqlDataReaderMethods.VisualInspection.GetViForOneOrder(orderNo);
                //testData = MST.MES.SqlDataReaderMethods.LedTest.GetTestRecordsForOneOrder(MST.MES.SqlDataReaderMethods.LedTest.TesterIdToName(), orderNo, -1);
                boxingData = MST.MES.SqlDataReaderMethods.Boxing.GetBoxingForOneOrder(kittingData.orderNo);

                DataTable result = new DataTable();
                if (kittingData.orderNo != null)
                {
                    DataTable sqlLedsForOrder = MST.MES.SqlOperations.SparingLedInfo.GetReelsForLot(orderNo);
                    backgroundWorker1.ReportProgress(50, 0);
                    Dictionary<string, Dictionary<string, DataTable>> detailedLedInfo = LedTools.FullLedInfo(sqlLedsForOrder);
                    backgroundWorker1.ReportProgress(80, 0);
                    result.Columns.Add("LED_12NC");
                    result.Columns.Add("ID");
                    result.Columns.Add("Partia");
                    result.Columns.Add("Ilosc zużyta");
                    result.Columns.Add("Ilosc aktualna");

                    foreach (var nc12Entry in detailedLedInfo)
                    {
                        foreach (var idEntry in nc12Entry.Value)
                        {
                            double totalQty = 0;
                            string partia = idEntry.Value.Rows[0]["Partia"].ToString();
                            detailedLedInfoDict.Add(nc12Entry.Key + idEntry.Key, idEntry.Value);
                            foreach (DataRow row in idEntry.Value.Rows)
                            {
                                string zlecenieString = row["ZlecenieString"].ToString();
                                if (zlecenieString != kittingData.orderNo) continue;

                                int usedLed = int.Parse(row["zuzycie"].ToString());
                                totalQty += usedLed;
                            }
                            int currentQty = int.Parse(idEntry.Value.Rows[idEntry.Value.Rows.Count - 1]["qty"].ToString());

                            ledsUsed += totalQty;
                            result.Rows.Add(nc12Entry.Key, idEntry.Key, partia, totalQty, currentQty);
                        }
                    }
                }
                sourceTable = result;
            }
        }

        private void buttonEndOrder_Click(object sender, EventArgs e)
        {
            if (userList.Contains(currentUser))
            {
                DialogResult dialogResult = MessageBox.Show("Możesz zakończyć zlecenie, jeżeli masz pewność, że wszystkie końcowki LED pozostałe po tym zleceniu zostały policzone i zaktualizowane w systemie." + Environment.NewLine + Environment.NewLine + "Czy chcesz zakończyć zlecenie?", "UWAGA", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    MST.MES.SqlOperations.Kitting.UpdateOrderEndDate(textBoxOrderNo.Text, DateTime.Now);
                    MST.MES.SqlOperations.SMT.UpdateLedUsedAmount(textBoxOrderNo.Text, (int)ledsUsed);

                    labelStatus.Text = $"Status: zlecenie zakończone: {DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}";
                    buttonEndOrder.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Brak uprawnień");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Parent = this;
            pictureBox1.BringToFront();
            pictureBox1.Width = textBoxOrderNo.Width - 4;
            pictureBox1.Height = textBoxOrderNo.Height - 4;
            pictureBox1.Location = new Point(textBoxOrderNo.Location.X + 2, textBoxOrderNo.Location.Y + 2);
            pictureBox1.Visible = false;

            DataGridViewButtonColumn butCol = new DataGridViewButtonColumn();
            butCol.HeaderText = "Edytuj Ilość";
            butCol.Name = "butCol";
            dataGridView1.Columns.Add(butCol);

        }


        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (kittingData.modelId != null)
            {
                dataGridView1.DataSource = sourceTable;
                dataGridView1.Columns["butCol"].DisplayIndex = dataGridView1.Columns.Count - 1;

                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }

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

                labelOrderInfo.Text =
                    $"Data utworzenia zlecenia: {kittingData.kittingDate.ToShortDateString()}" +
                    Environment.NewLine + $"Ilość zamówienia: {kittingData.orderedQty} szt.";


                labelModelInfo.Text = modelSpec.modelName + Environment.NewLine
                    + kittingData.modelId.Insert(4, " ").Insert(8, " ") + Environment.NewLine
                    + $"Ilość PCB/MB: {modelSpec.pcbCountPerMB + Environment.NewLine}"
                    + $"Ilość LED: {modelSpec.ledCountPerModel + Environment.NewLine}"
                    + $"Ilość Res: {modelSpec.resistorCountPerModel + Environment.NewLine}"
                    + $"Ilość Conn: {modelSpec.connectorCountPerModel + Environment.NewLine}";

                var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
                nfi.NumberGroupSeparator = " ";
                labelTeorUsage.Text = $"Teoretyczne zużycie LED: {smtData.totalManufacturedQty}szt. PCB x {modelSpec.ledCountPerModel}LED = {(smtData.totalManufacturedQty * modelSpec.ledCountPerModel).ToString("#,0", nfi)}";
                labelRealUsage.Text = $"Rzeczywiste zużycie LED: {ledsUsed.ToString("#,0", nfi)}";
                pictureBox1.Visible = false;

                labelProductionInfo.Text = $"Ilość po SMT: {smtData.totalManufacturedQty} szt." + Environment.NewLine;
                labelSmtDate.Text = $"Produkcja  od: {smtData.earliestStart.ToString("HH:mm dd-MM-yyyy")} do: {smtData.latestEnd.ToString("HH:mm dd-MM-yyyy")}";
                labelSmtLines.Text = "Linia SMT: " + string.Join(", ", smtData.smtLinesInvolved);

                labelPackedQty.Text = $"Spakowanych wyrobów: {boxingData.Count().ToString()} szt.";
                labelBoxesCount.Text = $"Ilość kartonów: {boxingData.Select(m => m.boxId).Distinct().Count()} szt.";

                if (smtData.totalManufacturedQty > 0)
                {
                    buttonChangeSmtQty.Enabled = true;
                }
                else
                {
                    buttonChangeSmtQty.Enabled = false;
                }

                int realNg = viData.ngCount;
                labelNg.Text = $"NG: {realNg} szt.";
                if (viData.ngCount > 0)
                {
                    int waitingForRepair = viData.ngScrapList.Where(r => !r.reworkOK.HasValue & r.viInspectionResult == "NG").Count();
                    string textForRepair = "";
                    if (waitingForRepair > 0)
                    {
                        textForRepair = $", oczekujących na naprawę: {waitingForRepair}szt.";
                    }
                    labelNg.Text += $" (naprawionych {viData.reworkedOkCout}szt.{textForRepair})";
                }
                labelScrap.Text = $"SCRAP: {viData.scrapCount} szt.";
                if (viData.reworkFailedCout > 0)
                {
                    labelScrap.Text += $" (w tym {viData.reworkFailedCout}szt. - nieudana naprawa)";
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>-1 && e.ColumnIndex>-1)
            {

                string ncId = dataGridView1.Rows[e.RowIndex].Cells["LED_12NC"].Value.ToString() + dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                if (detailedLedInfoDict.ContainsKey(ncId))
                {
                    Details detForm = new Details(detailedLedInfoDict[ncId]);
                    detForm.Show();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>-1 & e.ColumnIndex > -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name== "butCol")
                {
                    if (userList.Contains(currentUser))
                    {
                        string nc12 = dataGridView1.Rows[e.RowIndex].Cells["LED_12NC"].Value.ToString();
                        string id = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();

                        using (EditLedQty editForm = new EditLedQty(nc12, id, kittingData.orderNo))
                        {
                            if (editForm.ShowDialog() == DialogResult.OK)
                            {
                                ReloadCurrentOrder();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Brak uprawnień");
                    }
                }
            }
        }

        private void ReloadCurrentOrder()
        {
            pictureBox1.Visible = true;
            textBoxOrderNo.Text = kittingData.orderNo;
            backgroundWorker1.RunWorkerAsync();
            ledsUsed = 0;
        }

        private void buttonChangeSmtQty_Click(object sender, EventArgs e)
        {
            if (userList.Contains(currentUser))
            {
                using (ChangeSMTqty changeForm = new ChangeSMTqty(smtData.smtOrders))
                {
                    if (changeForm.ShowDialog() == DialogResult.OK)
                    {
                        ReloadCurrentOrder();
                    }
                }
            }
            else
            {
                MessageBox.Show("Brak uprawnień");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (LoadOrderFromHistory historyForm = new LoadOrderFromHistory())
            {
                if (historyForm.ShowDialog() == DialogResult.OK)
                {
                    textBoxOrderNo.Text = historyForm.selectedOrderNumber;
                    pictureBox1.Visible = true;
                    backgroundWorker1.RunWorkerAsync();
                    ledsUsed = 0;
                }
            }
        }
    }
}
