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
    
    public partial class Loai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Loai()
        {
            this.HangHoas = new HashSet<HangHoa>();
        }
    
        public int MaLoai { get; set; }
        public string TenLoai { get; set; }
        public string TenLoaiAlias { get; set; }
        public string MoTa { get; set; }
        public string Hinh { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HangHoa> HangHoas { get; set; }
    }
}
