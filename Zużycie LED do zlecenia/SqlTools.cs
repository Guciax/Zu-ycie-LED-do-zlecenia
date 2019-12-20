using System.Data.SqlClient;

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

        internal static void UpdateCurrentMstOrderNg(string ngCount, int recordId)
        {
            using (SqlConnection openCon = new SqlConnection(@"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;"))
            {
                openCon.Open();

                string updString = "UPDATE tb_SMT_Karta_Pracy SET NGIlosc=@NGIlosc WHERE id = @id";
                using (SqlCommand querySave = new SqlCommand(updString))
                {
                    querySave.Connection = openCon;
                    querySave.Parameters.AddWithValue("@NGIlosc", ngCount);
                    querySave.Parameters.AddWithValue("@id", recordId);
                    querySave.ExecuteNonQuery();
                }
            }
        }
    }
}