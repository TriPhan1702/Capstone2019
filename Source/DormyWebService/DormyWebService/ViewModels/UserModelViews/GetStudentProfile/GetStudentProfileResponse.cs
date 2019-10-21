﻿using System;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.ViewModels.UserModelViews.GetStudentProfile
{
    public class GetStudentProfileResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StartedSchoolYear { get; set; }
        public int Term { get; set; }
        public string StudentCardNumber { get; set; }
        public bool Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime BirthDay { get; set; }
        public string IdentityNumber { get; set; }
        public string IdentityCardImageIrl { get; set; }
        public GetStudentProfileResponsePriorityType PriorityType { get; set; }
        public GetStudentProfileResponseRoom Room { get; set; }
        public bool IsRoomLeader { get; set; }
        public int EvaluationScore { get; set; }

        public static GetStudentProfileResponse MapFromStudent(Student student, Entities.ParamEntities.Param param)
        {
            GetStudentProfileResponsePriorityType priorityType = null;

            if (param != null)
            {
                priorityType = new GetStudentProfileResponsePriorityType()
                {
                    Name = param.Name,
                    ParamTypeId = param.ParamTypeId,
                    ParamId = param.ParamId
                };
            }

            return new GetStudentProfileResponse()
            {
                Id = student.StudentId,
                //if null then room is null
                Room = (student.Room != null) ? new GetStudentProfileResponseRoom()
                {
                    Id = student.Room.RoomId,
                    Name = student.Room.Name,

                }: null,
                PriorityType = priorityType,
                Gender = student.Gender,
                Address = student.Address,
                BirthDay = student.BirthDay,
                EvaluationScore = student.EvaluationScore,
                IdentityCardImageIrl = student.IdentityCardImageUrl,
                IdentityNumber = student.IdentityNumber,
                IsRoomLeader = student.IsRoomLeader,
                PhoneNumber = student.PhoneNumber,
                Name = student.Name,
                StartedSchoolYear = student.StartedSchoolYear,
                StudentCardNumber = student.StudentCardNumber,
                Term = student.Term,
            };
        }
    }
}