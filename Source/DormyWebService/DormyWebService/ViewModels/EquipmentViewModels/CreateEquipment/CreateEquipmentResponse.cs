﻿using System;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Utilities;

namespace DormyWebService.ViewModels.EquipmentViewModels.CreateEquipment
{
    public class CreateEquipmentResponse
    {
        public int EquipmentId { get; set; }
//        public string Code { get; set; }
//        public int? RoomId { get; set; }
//        public string ImageUrl { get; set; }
//        public string Status { get; set; }
//        public decimal Price { get; set; }
//        public string CreatedDate { get; set; }
//        public string LastUpdated { get; set; }

        public static CreateEquipmentResponse CreateFromEquipment(Equipment equipment)
        {
            var result = new CreateEquipmentResponse()
            {
//                CreatedDate = equipment.CreatedDate.ToString(GlobalParams.DateTimeResponseFormat),
//                LastUpdated = equipment.LastUpdated.ToString(GlobalParams.DateTimeResponseFormat),
                EquipmentId = equipment.EquipmentId,
//                ImageUrl = equipment.ImageUrl,
//                Code = equipment.Code,
//                Price = equipment.Price,
//                Status = equipment.Status,
//                RoomId = equipment.RoomId
            };

            return result;
        }
    }
}