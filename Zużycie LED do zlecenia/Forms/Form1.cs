using Graffiti.MST;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Zużycie_LED_do_zlecenia.Forms;

namespace Zużycie_LED_do_zlecenia
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //bwLoadOrder.DoWork += bwLoadOrder_DoWork;
        }

        private double ledsUsed = 0;
        private double mbUsed = 0;
        private DataTable sourceTable = new DataTable();

        private string[] userList = new string[] { "piotr.dabrowski", "wojciech.komor", "katarzyna.kustra", "tomasz.jurkin", "grazyna.fabisiak", "mariola.czernis" };
        private string currentUser = Environment.UserName;

        private void textBoxOrderNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                pictureBox1.Visible = true;
                bwLoadOrder.RunWorkerAsync();
                ledsUsed = 0;
                e.Handled = true;
            }
        }

        private void textBoxOrderNo_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private Dictionary<string, IEnumerable<ComponentsTools.ComponentStruct>> detailedLedInfoDict = new Dictionary<string, IEnumerable<ComponentsTools.ComponentStruct>>();

        //off
        private async void bwLoadOrder_DoWork(object sender, DoWorkEventArgs e)
        {
            detailedLedInfoDict = new Dictionary<string, IEnumerable<ComponentsTools.ComponentStruct>>();
            string orderNo = textBoxOrderNo.Text.Trim();
            CurrentOrder.orderNo = orderNo;
            await MesDataStorage.LoadMesDataAsync(orderNo);

            if (MesDataStorage.kittingData.modelId != null)
            {
                DataTable result = new DataTable();
                if (MesDataStorage.kittingData.orderNo != null)
                {
                    GraffitiComponents.LoadComponents();
                    //DataTable sqlLedsForOrder = MST.MES.SqlOperations.SparingLedInfo.GetReelsForLot(orderNo);
                    var componentsUsedInOrder = GraffitiComponents.allComponents;
                    //var grouppedBy12Nc = componentsUsedInOrder.GroupBy(c => c.Nc12).Select(group => new { Nc12 = group.Key, Items = group.ToList() });
                    var grouppedBy12NcAndId = componentsUsedInOrder.GroupBy(c => new { c.Nc1_Rank, c.Id });

                    //bwLoadOrder.ReportProgress(50, 0);
                    //Dictionary<string, Dictionary<string, DataTable>> detailedLedInfo = LedTools.FullLedInfo(sqlLedsForOrder);
                    //bwLoadOrder.ReportProgress(80, 0);
                    result.Columns.Add("12NC");
                    result.Columns.Add("ID");
                    result.Columns.Add("Partia");
                    result.Columns.Add("Ilosc zużyta");
                    result.Columns.Add("Ilosc aktualna");

                    result.Rows.Add("Płyty PCB");


                    var detailedLedInfo = GraffitiComponents.componentsWithHistory;

                    foreach (var comp in grouppedBy12NcAndId)
                    {
                        if (!comp.Key.Nc1_Rank.StartsWith("4010560") & !comp.Key.Nc1_Rank.StartsWith("4010460") & !comp.Key.Nc1_Rank.StartsWith("4010441") & !comp.Key.Nc1_Rank.StartsWith("4010440")) continue;
                        var detailesForReel = detailedLedInfo.Where(x => x.Nc1_Rank == comp.Key.Nc1_Rank & x.Id == comp.Key.Id);

                        int quantityUsed = calculateLedUsedInOrder(detailesForReel, orderNo);

                        if (comp.Key.Nc1_Rank.StartsWith("4010560") || comp.Key.Nc1_Rank.StartsWith("4010460"))
                        {
                            ledsUsed += quantityUsed;
                            var newRow = result.NewRow();
                            newRow[0] = comp.Key.Nc1_Rank;
                            newRow[1] = comp.Key.Id;
                            newRow[2] = comp.First().deliveryDate.ToString("dd-MM-yyyy");
                            newRow[3] = quantityUsed;
                            newRow[4] = comp.OrderBy(x => x.operationDate).Last().Quantity;

                            result.Rows.InsertAt(newRow, 0);
                        }
                        else
                        {
                            mbUsed += quantityUsed;
                            result.Rows.Add(comp.Key.Nc1_Rank, comp.Key.Id, comp.First().deliveryDate.ToString("dd-MM-yyyy"), quantityUsed, comp.OrderBy(x => x.operationDate).Last().Quantity);
                        }

                        detailedLedInfoDict.Add(comp.Key.Nc1_Rank + comp.Key.Id, detailesForReel);
                    }

                    var ledDescrRow = result.NewRow();
                    ledDescrRow[0] = "Diody LED";
                    result.Rows.InsertAt(ledDescrRow, 0);
                }
                sourceTable = result;
            }
            ;
        }

        private int calculateLedUsedInOrder(IEnumerable<ComponentsTools.ComponentStruct> detailesForReel, string orderNo)
        {
            int result = 0;
            int prevQty = -1;
            var sortedByDate = detailesForReel.OrderBy(x => x.operationDate);
            foreach (var operation in sortedByDate)
            {
                if(operation.ConnectedToOrder.ToString() != orderNo)
                {
                    prevQty = -1;
                    continue;
                }

                if(prevQty == -1)
                {
                    prevQty = (int)operation.Quantity;
                    continue;
                }

                result += prevQty - (int)operation.Quantity;
            }

            return result;
        }

        private void buttonEndOrder_Click(object sender, EventArgs e)
        {
            if (!userList.Contains(currentUser))
            {
                MessageBox.Show("Brak uprawnień");
            }
            ConfirmOrder();
            //if (MesDataStorage.kittingData.ordertatus == MST.MES.OrderStatus.Status.ReadyToShip)
            //{
            //    //Ng waiting for repair - partial confirm
            //    if (CurrentOrder.VisualInspection.WaitingForRepair > 0)
            //    {
            //        PartialConfirm();
            //    }
            //    else
            //    {
            //        FullConfirm();
                    
            //    }
            //}

            //if(MesDataStorage.kittingData.ordertatus == MST.MES.OrderStatus.Status.ShippedNgNotDone)
            //{
            //    SecondConfirm();
            //}
        }

        private void ConfirmOrder()
        {
            MST.MES.SqlOperations.Kitting.UpdateOrderEndDate(textBoxOrderNo.Text, DateTime.Now);
            MST.MES.SqlOperations.SMT.UpdateLedUsedAmount(textBoxOrderNo.Text, (int)ledsUsed);
            MST.MES.OrderStatus.Status sts = MST.MES.OrderStatus.Status.SmtFinished;
            if (CurrentOrder.VisualInspection.WaitingForRepair > 0)
            {
                sts = MST.MES.OrderStatus.Status.ShippedNgNotDone;
            }
            double goodQty = MesDataStorage.boxingData.Count;
            double ngQty = CurrentOrder.VisualInspection.WaitingForRepair;
            double scrapQty = MesDataStorage.viData.scrapCount;
            MST.MES.OrderStatus.ChangeOrderQtyAndStatus(MesDataStorage.kittingData.orderNo, sts, goodQty, ngQty, scrapQty);
        }

        private void FullConfirm()
        {
            //DialogResult dialogResult = MessageBox.Show("Możesz zakończyć zlecenie, jeżeli masz pewność, że wszystkie końcowki LED pozostałe po tym zleceniu zostały policzone i zaktualizowane w systemie."
            //    + Environment.NewLine + $"Potwierdzonych zostanie: {MesDataStorage.boxingData.Count} wyrobu dobrego oraz {MesDataStorage.viData.scrapCount} scrap"
            //    + Environment.NewLine + Environment.NewLine + "Czy chcesz zakończyć zlecenie?", "UWAGA", MessageBoxButtons.YesNo) ;

            //if (dialogResult == DialogResult.Yes)
            //{
            //    MST.MES.SqlOperations.Kitting.UpdateOrderEndDate(textBoxOrderNo.Text, DateTime.Now);
            //    MST.MES.SqlOperations.SMT.UpdateLedUsedAmount(textBoxOrderNo.Text, (int)ledsUsed);
            //    MST.MES.OrderStatus.ChangeOrderStatus(MesDataStorage.kittingData.orderNo, MST.MES.OrderStatus.Status.Finished);

            //    Graffiti.MST.OrdersOperations.ConfirmOrder(int.Parse(MesDataStorage.kittingData.orderNo), MesDataStorage.boxingData.Count, MesDataStorage.viData.scrapCount);
            //    labelStatus.Text = $"Status: zlecenie zakończone: {DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}";
            //    buttonEndOrder.Visible = false;
            //}
        }
        private void PartialConfirm()
        {
            //DialogResult partialConfirmDialog = MessageBox.Show($"Na naprawę wiąż oczekuje {CurrentOrder.VisualInspection.WaitingForRepair} NG. Możliwe jest częściowe przesunięcie."
            //            + Environment.NewLine + "Po naprawie konieczne będzie końcowe rozliczenie naprawionych wyrobów"
            //            + Environment.NewLine + $"Potwierdzonych zostanie: {MesDataStorage.boxingData.Count} wyrobu dobrego oraz {MesDataStorage.viData.scrapCount} scrap"
            //            + Environment.NewLine + Environment.NewLine + "Czy chcesz częściowo przesunąć zlecenie?", "UWAGA", MessageBoxButtons.YesNo);

            //if (partialConfirmDialog == DialogResult.Yes)
            //{
            //    MST.MES.SqlOperations.Kitting.FinishOrder(
            //        CurrentOrder.orderNo,
            //        MesDataStorage.boxingData.Count,
            //        CurrentOrder.VisualInspection.WaitingForRepair,
            //        MesDataStorage.viData.scrapCount,
            //        MST.MES.OrderStatus.Status.ShippedNgNotDone);

            //    MST.MES.SqlOperations.SMT.UpdateLedUsedAmount(textBoxOrderNo.Text, (int)ledsUsed);

            //    Graffiti.MST.OrdersOperations.ConfirmOrder(int.Parse(MesDataStorage.kittingData.orderNo), MesDataStorage.boxingData.Count, MesDataStorage.viData.scrapCount);
            //    labelStatus.Text = $"Status: zlecenie częściowo potwierdzone: {DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}";
            //    buttonEndOrder.Visible = true;
            //}
        }
        private void SecondConfirm()
        {
            using (SecondConfirm confForm = new Forms.SecondConfirm((int)MesDataStorage.kittingData.confirmedNgQty))
            {
                if(confForm.ShowDialog() == DialogResult.OK)
                {
                    MST.MES.SqlOperations.Kitting.FinishOrder(
                        CurrentOrder.orderNo, 
                        (int)MesDataStorage.kittingData.confirmedGoodQty + confForm.goodQty, 
                        0, 
                        (int)MesDataStorage.kittingData.confirmedScrQty + confForm.scrQty, 
                        MST.MES.OrderStatus.Status.Finished);
                }
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
            if (MesDataStorage.kittingData != null)
            {
                dataGridView1.DataSource = sourceTable;
                dataGridView1.Columns["butCol"].DisplayIndex = dataGridView1.Columns.Count - 1;

                labelOrderInfo.Text =
                    $"Data utworzenia zlecenia: {MesDataStorage.kittingData.kittingDate.ToShortDateString()}" +
                    Environment.NewLine + $"Ilość zamówienia: {MesDataStorage.kittingData.orderedQty} szt.";

                labelModelInfo.Text = MesDataStorage.kittingData.modelSpec.modelName + Environment.NewLine
                    + MesDataStorage.kittingData.modelId.Insert(4, " ").Insert(8, " ") + Environment.NewLine
                    + $"Ilość PCB/MB: {MesDataStorage.kittingData.modelSpec.pcbCountPerMB + Environment.NewLine}"
                    + $"Ilość LED: {MesDataStorage.kittingData.modelSpec.ledCountPerModel + Environment.NewLine}"
                    + $"Ilość Res: {MesDataStorage.kittingData.modelSpec.resistorCountPerModel + Environment.NewLine}"
                    + $"Ilość Conn: {MesDataStorage.kittingData.modelSpec.connectorCountPerModel + Environment.NewLine}";

                var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
                nfi.NumberGroupSeparator = " ";
                var theorUsage = (MesDataStorage.smtData.totalManufacturedQty + MesDataStorage.smtData.totalNg) * MesDataStorage.kittingData.modelSpec.ledCountPerModel;
                labelTeorUsage.Text = $"Teoretyczne zużycie LED: {MesDataStorage.smtData.totalManufacturedQty + MesDataStorage.smtData.totalNg}szt. PCB x {MesDataStorage.kittingData.modelSpec.ledCountPerModel}LED = {(theorUsage).ToString("#,0", nfi)}";
                var waste = ((ledsUsed - theorUsage) / theorUsage * 100).ToString("F2");
                labelRealUsage.Text = $"Rzeczywiste zużycie LED: {ledsUsed.ToString("#,0", nfi)}   =>   {waste}%";
                lMbUsage.Text = $"Zużycie MB: {mbUsed}szt.";
                lPcbUsage.Text = $"Zużycie PCB: {mbUsed* MesDataStorage.kittingData.modelSpec.pcbCountPerMB}szt.";
                pictureBox1.Visible = false;

                labelProductionInfo.Text = $"Ilość po SMT: {MesDataStorage.smtData.totalManufacturedQty} szt." + Environment.NewLine;
                lSmtNg.Text = $"Odpad: {MesDataStorage.smtData.totalNg} szt.";
                labelSmtDate.Text = $"Produkcja  od: {MesDataStorage.smtData.earliestStart.ToString("HH:mm dd-MM-yyyy")} do: {MesDataStorage.smtData.latestEnd.ToString("HH:mm dd-MM-yyyy")}";
                labelSmtLines.Text = "Linia SMT: " + string.Join(", ", MesDataStorage.smtData.smtLinesInvolved);

                labelPackedQty.Text = $"Spakowanych wyrobów: {MesDataStorage.boxingData.Count().ToString()} szt.";
                labelBoxesCount.Text = $"Ilość kartonów: {MesDataStorage.boxingData.Select(m => m.boxId).Distinct().Count()} szt.";

                if (MesDataStorage.smtData.totalManufacturedQty > 0)
                {
                    buttonChangeSmtQty.Enabled = true;
                }
                else
                {
                    buttonChangeSmtQty.Enabled = false;
                }

                int realNg = MesDataStorage.viData.ngCount;
                labelNg.Text = $"NG: {realNg} szt.";
                if (MesDataStorage.viData.ngCount > 0)
                {
                    CurrentOrder.VisualInspection.WaitingForRepair = MesDataStorage.viData.ngScrapList.Where(r => !r.reworkOK.HasValue & r.viInspectionResult == "NG").Count();
                    string textForRepair = "";
                    if (CurrentOrder.VisualInspection.WaitingForRepair > 0)
                    {
                        textForRepair = $", oczekujących na naprawę: {CurrentOrder.VisualInspection.WaitingForRepair}szt.";
                    }
                    labelNg.Text += $" (naprawionych {MesDataStorage.viData.reworkedOkCout}szt.{textForRepair})";
                }
                labelScrap.Text = $"SCRAP: {MesDataStorage.viData.scrapCount} szt.";
                if (MesDataStorage.viData.reworkFailedCout > 0)
                {
                    labelScrap.Text += $" (w tym {MesDataStorage.viData.reworkFailedCout}szt. - nieudana naprawa)";
                }

                if (MesDataStorage.kittingData.endDate < MesDataStorage.kittingData.kittingDate) 
                {
                    labelStatus.Text = $"Status: zlecenie nie jest zakończone!";
                    buttonEndOrder.Visible = true;
                }
                else
                {
                    labelStatus.Text = $"Dnia {MesDataStorage.kittingData.endDate} przesunięte zostało:" + Environment.NewLine;
                    labelStatus.Text += $"Dobrych: {MesDataStorage.kittingData.confirmedGoodQty}" + Environment.NewLine;
                    labelStatus.Text += $"NG: {MesDataStorage.kittingData.confirmedNgQty}" + Environment.NewLine;
                    labelStatus.Text += $"SCRAP: {MesDataStorage.kittingData.confirmedScrQty}" + Environment.NewLine;

                    buttonEndOrder.Visible = true;
                }
                
                //else
                //{
                //    if (MesDataStorage.kittingData.ordertatus == MST.MES.OrderStatus.Status.Finished)
                //    {
                //        labelStatus.Text = $"Status: zlecenie zakończone: {MesDataStorage.kittingData.endDate.ToString("dd-MM-yyyy HH:mm:ss")}"
                //            + Environment.NewLine + $"Przesunięto: {MesDataStorage.kittingData.confirmedGoodQty} wyr. dobrego, {MesDataStorage.kittingData.confirmedScrQty} scrap";
                //        buttonEndOrder.Visible = false;
                //    }
                //    else
                //    {
                //        labelStatus.Text = $"Status: zlecenie częściowo przesunięte: {MesDataStorage.kittingData.endDate.ToString("dd-MM-yyyy HH:mm:ss")}" +
                //            Environment.NewLine + $"Przesunięto: {MesDataStorage.kittingData.confirmedGoodQty} wyr. dobrego, {MesDataStorage.kittingData.confirmedScrQty} scrap" +
                //            Environment.NewLine + $"Oczekujące na naprawę: {MesDataStorage.kittingData.confirmedNgQty} NG";
                //        buttonEndOrder.Visible = true;
                //    }
                //}
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                string ncId = dataGridView1.Rows[e.RowIndex].Cells["12NC"].Value.ToString() + dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                if (detailedLedInfoDict.ContainsKey(ncId))
                {
                    Details detForm = new Details(detailedLedInfoDict[ncId]);
                    detForm.Show();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 & e.ColumnIndex > -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "butCol")
                {
                    if (userList.Contains(currentUser))
                    {
                        string nc12 = dataGridView1.Rows[e.RowIndex].Cells["12NC"].Value.ToString();
                        string id = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                        if (nc12 != "" & id != "") 
                        {
                            using (EditLedQty editForm = new EditLedQty(nc12, id, MesDataStorage.kittingData.orderNo))
                            {
                                if (editForm.ShowDialog() == DialogResult.OK)
                                {
                                    ReloadCurrentOrder();
                                }
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
            textBoxOrderNo.Text = MesDataStorage.kittingData.orderNo;
            bwLoadOrder.RunWorkerAsync();
            ledsUsed = 0;
        }

        private void buttonChangeSmtQty_Click(object sender, EventArgs e)
        {
            if (userList.Contains(currentUser))
            {
                using (ChangeSMTqty changeForm = new ChangeSMTqty(MesDataStorage.smtData.smtOrders))
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
                    bwLoadOrder.RunWorkerAsync();
                    ledsUsed = 0;
                }
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[2].Value.ToString() != "") continue;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Style.BackColor = Color.Black;
                    cell.Style.ForeColor = Color.White;
                }
            }
        }

        private async void bwLoadOrder_DoWork_1(object sender, DoWorkEventArgs e)
        {
            detailedLedInfoDict = new Dictionary<string, IEnumerable<ComponentsTools.ComponentStruct>>();
            string orderNo = textBoxOrderNo.Text.Trim();
            CurrentOrder.orderNo = orderNo;
            MesDataStorage.LoadMesData(orderNo);

            if (MesDataStorage.kittingData.modelId != null)
            {
                DataTable result = new DataTable();
                if (MesDataStorage.kittingData.orderNo != null)
                {
                    GraffitiComponents.LoadComponents();
                    //DataTable sqlLedsForOrder = MST.MES.SqlOperations.SparingLedInfo.GetReelsForLot(orderNo);
                    var componentsUsedInOrder = GraffitiComponents.allComponents;
                    //var grouppedBy12Nc = componentsUsedInOrder.GroupBy(c => c.Nc12).Select(group => new { Nc12 = group.Key, Items = group.ToList() });
                    var grouppedBy12NcAndId = componentsUsedInOrder.GroupBy(c => new { c.Nc1_Rank, c.Id });

                    //bwLoadOrder.ReportProgress(50, 0);
                    //Dictionary<string, Dictionary<string, DataTable>> detailedLedInfo = LedTools.FullLedInfo(sqlLedsForOrder);
                    //bwLoadOrder.ReportProgress(80, 0);
                    result.Columns.Add("12NC");
                    result.Columns.Add("ID");
                    result.Columns.Add("Partia");
                    result.Columns.Add("Ilosc zużyta");
                    result.Columns.Add("Ilosc aktualna");

                    result.Rows.Add("Płyty PCB");


                    var detailedLedInfo = GraffitiComponents.componentsWithHistory;

                    foreach (var comp in grouppedBy12NcAndId)
                    {
                        if (!comp.Key.Nc1_Rank.StartsWith("4010560") & !comp.Key.Nc1_Rank.StartsWith("4010460") & !comp.Key.Nc1_Rank.StartsWith("4010441") & !comp.Key.Nc1_Rank.StartsWith("4010440")) continue;
                        var detailesForReel = detailedLedInfo.Where(x => x.Nc1_Rank == comp.Key.Nc1_Rank & x.Id == comp.Key.Id);

                        int quantityUsed = calculateLedUsedInOrder(detailesForReel, orderNo);

                        if (comp.Key.Nc1_Rank.StartsWith("4010560") || comp.Key.Nc1_Rank.StartsWith("4010460"))
                        {
                            ledsUsed += quantityUsed;
                            var newRow = result.NewRow();
                            newRow[0] = comp.Key.Nc1_Rank;
                            newRow[1] = comp.Key.Id;
                            newRow[2] = comp.First().deliveryDate.ToString("dd-MM-yyyy");
                            newRow[3] = quantityUsed;
                            newRow[4] = comp.OrderBy(x => x.operationDate).Last().Quantity;

                            result.Rows.InsertAt(newRow, 0);
                        }
                        else
                        {
                            mbUsed += quantityUsed;
                            result.Rows.Add(comp.Key.Nc1_Rank, comp.Key.Id, comp.First().deliveryDate.ToString("dd-MM-yyyy"), quantityUsed, comp.OrderBy(x => x.operationDate).Last().Quantity);
                        }

                        detailedLedInfoDict.Add(comp.Key.Nc1_Rank + comp.Key.Id, detailesForReel);
                    }

                    var ledDescrRow = result.NewRow();
                    ledDescrRow[0] = "Diody LED";
                    result.Rows.InsertAt(ledDescrRow, 0);
                }
                sourceTable = result;
            }
        }
    }
}