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

namespace LogManager.Api.Controllers
{
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

        // GET: RequestLogController/Details/5
        public ActionResult<RequestLogViewModel> Details(Guid id)
        {
            var requestLog = _requestLogService.Read(id);

            return _mapper.Map<RequestLogViewModel>(requestLog);
        }

        // POST: RequestLogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Insert(RequestLogViewModel requestLogViewModel)
        {
            var requestLog = _mapper.Map<RequestLog>(requestLogViewModel);
            _requestLogService.Insert(requestLog);

            return Ok(requestLogViewModel);
        }

        [HttpPost]
        public IActionResult InsertFromFile(IFormFile iFormFile)
        {
            var fileExtension = Path.GetExtension(iFormFile.FileName).ToUpper();
            if (fileExtension != _defaultExtension)
                return BadRequest("Arquivo com extensão incorreta. Verifique o arquivo e tente novamente.");

            var requestLogsViewModel = FromFileHelper.ReadRequestLogFromFile(iFormFile);
            var requestLogs = _mapper.Map<List<RequestLog>>(requestLogsViewModel);
            var inserted = _requestLogService.InsertRange(requestLogs) > 0;

            if (inserted)
                return Ok("Dados importados com sucesso.");
            else
                return BadRequest("Falha ao tentar importar os dados. Verifique o arquivo e tente novamente.");
        }

        // POST: RequestLogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, RequestLogViewModel requestLogViewModel)
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

        // GET: RequestLogController/Delete/5
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
