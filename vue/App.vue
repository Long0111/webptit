<template>
  <div>
    <TheHeader/>
    <div class="main">
      <TheNavbar/>
      <TheContent/>
    </div>
    
  </div>
</template>

<script>
// 1. import components 
import TheHeader from './components/layout/TheHeader.vue'
import TheNavbar from './components/layout/TheNavbar.vue'
import TheContent from './components/layout/TheContent.vue'
export default {
  name: 'App',
  components: {
    //2 : regiter components:
    TheHeader,TheNavbar,TheContent
  }
}
</script>

<style>

@import url("./style/main.css");
body{
    font-family: GoogleSands;
    font-size: 13px;
}
</style>
