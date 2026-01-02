using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scaffold.Files.Exporters.Contract;
using RestWithAspNet10Scaffold.Files.Exporters.Factory;

namespace RestWithAspNet10Scaffold.Files.Exporters.Impl;

public class ExcelFileExporter : IFileExporter
{
    public FileContentResult ExportFile(List<PersonDto> persons)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Persons");
        
        // Add header row
        worksheet.Cell(1, 1).Value = "First Name";
        worksheet.Cell(1, 2).Value = "Last Name";
        worksheet.Cell(1, 3).Value = "Address";
        worksheet.Cell(1, 4).Value = "Gender";
        worksheet.Cell(1, 5).Value = "Enabled";
        
        var headerRange = worksheet.Range(1, 1, 1, 5);
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        
        // Add data rows
        int rowNumber = 2;
        foreach (var person in persons)
        {
            worksheet.Cell(rowNumber, 1).Value = person.FirstName;
            worksheet.Cell(rowNumber, 2).Value = person.LastName;
            worksheet.Cell(rowNumber, 3).Value = person.Address;
            worksheet.Cell(rowNumber, 4).Value = person.Gender;
            worksheet.Cell(rowNumber, 5).Value = person.Enabled == true ? "Yes" : "No";
            rowNumber++;
        }

        worksheet.Columns().AdjustToContents();
        
        using var memoryStream = new MemoryStream();
        workbook.SaveAs(memoryStream);
        var fileBytes = memoryStream.ToArray();
        
        return new FileContentResult(fileBytes, MediaTypes.ApplicationExcel)
        {
            FileDownloadName = $"Persons_exported_{DateTime.UtcNow:yyyyMMddHHmmss}.xlsx"
        };
    }
}