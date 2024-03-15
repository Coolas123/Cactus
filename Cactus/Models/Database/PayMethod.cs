using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("pay_method")]
    public class PayMethod
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("pay_method_setting_id")]
        public int PayMethodSettingId { get; set; }
    }
}
