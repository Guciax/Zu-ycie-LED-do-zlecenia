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
    public partial class EditLedQty : Form
    {
        private readonly string nc12;
        private readonly string id;
        private readonly string currentOrderNo;
        private bool needToSwitchOrderNo = false;
        string reelsCurrentOrderNo = "";
        string binName = "";
        int reelsCurrentQty = 0;

        public EditLedQty(string nc12, string id, string currentOrderNo)
        {
            InitializeComponent();
            this.nc12 = nc12;
            this.id = id;
            this.currentOrderNo = currentOrderNo;
        }

        private void EditLedQty_Load(object sender, EventArgs e)
        {
            var ledTable = MST.MES.SqlOperations.SparingLedInfo.GetInfoFor12NC_ID(nc12, id);
            if (ledTable.Rows.Count > 0)
            {

                reelsCurrentOrderNo = ledTable.Rows[0]["ZlecenieString"].ToString();
                binName = ledTable.Rows[0]["Tara"].ToString();
                reelsCurrentQty = int.Parse(ledTable.Rows[0]["Ilosc"].ToString());

                labelInfo.Text += Environment.NewLine + $"12NC: {nc12}     ID: {id}"
                    + Environment.NewLine + $"Aktualna ilość w bazie: {reelsCurrentQty}"+ Environment.NewLine
                    + Environment.NewLine + $"Rolka przypisana jest do zlecenia: {reelsCurrentOrderNo}" + Environment.NewLine;
                if (currentOrderNo != reelsCurrentOrderNo)
                {
                    labelInfo.Text += Environment.NewLine+$"Numer zlecenia rolki jest inny niż aktualny numer zlecenia!" + Environment.NewLine + Environment.NewLine
                                   + $"Zostaną wykonane operacje:" + Environment.NewLine
                                   + $"1. Przypisanie do zlecenia: {currentOrderNo}" + Environment.NewLine
                                   + $"2. Edycja ilości dla zlecenia {currentOrderNo}" + Environment.NewLine
                                   + $"3. Przypisanie rolki do zlecenia: {reelsCurrentOrderNo}";

                    needToSwitchOrderNo = true;
                }

                
            }
            else
            {
                MessageBox.Show("Brak tego komponentu w bazie danych");
            }
        }
        int newLedQty = 0;
        private void button1_Click(object sender, EventArgs e)
        {

            if (newLedQty >= 0) 
            {
                if (newLedQty < reelsCurrentQty)
                {
                    if (needToSwitchOrderNo)
                    {
                        MST.MES.SqlOperations.SparingLedInfo.UpdateLedZlecenieStringBinId(nc12, id, currentOrderNo, binName);

                        MST.MES.SqlOperations.SparingLedInfo.UpdateLedQuantity(nc12, id, newLedQty.ToString());

                        MST.MES.SqlOperations.SparingLedInfo.UpdateLedZlecenieStringBinId(nc12, id, reelsCurrentOrderNo, binName);
                    }
                    else
                    {
                        MST.MES.SqlOperations.SparingLedInfo.UpdateLedQuantity(nc12, id, newLedQty.ToString());
                    }
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                   MessageBox.Show("Nowa ilość musi być mniejsza od starej");
                }
            }
            else
            {
                MessageBox.Show("Nieprawidłowa ilość LED, popraw dane.");
                textBox1.Text = "";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out newLedQty))
            {
                labelResult.Text = "W wyniku tej operacji:" + Environment.NewLine + $"do zlecenia {currentOrderNo} zostanie dodane zużycie {reelsCurrentQty-newLedQty}";
            }
        }
    }
}
