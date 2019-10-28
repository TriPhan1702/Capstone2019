﻿using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.AccountEntities;

namespace DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.SendRenewContractRequest
{
    public class SendRenewContractRequestRequest
    {
        [Key]
        public int ContractRenewalFormId { get; set; }

        [Required]
        public Student Student { get; set; }

        public Staff Staff { get; set; }

        //Param
        [Required]
        public int Status { get; set; }

        public int Month { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}