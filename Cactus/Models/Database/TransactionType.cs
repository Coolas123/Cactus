using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("transaction_type_id")]
    public class TransactionType
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
    }
}
