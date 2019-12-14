using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QLHV_OOAD_Core.Models
{
    public class KiemTra
    {
        [Key]
        public string IDKT { get; set; }
        public string TenMonHoc { get; set; }
        public string HinhThuc { get; set; }
        public DateTime NgayKT { get; set; }
        public float Diem { get; set; }
        public string IDHK { get; set; }
       
    }
}
