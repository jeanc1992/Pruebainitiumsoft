using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace prueba.Models
{
    public partial class T_Listas
    { 
        [Key]
        public Guid ID_lista { get; set; }

        [Required]
        [MaxLength(30)]
        public string Descripcion { get; set; }

        [Required]
        public int Duracion { get; set; }

        public virtual List<T_ListasClientes> T_ListasClientes { get; set; }
    }
}
