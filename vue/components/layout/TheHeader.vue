<template>
    <div class="header">
        <div class="header-left">
            <div class="sibar"></div>
            <div class="logo"></div>
        </div>
        <div class="header-right"></div>
    </div>
</template>
<script>
export default {
    
}
</script>
<style scoped>
.header{
    display: flex;
    height: 48px;
    width: 100%;
    
}

.header .header-left{
    display: flex;
    width: 226px;
    height: 100%;
    flex-shrink: 0;
    align-items: center;
    padding-left: 16px;
    box-sizing: border-box;
    border-right: 1px solid #bbb;
}

.header .header-left .sibar{
    width: 24px;
    height: 24px;
    flex-shrink: 0;
    margin-right: 10px;
    background-image: url(../../assets/img/toggle.png);
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
}

.header .header-left .logo{
    width: 100%;
    height: 32px;
    background-image: url(../../assets/cukcuk-logo.png);
    background-position: 0 center;
    background-repeat: no-repeat;
    background-size: contain;
    cursor: pointer;
}

.header .header-right{
    width: 100%;
    height: 100%;
    z-index: 999;
    border-bottom: 1px solid #bbb;
    box-sizing: border-box;
}
</style>