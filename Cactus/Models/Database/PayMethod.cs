using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("pay_method")]
    public class PayMethod
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name", TypeName = "varchar(64)")]
        public string Name { get; set; }
        [Column("pay_method_setting_id")]
        public int PayMethodSettingId { get; set; }
        public PayMethodSetting PayMethodSetting { get; set; }
    }
}
