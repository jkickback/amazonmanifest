using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonManifest.DataTypes
{
    public class SpreadSheetRow
    {
        public int RowId { get; set; }
        public string Asin { get; set; }
        public string UPC { get; set; }
        public string EAN { get; set; }
        public string LPN { get; set; }
        public string FCSKU { get; set; }
        public string ANSKU { get; set; }
        public string ItemDesc { get; set; }


        public SpreadSheetRow(DataRow row, int index)
        {
            this.RowId = index;
            this.Asin = row["Asin"].ToString();
            this.UPC = row["UPC"].ToString();
            this.EAN = row["EAN"].ToString();
            this.LPN = row["LPN"].ToString();
            this.FCSKU = row["FCSKU"].ToString();
            this.ItemDesc = row["itemDesc"].ToString();
            //this.ANSKU = row["ANSKU"].ToString();
        }

    }


}
