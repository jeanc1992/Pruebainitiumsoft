using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace prueba.Models
{
    public partial class T_Clientes
    {
        [Key]
        public Guid ID_Cliente { get; set; }
        [Required]
        public double Cedula { get; set; }
        [Required]
        [StringLength(30,MinimumLength =3)]
        public string Nombre { get; set; }
        public DateTime CreatedOn { get; set; }

       public virtual List<T_ListasClientes> T_ListasClientes { get; set; }

    }
}
