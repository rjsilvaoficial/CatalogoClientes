using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoClientes.Dominio.Entidades
{
    [Table("Clientes")]
    public class Cliente
    {
        public int ClienteID { get; set; }

        //[StringLength(50)]
        public string Nome { get; set; }
        //[StringLength(150)]
        public string Email { get; set; }
        //[StringLength(150)]
        public string Endereco { get; set; }
        public byte[] Imagem { get; set; }
        //[StringLength(50)]
        public string ImagemTipo { get; set; }

    }
}
