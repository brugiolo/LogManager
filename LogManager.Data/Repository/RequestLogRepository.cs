using LogManager.Business.Interfaces;
using LogManager.Business.Models;
using LogManager.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogManager.Data.Repository
{
    public class RequestLogRepository : Repository<RequestLog>, IRequestLogRepository
    {
        public RequestLogRepository(LogManagerContext context) : base(context)
        {
        }
    }
}
