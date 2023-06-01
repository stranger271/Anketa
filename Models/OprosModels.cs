using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Oprosnik.Models
{
    public class Kurs
    {
        public int id { get; set; }

    
        public string name { get; set; }
    }
    
    public class Teacher
    { 
        [Key]
        [Required]
        public int Id_teacher { get; set; }

        [Required]
        [MaxLength(250)]
        public string Fio { get; set; }

        [Required]
        [UIHint("Boolean")]
        public bool Status { get; set; }
    }
    

    public class Template
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id_template { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(250)]
        public string Kurs_name { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Sdate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Edate { get; set; }

        [Required]
        [UIHint("Boolean")]
        public bool Status { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date_created { get; set; }
        
        [DataType(DataType.Text)]
        [MaxLength(250)]
        public string Teachers_in_template { get; set; }        

        public ICollection<Anketa> Anketas { get; set; }
        public Template() { Anketas = new List<Anketa>();  }//констурктор Шаблона создаем список с анкетами

    }

    public class Anketa
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id_anketa { get; set; }

        public int Id_template { get; set; }      // внешний ключ
        public Template Template { get; set; }    // навигационное свойство

        public int? P1 { get; set; }

        [UIHint("Boolean")]
        public bool P2 { get; set; }

        public int? P3 { get; set; }
        public int? P4 { get; set; }
        public int? P5 { get; set; }
        public int? P6 { get; set; }

        [UIHint("Boolean")]
        public bool P7 { get; set; }

        public int? P8 { get; set; }
        public int? P9 { get; set; }
        public int? P10 { get; set; }
        public int? P11 { get; set; }
        public int? P12 { get; set; }
        public int? P13 { get; set; }

   
        public string P14 { get; set; }
        public string P15 { get; set; }
        public string P16 { get; set; }
        public string P17 { get; set; }
        public string P18 { get; set; }
        public string P19 { get; set; }
        public string P20 { get; set; }

        [UIHint("Boolean")]
        public bool P21 { get; set; }
        public string P21_remark { get; set; }
        public string P22 { get; set; }

        public int? P23 { get; set; }
        public string P23_remark { get; set; }

        [UIHint("Boolean")]
        public bool Agreement { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date_created { get; set; }

    }




}