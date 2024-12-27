using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using YanKoltukBackend.Models.Entities;
using YanKoltukBackend.Services.Interfaces;

namespace YanKoltukBackend.Services.Implementations
{
    public class FileService : IFileService
    {
        public FileResult GenerateServiceLogsExcel(List<ServiceLog> serviceLogs, DateTime date)
        {
            using var wb = new XLWorkbook();
            /*var toSchoolLogs = serviceLogs.Where(
                log => log.TripType == TripType.ToSchool && log.Date.Date == date.Date).ToList();
            var fromSchoolLogs = serviceLogs.Where(
                log => log.TripType == TripType.FromSchool && log.Date.Date == date.Date).ToList();

            var ws = wb.Worksheets.Add("Service Logs");

            // to school table
            ws.Cell(1, 1).Value = "To School - " + date.ToString("dd-MM-yyyy");
            ws.Cell(2, 1).Value = "Student Name";
            ws.Cell(2, 2).Value = "School No";
            ws.Cell(2, 3).Value = "Pickup";
            ws.Cell(2, 4).Value = "Drop Off";

            for (int i = 0; i < toSchoolLogs.Count; i++)
            {
                var log = toSchoolLogs[i];
                ws.Cell(i + 3, 1).Value = $"{log.Student.Name}";
                ws.Cell(i + 3, 2).Value = log.Student.SchoolNo;
                ws.Cell(i + 3, 3).Value = log.PickupTime?.ToString(@"hh\:mm") ?? "N/A";
                ws.Cell(i + 3, 4).Value = log.DropOffTime?.ToString(@"hh\:mm") ?? "N/A";
            }

            // from school table
            int startRow = toSchoolLogs.Count + 5;
            ws.Cell(startRow, 1).Value = "From School - " + date.ToString("dd-MM-yyyy");
            ws.Cell(startRow + 1, 1).Value = "Student Name";
            ws.Cell(startRow + 1, 2).Value = "School No";
            ws.Cell(startRow + 1, 3).Value = "Pickup";
            ws.Cell(startRow + 1, 4).Value = "Drop Off";

            for (int i = 0; i < fromSchoolLogs.Count; i++)
            {
                var log = fromSchoolLogs[i];
                ws.Cell(startRow + i + 2, 1).Value = $"{log.Student.Name}";
                ws.Cell(startRow + i + 2, 2).Value = log.Student.SchoolNo;
                ws.Cell(startRow + i + 2, 3).Value = log.PickupTime?.ToString(@"hh\:mm") ?? "N/A";
                ws.Cell(startRow + i + 2, 4).Value = log.DropOffTime?.ToString(@"hh\:mm") ?? "N/A";
            }
            */
            using var stream = new MemoryStream();
            wb.SaveAs(stream);
            var content = stream.ToArray();
            return new FileContentResult(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = $"ServiceLogs_{date:ddMMyyyy}.xlsx"
            };
        }
    }
}
