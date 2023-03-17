<template>
  <div style="position: relative;">
    <div class="snow" v-for="(snow, i) in snows" :key="i" :style="{top: snow.top, left: snow.left, animation: snow.dur}">
        <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-snowflake" width="10" height="10" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
            <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
            <path d="M10 4l2 1l2 -1m-2 -2v6.5l3 1.72" />
            <path d="M10 4l2 1l2 -1m-2 -2v6.5l3 1.72" transform="rotate(60 12 12)" />
            <path d="M10 4l2 1l2 -1m-2 -2v6.5l3 1.72" transform="rotate(120 12 12)" />
            <path d="M10 4l2 1l2 -1m-2 -2v6.5l3 1.72" transform="rotate(180 12 12)" />
            <path d="M10 4l2 1l2 -1m-2 -2v6.5l3 1.72" transform="rotate(240 12 12)" />
            <path d="M10 4l2 1l2 -1m-2 -2v6.5l3 1.72" transform="rotate(300 12 12)" />
        </svg>
    </div>
  </div>
</template>

<script>
export default {
    data(){
        return{
            snows: [
                {top: '-37px', left: "97px", dur:"snow 6s linear infinite"},
                {top: '-28px', left: "95%", dur:"snow 6s linear infinite"},
                {top: '-27px', left: "90%", dur:"snow 6s linear infinite"},
                {top: '-27px', left: "85%", dur:"snow 7.1s linear infinite"},
                {top: '-43px', left: "79%", dur:"snow 6s linear infinite"},
                {top: '-15px', left: "83%", dur:"snow 5s linear infinite"},
                {top: '-40px', left: "75%", dur:"snow 5s linear infinite"},
                {top: '-20px', left: "94%", dur:"snow 7.6s linear infinite"},
                {top: '-13px', left: "77%", dur:"snow 5s linear infinite"},
                {top: '-38px', left: "68%", dur:"snow 6s linear infinite"},
                {top: '-10px', left: "63%", dur:"snow 5s linear infinite"},
                {top: '-31px', left: "54%", dur:"snow 6.6s linear infinite"},
                {top: '-35px', left: "50%", dur:"snow 6s linear infinite"},
                {top: '-10.0px', left: "47%", dur:"snow 5s linear infinite"},
                {top: '-39px', left: "43%", dur:"snow 5s linear infinite"},
                {top: '-6.5px', left: "39%", dur:"snow 5s linear infinite"},
                {top: '-27px', left: "37%", dur:"snow 5s linear infinite"},
                {top: '-10px', left: "33%", dur:"snow 5.7s linear infinite"},
                {top: '-7px', left: "28%", dur:"snow 5.8s linear infinite"},
                {top: '-43px', left: "25%", dur:"snow 6s linear infinite"},
                {top: '-0px', left: "24%", dur:"snow 6s linear infinite"},
                {top: '-12px', left: "18%", dur:"snow 6s linear infinite"},
                {top: '-33px', left: "14%", dur:"snow 6s linear infinite"},
                {top: '-15px', left: "10%", dur:"snow 6s linear infinite"},
                {top: '-36px', left: "8%", dur:"snow 5s linear infinite"},
                {top: '-9px', left: "6%", dur:"snow 6.6s linear infinite"},
                {top: '-29px', left: "3%", dur:"snow 6.6s linear infinite"},
                {top: '-17px', left: "1%", dur:"snow 6.6s linear infinite"},
            ]
        }
    }
}
</script>

<style>
.snow{
  position: absolute;
  z-index: 1;
  animation-direction: reverse;
}
@keyframes snow {
  to {
    transform: translateY(10vh);
  }
}


</style>