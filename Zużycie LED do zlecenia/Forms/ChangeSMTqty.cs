using MST.MES;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Zużycie_LED_do_zlecenia.Forms
{
    public partial class ChangeSMTqty : Form
    {
        private readonly List<OrderStructureByOrderNo.SmtRecords> smtRecords;

        public ChangeSMTqty(List<OrderStructureByOrderNo.SmtRecords> smtRecords)
        {
            InitializeComponent();
            this.smtRecords = smtRecords;
        }

        private void ChangeSMTqty_Load(object sender, EventArgs e)
        {
            foreach (var smtRecord in smtRecords)
            {
                dataGridView1.Rows.Add(smtRecord.smtStartDate,
                    smtRecord.smtEndDate,
                    smtRecord.smtLine,
                    smtRecord.manufacturedQty,
                    smtRecord.ng,
                    smtRecord.operatorSmt,
                    smtRecord.orderInfo.modelId.Insert(4, " ").Insert(8, " "),
                    smtRecord.recordId
                );
            }

            labelOrderNo.Text += smtRecords.First().orderInfo.orderNo;
            labelBaseQty.Text += smtRecords.First().orderInfo.totalManufacturedQty;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            bool anythingChanged = false;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string qtyString = row.Cells["ColQty"].Value.ToString();
                int qtyValue = 0;
                if (int.TryParse(qtyString, out qtyValue))
                {
                    if (qtyValue != GetQtyOriginalValue(row))
                    {
                        row.Cells["ColQty"].Style.BackColor = Color.Orange;
                        anythingChanged = true;
                    }
                    else
                    {
                        row.Cells["ColQty"].Style.BackColor = Color.White;
                    }
                }
                else
                {
                    ReturnOriginalValue(row);
                }

                string ngString = row.Cells["ColNg"].Value.ToString();
                int ngValue = 0;
                if (int.TryParse(ngString, out ngValue))
                {
                    if (ngValue != GetNgOriginalValue(row))
                    {
                        row.Cells["ColNg"].Style.BackColor = Color.Orange;
                        anythingChanged = true;
                    }
                    else
                    {
                        row.Cells["ColNg"].Style.BackColor = Color.White;
                    }
                }
                else
                {
                    ReturnOriginalValue(row);
                }
            }

            if (anythingChanged)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }

            UpdateNewTotalQty();
            dataGridView1.CurrentCell = null;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void UpdateNewTotalQty()
        {
            int q = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                q += int.Parse(row.Cells["ColQty"].Value.ToString());
            }

            labelNewQty.Text = $"Nowa całkowita ilość: {q}";
        }

        private void ReturnOriginalValue(DataGridViewRow row)
        {
            int dbId = int.Parse(row.Cells["ColDbId"].Value.ToString());
            foreach (var smtRecord in smtRecords)
            {
                if (smtRecord.recordId != dbId)
                {
                    continue;
                }

                row.Cells["ColQty"].Value = smtRecord.manufacturedQty;
                row.Cells["ColNg"].Value = smtRecord.ng;
                break;
            }
        }

        private int GetQtyOriginalValue(DataGridViewRow row)
        {
            int dbId = int.Parse(row.Cells["ColDbId"].Value.ToString());
            foreach (var smtRecord in smtRecords)
            {
                if (smtRecord.recordId != dbId)
                {
                    continue;
                }

                return smtRecord.manufacturedQty;
            }

            return 0;
        }

        private int GetNgOriginalValue(DataGridViewRow row)
        {
            int dbId = int.Parse(row.Cells["ColNg"].Value.ToString());
            foreach (var smtRecord in smtRecords)
            {
                if (smtRecord.recordId != dbId)
                {
                    continue;
                }

                return (int)smtRecord.ng;
            }

            return 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["ColQty"].Style.BackColor == Color.Orange)
                {
                    int dbId = int.Parse(row.Cells["ColDbId"].Value.ToString());
                    int newQty = int.Parse(row.Cells["ColQty"].Value.ToString());
                    SqlTools.UpdateCurrentMstOrderQuantity(newQty, dbId);
                }
                if (row.Cells["ColNg"].Style.BackColor == Color.Orange)
                {
                    int dbId = int.Parse(row.Cells["ColDbId"].Value.ToString());
                    string newQty = row.Cells["ColNg"].Value.ToString();
                    SqlTools.UpdateCurrentMstOrderNg(newQty, dbId);
                }
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}