$(document).ready(function(){
    loadData();
    var formMode = "edit";
    var employeeIDorUpdate = null;
    // 2. thực hiện binding dữ liệu lên UI
    // thực hiện gán các sự kiện
    //nhấn thêm mới:
    $("#btnAdd").click(function(){
        formMode = "add";
        //hiển thị form mới
        $("#dlgDialog").show();
        // focus vào ô đầu tiên
        $("#txtEmployeeCode").focus();
    })
    //ẩn form
    $(".m-dialog-close").click(function(){
        // ẩn form thêm mới
        $("#dlgDialog").hide();
    })
    // nhấn đúp chuột sự kiện
    console.log("Gán sự kiện!");
    // $(".m-table").on("dblclick","tr", rowDblClick);
    $(".m-table").on("dblclick", "tr", function(){
        formMode = "edit";
        // Hiển thị form mới
        console.log(this);
        //lấy ra dữ liệu tương ứng của dòng dữ liệu hiện tại
        let employee = $(this).data("entity");
        employeeIDorUpdate = employee.EmployeeId;
        $("#txtEmployeeCode").val(employee.EmployeeCode);
        $("#txtFullName").val(employee.FullName);
        $("#cbxGender").val(employee.Gender);
        $("#dtDateOfBirth").val(employee.DateOfBirth);

        $("#txtIdentityNumber").val(employee.IdentityNumber);
        $("#dtIdentityDate").val(employee.IdentityDate);
        $("#txtAddress").val(employee.Address);
        $("#txtPhoneNumber").val(employee.PhoneNumber);
        $("#txtEmail").val(employee.Email);
        $("#txtPositionName").val(employee.PositionName);
        $("#cbxDepartmentName").val(employee.DepartmentName);

        $("#txtSalary").val(employee.Salary);
        $("#cbxWorkStatus").val(employee.WorkStatus);
        $("#dlgDialog").show();
    })

    $(".m-table").on("click","tr", function(){
        // xóa hết những dòng đã chọn
        $(this).siblings().removeClass("row-selected");
        this.classList.add("row-selected");
    })
    
    //validate dữ liệu
    $("#btnSave").click(function(){
        // 1. validate dữ liệu
        // họ và tên không được phép trống
        // Mã nhân viên không được trống
        // Ngày sinh không được lớn hơn ngày hiện tại
        // Email phải đúng định dạng
        // Tiền lương phải là số

        let employeeCode = $("#txtEmployeeCode").val();
        let fullName = $("#txtFullName").val();
        let gender = $("#cbxGender").val();
        let dob = $("#dtDateOfBirth").val();

        let identityNumber = $("#txtIdentityNumber").val();
        let identityDate = $("#dtIdentityDate").val();
        let address = $("#txtAddress").val();
        let phoneNumber = $("#txtPhoneNumber").val();
        let email = $("#txtEmail").val();
        
        let positionName = $("#txtPositionName").val();
        let departmentName = $("#cbxDepartmentName").val();
        let salary = $("#txtSalary").val();
        let workStatus = $("#cbxWorkStatus").val();

        

        if (employeeCode == null || employeeCode === "") {
            // alert(resoure["VI"].employeeNotEmpty);
            alert("Tên không được để trống.");
        }
        console.log(dob);
        if (dob) {
            dob = new Date(dob);
        }
        if (dob > new Date()) {
            alert("Ngày sinh không được phép lớn hơn ngày hiện tại");
        }
    

    //2. build ojbect
    let employee =  {
        "EmployeeCode": employeeCode,
        "FullName": fullName,
        "Gender": gender,
        "DateOfBirth": dob,
        "IdentityNumber": identityNumber,
        "IdentityDate": identityDate,
        "Address": address,
        "PhoneNumber": phoneNumber,
        "Email": email,
        "PositionName": positionName,
        "DepartmentName": departmentName,
        "Salary": salary,
        "WorkStatus": workStatus
        
    }
    // 3 gọi api thực hiện thêm mới
    // 4 hiển thị loadding
    if (formMode == "add") {
        $.ajax({
            type: "POST",
            url: "https://cukcuk.manhnv.net/api/v1/Employees",
            data: JSON.stringify(employee),
            dataType: "json",
            contentType:"application/json",
            success: function(response){
                // 5 sau khi thêm mới xong ấn loading, ẩn form chi tiết, loading lại dữ liệu
                $(`.m-loading`).hide();
                $("#dlgDialog").hide();
                loadData();
                
            },
            Error: function(response){
                alert(response.responseJSON.userMsg);
                $(`.m-loading`).hide();
            }
        });
    }else if(formMode == "edit"){
        $.ajax({
            type: "PUT",
            url: `https://cukcuk.manhnv.net/api/v1/Employees/${employeeIDorUpdate}`,
            data: JSON.stringify(employee),
            dataType: "json",
            contentType:"application/json",
            success: function(response){
                // 5 sau khi thêm mới xong ấn loading, ẩn form chi tiết, loading lại dữ liệu
                $(`.m-loading`).hide();
                $("#dlgDialog").hide();
                loadData();
                
            },
            Error: function(response){
                alert(response.responseJSON.userMsg);
                $(`.m-loading`).hide();
            }
        });
    }
    

    })
    // hiển thị trạng thái lỗi khi không nhập vào các trường bắt buộc
    $("input[required]").blur(function(){
        var me = this;
        validateInputRequired(me);
    })

    $("#btnDelete").click(function(){
        formMode = "delete";
    
        
    
            if (formMode == "delete" ) {
                $.ajax({
                    type: "DELETE",
                    url: `https://cukcuk.manhnv.net/api/v1/Employees/${employeeIDorUpdate}`,
                    dataType: "json",
                    contentType:"application/json",
                    success: function(response){
                        // 5 sau khi thêm mới xong ấn loading, ẩn form chi tiết, loading lại dữ liệu
                        $(`.m-loading`).hide();
                        $("#dlgDialog").hide();
                        loadData();
                        
                    },
                    Error: function(response){
                        alert(response.responseJSON.userMsg);
                        $(`.m-loading`).hide();
                    }
                });
            }
    })

})



    /**--------------- /
     * load dữ liệu
     * Author: NTLONG (20/7/2023)
     */

function loadData(){
    $(`.m-loading`).show();
    console.log("Bắt đàu lấy dữ liệu");
    $.ajax({
        type: "GET",
        url: "https://cukcuk.manhnv.net/api/v1/Employees",
        
        success: function(response){
            // debugger;
            for (const employee of response) {
                let employeeCode = employee.EmployeeCode;
                let fullName = employee.FullName;
                let genderName = employee["GenderName"];
                let dob = employee["DateOfBirth"];
                let salary = employee.Salary;
                let phoneNumber = employee.PhoneNumber;
                let email = employee.Email;
                let address = employee.Address;
                let identityDate = employee.IdentityDate; //ngay cấp
                let departmentName = employee.DepartmentName;
                let identityNumber = employee.IdentityNumber; //cmt
                let workStatus = employee.WorkStatus; //ttcv
                let positionName = employee.PositionName;

                // fix
                salary = Math.floor(Math.random() * 10)*1000000;
                if (salary) {
                    salary = new Intl.NumberFormat('vn-VI', { style: 'currency', currency: 'VND' }).format(salary);
                }else{
                    salary="";
                }
                
                if (dob) {
                    dob = new Date(dob);
                    let date = dob.getDate();
                    date = date<10?`0${date}`:date;
                    let month = dob.getMonth() + 1;
                    month = month<10?`0${month}`:month;
                    let year = dob.getFullYear();

                    dob = `${date}/${month}/${year}`;
                }else{
                    dob="";
                }

                if (identityDate) {
                    identityDate = new Date(identityDate);
                    let date = identityDate.getDate();
                    date = date<10?`0${date}`:date;
                    let month = identityDate.getMonth() + 1;
                    month = month<10?`0${month}`:month;
                    let year = identityDate.getFullYear();

                    identityDate = `${date}/${month}/${year}`;
                }else{
                    identityDate="";
                }

                if (genderName) {
                    
                }else{
                    genderName="";
                }

                

                var el = $(`<tr>
                            <td class="text-align-center">
                                <input type="checkbox" checked/>
                            </td>
                            <td class="text-align-left">${employeeCode}</td>
                            <td class="text-align-left">${fullName}</td>
                            <td class="text-align-left">${genderName}</td>
                            <td class="text-align-center">${dob}</td>
                            <td class="text-align-left">${identityNumber}</td>
                            <td class="text-align-left">${identityDate}</td>
                            <td class="text-align-left">${address}</td>
                            <td class="text-align-left">${phoneNumber}</td>
                            <td class="text-align-left">${email}</td>
                            <td class="text-align-left">${positionName}</td>
                            <td class="text-align-left">${departmentName}</td>
                            <td class="text-align-right">${salary}</td>
                            <td class="text-align-left">${workStatus}</td>
                            

                        </tr>`);
                        el.data("entity", employee);
                $("table#tblEmployee tbody").append(el);
                $(`.m-loading`).hide();
                console.log("Binding dữ liệu xong!");
            }
            
        },
        Error: function(response){
            debugger
        }
    });
}

    // hiển thị trạng thái lỗi khi nhập sai
    // $("#txtEmployeeCode").blur(function(){
    //     let employeeCode = $("#txtEmployeeCode").val();
    //     if (employeeCode == null || employeeCode === "") {
    //         // set style cho ô nhập liệu có boder màu đỏ:
    //         $("#txtEmployeeCode").addClass("m-input-error");
    //         // lệnh thuần
    //         // document.getElementById("txtEmployeeCode").classList.add("m-input-error");
    //         // set thông tin lỗi tương ứng khi người dùng hover vào ô nhập liệu:
    //         $("#txtEmployeeCode").attr("title", "Thông tin người dùng không được để trống");
    //     } else{
    //         $("#txtEmployeeCode").removeClass("m-input-error");
    //         $("#txtEmployeeCode").removeAttr("title");
    //     }

    // })

    

    /**--------------- /
     * nhấn đúp chuột
     * Author: NTLONG (20/7/2023)
     */

    function rowDblClick(){
        formMode = "edit";
        // Hiển thị form mới
        console.log(this);
        //lấy ra dữ liệu tương ứng của dòng dữ liệu hiện tại
        let employee = $(this).data("entity");
        employeeIDorUpdate = employee.EmployeeId;
        employeeIDorUpdate = employee.EmployeeId;
        $("#txtEmployeeCode").val(employee.EmployeeCode);
        $("#txtFullName").val(employee.FullName);
        $("#cbxGender").val(employee.Gender);
        $("#dtDateOfBirth").val(employee.DateOfBirth);

        $("#txtIdentityNumber").val(employee.IdentityNumber);
        $("#dtIdentityDate").val(employee.IdentityDate);
        $("#txtAddress").val(employee.Address);
        $("#txtPhoneNumber").val(employee.PhoneNumber);
        $("#txtEmail").val(employee.Email);
        $("#txtPositionName").val(employee.PositionName);
        $("#cbxDepartmentName").val(employee.DepartmentName);

        $("#txtSalary").val(employee.Salary);
        $("#cbxWorkStatus").val(employee.WorkStatus);
        $("#dlgDialog").show();
    }
    
    /**--------------- /
     * validate dữ liệu
     * Author: NTLONG (20/7/2023)
     */

    function validateInputRequired(input){
        var me=this;
        let value = $(input).val();
        if (value == null || value === "") {
            // set style cho ô nhập liệu có boder màu đỏ:
            $(input).addClass("m-input-error");
            // lệnh thuần
            // document.getElementById("txtEmployeeCode").classList.add("m-input-error");
            // set thông tin lỗi tương ứng khi người dùng hover vào ô nhập liệu:
            $(input).attr("title", "Thông tin người dùng không được để trống");
        } else{
            $(input).removeClass("m-input-error");
            $(input).removeAttr("title");
        }
    }


