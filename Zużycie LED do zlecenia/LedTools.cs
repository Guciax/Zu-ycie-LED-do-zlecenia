using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zużycie_LED_do_zlecenia
{
    public class LedTools
    {
        public static Dictionary<string, Dictionary<string, DataTable>> FullLedInfo(DataTable lotSqlTable)
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
            DataTable templateTable = new DataTable();
            templateTable.Columns.Add("nc12");
            templateTable.Columns.Add("id");
            templateTable.Columns.Add("Partia");
            templateTable.Columns.Add("qty");
            templateTable.Columns.Add("zuzycie");
            templateTable.Columns.Add("zlecenieString");
            templateTable.Columns.Add("Data_Czas");

            foreach (DataRow row in detailedLedTable.Rows)
            {
                string zlecenieString = row["zlecenieString"].ToString();
                if (zlecenieString == "") continue;

                string nc12 = row["NC12"].ToString();
                string id = row["ID"].ToString();
                string partia = row["Partia"].ToString();
                int zuzycie = 0;

                string qty = row["Ilosc"].ToString();

                if (!result.ContainsKey(nc12))
                {
                    result.Add(nc12, new Dictionary<string, DataTable>());
                }

                if (!result[nc12].ContainsKey(id))
                {
                    result[nc12].Add(id, templateTable.Clone());
                }

                if (result[nc12][id].Rows.Count > 0)
                {
                    if (result[nc12][id].Rows[result[nc12][id].Rows.Count - 1]["zlecenieString"].ToString() == zlecenieString)
                    {
                        int lastQty = int.Parse(result[nc12][id].Rows[result[nc12][id].Rows.Count - 1]["qty"].ToString());
                        zuzycie = lastQty - int.Parse(qty);
                    }
                }

                result[nc12][id].Rows.Add(nc12, id, partia, qty, zuzycie, zlecenieString, row["Data_Czas"].ToString());
            }
            return result;
        }
    }
}
