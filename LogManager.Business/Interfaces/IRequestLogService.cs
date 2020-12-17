using LogManager.Business.Models;
using System;
using System.Collections.Generic;

namespace LogManager.Business.Interfaces
{
    public interface IRequestLogService : IDisposable
    {
        int Insert(RequestLog requestLog);
        int InsertRange(IEnumerable<RequestLog> requestLog);
        RequestLog Read(Guid id);
        int Update(RequestLog requestLog);
        int Delete(Guid id);
        IEnumerable<RequestLog> List();
        IEnumerable<RequestLog> Search(string text);
    }
}
