﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("donator")]
    public class Donator
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }
        [Column("donation_option_id")]
        public int DonationOptionId { get; set; }
        public DonationOption DonationOption { get; set; }
        [Column("donation_target_type_id")]
        public int DonationTargetTypeId { get; set; }
        [Column("transaction_id")]
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}
