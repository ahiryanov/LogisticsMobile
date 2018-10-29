using System.Collections.Generic;

namespace LogisticsMobile
{
    public class TransferInfo
    {
        public List<Equipment> Equipments { get; set; }
        public int UserID { get; set; } 
        public string NewPosition { get; set; }
    }
}
