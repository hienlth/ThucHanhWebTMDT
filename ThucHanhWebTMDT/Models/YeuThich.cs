//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ThucHanhWebTMDT.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class YeuThich
    {
        public int MaYT { get; set; }
        public Nullable<int> MaHH { get; set; }
        public string MaKH { get; set; }
        public Nullable<System.DateTime> NgayChon { get; set; }
        public string MoTa { get; set; }
    
        public virtual HangHoa HangHoa { get; set; }
        public virtual KhachHang KhachHang { get; set; }
    }
}
