﻿using System;
using System.ComponentModel.DataAnnotations;
using DormyAppService.Models.AccountModels;
using DormyAppService.Models.Enums;

namespace DormyAppService.Models.Contract
{
    public class Contract
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Student Student { get; set; }

        [Required]
        public ContractStatusEnum Status { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastUpdate { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

    }
}