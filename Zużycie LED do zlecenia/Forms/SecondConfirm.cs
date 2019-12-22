using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zużycie_LED_do_zlecenia.Forms
{
    public partial class SecondConfirm : Form
    {
        private readonly int ngToConfirm;
        public int goodQty;
        public int scrQty;

        public SecondConfirm(int ngToConfirm)
        {
            InitializeComponent();
            this.ngToConfirm = ngToConfirm;
        }

        private void SecondConfirm_Load(object sender, EventArgs e)
        {
            lInfo.Text = $"Do rozliczenia pozostało {ngToConfirm} szt. NG.";
            button1.Text = $"Pozostało {ngToConfirm}";
        }

        private void numGoodQty_ValueChanged(object sender, EventArgs e)
        {
            var userQty = numGoodQty.Value + numScrQty.Value;
            if (userQty == ngToConfirm)
            {
                button1.Text = "OK";
            }
            else
            {
                if (ngToConfirm - userQty >= 0)
                {
                    button1.Text = $"Pozostało {ngToConfirm - userQty} szt.";
                }
                else
                {
                    button1.Text = $"Za dużo.";
                }
            }
        }

        private void numScrQty_ValueChanged(object sender, EventArgs e)
        {
            var userQty = numGoodQty.Value + numScrQty.Value;
            if (userQty == ngToConfirm)
            {
                button1.Text = "OK";
            }
            else
            {
                if (ngToConfirm - userQty >= 0)
                {
                    button1.Text = $"Pozostało {ngToConfirm - userQty} szt.";
                }
                else
                {
                    button1.Text = $"Za dużo.";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(button1.Text == "OK")
            {
                Graffiti.MST.OrdersOperations.ConfirmRemainingNg(int.Parse(CurrentOrder.orderNo), (double)numGoodQty.Value, (double)numScrQty.Value);
                goodQty = (int)numGoodQty.Value;
                scrQty = (int)numScrQty.Value;
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
