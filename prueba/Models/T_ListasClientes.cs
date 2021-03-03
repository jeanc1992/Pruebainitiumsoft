using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace prueba.Models
{
    public partial class T_ListasClientes
    {
        [Key]
        public Guid ID_ListaCliente { get; set; }

        public Guid ID_Cliente { get; set; }

        public Guid ID_Lista { get; set; }

        [Required]
        public DateTime Fecha { get; set; }
      
        [Required]
        public int Duracion { get; set; }

        [ForeignKey(nameof(ID_Lista))]
        public virtual T_Listas ID_ListaNavigation { get; set; }

        [ForeignKey(nameof(ID_Cliente))]
        public virtual T_Clientes ID_ClientesNavigation { get; set; }
    }
}
