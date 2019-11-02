﻿using System.Collections.Generic;
using System.Threading.Tasks;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.EditRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.GetRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.GetRoomBookingDetail;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.ResolveRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.SendRoomBooking;

namespace DormyWebService.Services.TicketServices
{
    public interface IRoomBookingService
    {
        Task<RoomBookingRequestForm> FindById(int id);

        Task<SendRoomBookingResponse> SendRequest(SendRoomBookingRequest request);

        Task<bool> EditRoomRequest(EditRoomBookingRequest request);

        Task<ResolveRoomBookingResponse> ResolveRequest(ResolveRoomBookingRequest request);

        Task<AdvancedGetRoomBookingResponse> AdvancedGetRoomRequest(string sorts, string filters, int? page, int? pageSize);

        Task<bool> DeleteRoomBooking(int id);

        Task<bool> StudentHasRoomRequestWithStatus(int studentId, List<string> statuses);

        Task<GetRoomBookingDetailResponse> GetRoomBookingDetail(int id);

        Task<bool> AutoRejectRoomBooking();
    }
}