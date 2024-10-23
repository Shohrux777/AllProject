<template>
  <div class="lab_result_show_picture_box">
    <div class="lab_result_show_picture rounded">
        <div class="print_update">
            <div class="print_button">
                <mdb-btn color="primary" class="m-0 p-0"   p="r4 l4 t2 b2" @click="print_lab" style="font-size:9px;">{{$t('print')}}</mdb-btn>  
                <mdb-btn color="info" class="m-0 p-0 ml-2"   p="r4 l4 t2 b2" @click="update_lab" style="font-size:9px;">{{$t('Edit')}}</mdb-btn>  
            </div>
            <svg @click="close" xmlns="http://www.w3.org/2000/svg" style="border-radius: 50%; cursor:pointer;" class="icon icon-tabler icon-tabler-x bg-danger p-1" width="21" height="21" viewBox="0 0 24 24" stroke-width="2.7" stroke="#fff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                <line x1="18" y1="6" x2="6" y2="18" />
                <line x1="6" y1="6" x2="18" y2="18" />
            </svg>
        </div>
        <div class="show_picture" id="print_lab_picture">
            <img width="100%" :src="hostname1 + picture" alt="">
        </div>
    </div>
  </div>
</template>

<script>
import {mdbBtn} from "mdbvue";
export default {
    components: {
      mdbBtn,
    },
    data() {
      return {
        hostname1: this.$store.state.hostname1,
      }
    },
    props:{
        picture:{
            type: String,
            default: ''
        }
    },
    methods:{
        async print_lab(){
            const printableContent = document.getElementById('print_lab_picture');
            console.log(printableContent);
            const printWindow = window.open('/', '_blank', 'left=0,top=0,width=750,height=650,toolbar=0,scrollbars=0,status=0');
            printWindow.document.write(printableContent.innerHTML);
            // printWindow.document.close();
            printWindow.focus();
            printWindow.print();
            printWindow.close();
        },
        close(){
            this.$emit('close')
        },
        update_lab(){
            this.$emit('update')
        }
    }
}
</script>

<style>
body{
    margin: 0 !important;
    padding: 0 !important;
}
.lab_result_show_picture_box{
    position: fixed;
    top:0px;
    left: 0px;
    bottom: 0px;
    right: 0px;
    z-index: 111;
    display: flex;
    justify-content: center;
    align-items: center;
    background: rgba(44, 44, 44, 0.7);
}
.lab_result_show_picture_box .lab_result_show_picture{ 
    background: white;
    width: 95%;
    height: 95vh;
    overflow: hidden;
    overflow-y: scroll;
}
.lab_result_show_picture_box .lab_result_show_picture .print_update{
    border-bottom: 1px solid rgb(126, 147, 255);
    width: 100%;
    height: 35px;
    display: flex;
    align-items: center;
    justify-content: space-between;
    padding: 0px 5px;
    z-index: 11;
    position: sticky;
    top: 0;
    background: #fff;
}
.lab_result_show_picture_box .lab_result_show_picture .show_picture{
    margin: 0;
    padding: 0;
    
}
.lab_result_show_picture_box .lab_result_show_picture .show_picture img{
    margin: 0;
    padding: 0;
}
</style>