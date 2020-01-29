namespace TaskV2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Identidad")]
    public partial class Identidad
    {
        [Key]
        public int IdIdentidad { get; set; }

        public int? IdUsuarios { get; set; }

        [StringLength(50)]
        public string NombreIndentidad { get; set; }

        [StringLength(50)]
        public string CorreoIdentidad { get; set; }
    }
}
