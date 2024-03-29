﻿using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.NewsEntities;

namespace DormyWebService.ViewModels.NewsViewModels.CreateNews
{
    public class CreateNewsRequest
    {
        [Required]
        public int AuthorId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string AttachedFileUrl { get; set; }

        public static News NewNewsFromRequest(CreateNewsRequest request, Admin author)
        {
            return new News()
            {
                AttachedFileUrl = request.AttachedFileUrl,
                Author = author,
                Content = request.Content,
                CreatedDate = DateTime.Now.AddHours(7),
                LastUpdate = DateTime.Now.AddHours(7),
                Status = NewsStatus.Active,
                Title = request.Title
            };
        }
    }
}