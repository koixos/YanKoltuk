using Microsoft.AspNetCore.Mvc;
using YanKoltukBackend.Models.Entities;

namespace YanKoltukBackend.Services.Interfaces
{
    public interface IFileService
    {
        FileResult GenerateServiceLogsExcel(List<ServiceLog> serviceLogs, DateTime date);
    }
}
