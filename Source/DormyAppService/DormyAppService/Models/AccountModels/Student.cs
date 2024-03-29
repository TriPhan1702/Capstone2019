﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyAppService.Models.RoomModels;
using DormyAppService.Models.TicketModels;

namespace DormyAppService.Models.AccountModels
{
    [Table("Students")]
    public class Student : ApplicationUser
    {
        //Năm nhập học
        [Required]
        public int StartedSchoolYear { get; set; }

        //CMND
        [Required]
        public string IdentityNumber { get; set; }

        //Hình CMND
        public string IdentityCardImageUrl { get; set; }

        //MSSV
        [Required]
        public string StudentCardNumber { get; set; }

        //Hình thẻ SV
        public string StudentCardImageUrl { get; set; }

        //Khóa học
        public int Term { get; set; }

        [Required]
        //Viện ưu tiên
        public StudentPriorityType PriorityType { get; set; }

        //Hình đối tượng ưu tiên
        public string PriorityImageUrl { get; set; }

        //Phòng đang ở
        public Room Room { get; set; }

        public bool Gender { get; set; }

        //Địa chỉ
        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        public bool IsRoomLeader { get; set; }

        //Điểm đánh giá
        public int EvaluationScore { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal AccountBalance { get; set; }

        public Contract.Contract CurrentContract { get; set; }
    }
}