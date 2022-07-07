using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoClientes.Dominio.Entidades
{
    [Table("Clientes")]
    public class Cliente
    {
        public int ClienteId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="Nome é obrigatório!")]
        public string Nome { get; set; }
        [StringLength(150)]
        [Required(ErrorMessage = "Email é obrigatório!")]
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(150)]
        [Required(ErrorMessage = "Endereço é obrigatório!")]
        public string Endereco { get; set; }
        public byte[] Imagem { get; set; }
        //[StringLength(50)]
        public string ImagemTipo { get; set; }

    }
}
