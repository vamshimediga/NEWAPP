
using Dapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Data.Common;
using System.Data.SqlClient;
namespace NEWAPP.Controllers
{
    public class FileController : Controller
    {
        public readonly DbConnection _connection;
        public readonly IFile _file;
        public FileController(IConfiguration configuration,IFile file)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            _file = file;
        }
        [HttpGet]
        public ActionResult index()
        {
            List<Xlsheet> Xlsheet= _file.GetXlsheet();
            return View(Xlsheet);
        }

        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }

        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("File is not selected or empty.");

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Set the license context for EPPlus
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                        return BadRequest("The uploaded file does not contain any worksheets.");

                    int startRow = 2; // Skip header row
                    int rowCount = worksheet.Dimension.Rows;
                    await _connection.OpenAsync();

                    for (int row = startRow; row <= rowCount; row++)
                    {
                        var companyName = worksheet.Cells[row, 1].Value?.ToString();
                        var phone = worksheet.Cells[row, 2].Value?.ToString();
                        var companyOwner = worksheet.Cells[row, 3].Value?.ToString();

                        var parameters = new DynamicParameters();
                        parameters.Add("@CompanyName", string.IsNullOrEmpty(companyName) ? (object)DBNull.Value : companyName);
                        parameters.Add("@Phone", string.IsNullOrEmpty(phone) ? (object)DBNull.Value : phone);
                        parameters.Add("@CompanyOwner", string.IsNullOrEmpty(companyOwner) ? (object)DBNull.Value : companyOwner);

                        var query = "INSERT INTO xlsheet (COMPANY_NAME, PHONE, COMPANY_OWNER) VALUES (@CompanyName, @Phone, @CompanyOwner)";

                        await _connection.ExecuteAsync(query, parameters);
                    }

                }
                 System.IO.File.Delete(filePath);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
