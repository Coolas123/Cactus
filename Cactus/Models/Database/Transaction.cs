using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("transaction")]
    public class Transaction
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("method_id")]
        public int MethodId { get; set; }
        [Column("transaction_type_id")]
        public int TransactionTypeId { get; set; }
        [Column("sended")]
        public decimal Sended { get; set; }
        [Column("received")]
        public decimal Received { get; set; }
        [Column("created")]
        public DateTime Created { get; set; }
        [Column("status_id")]
        public int StatusId { get; set; }
        [Column("setting_id")]
        public int SettingId { get; set; }
    }
}
