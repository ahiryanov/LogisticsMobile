//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LogisticsMobile
{
    using System;
    using System.Collections.Generic;
    
    public partial class TransferEquipment
    {
        public int idTransfer { get; set; }
        public int idEquipment { get; set; }
        public Nullable<int> idManager { get; set; }
        public Nullable<System.DateTime> TransferDateTime { get; set; }
        public string TransferFrom { get; set; }
        public string TransferTo { get; set; }
    }
}
