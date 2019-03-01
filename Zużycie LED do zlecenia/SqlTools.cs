using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zużycie_LED_do_zlecenia
{
    public class SqlTools
    {
        public static void UpdateCurrentMstOrderQuantity(int newQty, int recordId)
        {
            using (SqlConnection openCon = new SqlConnection(@"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;"))
            {
                openCon.Open();

                string updString = "UPDATE tb_SMT_Karta_Pracy SET IloscWykonana=@qty WHERE id = @id";
                using (SqlCommand querySave = new SqlCommand(updString))
                {
                    querySave.Connection = openCon;
                    querySave.Parameters.AddWithValue("@qty", newQty);
                    querySave.Parameters.AddWithValue("@id", recordId);
                    querySave.ExecuteNonQuery();
                }
            }
        }
    }
}
