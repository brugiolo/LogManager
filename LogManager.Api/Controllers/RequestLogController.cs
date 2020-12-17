using AutoMapper;
using LogManager.Api.Helpers;
using LogManager.Api.ViewModels;
using LogManager.Business.Interfaces;
using LogManager.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LogManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestLogController : Controller
    {
        private readonly IRequestLogService _requestLogService;
        private readonly IMapper _mapper;

        private readonly string _defaultExtension = ".TXT";

        public RequestLogController(IRequestLogService requestLogService, IMapper mapper)
        {
            _requestLogService = requestLogService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<RequestLogViewModel> Read(Guid id)
        {
            var requestLog = _requestLogService.Read(id);

            return _mapper.Map<RequestLogViewModel>(requestLog);
        }

        [HttpGet("List")]
        public ActionResult<IEnumerable<RequestLogViewModel>> List()
        {
            var requestLog = _requestLogService.List();

            return _mapper.Map<List<RequestLogViewModel>>(requestLog);
        }

        [HttpGet("Search")]
        public ActionResult<IEnumerable<RequestLogViewModel>> Search(string text)
        {
            var requestLog = _requestLogService.Search(text);

            return _mapper.Map<List<RequestLogViewModel>>(requestLog);
        }

        [HttpPost]
        public ActionResult Insert(RequestLogViewModel requestLogViewModel)
        {
            var requestLog = _mapper.Map<RequestLog>(requestLogViewModel);
            _requestLogService.Insert(requestLog);

            return Ok(requestLogViewModel);
        }

        [HttpPost("InsertFromFile")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult InsertFromFile()
        {
            var formFile = Request.Form.Files.FirstOrDefault();
            if (formFile == null)
                return BadRequest("Fail trying to get the content. Check the file and try again.");

            var fileExtension = Path.GetExtension(formFile.FileName).ToUpper();
            if (fileExtension != _defaultExtension)
                return BadRequest("Incorrect file extension. Check the file and try again.");

            var requestLogsViewModel = FromFileHelper.ReadRequestLogFromFile(formFile);
            var requestLogs = _mapper.Map<List<RequestLog>>(requestLogsViewModel);
            var inserted = _requestLogService.InsertRange(requestLogs) > 0;

            if (inserted)
                return Ok("Data imported successfully.");
            else
                return BadRequest("Failed to try to import the data. Check the file and try again.");
        }

        [HttpPut("{id}")]
        public ActionResult Update(Guid id, RequestLogViewModel requestLogViewModel)
        {
            if (!ModelState.IsValid || id != requestLogViewModel.Id)
                return BadRequest();

            var requestLog = _requestLogService.Read(id);
            requestLog.Adress = requestLogViewModel.Adress;
            requestLog.Client = requestLogViewModel.Client;
            requestLog.ContentLength = requestLogViewModel.ContentLength;
            requestLog.DateTime = requestLogViewModel.DateTime;
            requestLog.Ip = requestLogViewModel.Ip;
            requestLog.Method = requestLogViewModel.Method;
            requestLog.Status = requestLogViewModel.Status;

            _requestLogService.Update(requestLog);

            return Ok(requestLogViewModel);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var requestLog = _requestLogService.Read(id);
            var requestLogViewModel = _mapper.Map<RequestLogViewModel>(requestLog);

            if (requestLogViewModel == null)
                return NotFound();

            _requestLogService.Delete(id);

            return Ok(requestLogViewModel);
        }
    }
}
