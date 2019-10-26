﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Entities.TicketEntities;

namespace DormyWebService.ViewModels.IssueTicketViewModels.SendIssueTicket
{
    public class SendIssueTicketRequest
    {
        //Param
        [Required]
        public int Type { get; set; }

        [Required]
        public int OwnerId { get; set; }

        public int? EquipmentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int? RoomId { get; set; }

        public static IssueTicket EntityFromRequest(SendIssueTicketRequest request)
        {
            return new IssueTicket()
            {
                CreatedDate = DateTime.Now,
                LastUpdated = DateTime.Now,
                Description = request.Description,
                EquipmentId = request.EquipmentId,
                ImageUrl = request.ImageUrl,
                OwnerId = request.OwnerId,
                RoomId = request.RoomId,
                //Set Status to pending
                Status = IssueStatus.Pending,
                Title = request.Title,
                Type = request.Type,
            };
        }
    }
}