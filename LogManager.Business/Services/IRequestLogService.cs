using LogManager.Business.Interfaces;
using LogManager.Business.Models;
using System;
using System.Collections.Generic;

namespace LogManager.Business.Services
{
    public class RequestLogService : IRequestLogService
    {
        private readonly IRequestLogRepository _requestLogRepository;

        public RequestLogService(IRequestLogRepository requestLogRepository)
        {
            _requestLogRepository = requestLogRepository;
        }

        public void InsertAndDoNotUpdate(RequestLog requestLog)
        {
            _requestLogRepository.Insert(requestLog);
        }

        public void Insert(RequestLog requestLog)
        {
            _requestLogRepository.Insert(requestLog);
            _requestLogRepository.SaveChanges();
        }

        public RequestLog Read(Guid id)
        {
            return _requestLogRepository.Read(id);
        }

        public void Update(RequestLog requestLog)
        {
            _requestLogRepository.Update(requestLog);
            _requestLogRepository.SaveChanges();
        }

        public IEnumerable<RequestLog> List()
        {
            return _requestLogRepository.List();
        }

        public void Delete(Guid id)
        {
            _requestLogRepository.Delete(id);
            _requestLogRepository.SaveChanges();
        }

        public void Dispose()
        {
            _requestLogRepository?.Dispose();
        }
    }
}
