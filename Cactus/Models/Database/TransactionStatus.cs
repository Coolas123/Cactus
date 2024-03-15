using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("transaction_status")]
    public class TransactionStatus
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
    }
}
