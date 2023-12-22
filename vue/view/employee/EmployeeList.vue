<template>
  <div class="page-content m-page-content">
    <div class="m-page-header">
      <div class="m-page-title">Danh sách nhân viên</div>
      <div class="m-page-button">
        <button
          id="btnAdd"
          @click="btnAddOnClick"
          class="m-btn m-btn-icon m-btn-icon-add"
        >
          Thêm mới nhân viên
        </button>
      </div>
    </div>
    <div class="m-page-toolbar">
      <div class="m-toolbar-left">
        <input
          type="text"
          style="width: 200px"
          class="m-input"
          placeholder="Tìm kiếm theo tên, số hồ sơ"
        />
        <select name="" id="">
          <option value="">Tất cả phòng ban</option>
          <option value="">Phòng đào tạo</option>
        </select>
        <select name="" id="">
          <option value="">Tất cả vị trí</option>
          <option value="">Giám đốc</option>
          <option value="">Nhân viên</option>
        </select>
      </div>
      <div class="m-toolbar-right">
        <button id="btnRefresh" class="m-btn-refresh"></button>
      </div>
    </div>
    <div class="m-page-grid">
      <div class="m-grid">
        <table id="tblEmployee" class="m-table">
          <thead>
            <tr>
              <th class="text-align-center" style="width: 20px">#</th>
              <th class="text-align-left" style="min-width: 70px">
                Mã nhân viên
              </th>
              <th class="text-align-left" style="min-width: 170px">
                Họ và tên
              </th>
              <th class="text-align-left" style="width: 70px">Giới tính</th>
              <th class="text-align-center" style="width: 100px">Ngày sinh</th>
              <th class="text-align-center" style="min-width: 130px">
                Số CMTND/ Căn cước
              </th>
              <th class="text-align-center" style="width: 100px">Ngày cấp</th>
              <th class="text-align-left" style="min-width: 70px">Địa chỉ</th>
              <th class="text-align-right" style="width: 100px">Điện thoại</th>
              <th class="text-align-left" style="width: 100px">Email</th>
              <th class="text-align-left" style="min-width: 150px">Chức vụ</th>
              <th class="text-align-left" style="min-width: 150px">
                Phòng ban
              </th>
              <th class="text-align-right" style="width: 150px">
                Mức lương cơ bản
              </th>
              <th class="text-align-left" style="min-width: 100px">
                Tình trạng công việc
              </th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="employee in employees"
              :key="employee.EmployeeId"
              class="row-selected"
              @dblclick="rowOnDblClick(employee)"
            >
              <td class="text-align-center">
                <input type="checkbox" checked="" />
              </td>
              <td class="text-align-left">{{ employee.EmployeeCode }}</td>
              <td class="text-align-left">{{ employee.FullName }}</td>
              <td class="text-align-left">{{ employee.GenderName }}</td>
              <!-- <td class="text-align-center">{{formatDate(employee.DateOfBirth)}}</td> -->
              <td class="text-align-center">
                {{ formatDate(employee.DateOfBirth) }}
              </td>

              <td class="text-align-right">{{ employee.IdentityNumber }}</td>
              <td class="text-align-right">{{ employee.IdentityDate }}</td>
              <td class="text-align-right">{{ employee.Address }}</td>
              <td class="text-align-right">{{ employee.PhoneNumber }}</td>
              <td class="text-align-right">{{ employee.Email }}</td>
              <td class="text-align-right">{{ employee.PositionName }}</td>
              <td class="text-align-left">{{ employee.DepartmentName }}</td>
              <td class="text-align-right">{{ employee.Salary }}</td>
              <td class="text-align-right">{{ employee.WorkStatus }}</td>


            </tr>
          </tbody>
        </table>
      </div>
      <div class="m-row">
        <div class="m-paging">
          <div class="m-paging-left">Hiển thị 1-100 nhân viên</div>
          <div class="m-paging-center">
            <button class="m-btn-first"></button>
            <button class="m-btn-prev"></button>
            <div class="m-page-number-group">
              <button class="m-page-number">1</button>
              <button class="m-page-number">2</button>
              <button class="m-page-number">3</button>
              <button class="m-page-number">4</button>
            </div>
            <button class="m-btn-next"></button>
            <button class="m-btn-last"></button>
          </div>
          <div class="m-paging-right">
            <select name="" id="">
              <option value="10">10 nhân viên/trang</option>
              <option value="20">20 nhân viên/trang</option>
              <option value="50">50 nhân viên/trang</option>
            </select>
          </div>
        </div>
      </div>
    </div>
  </div>
  <EmployeeDetail
    :isShow="isShowDialog"
    :employeeSelected="employeeSelected"
    :employeeSelectedId="employeeSelectedId"
    :employeeName="fullName"
    :formMode="formDetailMode"
    @abc="showEmployeeDetaiDialog"
  />
</template>
<script>
import axios from "axios";
import EmployeeDetail from "./EmployeeDetail.vue";
export default {
  name: "EmployeeList",
  components: {
    EmployeeDetail,
  },
  /**
   * GD 1: beforeCreated (setup)
   */
  beforeCreate() {
    console.log("1. beforeCreated");
    console.log("không thể truy cập vào data");
    // let fullName = this.fullName;
    // console.log(fullName);
    // let name = this.getName();
    // console.log(name);
  },
  /**
   * GD 2: created (setup)
   */
  created() {
    try {
      var me = this;
      console.log("2. created");
      console.log("Có thể truy cập vào data và even, methods");
      console.log("Chưa thể truy cập được vào DOM");
      let fullName = this.fullName;
      console.log(fullName);
      let name = this.getName();
      console.log(name);
      axios
        .get("https://cukcuk.manhnv.net/api/v1/Employees")
        .then((response) => {
          console.log(response.data);
          me.employees = response.data;
        });
      // Gọi api thực hiện lấy dữ liệu --> sử dụng axios:
    } catch (error) {
      console.log(error);
    }
  },
  /**
   * GD 3: beforeMount (setup)
   */
  beforeMount() {
    console.log("3. beforeMount");
    console.log("Chưa thể truy cập được vào DOM");
    let fullName = this.fullName;
    console.log(fullName);
    let name = this.getName();
    console.log(name);
  },
  /**
   * GD 4: mounted (setup)
   */
  mounted() {
    console.log("4. mounted");
    console.log("Có thể truy cập được vào DOM");
    let fullName = this.fullName;
    console.log(fullName);
    let name = this.getName();
    console.log(name);
  },
  /**
   * GD 5: beforeUpdate (setup)
   */
  beforeUpdate() {
    console.log("5. beforeUpdate");
  },
  /**
   * GD 6: updated (setup)
   */
  updated() {
    console.log("6. updated");
  },
  /**
   * GD 7: beforeUnmount (setup)
   */
  beforeUnmount() {
    console.log("7. beforeUnmount");
  },
  /**
   * GD 8: unmounted (setup)
   */
  unmounted() {
    console.log("8. unmounted");
  },
  methods: {
    /**
     * Thêm mới nhân viên
     * @param {Boolean} isShow - true: hiển thị, false -ẩn
     * Arthor: NTLONG (8/8/2023)
     */
    btnAddOnClick() {
      // document.getElementById("dlgDialog").style.display = "block";
      // this.isShowDialog = true;
      this.employeeSelected = {};
      this.formDetailMode = 1;
      this.employeeSelectedId = null;
      this.showEmployeeDetaiDialog(true);
      console.log(this.isShowDialog);
    },
    /**
     * Hiển thị form thêm mới nhân viên
     * @param {Boolean} isShow - true: hiển thị, false -ẩn
     * Arthor: NTLONG (8/8/2023)
     */
    showEmployeeDetaiDialog(isShow) {
      this.isShowDialog = isShow;
    },
    /**
     * Hiển thị form thêm mới nhân viên khi đúp chuột
     * @param {Boolean} isShow - true: hiển thị, false -ẩn
     * Arthor: NTLONG (8/8/2023)
     */
    rowOnDblClick(employee) {
      console.log(employee);
      this.formDetailMode = 0;
      this.employeeSelected = employee;
      this.employeeSelectedId = employee.EmployeeId;
      this.fullName = employee.FullName;
      this.showEmployeeDetaiDialog(true);
    },
    getName() {
      return this.fullName;
    },
    formatDate(dob) {
      if (dob) {
        dob = new Date(dob);
        let date = dob.getDate();
        date = date < 10 ? `0${date}` : date;
        let month = dob.getMonth() + 1;
        month = month < 10 ? `0${month}` : month;
        let year = dob.getFullYear();

        dob = `${date}/${month}/${year}`;
      } else {
        dob = "";
      }
      return dob;
    },
  },
  watch: {
    employeeSelectedId: function (value) {
      console.log("parent: " + value);
    },
  },
  data() {
    return {
      fullName: null,
      employees: null,
      employeeSelected: {},
      employeeSelectedId: null,
      isShowDialog: false,
      formDetailMode: 0,
    };
  },
  filters: {
    formatDateFilter(dob) {
      try {
        if (dob) {
          dob = new Date(dob);
          let date = dob.getDate();
          date = date < 10 ? `0${date}` : date;
          let month = dob.getMonth() + 1;
          month = month < 10 ? `0${month}` : month;
          let year = dob.getFullYear();

          dob = `${date}/${month}/${year}`;
        } else {
          dob = "";
        }
        return dob;
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>
<style scoped>
@import url("../../style/page/employee.css");
</style>
