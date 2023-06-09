﻿using AutoMapper;
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
using MISA.WebFresher2023.Demo.Common.Attribute;
using System.Reflection;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;

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
        /// Kiểm tra bản ghi cần thêm 
        /// </summary>
        /// <param name="entity">Bản ghi</param>
        /// <exception cref="BadRequestException">Thông tin không đúng</exception>
        /// Author: LeDucTiep (23/05/2023)
        public override async Task<List<int>> PostValidate(EmployeeDto entity)
        {
            List<int> errorList = new();

            // Kiểm tra tồn tại phòng ban 
            bool isExisted = await _employeeRepository.CheckExistedAsync(entity.DepartmentId, "Department");
            if (!isExisted)
                errorList.Add((int)DepartmentErrorCode.IdNotFound);

            // Kiểm tra tồn tại chức vụ
            if (entity.PositionId != null)
            {
                isExisted = await _employeeRepository.CheckExistedAsync((Guid)entity.PositionId, "Position");

                if (!isExisted)
                    errorList.Add((int)PositionErrorCode.IdNotFound);
            }

            // Kiểm tra phải chưa tồn tại mã nhân viên 
            isExisted = await _employeeRepository.CheckExistedEmployeeCodeAsync(entity.EmployeeCode);
            if (isExisted)
                errorList.Add((int)EmployeeErrorCode.CodeDuplicated);


            return errorList;
        }

        /// <summary>
        /// Validate update một bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <param name="entity">Giá trị bản ghi</param>
        /// Author: LeDucTiep (08/06/2023)
        public override async Task<List<int>> UpdateValidate(Guid id, EmployeeDto entity)
        {
            List<int> errorList = new();

            // Kiểm tra tồn tại phòng ban 
            bool isExisted = await _employeeRepository.CheckExistedAsync(entity.DepartmentId, "Department");
            if (!isExisted)
                // Xử lý lỗi
                errorList.Add((int)DepartmentErrorCode.IdNotFound);


            // Kiểm tra tồn tại chức vụ
            if (entity.PositionId != null)
            {
                isExisted = await _employeeRepository.CheckExistedAsync((Guid)entity.PositionId, "Position");
                if (!isExisted)
                    errorList.Add((int)PositionErrorCode.IdNotFound);
            }

            // Kiểm tra phải chưa tồn tại mã nhân viên, ngoại trừ mã trước khi sửa
            isExisted = await _employeeRepository.CheckDuplicatedEmployeeEditCodeAsync(entity.EmployeeCode, id);
            if (isExisted)
                errorList.Add((int)EmployeeErrorCode.CodeDuplicated);

            // Kiểm tra có Id nhân viên cần sửa không
            isExisted = await _baseRepository.CheckExistedAsync(id);
            if (!isExisted)
                errorList.Add((int)EmployeeErrorCode.IdNotFound);

            return errorList;
        }

        /// <summary>
        /// Xóa một bản ghi theo id 
        /// </summary>
        /// <param name="id">Id của bản ghi </param>
        /// <exception cref="BadRequestException">Lỗi không tìm thấy </exception>
        /// Author: LeDucTiep (23/05/2023)
        public override async Task<List<int>> DeleteValidate(Guid id)
        {
            List<int> errorList = new();

            // Kiểm tra có tồn tại bản ghi không 
            bool isExistedCode = await _baseRepository.CheckExistedAsync(id);

            // Nếu có lỗi xảy ra thì ném lỗi 
            if (!isExistedCode)
            {
                errorList.Add((int)EmployeeErrorCode.IdNotFound);
            }

            return errorList;
        }

        /// <summary>
        /// Hàm kiểm tra mã nhân viên đã tồn tại chưa
        /// </summary>
        /// <param name="code">Mã nhân viên</param>
        /// <returns>bool</returns>
        /// Author: LeDucTiep (23/05/2023)
        public async Task<bool> CheckEmployeeCodeAsync(string code)
        {
            PropertyInfo? property = typeof(EmployeeDto).GetProperty("EmployeeCode");

            if (property != null)
            {

                // Xét độ dài 
                var attributeMaxLength = (MSMaxLengthAttribute?)property.GetCustomAttributes(typeof(MSMaxLengthAttribute), false).FirstOrDefault();

                if (attributeMaxLength != null && code != null)
                {
                    int valueLength = code.Length;
                    int maxLength = attributeMaxLength.Length;
                    bool isTooLong = valueLength > maxLength;
                    if (isTooLong)
                    {
                        List<int> errorList = new() { (int)EmployeeErrorCode.EmployeeCodeTooLong };
                        throw new BadRequestException(errorList);
                    }
                }
            }

            return await _employeeRepository.CheckExistedEmployeeCodeAsync(code);
        }

        /// <summary>
        /// Hàm kiểm tra mã EmployeeCode muốn sửa đã tồn tại chưa
        /// </summary>
        /// <param name="employeeCode">EmployeeCode</param>
        /// <returns>bool</returns>
        /// Author: LeDucTiep (23/05/2023)
        public async Task<bool> CheckDuplicatedEmployeeEditCodeAsync(string employeeCode, string itsCode)
        {
            PropertyInfo? property = typeof(EmployeeDto).GetProperty("EmployeeCode");

            if (property != null)
            {

                // Xét độ dài 
                var attributeMaxLength = (MSMaxLengthAttribute?)property.GetCustomAttributes(typeof(MSMaxLengthAttribute), false).FirstOrDefault();

                if (attributeMaxLength != null && employeeCode != null)
                {
                    int valueLength = employeeCode.Length;
                    int maxLength = attributeMaxLength.Length;
                    bool isTooLong = valueLength > maxLength;
                    if (isTooLong)
                    {
                        List<int> errorList = new() { (int)EmployeeErrorCode.EmployeeCodeTooLong };
                        throw new BadRequestException(errorList);
                    }
                }
            }

            return await _employeeRepository.CheckDuplicatedEmployeeEditCodeAsync(employeeCode, itsCode);
        }

        /// <summary>
        /// Hàm phân lấy danh sách nhân viên theo trang 
        /// </summary>
        /// <param name="pageSize">Kích thước trang </param>
        /// <param name="pageNumber">Số thứ tự trang</param>
        /// <param name="employeeSearchTerm">Từ khóa tìm kiếm</param>
        /// <returns>EmployeePage</returns>
        /// Author: LeDucTiep (23/05/2023)
        public async Task<EmployeePage> GetPageAsync(EmployeePageArgument employeePageArgument)
        {
            List<int> errorCodes = new();

            // Lỗi kích thước trang 
            if (employeePageArgument.PageSize <= 0)
            {
                errorCodes.Add((int)PagingErrorCode.InvalidPageSize);
            }

            // Lỗi số thứ tự trang 
            if (employeePageArgument.PageNumber <= 0)
            {
                errorCodes.Add((int)PagingErrorCode.InvalidPageNumber);
            }

            // Kiểm tra độ dài chuỗi tìm kiếm 
            System.Reflection.PropertyInfo[] properties = typeof(EmployeePageArgument).GetProperties();
            foreach (System.Reflection.PropertyInfo property in properties)
            {
                var value = property.GetValue(employeePageArgument, null);

                // Lỗi độ dài từ khóa tìm kiếm
                var attributeMaxLength = (MSMaxLengthAttribute?)property.GetCustomAttributes(typeof(MSMaxLengthAttribute), false).FirstOrDefault();

                if (attributeMaxLength != null && value != null && value.ToString().Length > attributeMaxLength.Length)
                {
                    errorCodes.Add(attributeMaxLength.ErrorCode);
                }
            }

            // Nếu có lỗi 
            if (errorCodes.Count > 0)
                throw new BadRequestException(errorCodes);

            return await _employeeRepository.GetPageAsync(employeePageArgument);
        }

        /// <summary>
        /// Hàm lấy mã nhân viên mới 
        /// </summary>
        /// <returns>Mã nhân viên mới </returns>
        /// Author: LeDucTiep (23/05/2023)
        public async Task<string> GetNewEmployeeCodeAsync()
        {
            return await _employeeRepository.GetNewEmployeeCodeAsync();
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
            XLWorkbook xlWorkbook = new();

            // Tạo sheet 
            IXLWorksheet sheet1 = xlWorkbook.Worksheets.Add(ExportExcelResource.SheetName);

            // Nạp dữ liêu cho table 
            // Lấy danh sách các nhân viên 
            IEnumerable<EmployeeExport> employeeList = await _employeeRepository.GetEmployeeExportAsync();

            // Ghi thông tin lên sheet 
            LoadEmployeeExportData(sheet1, employeeList);

            // Thêm style
            StyleSheet(sheet1, employeeList);

            // Trả về workbook 
            return xlWorkbook;
        }

        /// <summary>
        /// Hàm định dạng, căn chỉnh excel
        /// </summary>
        /// <param name="sheet">Trang tính</param>
        /// <param name="employeeList">Danh sách nhân viên</param>
        private void StyleSheet(IXLWorksheet sheet, IEnumerable<EmployeeExport> employeeList)
        {
            int rowNumber = employeeList.Count() + 3;
            int columnNumber = new EmployeeExport().GetType().GetProperties().Length + 1;
            // Định dạng lại sheet 
            // Định dạng border 2 dòng đầu 
            sheet.Range(1, 1, 1, columnNumber).Style.Border.OutsideBorderColor = XLColor.LightGray;
            sheet.Range(2, 1, 2, columnNumber).Style.Border.OutsideBorderColor = XLColor.LightGray;
            sheet.Range(1, 1, 1, columnNumber).Merge();
            sheet.Range(2, 1, 2, columnNumber).Merge();

            // Độ cao của 2 dòng tiêu đề 
            sheet.Row(1).Height = 20.5;
            sheet.Row(2).Height = 22;
            sheet.Rows(3, rowNumber).Height = 15;

            // Định dạng cho toàn trang
            var tempRangeStyle = sheet.Range(1, 1, rowNumber, columnNumber).Style;
            tempRangeStyle.Alignment.WrapText = true;
            tempRangeStyle.Border.InsideBorder = XLBorderStyleValues.Thin;
            tempRangeStyle.Border.OutsideBorder = XLBorderStyleValues.Thin;

            // Định dạng cho 3 dòng đầu
            tempRangeStyle = sheet.Range(1, 1, 3, columnNumber).Style;
            tempRangeStyle.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            tempRangeStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            tempRangeStyle.Font.FontName = "Arial";
            tempRangeStyle.Font.Bold = true;

            // Định dạng table có border màu đen 
            tempRangeStyle = sheet.Range(3, 1, rowNumber, columnNumber).Style;
            tempRangeStyle.Border.InsideBorderColor = XLColor.Black;
            tempRangeStyle.Border.OutsideBorderColor = XLColor.Black;

            // Định dạng Font cho dòng đầu 
            tempRangeStyle = sheet.Cell("A1").Style;
            tempRangeStyle.Font.FontSize = 16;

            // Định dạng Font cho tên cột  
            tempRangeStyle = sheet.Range(3, 1, 3, columnNumber).Style;
            tempRangeStyle.Font.FontSize = 10;
            tempRangeStyle.Fill.BackgroundColor = XLColor.FromArgb(255, 216, 216, 216);

            // Định dạng Text cho dữ liệu trong bảng 
            tempRangeStyle = sheet.Range(4, 1, rowNumber, columnNumber).Style;
            tempRangeStyle.Font.FontName = "Times New Roman";
            tempRangeStyle.Font.FontSize = 11;
            tempRangeStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            tempRangeStyle.Alignment.Vertical = XLAlignmentVerticalValues.Bottom;

            // Căn giữa ngày tháng
            sheet.Column(5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        /// <summary>
        /// Hàm lấy tất cả employee và ghi vào sheet
        /// </summary>
        /// <param name="sheet">Trang cần ghi dữ liệu vào</param>
        /// <returns>Số dòng của sheet</returns>
        /// Author: LeDucTiep (07/06/2023)
        private void LoadEmployeeExportData(IXLWorksheet sheet, IEnumerable<EmployeeExport> employeeList)
        {
            // Tiêu đề của bảng
            sheet.Cell("A1").Value = ExportExcelResource.SheetTitle;

            // Danh sách tên các cột 
            string[] headers = new string[]
            {
                ExportExcelResource.NumericalOrder,
                ExportExcelResource.EmployeeCode,
                ExportExcelResource.FullName,
                ExportExcelResource.Gender,
                ExportExcelResource.DateOfBirth,
                ExportExcelResource.IdentityNumber,
                ExportExcelResource.PositionName,
                ExportExcelResource.DeparmentName,
                ExportExcelResource.BankAccountNumber,
                ExportExcelResource.NameOfBank,
                ExportExcelResource.BankAccountBranch,
            };

            // Gán tên cho các cột 
            for (int i = 0; i < headers.Length; i++)
                sheet.Cell(3, i + 1).Value = headers[i];


            // Danh sách độ rộng của các cột 
            int[] arrayColumnWidth = new int[headers.Length];



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
        }
        #endregion
    }
}
