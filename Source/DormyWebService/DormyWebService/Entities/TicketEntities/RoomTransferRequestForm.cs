﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.RoomEntities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sieve.Attributes;

namespace DormyWebService.Entities.TicketEntities
{
    public class RoomTransferRequestForm
    {
        [Key]
        [Sieve(CanFilter = true, CanSort = true)]
        public int RoomTransferRequestFormId { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime LastUpdated { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime RejectDate { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime TransferDate { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(500)]
        public string Reason { get; set; }

        [Required]
        [ForeignKey("Student")]
        [Sieve(CanFilter = true, CanSort = true)]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [Required]
        public int TargetRoomType { get; set; }

        [ForeignKey("Room")]
        public int? RoomId { get; set; }
        public Room Room { get; set; }

        //Param
        [Required]
        public string Status { get; set; }
    }
}