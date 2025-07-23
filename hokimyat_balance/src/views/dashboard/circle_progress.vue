<template>
  <div class="card w-100">
    <div class="d-flex justify-content-between">
        <div class="">
            <div class="">
                <div class="icon_bg_style" :class="progress_bg">
                    <svg viewBox="0 0 24 24" width="27" height="27">
                    <path
                        fill="#fff"
                        d="M12 12c2.67 0 8 1.34 8 4v2H4v-2c0-2.66 5.33-4 8-4zm0-2a4 4 0 1 0 0-8 4 4 0 0 0 0 8z"
                    />
                    </svg>
                </div>
                
            </div>
            <div class="label_circle m-0 p-0">
                <p class="m-0 p-0">{{label}}</p>
            </div>
            <div class="label_circle1 m-0 p-0 mt-2" style="text-align: left;">
                <p class="m-0 p-0" > <span>Debit:</span> <span style="font-weight: normal;" class="mr-2">{{qty}}</span> </p>
                <p class="m-0 p-0" > <span>Balance:</span> <span style="font-weight: normal;" class="mr-2">{{activ_qty}}</span> </p>

            </div>
        </div>
        <div class="circle">
            <svg viewBox="0 0 36 36" class="circular-chart">
            <path
                class="circle-bg"
                d="M18 2.0845
                a 15.9155 15.9155 0 0 1 0 31.831
                a 15.9155 15.9155 0 0 1 0 -31.831"
            />
            <path
                class="circle-progress " :class="progress_bg"
                :stroke-dasharray="dashArray"
                d="M18 2.0845
                a 15.9155 15.9155 0 0 1 0 31.831
                a 15.9155 15.9155 0 0 1 0 -31.831"
            />
            <text x="18" y="20.35" class="percentage-text">{{ activ_qty }}/{{ qty }}</text>
            </svg>
        </div>
    </div>
    
    </div>
</template>

<script>
export default {
    data(){
        return{
            status:false,
        }
    },
    props: {
      label:{
        type:String,
        default: ''
      },
      qty: {
        type:Number,
        default: 0
      },
      activ_qty: {
        type:Number,
        default: 0
      },
      progress_bg: {
        type:String,
        default: ''
      }

    },
    computed:  {
      percentage() {
        if (this.qty === 0) return 0;
        return Math.min(100, (this.activ_qty / this.qty) * 100);
      },
      dashArray() {
        // avoid 0-length bug
        let pct = this.percentage;
        if (pct < 1) pct = 0.5;
        return `${pct}, 100`;
      },  
    },
}
</script>

<style scoped>
.card {
  width: 100%;
  padding: 10px;
  border-radius: 12px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.1);
  background: white;
  text-align: center;
  font-family: sans-serif;
}

.icon {
  margin-bottom: 5px;
}

.label_circle {
  /* font-weight: bold; */
  font-size: 17px;
  margin-bottom: 8px;
}
.label_circle1{
  font-size: 15px;
  margin-bottom: 8px;
}
.label_circle p{
    color: #6e6e6e;
    /* font-weight: bold; */
    /* font-style: italic; */
}
.circle {
  width: 130px;
  height: 130px;
  margin: 0 10px;
}

.circular-chart {
  display: block;
  max-width: 100%;
  max-height: 100%;
}

.circle-bg {
  fill: none;
  stroke:  #ffe5b4;
  stroke-width: 2.5;
}

.circle-progress {
  fill: none;
  stroke-width: 2.5;
  stroke-linecap: round;
  transform: rotate(-90deg);
  transform-origin: center;
  transition: stroke-dasharray 0.5s ease;
}
.progress_bg_success{
    stroke: #28d886;
    background: #28d886;
}
.progress_bg_danger{
    stroke: #f84e4e;
    background: #fd6767;

}
.progress_bg_warning{
    stroke: #eeba2c;
    background: #eeba2c;

}
.progress_bg_info{
    stroke: #2ca0ee;
}
.percentage-text {
  fill: #333;
  font-size: 3.3px;
  text-anchor: middle;
}
.icon_bg_style{
    width: 40px;
    height: 40px;
    display: flex;
    justify-content: center;
    align-items: center;
    border-radius: 7px;
    margin-bottom: 10px;
}
</style>