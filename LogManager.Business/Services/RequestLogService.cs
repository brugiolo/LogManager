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

        public int InsertRange(IEnumerable<RequestLog> requestLogs)
        {
            foreach (var requestLog in requestLogs)
                _requestLogRepository.Insert(requestLog);
            
            return _requestLogRepository.SaveChanges();
        }

        public int Insert(RequestLog requestLog)
        {
            _requestLogRepository.Insert(requestLog);
            return _requestLogRepository.SaveChanges();
        }

        public RequestLog Read(Guid id)
        {
            return _requestLogRepository.Read(id);
        }

        public int Update(RequestLog requestLog)
        {
            _requestLogRepository.Update(requestLog);
            return _requestLogRepository.SaveChanges();
        }

        public IEnumerable<RequestLog> List()
        {
            return _requestLogRepository.List();
        }

        public int Delete(Guid id)
        {
            _requestLogRepository.Delete(id);
            return _requestLogRepository.SaveChanges();
        }

        public void Dispose()
        {
            _requestLogRepository?.Dispose();
        }
    }
}
