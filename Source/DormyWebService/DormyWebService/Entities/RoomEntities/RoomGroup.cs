﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.AccountEntities;

namespace DormyWebService.Entities.RoomEntities
{
    public class RoomGroup
    {
        [Key]
        public int RoomGroupId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int RoomNumber { get; set; }
    }
}