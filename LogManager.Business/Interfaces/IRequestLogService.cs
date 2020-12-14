using LogManager.Business.Models;
using System;
using System.Collections.Generic;

namespace LogManager.Business.Interfaces
{
    public interface IRequestLogService : IDisposable
    {
        void Insert(RequestLog requestLog);
        void InsertAndDoNotUpdate(RequestLog requestLog);
        RequestLog Read(Guid id);
        void Update(RequestLog requestLog);
        void Delete(Guid id);
        IEnumerable<RequestLog> List();
    }
}
