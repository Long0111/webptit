using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Dapper;
using MISA.Web01.Api.Model;

namespace MISA.Web01.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        /// <summary>
        /// Lấy danh sách toàn bộ nhân viên
        /// </summary>
        /// <returns>
        /// 200 - Danh sách khách hàng
        /// 204 - không có dữ liệu
        /// </returns>
        /// CreatedBy: NTLONG (11/08/2023)
        [HttpGet()]
        public IActionResult Get() 
        {
            // Khởi tạo kết nối database:
            var connectionString = "Host=127.0.0.1; Port=3333; Database = misa.web.ntlong; User Id = root; Password = a0982990412";
            // 1.1 Khởi tạo kết nối với MariaDb:
            var sqlConnection = new MySqlConnection(connectionString);
            // 2. Lấy dữ liệu:
            // 2.1 Câu lệnh truy vấn dữ liệu:
            var sqlCommand = "SELECT * FROM Employee";

            // 2.2 Thực hiện lấy dữ liệu:
            var employees = sqlConnection.Query<object>(sql: sqlCommand);
            // Trả kết quả cho Client:

            return Ok(employees) ;
        }

        /// <summary>
        /// Lấy nhân viên theo Id
        /// </summary>
        /// <param name="employeeId"> Id của nhân viên</param>
        /// <returns>
        /// 
        /// </returns>
        /// CreatedBy: NTLONG (11/08/2023)
        [HttpGet("{employeeId}")]
        public IActionResult GetById(string employeeId)
        {
            try
            {
                // Khởi tạo kết nối database:
                var connectionString = "Host=127.0.0.1; Port=3333; Database = misa.web.ntlong; User Id = root; Password = a0982990412";
                // 1.1 Khởi tạo kết nối với MariaDb:
                var sqlConnection = new MySqlConnection(connectionString);
                // nếu thực hiện gọi tiếp các hàm khác thì không cần try cacth các hàm bên trong
                // 2. Lấy dữ liệu:
                // 2.1 Câu lệnh truy vấn dữ liệu:
                var sqlCommand = $"SELECT * FROM Employee WHERE EmployeeId = @EmployeeId";
                // Lưu ý: nếu có tham số truyền cho câu lệnh truy vấn sql
                DynamicParameters Parameters = new DynamicParameters();
                Parameters.Add("@EmployeeId", employeeId);
                // 2.2 Thực hiện lấy dữ liệu:
                var employee = sqlConnection.QueryFirstOrDefault<object>(sql: sqlCommand, Parameters);
                // Trả kết quả cho Client:

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Lấy nhân viên theo Id
        /// </summary>
        /// <param name="employee"> Thông tin nhân viên</param>
        /// <returns>
        /// 201 - thêm mới thành công
        /// 400 - dữ liệu đầu vào không hợp lệ
        /// 500 - có exception
        /// </returns>
        /// CreatedBy: NTLONG (15/08/2023)
        [HttpPost]
        public IActionResult Post(Employee employee)
        {
            try
            {
                //Tạo mới EmployeeId:
                employee.EmployeeId = Guid.NewGuid();

                // khai báo thông tinc cần thiết
                var error = new ErrorService();
                var errorData = new Dictionary<string, string>();
                var errorMsgs = new List<string>();

                // bước 1. Validate dữ liệu

                // 1.1 Thông tin mã nhân viên bắt buộc nhập
                if (string.IsNullOrEmpty(employee.EmployeeCode))
                {
                    errorData.Add("EmployeeCode", Resources.ResourceVN.ValidateError_EmployeeCodeEmpty);
                    errorMsgs.Add("Mã nhân viên không được phép để trống");
                }
                // 1.2 Thông tin họ và tên không được phép để trống:
                if (string.IsNullOrEmpty(employee.FullName))
                {
                    errorData.Add("FullName", Resources.ResourceVN.ValidateError_FullNameEmpty);
                    errorMsgs.Add("Họ và tên không được phép để trống");
                }
                // 1.3 Email phải đúng định dạng
                if (!IsValidEmail(email: employee.Email))
                {
                    errorData.Add("Email", Resources.ResourceVN.ValidateError_EmailEx);
                    errorMsgs.Add("Email không đúng định dạng");
                }
                // mã nhân viên không được phép trùng
                if (CheckEmployeeCode(employee.EmployeeCode))
                {
                    errorData.Add("EmployeeCode", Resources.ResourceVN.ValidateError_EmployeeCodeEx);
                    errorMsgs.Add("Mã nhân viên không được phép trùng");
                }
                // email không được phép trùng
                if (CheckEmail(employee.Email))
                {
                    errorData.Add("Email", Resources.ResourceVN.ValidateError_EmailEx);
                    errorMsgs.Add("Email không được phép trùng");
                }
                // 1.4 Ngày sinh không đươch lớn hơn ngày hiện tại:

                // ...
                if (errorData.Count > 0)
                {
                    error.UseMsg = "Dữ liệu không hợp lệ";
                    error.Data = errorMsgs;
                    return BadRequest(error);
                }
                // bước 2: khởi tạo kết nối database:
                var connectionString = "Host=127.0.0.1; Port=3333; Database = misa.web.ntlong; User Id = root; Password = a0982990412";
                var mySqlConnection = new MySqlConnection(connectionString);
                // bước 3: Thwucj hiện thêm mới dữ liệu vào database
                var sqlCommandText = "Proc_InsertEmployee";
                // Mở kết nối đến database
                mySqlConnection.Open();

                //Đọc các tham số đầu vào của store:
                var sqlCommand = mySqlConnection.CreateCommand();
                sqlCommand.CommandText = sqlCommandText;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlCommandBuilder.DeriveParameters(sqlCommand);

                var dynamicParam = new DynamicParameters();
                foreach ( MySqlParameter parameter in sqlCommand.Parameters )
                {
                    // tên của tham số:
                    var paramName = parameter.ParameterName;
                    var propName = paramName.Replace("@m_", "");
                    var entityProperty = employee.GetType().GetProperty(propName);
                    if (entityProperty != null)
                    {
                        var propValue = employee.GetType().GetProperty(propName).GetValue(employee);
                        // thực hiện dán giá trị của các param:
                        dynamicParam.Add(paramName, propValue);
                    }
                    else
                    {
                        dynamicParam.Add(paramName, null);
                    }

                }

                
                /*dynamicParam.Add("@m_EmployeeId",Guid.NewGuid());
                dynamicParam.Add("@m_EmployeeCode", employee.EmployeeCode);
                dynamicParam.Add("@m_FullName", employee.FullName);
                dynamicParam.Add("@m_DateOfBirth", employee.DateOfBirth);
                dynamicParam.Add("@m_Gender", employee.Gender);
                dynamicParam.Add("@m_IdentityNumber", employee.IdentityNumber);
                dynamicParam.Add("@m_IdentityDate", employee.IdentityDate);
                dynamicParam.Add("@m_IdentityPlace", employee.IdentityPlace);
                dynamicParam.Add("@m_Email", employee.Email);
                dynamicParam.Add("@m_PhoneNumber", employee.PhoneNumber);
                dynamicParam.Add("@m_TaxCode", employee.TaxCode);
                dynamicParam.Add("@m_Salary", employee.Salary);
                dynamicParam.Add("@m_JoinDate", employee.JoinDate);
                dynamicParam.Add("@m_WorkStatus", employee.WorkStatus);
                dynamicParam.Add("@m_PositionId", employee.PositionId);
                dynamicParam.Add("@m_DeparmentId", employee.DeparmentId);
                dynamicParam.Add("@m_CreatedDate", DateTime.Now);
                dynamicParam.Add("@m_CreatedBy", "ntlong");
                dynamicParam.Add("@m_ModifiedDate", null);
                dynamicParam.Add("@m_ModifiedBy", null);*/


                
                var res = mySqlConnection.Execute(sql: sqlCommandText, param: dynamicParam, commandType:System.Data.CommandType.StoredProcedure);
                // bước 4 trả thông tin về cho client
                if (res>0)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return Ok(res);
                }

            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Lấy nhân viên theo Id
        /// </summary>
        /// <param name="employeeCode"> Thông tin nhân viên</param>
        /// <returns>
        /// true - đã bị trùng; false - không bị trùng
        /// </returns>
        /// CreatedBy: NTLONG (15/08/2023)
        private bool CheckEmployeeCode(string employeeCode)
        {
            var connectionString = "Host=127.0.0.1; Port=3333; Database = misa.web.ntlong; User Id = root; Password = a0982990412";
            var mySqlConnection = new MySqlConnection(connectionString);
            var sqlCheck = "SELECT EmployeeCode FROM Employee WHERE EmployeeCode = @EmployeeCode";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("@EmployeeCode", employeeCode);
            var res = mySqlConnection.QueryFirstOrDefault<string>(sqlCheck, param: dynamicParams);
            if (res != null)
            {
                return true; 
            }
            else
            {
                return false;
            }
        }

        private bool CheckEmail(string Email)
        {
            var connectionString = "Host=127.0.0.1; Port=3333; Database = misa.web.ntlong; User Id = root; Password = a0982990412";
            var mySqlConnection = new MySqlConnection(connectionString);
            var sqlCheck = "SELECT Email FROM Employee WHERE Email = @Email";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("@Email", Email);
            var res = mySqlConnection.QueryFirstOrDefault<string>(sqlCheck, param: dynamicParams);
            if (res != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private IActionResult HandleException(Exception ex)
        {
            var error = new ErrorService();
            error.DevMsg = ex.Message;
            error.UseMsg = Resources.ResourceVN.Error_Exception;
            return StatusCode(500, error);
        }

        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Sửa nhân viên theo Id
        /// </summary>
        /// <param name="employeeId"> Id của nhân viên</param>
        /// <returns>
        /// 
        /// </returns>
        /// CreatedBy: NTLONG (11/08/2023)
        /*[HttpPut("{employeeId}")]
        public IActionResult Put(Employee employee)
        {
            try
            {
                //Tạo mới EmployeeId:
                // employee.EmployeeId = Guid.NewGuid();

                // khai báo thông tinc cần thiết
                var error = new ErrorService();
                var errorData = new Dictionary<string, string>();
                var errorMsgs = new List<string>();

                // bước 1. Validate dữ liệu

                // 1.1 Thông tin mã nhân viên bắt buộc nhập
                if (string.IsNullOrEmpty(employee.EmployeeCode))
                {
                    errorData.Add("EmployeeCode", Resources.ResourceVN.ValidateError_EmployeeCodeEmpty);
                    errorMsgs.Add("Mã nhân viên không được phép để trống");
                }
                // 1.2 Thông tin họ và tên không được phép để trống:
                if (string.IsNullOrEmpty(employee.FullName))
                {
                    errorData.Add("FullName", Resources.ResourceVN.ValidateError_FullNameEmpty);
                    errorMsgs.Add("Họ và tên không được phép để trống");
                }
                // 1.3 Email phải đúng định dạng
                if (!IsValidEmail(email: employee.Email))
                {
                    errorData.Add("Email", Resources.ResourceVN.ValidateError_EmailEx);
                    errorMsgs.Add("Email không đúng định dạng");
                }
                // mã nhân viên không được phép trùng
                if (CheckEmployeeCode(employee.EmployeeCode))
                {
                    errorData.Add("EmployeeCode", Resources.ResourceVN.ValidateError_EmployeeCodeEx);
                    errorMsgs.Add("Mã nhân viên không được phép trùng");
                }
                // 1.4 Ngày sinh không đươch lớn hơn ngày hiện tại:

                // ...
                if (errorData.Count > 0)
                {
                    error.UseMsg = "Dữ liệu không hợp lệ";
                    error.Data = errorMsgs;
                    return BadRequest(error);
                }
                // bước 2: khởi tạo kết nối database:
                var connectionString = "Host=127.0.0.1; Port=3333; Database = misa.web.ntlong; User Id = root; Password = a0982990412";
                var mySqlConnection = new MySqlConnection(connectionString);
                // bước 3: Thwucj hiện thêm mới dữ liệu vào database
                *//*var sqlCommandText = "Proc_UpdateEmployee";
                // Mở kết nối đến database
                mySqlConnection.Open();

                //Đọc các tham số đầu vào của store:
                var sqlCommand = mySqlConnection.CreateCommand();
                sqlCommand.CommandText = sqlCommandText;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlCommandBuilder.DeriveParameters(sqlCommand);

                var dynamicParam = new DynamicParameters();
                foreach (MySqlParameter parameter in sqlCommand.Parameters)
                {
                    // tên của tham số:
                    var paramName = parameter.ParameterName;
                    var propName = paramName.Replace("@m_", "");
                    var entityProperty = employee.GetType().GetProperty(propName);
                    if (entityProperty != null)
                    {
                        var propValue = employee.GetType().GetProperty(propName).GetValue(employee);
                        // thực hiện dán giá trị của các param:
                        dynamicParam.Add(paramName, propValue);
                    }
                    else
                    {
                        dynamicParam.Add(paramName, null);
                    }

                }*//*


                var res = mySqlConnection.Execute(sql: sqlCommandText, param: dynamicParam, commandType: System.Data.CommandType.StoredProcedure);
                // bước 4 trả thông tin về cho client
                if (res > 0)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return Ok(res);
                }

            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }*/

        [HttpPut("{employeeId}")]
        public IActionResult Put(string employeeId, Employee employee)
        {
            try
            {
                // khai báo thông tinc cần thiết
                var error = new ErrorService();
                var errorData = new Dictionary<string, string>();
                var errorMsgs = new List<string>();

                // bước 1. Validate dữ liệu

                // 1.1 Thông tin mã nhân viên bắt buộc nhập
                if (string.IsNullOrEmpty(employee.EmployeeCode))
                {
                    errorData.Add("EmployeeCode", Resources.ResourceVN.ValidateError_EmployeeCodeEmpty);
                    errorMsgs.Add("Mã nhân viên không được phép để trống");
                }
                // 1.2 Thông tin họ và tên không được phép để trống:
                if (string.IsNullOrEmpty(employee.FullName))
                {
                    errorData.Add("FullName", Resources.ResourceVN.ValidateError_FullNameEmpty);
                    errorMsgs.Add("Họ và tên không được phép để trống");
                }
                // 1.3 Email phải đúng định dạng
                if (!IsValidEmail(email: employee.Email))
                {
                    errorData.Add("Email", Resources.ResourceVN.ValidateError_EmailEx);
                    errorMsgs.Add("Email không đúng định dạng");
                }
                // mã nhân viên không được phép trùng
                if (CheckEmployeeCode(employee.EmployeeCode))
                {
                    errorData.Add("EmployeeCode", Resources.ResourceVN.ValidateError_EmployeeCodeEx);
                    errorMsgs.Add("Mã nhân viên không được phép trùng");
                }
                // email không được phép trùng
                if (CheckEmail(employee.Email))
                {
                    errorData.Add("Email", Resources.ResourceVN.ValidateError_EmailEx);
                    errorMsgs.Add("Email không được phép trùng");
                }
                // 1.4 Ngày sinh không đươch lớn hơn ngày hiện tại:

                // ...
                if (errorData.Count > 0)
                {
                    error.UseMsg = "Dữ liệu không hợp lệ";
                    error.Data = errorMsgs;
                    return BadRequest(error);
                }
                // Khởi tạo kết nối database và thực hiện xử lý validate tương tự như POST
                var connectionString = "Host=127.0.0.1; Port=3333; Database = misa.web.ntlong; User Id = root; Password = a0982990412";
                var mySqlConnection = new MySqlConnection(connectionString);
                // 2. Lấy dữ liệu:
                // 2.1 Câu lệnh truy vấn dữ liệu:
                var sqlCommand = @"
                    UPDATE Employee  e
                    SET EmployeeId = @EmployeeId,
                        EmployeeCode = @EmployeeCode,
                        FullName = @FullName,
                        DateOfBirth = @DateOfBirth,
                        Gender = @Gender,
                        IdentityNumber = @IdentityNumber,
                        IdentityDate = @IdentityDate,
                        IdentityPlace = @IdentityPlace,
                        Email = @Email,
                        PhoneNumber = @PhoneNumber,
                        TaxCode = @TaxCode,
                        Salary = @Salary,
                        JoinDate = @JoinDate,
                        WorkStatus = @WorkStatus,
                        PositionId = @PositionId,
                        DeparmentId = @DeparmentId,
                        CreatedDate = @CreatedDate,
                        CreatedBy = @CreatedBy,
                        ModifiedDate = @ModifiedDate,
                        ModifiedBy = @ModifiedBy
                    WHERE EmployeeId = @EmployeeId";
                // Lưu ý: nếu có tham số truyền cho câu lệnh truy vấn sql
                DynamicParameters Parameters = new DynamicParameters();
                Parameters.Add("@EmployeeId", employeeId);
                Parameters.Add("@EmployeeCode", employee.EmployeeCode);
                Parameters.Add("@FullName", employee.FullName);
                Parameters.Add("@DateOfBirth", employee.DateOfBirth);
                Parameters.Add("@Gender", employee.Gender);
                Parameters.Add("@IdentityNumber", employee.IdentityNumber);
                Parameters.Add("@IdentityDate", employee.IdentityDate);
                Parameters.Add("@IdentityPlace", employee.IdentityPlace);
                Parameters.Add("@Email", employee.Email);
                Parameters.Add("@PhoneNumber", employee.PhoneNumber);
                Parameters.Add("@TaxCode", employee.TaxCode);
                Parameters.Add("@Salary", employee.Salary);
                Parameters.Add("@JoinDate", employee.JoinDate);
                Parameters.Add("@WorkStatus", employee.WorkStatus);
                Parameters.Add("@PositionId", employee.PositionId);
                Parameters.Add("@DeparmentId", employee.DeparmentId);
                Parameters.Add("@CreatedDate", DateTime.Now);
                Parameters.Add("@CreatedBy", "ntlong");
                Parameters.Add("@ModifiedDate", null);
                Parameters.Add("@ModifiedBy", null);
                // 2.2 Thực hiện lấy dữ liệu:
                var existingEmployee = mySqlConnection.QueryFirstOrDefault<Employee>(sql: sqlCommand, Parameters);

                return Ok(existingEmployee);
                /*if (existingEmployee == null)
                {
                    return NotFound();
                }

                // Thực hiện cập nhật các thông tin mới cho existingEmployee tại đây
                var res = mySqlConnection.Execute(sql: sqlCommand, param: Parameters, commandType: System.Data.CommandType.StoredProcedure);
                // ... (Xử lý validate và gọi thủ tục lưu trữ tương tự như POST)

                // bước 4 trả thông tin về cho client
                if (res > 0)
                {
                    return Ok(res); // Thay đổi Status code thành 204 No Content khi cập nhật thành công
                }
                else
                {
                    return StatusCode(500, "Update operation failed");
                }*/
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        [HttpDelete("{employeeId}")]
        public IActionResult Delete(Guid employeeId)
        {
            // Khởi tạo kết nối database và thực hiện xử lý validate tương tự như POST
            var connectionString = "Host=127.0.0.1; Port=3333; Database = misa.web.ntlong; User Id = root; Password = a0982990412";
            var mySqlConnection = new MySqlConnection(connectionString);
            // 2. Lấy dữ liệu:
            // 2.1 Câu lệnh truy vấn dữ liệu:
            var sqlCommand = "DELETE FROM Employee WHERE EmployeeId = @EmployeeId";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("@EmployeeId", employeeId);
            var rowsAffected = mySqlConnection.Execute(sql: sqlCommand, param: dynamicParams);
            return Ok(rowsAffected);
        }
    }
}
