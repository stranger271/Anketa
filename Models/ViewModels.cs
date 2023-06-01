using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oprosnik.Models
{
    public class ResultViewModel
    {         
        public Template Template { get; set; }
        public int TotalAnkets { get; set; }
         
       
        public float AvgP1 { get; set; }
        public string P1 { get; set; }

        public Dictionary<string,float> ProcP2 { get; set; }

       
        public float AvgP3 { get; set; }
        public string P3 { get; set; }

        public float AvgP4 { get; set; }
        public string P4 { get; set; }
        
        public float AvgP5 { get; set; }
        public string P5 { get; set; }

        public float AvgP6 { get; set; }
        public string P6 { get; set; }

        public Dictionary<string, float> ProcP7 { get; set; }
               
        public float AvgP8 { get; set; }
        public string P8 { get; set; }

        public float AvgP9 { get; set; }
        public string P9 { get; set; }

        public float AvgP10 { get; set; }
        public string P10 { get; set; }

        public float AvgP11 { get; set; }
        public string P11 { get; set; }

        public float AvgP12 { get; set; }
        public string P12 { get; set; }

        public float AvgP13 { get; set; }
        public string P13 { get; set; }

        public Dictionary<string, float> AvgP14 { get; set; }
        public Dictionary<string, string> P14 { get; set; }

        public Dictionary<string, float> AvgP15 { get; set; }
        public Dictionary<string, string> P15 { get; set; }
        public Dictionary<string, float> AvgP16 { get; set; }
        public Dictionary<string, string> P16 { get; set; }
        public Dictionary<string, float> AvgP17 { get; set; }
        public Dictionary<string, string> P17 { get; set; }
        public Dictionary<string, float> AvgP18 { get; set; }
        public Dictionary<string, string> P18 { get; set; }
        public Dictionary<string, float> AvgP19 { get; set; }
        public Dictionary<string, string> P19 { get; set; }
        public Dictionary<string, float> AvgP20 { get; set; }
        public Dictionary<string, string> P20 { get; set; }

        public Dictionary<string, float> ProcP21 { get; set; }

        public string P21_remark { get; set; }

        public string P22 { get; set; }

     
        public float AvgP23 { get; set; }
        public string P23 { get; set; }
        public string P23_remark { get; set; } 

    }


    public class AnketaViewModel
    {
        [Required]
        public int Id_template { get; set; }

        [Required]
        [Range(0,10)]
        public int P1 { get; set; }
        
        [Required]        
        public bool P2 { get; set; }

        [Required]
        [Range(0, 10)]
        public int P3 { get; set; }

        [Required]
        [Range(0, 10)]
        public int P4 { get; set; }

        [Required]
        [Range(0, 10)]
        public int P5 { get; set; }

        [Required]
        [Range(0, 10)]
        public int P6 { get; set; }

        [Required]
        public bool P7 { get; set; }

        [Required]
        [Range(0, 10)]
        public int P8 { get; set; }

        [Required]
        [Range(0, 10)]
        public int P9 { get; set; }

        [Required]
        [Range(0, 10)]
        public int P10 { get; set; }

        [Required]
        [Range(0, 10)]
        public int P11 { get; set; }

        [Required]
        [Range(0, 10)]
        public int P12 { get; set; }

        [Required]
        [Range(0, 10)]
        public int P13 { get; set; }

        public Dictionary<string, string> P14 { get; set; }
        public Dictionary<string, string> P15 { get; set; }
        public Dictionary<string, string> P16 { get; set; }
        public Dictionary<string, string> P17 { get; set; }
        public Dictionary<string, string> P18 { get; set; }
        public Dictionary<string, string> P19 { get; set; }
        public Dictionary<string, string> P20 { get; set; }

        [Required]
        public bool P21 { get; set; }

        public string P21_remark { get; set; }

        public string P22 { get; set; }

        [Required]
        [Range(1, 6)]
        public int P23 { get; set; }
        public string P23_remark { get; set; }

        
        public bool Agreement { get; set; }

    }

}