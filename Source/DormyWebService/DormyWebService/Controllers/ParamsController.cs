﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.ParamEntities;
using DormyWebService.Services.ParamServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.Param;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class ParamsController : ControllerBase
    {
        private readonly IParamService _paramService;

        public ParamsController(IParamService paramService)
        {
            _paramService = paramService;
        }

        /// <summary>
        /// Get All Params in database
        /// </summary>
        /// <remarks>authorization disabled for debug purposes</remarks>
        /// <returns></returns>
        //TODO: Authorization
        [HttpGet]
        public async Task<ActionResult<List<Param>>> GetAllParams()
        {
            return await _paramService.FindAllAsync();
        }

        /// <summary>
        /// Get all Param of a Param Type
        /// </summary>
        /// <param name="paramTypeId"></param>
        /// <returns></returns>
        //TODO: Authorization
        [HttpGet("GetAllByParamType/{paramTypeId}")]
        public async Task<ActionResult<List<ParamModelView>>> GetAllByParamType(int paramTypeId)
        {
            return await _paramService.FindAllByParamType(paramTypeId);
        }

        /// <summary>
        /// Find a param by id
        /// </summary>
        /// <remarks>authorization disabled for debug purposes</remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        //TODO: Authorization
        [HttpGet("{id}")]
        public async Task<ActionResult<Param>> FindById(int id)
        {
            return await _paramService.FindById(id);
        }

        /// <summary>
        /// Get all user status stored in server
        /// </summary>
        /// <remarks>authorization disabled for debug purposes</remarks>
        /// <returns></returns>
        [HttpGet("UserStatus")]
        public ActionResult<List<string>> GetUserStatus()
        {
            return UserStatus.GetAllStatus();
        }
    }
}