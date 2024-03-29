﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.ViewModels.EquipmentViewModels.UpdateEquipment
{
    public class UpdateEquipmentRequest
    {
        [Required]
        public int EquipmentId { get; set; }

        [Required]
        public int EquipmentTypeId { get; set; }

        [Required]
        public string Code { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Room Id must be a number")]
        public string RoomId { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public decimal Price { get; set; }

        public static Equipment UpdateToEquipment(Equipment equipment, UpdateEquipmentRequest request, Room room)
        {
            equipment.Code = request.Code;
            equipment.ImageUrl = request.ImageUrl;
            equipment.LastUpdated = DateTime.Now;
            equipment.Price = request.Price;
            equipment.Room = room;
            equipment.Status = request.Status;
            equipment.EquipmentTypeId = request.EquipmentTypeId;
            return equipment;
        }
    }
}