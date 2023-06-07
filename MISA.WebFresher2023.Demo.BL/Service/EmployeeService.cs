using AutoMapper;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.Common.Constant;
using MISA.WebFresher2023.Demo.Common.MyException;
using MISA.WebFresher2023.Demo.Common.Resource;
using MISA.WebFresher2023.Demo.DL.Entity;
using MISA.WebFresher2023.Demo.DL.Model;
using MISA.WebFresher2023.Demo.DL.Repository;
using MISA.WebFresher2023.Demo.Enum;
using System.Globalization;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Office2016.Excel;
using DocumentFormat.OpenXml.EMMA;

namespace MISA.WebFresher2023.Demo.BL.Service
{
    /// <summary>
    /// Class dịch vụ nhân viên 
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public class EmployeeService : BaseService<Employee, EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>, IEmployeeService
    {
        #region Field
        public IEmployeeRepository _employeeRepository;
        #endregion

        #region Contructor
        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IMapper mapper) : base(employeeRepository, mapper)
        {
            _employeeRepository = employeeRepository;
        }
        #endregion

        #region Method
        /// <summary>
        /// Hàm kiểm tra mã nhân viên đã tồn tại chưa
        /// </summary>
        /// <param name="code">Mã nhân viên</param>
        /// <returns>bool</returns>
        /// Author: LeDucTiep (23/05/2023)
        public async Task<bool> CheckEmployeeCode(string code)
        {
            return await _employeeRepository.CheckEmployeeCode(code);
        }

        /// <summary>
        /// Hàm kiểm tra mã EmployeeCode muốn sửa đã tồn tại chưa
        /// </summary>
        /// <param name="employeeCode">EmployeeCode</param>
        /// <returns>bool</returns>
        /// Author: LeDucTiep (23/05/2023)
        public async Task<bool> CheckDuplicatedEmployeeEditCode(string employeeCode, string itsCode)
        {
            return await _employeeRepository.CheckDuplicatedEmployeeEditCode(employeeCode, itsCode);
        }

        /// <summary>
        /// Hàm phân lấy danh sách nhân viên theo trang 
        /// </summary>
        /// <param name="pageSize">Kích thước trang </param>
        /// <param name="pageNumber">Số thứ tự trang</param>
        /// <param name="employeeSearchTerm">Từ khóa tìm kiếm</param>
        /// <returns>EmployeePage</returns>
        /// Author: LeDucTiep (23/05/2023)
        public async Task<EmployeePage> GetPageAsync(int pageSize, int pageNumber, string? employeeSearchTerm)
        {
            // Lỗi kích thước trang 
            if (pageSize <= 0)
            {
                throw new PagingArgumentException(PagingErrorMessage.InvalidPageSize, (int)PagingErrorCode.InvalidPageSize);
            }

            // Lỗi số thứ tự trang 
            if (pageNumber <= 0)
            {
                throw new PagingArgumentException(PagingErrorMessage.InvalidPageNumber, (int)PagingErrorCode.InvalidPageNumber);
            }

            // Lỗi độ dài từ khóa tìm kiếm
            if (employeeSearchTerm != null && employeeSearchTerm.Length > 255)
            {
                throw new PagingArgumentException(PagingErrorMessage.InvalidEmployeeSearchTerm, (int)PagingErrorCode.InvalidEmployeeSearchTerm);
            }

            return await _employeeRepository.GetPageAsync(pageSize, pageNumber, employeeSearchTerm);
        }

        /// <summary>
        /// Hàm lấy mã nhân viên mới 
        /// </summary>
        /// <returns>Mã nhân viên mới </returns>
        /// Author: LeDucTiep (23/05/2023)
        public async Task<string> GetNewEmployeeCode()
        {
            return await _employeeRepository.GetNewEmployeeCode();
        }

        /// <summary>
        /// Hàm lấy tất cả employee để xuất file 
        /// </summary>
        /// <returns>Danh sách employee</returns>
        /// Author: LeDucTiep (07/06/2023)
        public async Task<IEnumerable<EmployeeExport>> ExportJsonAsync()
        {
            return await _employeeRepository.GetEmployeeExportAsync();
        }

        /// <summary>
        /// Hàm lấy danh sách employee và tạo ra file excel
        /// </summary>
        /// <returns>XLWorkbook</returns>
        /// Author: LeDucTiep (07/06/2023)
        public async Task<XLWorkbook> ExportExcelAsync()
        {
            // Tạo ra workbook 
            XLWorkbook xlWorkbook = new XLWorkbook();

            // Tạo sheet 
            IXLWorksheet sheet1 = xlWorkbook.Worksheets.Add(ExportExcelResource.SheetName);

            // Ghi thông tin lên sheet 
            int rowNumber = await LoadEmployeeExportData(sheet1);

            // Định dạng lại sheet 
            // Định dạng cho toàn trang
            var tempRangeStyle = sheet1.Range(1, 1, rowNumber, 9).Style;
            tempRangeStyle.Alignment.WrapText = true;
            tempRangeStyle.Border.InsideBorder = XLBorderStyleValues.Thin;
            tempRangeStyle.Border.OutsideBorder = XLBorderStyleValues.Thin;

            // Định dạng cho 3 dòng đầu
            tempRangeStyle = sheet1.Range(1, 1, 3, 9).Style;
            tempRangeStyle.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            tempRangeStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            tempRangeStyle.Font.FontName = "Arial";
            tempRangeStyle.Font.Bold = true;

            // Định dạng border 2 dòng đầu 
            sheet1.Range("A1:I1").Style.Border.OutsideBorderColor = XLColor.LightGray;
            sheet1.Range("A2:I2").Style.Border.OutsideBorderColor = XLColor.LightGray;

            // Định dạng table có border màu đen 
            tempRangeStyle = sheet1.Range(3, 1, rowNumber, 9).Style;
            tempRangeStyle.Border.InsideBorderColor = XLColor.Black;
            tempRangeStyle.Border.OutsideBorderColor = XLColor.Black;


            // Định dạng Font cho dòng đầu 
            tempRangeStyle = sheet1.Cell("A1").Style;
            tempRangeStyle.Font.FontSize = 16;

            // Định dạng Font cho tên cột  
            tempRangeStyle = sheet1.Range(3, 1, 3, 9).Style;
            tempRangeStyle.Font.FontSize = 10;
            tempRangeStyle.Fill.BackgroundColor = XLColor.FromArgb(255, 216, 216, 216);

            // Định dạng Text cho dữ liệu trong bảng 
            tempRangeStyle = sheet1.Range(4, 1, rowNumber, 9).Style;
            tempRangeStyle.Font.FontName = "Times New Roman";
            tempRangeStyle.Font.FontSize = 11;
            tempRangeStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            tempRangeStyle.Alignment.Vertical = XLAlignmentVerticalValues.Bottom;
            tempRangeStyle.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            // Trả về workbook 
            return xlWorkbook;
        }

        /// <summary>
        /// Hàm lấy tất cả employee và ghi vào sheet
        /// </summary>
        /// <param name="sheet">Trang cần ghi dữ liệu vào</param>
        /// <returns>Số dòng của sheet</returns>
        /// Author: LeDucTiep (07/06/2023)
        public async Task<int> LoadEmployeeExportData(IXLWorksheet sheet)
        {
            // Tiêu đề của bảng
            sheet.Cell("A1").Value = ExportExcelResource.SheetTitle;
            sheet.Range("A1", "I1").Merge();
            sheet.Range("A2", "I2").Merge();

            // Độ cao của 2 dòng tiêu đề 
            sheet.Row(1).Height = 20.5;
            sheet.Row(2).Height = 22;

            // Danh sách tên các cột 
            string[] headers = new string[]
            {
                ExportExcelResource.NumericalOrder,
                ExportExcelResource.EmployeeCode,
                ExportExcelResource.FullName,
                ExportExcelResource.Gender,
                ExportExcelResource.DateOfBirth,
                ExportExcelResource.PositionName,
                ExportExcelResource.DeparmentName,
                ExportExcelResource.BankAccountNumber,
                ExportExcelResource.NameOfBank,
            };

            // Gán tên cho các cột 
            sheet.Cell("A3").Value = headers[0];
            sheet.Cell("B3").Value = headers[1];
            sheet.Cell("C3").Value = headers[2];
            sheet.Cell("D3").Value = headers[3];
            sheet.Cell("E3").Value = headers[4];
            sheet.Cell("F3").Value = headers[5];
            sheet.Cell("G3").Value = headers[6];
            sheet.Cell("H3").Value = headers[7];
            sheet.Cell("I3").Value = headers[8];

            // Danh sách độ rộng của các cột 
            int[] arrayColumnWidth = new int[9];

            // Nạp dữ liêu cho table 
            // Lấy danh sách các nhân viên 
            IEnumerable<EmployeeExport> employeeList = await _employeeRepository.GetEmployeeExportAsync();

            // Duyệt qua các thuộc tính của nhân viên 
            System.Reflection.PropertyInfo[] properties = new EmployeeExport().GetType().GetProperties();

            // Số thứ tự nhân viên 
            int rowCount = 0;

            // Duyệt qua các nhân viên 
            foreach (EmployeeExport employee in employeeList)
            {
                rowCount++;

                // Số thứ tự cột 
                int columnCount = 1;

                // Ghi giá trị cho cột số thứ tự 
                sheet.Cell(3 + rowCount, columnCount).Value = rowCount;

                // Tìm ra độ rộng lớn nhất của cột số thứ tự
                arrayColumnWidth[0] = Math.Max(arrayColumnWidth[0], $"{rowCount}".Length);

                // Duyệt qua các thuộc tính của nhân viên 
                foreach (System.Reflection.PropertyInfo property in properties)
                {
                    columnCount++;

                    // Tên thuộc tính 
                    var field = property.Name;

                    // Giá trị ghi lên excel 
                    string value = "";

                    // Mỗi kiểu dữ liệu có một cách ghi riêng
                    Type type = property.PropertyType;

                    // Kiểu giới tính 
                    if (type == typeof(Gender?))
                    {
                        Gender? gender = (Gender?)property.GetValue(employee);

                        if (gender == Gender.Nu)
                            value = ExportExcelResource.Female;
                        else if (gender == Gender.Nam)
                            value = ExportExcelResource.Male;
                    }
                    // Kiểu ngày tháng
                    else if (type == typeof(DateTime?))
                    {
                        DateTime? dateTime = (DateTime?)property.GetValue(employee);

                        if (dateTime != null)
                        {
                            value = ((DateTime)dateTime).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                    }
                    // Kiểu số hoặc chuỗi 
                    else
                    {
                        value = property.GetValue(employee)?.ToString() ?? "";
                    }

                    // Ghi dữ liệu cho 1 ô excel
                    sheet.Cell(3 + rowCount, columnCount).Value = value;

                    // Tìm ra ô dài nhất 
                    arrayColumnWidth[columnCount - 1] = Math.Max(arrayColumnWidth[columnCount - 1], value.Length);
                }
            }

            // So sánh ô dài nhất với ô tiêu đề
            for (int index = 0; index < arrayColumnWidth.Length; index++)
            {
                int headerLength = headers[index].Length;
                arrayColumnWidth[index] = Math.Max(arrayColumnWidth[index], headerLength);
            }

            // Tìm ra độ rộng hợp lý của cột 
            for (int index = 0; index < arrayColumnWidth.Length; index++)
            {
                if (index == 7)
                    sheet.Column(index + 1).Width = arrayColumnWidth[index] > 15 ? 15 : arrayColumnWidth[index] * 1.5;
                else
                    sheet.Column(index + 1).Width = arrayColumnWidth[index] > 25 ? 25 : arrayColumnWidth[index] * 1.5;
            }

            // trả về số lượng dòng 
            return rowCount + 3;
        }
        #endregion
    }
}
