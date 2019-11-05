using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.ContractEntities;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.Entities.ParamEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Repositories;
using DormyWebService.Services.EquipmentServices;
using DormyWebService.Services.ParamServices;
using DormyWebService.Services.UserServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.RoomViewModels;
using DormyWebService.ViewModels.RoomViewModels.ArrangeRoom;
using DormyWebService.ViewModels.RoomViewModels.CreateRoom;
using DormyWebService.ViewModels.RoomViewModels.GetRoomTypeInfo;
using DormyWebService.ViewModels.RoomViewModels.UpdateRoom;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.ImportRoomBooking;
using Microsoft.EntityFrameworkCore.Internal;
using Sieve.Models;
using Sieve.Services;
using Enumerable = System.Linq.Enumerable;

namespace DormyWebService.Services.RoomServices
{
    public class RoomService : IRoomService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private IMapper _mapper;
        private readonly IParamService _param;
        private readonly ISieveProcessor _sieveProcessor;

        public RoomService(IRepositoryWrapper repoWrapper, IMapper mapper, IParamService param,
            ISieveProcessor sieveProcessor)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
            _param = param;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<Room> FindById(int id)
        {
            var result = await _repoWrapper.Room.FindByIdAsync(id);

            //Check if there's this user in database
            if (result == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: Room is not found");
            }

            return result;
        }

        public async Task<List<Room>> ParseRoomAsync(List<CreateRoomRequest> createRoomRequests)
        {
            List<Room> rooms = new List<Room>();
            foreach (CreateRoomRequest createRoomRequest in createRoomRequests)
            {
                Room room = CreateRoomRequest.NewRoomFromRequest(createRoomRequest);
                Param param = await _repoWrapper.Param.FindByIdAsync(createRoomRequest.RoomType);
                if (param.Name == "Standard Room")
                {
                    room.Capacity = 8;
                }
                else
                {
                    room.Capacity = 4;
                }
                room.Price = param.DecimalValue.Value;
                List<RoomTypesAndEquipmentTypes> roomTypesAndEquipmentTypes = (await _repoWrapper.RoomTypesAndEquipmentTypes.FindAllAsyncWithCondition(x => x.RoomTypeId == param.ParamId)).ToList();
                foreach (RoomTypesAndEquipmentTypes roomTypesAndEquipmentType in roomTypesAndEquipmentTypes)
                {
                    List<Equipment> equipments = (await _repoWrapper.Equipment.FindAllAsyncWithCondition(x => x.EquipmentTypeId == roomTypesAndEquipmentType.EquipmentTypeId && x.RoomId == null)).Take(roomTypesAndEquipmentType.Amount).ToList();
                    room.Equipments = equipments;
                    RoomsAndEquipmentTypes roomsAndEquipmentTypes = new RoomsAndEquipmentTypes
                    {
                        Room = room,
                        EquipmentTypeId = roomTypesAndEquipmentType.EquipmentTypeId,
                        Quantity = roomTypesAndEquipmentType.Amount,
                        RealQuantity = equipments.Count
                    };
                    await _repoWrapper.RoomsAndEquipmentTypes.CreateAsync(roomsAndEquipmentTypes);
                }
                rooms.Add(room);
            }
            return rooms;
        }

        public async Task<BuildingResponse> CreateBuilding(CreateBuildingRequest requestModel)
        {
            try
            {
                Building building = CreateBuildingRequest.NewBuildingAndRoomsFromRequest(requestModel);
                building.Rooms = await ParseRoomAsync(requestModel.CreateRoomRequests);
                var result = await _repoWrapper.Building.CreateAsync(building);
                await _repoWrapper.Save();
                return new BuildingResponse()
                {
                    BuildingId = result.BuildingId
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<List<Room>> AdvancedGetRooms(string sorts, string filters, int? page, int? pageSize)
        {
            var sieveModel = new SieveModel()
            {
                PageSize = pageSize,
                Sorts = sorts,
                Page = page,
                Filters = filters
            };

            var rooms = await _repoWrapper.Room.FindAllAsync();

            if (rooms == null || rooms.Any() == false)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: No room is found");
            }

            var result = _sieveProcessor.Apply(sieveModel, rooms.AsQueryable()).ToList();
            return result;
        }

        public async Task<UpdateRoomResponse> UpdateRoom(UpdateRoomRequest requestModel)
        {
            //Find and check if room exists in database
            var room = await FindById(requestModel.RoomId);

            //Check if room type exists
            if (!await _param.IsOfParamType(requestModel.RoomType, GlobalParams.ParamTypeRoomType))
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: RoomType is not valid");
            }

            //Update room with new information
            room = UpdateRoomRequest.UpdateToRoom(room, requestModel);


            //Update to database
            room = await _repoWrapper.Room.UpdateAsync(room, room.RoomId);

            var equipments =
                (List<Equipment>)await _repoWrapper.Equipment.FindAllAsyncWithCondition(e => e.RoomId == room.RoomId);
            List<int> equipmentIds = null;

            if (EnumerableExtensions.Any(equipments))
            {
                equipmentIds = equipments.Select(e => e.EquipmentId).ToList();
            }

            return UpdateRoomResponse.ResponseFromRoom(room, equipmentIds);
        }

        public async Task<ArrangeRoomResponse> ImportRoomBookingRequests(List<ImportRoomBookingRequest> requests)
        {
            //Get all students from email in request
            var students =
                (List<Student>)await _repoWrapper.Student.FindAllAsyncWithCondition(s =>
                   requests.Exists(r => r.Email == s.Email));
            if (students == null || !students.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: No student is found");
            }

            //Get max day to create new room booking
            var maxDayForApproveRoomBooking = await _param.FindById(GlobalParams.MaxDayForApproveRoomBooking);
            if (maxDayForApproveRoomBooking?.Value == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: maxDayForApproveRoomBooking not found");
            }

            // Create List of objects that contains a Student, created RoomBooking from that student
            var importStudentAndRequests = new List<ImportStudentAndRequest>();
            foreach (var request in requests)
            {
                var student = students.Find(s => s.Email == request.Email);
                var roomBooking = await _repoWrapper.RoomBooking.CreateAsync(
                    ImportRoomBookingRequest.EntityFromRequest(request, student.StudentId,
                        maxDayForApproveRoomBooking.Value.Value));
                importStudentAndRequests.Add(new ImportStudentAndRequest() { Student = student, RoomBooking = roomBooking });
            }

            //Get list of available room
            var availableRooms = (List<Room>)await _repoWrapper.Room.FindAllAsyncWithCondition(r => r.CurrentNumberOfStudent < r.Capacity);

            //Check if there are rooms available
            if (availableRooms == null || !availableRooms.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: No Room is Available");
            }

            //Sort room list sorted by available spot
            availableRooms.Sort((x, y) => (x.Capacity - x.CurrentNumberOfStudent).CompareTo(y.Capacity - y.CurrentNumberOfStudent));

            var arrangedStudents = new List<ImportStudentAndRequest>();
            var unArrangedStudents = new List<Student>();
            var arrangedRooms = new List<Room>();

            //Go through every object
            for (var i = 0; i < importStudentAndRequests.Count; i++)
            {
                //Get the student from object
                var student = importStudentAndRequests[i].Student;
                var roomBooking = importStudentAndRequests[i].RoomBooking;

                //if already ran out of available room, add remaining students to unArrangedStudents
                if (availableRooms.Count <= 0)
                {
                    unArrangedStudents.Add(student);
                }
                //If there's still room, try to arrange room for current student
                else
                {
                    //Go through every available rooms, which has been previously sorted
                    for (var j = 0; j < availableRooms.Count; j++)
                    {
                        var currentRoom = availableRooms[j];
                        //If there's a room that satisfies student's requirements, add student to that room
                        if (roomBooking.TargetRoomType == currentRoom.RoomType && student.Gender == currentRoom.Gender)
                        {
                            //add student to room
                            student.RoomId = currentRoom.RoomId;
                            if (!arrangedRooms.Contains(currentRoom))
                            {
                                arrangedRooms.Add(currentRoom);
                            }
                            //Increase current student number of room
                            currentRoom.CurrentNumberOfStudent++;
                            //add student to arrangedStudentList to save to database 
                            arrangedStudents.Add(new ImportStudentAndRequest() { Student = student, RoomBooking = roomBooking });
                            //add room Id into request;
                            roomBooking.RoomId = currentRoom.RoomId;
                            //Pend request update
                            await _repoWrapper.RoomBooking.UpdateAsyncWithoutSave(roomBooking,
                                roomBooking.RoomBookingRequestFormId);
                            //Add arranged student to list of pending database update
                            await _repoWrapper.Student.UpdateAsyncWithoutSave(student, student.StudentId);
                            //If room is full after adding the student
                            if (currentRoom.Capacity == currentRoom.CurrentNumberOfStudent)
                            {
                                //Remove full current room from available room list so we won't have to check this room again
                                availableRooms.RemoveAt(j);
                                //Add room to pending changes to database
                                await _repoWrapper.Room.UpdateAsyncWithoutSave(currentRoom, currentRoom.RoomId);
                            }
                            //Break loop and move on to next request and go through list of available room again
                            break;
                        }

                        //if at the end of available room list and hasn't found a suitable room yet, add current student to unArrangedStudents list
                        if (j == availableRooms.Count - 1)
                        {
                            //Add current student to unArrangedStudentList
                            unArrangedStudents.Add(student);
                        }
                    }
                }
            }

            //Save all students, rooms and requests at once, roll back everything if something went wrong
            await _repoWrapper.Save();

            //If save is successful, preparing response message
            return ArrangeRoomResponse.ArrangeRoomListFromEntities(arrangedStudents, unArrangedStudents, arrangedRooms);
        }

        public async Task<List<GetRoomTypeInfoResponse>> GetRoomTypeInfo()
        {
            //Get All active rooms with vacancy in database
            var rooms = (List<Room>)await _repoWrapper.Room.FindAllAsyncWithCondition(r => r.RoomStatus == RoomStatus.Active && r.CurrentNumberOfStudent < r.Capacity);

            if (rooms == null || !rooms.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: Couldn't find any room");
            }

            //Get All Room Types
            var roomTypes =
                (List<Param>)await _repoWrapper.Param.FindAllAsyncWithCondition(p =>
                   p.ParamTypeId == GlobalParams.ParamTypeRoomType);

            if (roomTypes == null || !roomTypes.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: Couldn't find any room type");
            }

            var result = new List<GetRoomTypeInfoResponse>();

            foreach (var room in rooms)
            {
                //If result list doesn't have this room type
                if (!result.Exists(t => t.RoomTypeId == room.RoomType && t.Gender == room.Gender))
                {
                    var roomType = roomTypes.Find(t => t.ParamId == room.RoomType);
                    result.Add(new GetRoomTypeInfoResponse()
                    {
                        RoomTypeId = room.RoomType,
                        RoomTypeName = roomType.Name,
                        RoomTypePrice = roomType.DecimalValue,
                        RoomTypeVacancy = 0 + (room.Capacity - room.CurrentNumberOfStudent),
                        Gender = room.Gender
                    });
                }
                else
                {
                    var resultRoomType = result.Find(t => t.RoomTypeId == room.RoomType);
                    resultRoomType.RoomTypeVacancy += (room.Capacity - room.CurrentNumberOfStudent);
                }
            }

            return result;
        }

        public async Task<List<Building>> GetAllBuilding()
        {
            return (await _repoWrapper.Building.FindAllAsync()).ToList();
        }

        public async Task<List<RoomsAndEquipmentTypes>> GetAllMissingEquipmentRoom()
        {
            return (await _repoWrapper.RoomsAndEquipmentTypes.FindAllAsync()).ToList();   
        }

        //
        //        private List<Room> SplitRoomByGender(List<Room> src, bool gender)
        //        {
        //            return src.Where(room => room.Gender == gender).ToList();
        //        }

        //        private List<Student> ArrangRoom(List<Room> rooms, List<Student> students)
        //        {
        //            //Go through each room
        //            foreach (var room in rooms)
        //            {
        //                if
        //            }
        //        }
    }
}