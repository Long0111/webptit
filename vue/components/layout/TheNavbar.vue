<template>
    <div class="navbar">
            <a class="menu-item menu-item-focus">
                <div class="menu-icon m-icon m-icon-dashboard"></div>
                <div class="menu-text">Tổng quan</div>
            </a>
            <router-link class="menu-item" to="/employee">
                <div class="menu-icon m-icon m-icon-employee"></div>
                <div class="menu-text">Danh sách nhân viên</div>
            </router-link>
            <router-link class="menu-item" to="/repost">
                <div class="menu-icon m-icon m-icon-document"></div>
                <div class="menu-text">Báo cáo</div>
            </router-link>
            <a class="menu-item">
                <div class="menu-icon m-icon m-icon-setting"></div>
                <div class="menu-text">Cài đặt</div>
            </a>
        </div>
</template>
<script>
export default {
    
}
</script>
<style scoped>
@import url("../../style/layout/navbar.css");
</style>