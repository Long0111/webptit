

<template>
  <div id="dlgDialog" class="m-dialog" v-bind:class="{ isShowDialog: isShow }">
    <div class="m-dialog-content">
      <div class="m-dialog-header">
        <div class="m-dialog-title" style="text-overflow: 60">
          THÔNG TIN NHÂN VIÊN
        </div>
        <button class="m-dialog-close" @click="btnCloseOnClick">
          <i class="fas fa-window-close"></i>
        </button>
      </div>

      <div class="m-dialog-main">
        <div class="m-dialog-img">
          <input type="file" id="uploadInput" style="display: none" />
          <label for="uploadInput">
            <button>Chọn ảnh</button>
            <div>(Vui lòng chọn ảnh có định dạng .jpg .jpeg .png .gif)</div>
          </label>
          <img
            id="previewImage"
            src="#"
            alt="Hình ảnh xem trước"
            style="display: none; max-width: 300px"
          />

          <!-- <script>
                                // Gán một hàm xử lý sự kiện khi người dùng chọn tệp tin
                                document.getElementById("uploadInput").addEventListener("change", function(event) {
                                    var input = event.target;
                                    var reader = new FileReader();

                                    // Xử lý khi đọc xong tệp tin ảnh
                                    reader.onload = function() {
                                        var dataURL = reader.result;
                                        var previewImage = document.getElementById("previewImage");

                                        // Hiển thị hình ảnh xem trước
                                        previewImage.src = dataURL;
                                        previewImage.style.display = "block";
                                    };

                                    // Đọc tệp tin ảnh
                                    reader.readAsDataURL(input.files[0]);
                                });
                            </script> -->
        </div>
        <div class="m-dialog-body">
          <div class="m-dialog-a">
            <label for="" style="border-bottom: 5px solid #019160"
              >A. THÔNG TIN CHUNG</label
            >
            <div class="m-dialog-left">
              <label for="">Mã nhân viên</label>
              <div class="m-row">
                <input
                id="txtEmployeeCode"
                v-model="employee.EmployeeCode"
                required
                type="text"
                ref="txtEmployeeCode"
                style="width: 200px"
                class="m-input"
                placeholder="Nhập mã nhân viên"
                @blur="validateRequired"
              />
              </div>
              <label for="">Ngày sinh</label>
              <div class="m-row">
                <input
                  id="dtDateOfBirth"
                  v-model="employee.DateOfBirth"
                  type="date"
                  style="width: 200px"
                  class="m-input"
                />
              </div>
              <label for="">Số CMTND/ Căn cước</label>
              <div class="m-row">
                <input
                  id="txtIdentityNumber"
                  v-model="employee.IdentityNumber"
                  required
                  type="text"
                  style="width: 200px"
                  class="m-input text-align-right"
                />
              </div>
              <label for="">Nơi cấp</label>
              <div class="m-row">
                <input
                  id="txtAddress"
                  v-model="employee.Address"
                  required
                  type="text"
                  style="width: 200px"
                  class="m-input text-align-right"
                />
              </div>
              <label for="">Email</label>
              <div class="m-row">
                <input
                  id="txtEmail"
                  v-model="employee.Email"
                  type="text"
                  style="width: 200px"
                  class="m-input"
                />
              </div>
            </div>

            <div class="m-dialog-right">
              <label for="">Họ và tên</label>
              <div class="m-row">
                <input
                  id="txtFullName"
                  v-model="employee.FullName"
                  required
                  type="text"
                  ref="txtFullName"
                  style="width: 200px"
                  class="m-input"
                  placeholder="Nhập họ và tên"
                  @blur="validateRequired"
                />
              </div>
              <label for="">Giới tính</label>
              <div class="m-row">
                <select name="" id="cbxGender" style="width: 234px">
                  <option value="0">Nam</option>
                  <option value="1">Nữ</option>
                </select>
              </div>
              <label for="">Ngày cấp</label>
              <div class="m-row">
                <input
                  id="dtIdentityDate"
                  v-model="employee.IdentityDate"
                  type="date"
                  style="width: 200px"
                  class="m-input"
                />
              </div>
              <label for="">Số điện thoại</label>
              <div class="m-row">
                <input
                  id="txtPhoneNumber"
                  v-model="employee.PhoneNumber"
                  required
                  type="text"
                  style="width: 200px"
                  class="m-input text-align-right"
                />
              </div>
            </div>
          </div>
          <div class="m-dialog-b">
            <label
              for=""
              style="
                position: fixed;
                bottom: 248px;
                border-bottom: 5px solid #019160;
              "
              >B. THÔNG TIN CÔNG VIỆC</label
            >
            <div class="m-dialog-left">
              <label for="">Vị trí</label>
              <div class="m-row">
                <select name="" id="txtPositionName" style="width: 234px">
                  <option value="VT66">Giám đốc</option>
                  <option value="VT66">Nhân viên</option>
                </select>
              </div>
              <!-- <label for="">Vị trí</label>
                                    <div class="m-row">
                                        <input id="txtPositionName" required type="text" style="width: 200px;" class="m-input " placeholder="Nhập họ và tên">
                                    </div> -->
              <label for="">Mã số thuế cá nhân</label>
              <div class="m-row">
                <input
                  id=""
                  required
                  type="text"
                  style="width: 200px"
                  class="m-input text-align-right"
                />
              </div>
              <label for="">Ngày gia nhập công ty</label>
              <div class="m-row">
                <input id="" type="date" style="width: 200px" class="m-input" />
              </div>
            </div>

            <div class="m-dialog-right">
              <!-- <label for="">Phòng ban</label>
                                    <div class="m-row">
                                        <select name="" id="cbxDepartmentName" style="width: 234px;">
                                            <option >Phòng nhân sự</option>
                                            <option >Phòng kế toán</option>
                                        </select>
                                    </div> -->
              <label for="">Phòng ban</label>
              <div class="m-row">
                <select
                  v-model="employee.DepartmentId"
                  name=""
                  id="cbxDeparment"
                  ref="cbxDeparment"
                  @blur="validateRequired"
                  style="width: 200px"
                >
                  <option value="75cce5a6-4f7f-3671-3cf3-eb0223d0a4f7">
                    Phòng nhân sự
                  </option>
                  <option value="78aafe4a-67a7-2076-3bf3-eb0223d0a4f7">
                    Phòng tuyển dụng
                  </option>
                  <option value="35e773ea-5d44-2dda-26a8-6d513e380bde">
                    Phòng Marketing
                  </option>
                  <option value="45ac3d26-18f2-18a9-3031-644313fbb055">
                    Phòng đào tạo
                  </option>
                </select>
              </div>
              <label for="">Mức lương cơ bản</label>
              <div class="m-row">
                <input
                  id="txtSalary"
                  v-model="employee.Salary"
                  type="text"
                  style="width: 200px"
                  class="m-input text-align-right"
                  @input="inputSalaryFormat"
                  @keydown="txtSalaryKeyDown"
                />
              </div>
              <label for="">Tình trạng công việc</label>
              <div class="m-row">
                <select name="" id="cbxWorkStatus" style="width: 234px">
                  <option value="0">Đang làm việc</option>
                  <option value="1">OFF</option>
                </select>
              </div>
            </div>
          </div>
          <div class="m-dialog-footer">
            <button id="btnDelete" class="m-btn" @click="btnDeleteOnClick">
              Xóa
            </button>
            <button id="btnSave" class="m-btn" @click="btnSaveOnClick">
              Lưu
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import axios from "axios";
export default {
  methods: {
    txtSalaryKeyDown() {
      //Kiểm tra giá trị vừa nhập, nếu khoong phải là số
    },
    /**
     * Định dạng dữ liệu lương khi nhập liệu
     * Author: NTLONG(09/08/2023)
     */
    inputSalaryFormat() {
      // Lấy ra value của input (mức lương)
      var value = event.currentTarget.value;
      var currentText = event.data;
      // Thực hiện định dạng lại dữ liệu
      if (currentText == ".") {
        return;
      }
      if (value) {
        value = value.replaceAll(",", "");
        value = Number(value);
        if (isNaN(value)) {
          return;
        }
      }
      value = new Intl.NumberFormat("en-VI").format(value);
      event.currentTarget.value = value;

      // Set lại giá trị hiển thị trên input:
      console.log(value);
    },
    /**
     * thực hiện validate dữ liệu bắt buộc nhập
     * Author: NTLONG(09/08/2023)
     */
    validateRequired() {
      var value = event.currentTarget.value;
      if (!value) {
        //thêm border màu đỏ cho input hiện tại
        event.currentTarget.classList.add("m-input-error");
        // thêm title thông báo lỗi cho input hiện tại
        event.currentTarget.setAttribute(
          "title",
          "Thông tin này không được phép để trống"
        );
      }
    },
    btnCloseOnClick() {
      this.$emit("abc", false);
      // this.isShow = false
    },
    btnDeleteOnClick(){
      var me = this;

      if (me.employeeSelectedId) {
        // Gọi API thực hiện xóa nhân viên
        axios
          .delete(`https://cukcuk.manhnv.net/api/v1/Employees/${me.employeeSelectedId}`)
          .then(function() {
            // Xử lý khi xóa thành công
            alert("Xóa nhân viên thành công");
            me.$emit("abc", false); // Đóng dialog sau khi xóa thành công
          })
          .catch(function(error) {
            // Xử lý khi xảy ra lỗi
            alert("Lỗi xóa nhân viên");
            console.log(error);
          });
      } else {
        alert("Không có nhân viên để xóa");
      }

    },
    /**
     * Thực hiên cất dữ liệu
     * Authour: NTLONG
     */
    btnSaveOnClick() {
      var me = this;
      // 1. validate dữ liệu
      var isValid = me.validateData();
      if (!isValid) {
        return;
      }
      // 2. build object thông tin nhân viên
      if (me.employee.Salary) {
        me.employee.Salary = Number(me.employee.Salary);
      }
      // 3. Gọi api thực hiện thêm mới
      // 3.1 Kiểm tra trạng thái form mới hay sửa
      if (this.formMode == 1) {
        axios
          .post("https://cukcuk.manhnv.net/api/v1/Employees", me.employee)
          .then(function () {
            //  nếu thêm mới thành công thì toast msg thông báo thành công
            alert("them moi thanh cong");
            me.$emit("abc", false);
          })
          .catch((error) => {
            let response = error.response;
            switch (response.status) {
              case 400:
              case 500:
                var userMsg = response.data["userMsg"];
                alert(userMsg);
                break;

              default:
                break;
            }
            console.log(error);
          });
      } else {
        axios
          .put(
            `https://cukcuk.manhnv.net/api/v1/Employees/${me.employeeSelectedId}`,
            me.employee
          )
          .then(function () {
            //  nếu cập nhật validate hoặc lỗi từ back-end thì hiển thị thông báo tương ứng:
            alert("sua thanh cong");
            me.$emit("abc", false);
          })
          .catch(function () {
            // Nếu có lỗi thì validate hoặc lỗi từ back-end thì hiển thị thông báo tương ứng
            alert("lỗi");
            me.$emit("abc", false);
          });
      }
    },
    validateData() {
      var isValid = true;
      if (!this.employee.EmployeeCode) {
        isValid = false;
        //thêm border màu đỏ cho input hiện tại
        this.$refs["txtEmployeeCode"].classList.add("m-input-error");
        // thêm title thông báo lỗi cho input hiện tại
        this.$refs["txtEmployeeCode"].setAttribute(
          "title",
          "Thông tin này không được phép để trống"
        );
      }
      if (!this.employee.FullName) {
        isValid = false;
        //thêm border màu đỏ cho input hiện tại
        this.$refs["txtFullName"].classList.add("m-input-error");
        // thêm title thông báo lỗi cho input hiện tại
        this.$refs["txtFullName"].setAttribute(
          "title",
          "Thông tin này không được phép để trống"
        );
      }
      // if (!this.employee.DepartmentId) {
      //   isValid = false;
      //   //thêm border màu đỏ cho input hiện tại
      //   this.$refs["cbxDeparment"].classList.add("m-input-error");
      //   // thêm title thông báo lỗi cho input hiện tại
      //   this.$refs["cbxDeparment"].setAttribute(
      //     "title",
      //     "Thông tin này không được phép để trống"
      //   );
      // }
      return isValid;
    },
  },
  props: [
    "isShow",
    "employeeSelected",
    "employeeSelectedId",
    "employeeName",
    "formMode",
  ],
  // beforeUpdate(){
  //   if (!this.employee.EmployeeCode) {
  //     //thêm border màu đỏ cho input hiện tại
  //       event.currentTarget.classList.add("m-input-error");
  //       // thêm title thông báo lỗi cho input hiện tại
  //       event.currentTarget.setAttribute('title','Thông tin anyf không được phép để trống')
  //   }else{
  //     //thêm border màu đỏ cho input hiện tại
  //       event.currentTarget.classList.remove("m-input-error");
  //       // thêm title thông báo lỗi cho input hiện tại
  //       event.currentTarget.setAttribute('title','')
  //   }
  //   console.log(111);
  // },
  watch: {
    employeeSelected: function (value) {
      // this.employee = value;
      // alert(value);
      console.log(value);
    },
    employeeSelectedId: function (value) {
      var me = this;
      if (value) {
        //goi Api thực hiện lấy dữ liệu
        // hiện thị loading
        axios
          .get(`https://cukcuk.manhnv.net/api/v1/Employees/${value}`)
          .then(function (response) {
            switch (response.status) {
              case 200:
                me.employee = response.data;
                break;
              case 201:
                break;
              default:
                break;
            }

            console.log(response);
            //ẩn loading
          })
          .catch(function (response) {
            switch (response.status) {
              case 401:
                //đứa ra cảnh báo lỗi ...
                break;
              case 404:
                break;
              default:
                break;
            }
          });
      } else {
        //reset lại thông tin nhân viên
        //Gọi Api lấy dữ liệu nhân viên mới:
        me.employee = {};
        axios
          .get("https://cukcuk.manhnv.net/api/v1/Employees/NewEmployeeCode")
          .then(function (res) {
            me.employee.EmployeeCode = res.data;
          })
          .catch(function (res) {
            console.log(res);
          });
      }
    },
  },

  data() {
    return {
      employee: {},
      fullName: null,
      // isShow: true
    };
  },
};
</script>
<style scoped>
@import "../../style/components/dialog.css";
</style>
