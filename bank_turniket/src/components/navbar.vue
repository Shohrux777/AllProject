<template>
  <div class="navbar_Nav_Edit d-flex justify-content-between"  container>
            <div class="d-flex align-items-center">
                <span to="#"> <span class="text-white">{{title}}</span> </span>
            </div>
            <div >
                <div class="d-flex  align-items-center" v-if="added_status">
                    <div class="px-2 btn_hover" @click="$emit('add')"> 
                        <small class="text-white" style="font-size: 12px;">
                            <MDBIcon icon="plus" />
                            Add
                        </small> 
                    </div>
                    <div class="px-2 btn_hover" > 
                        <small class="text-white" style="font-size: 12px;">
                            <MDBIcon icon="edit" />
                            Edit
                        </small> 
                    </div>
                    <div class="px-2 btn_hover"> 
                        <small class="text-white" style="font-size: 12px;">
                            <MDBIcon  icon="trash" />
                            Delete
                        </small> 
                    </div>
                </div>
                <div v-else class="excel d-flex  align-items-center">
                    <div class="px-2 btn_hover"> 
                        <small class="text-white" style="font-size: 12px;">
                            <MDBIcon  icon="file-excel" />
                            EXCEL
                        </small> 
                    </div>
                    <div class="px-2 btn_hover"> 
                        <small class="text-white" style="font-size: 12px;">
                            <MDBIcon  icon="file-pdf"  far/>
                            PDF
                        </small> 
                    </div>
                </div>
            </div>
            
        </div>
</template>

<script>
  import {
    MDBIcon,
  } from 'mdb-vue-ui-kit';

  export default {
    components: {
        MDBIcon,
    },
    props:{
        title: String,
        added_status:{
        type:Boolean,
        default:true
      },
    }
  };
</script>

<style>
.navbar_Nav_Edit{
    background: #475a65;
    padding: 7px 12px;
}
.btn_hover{
    cursor:pointer;
}
.btn_hover small{
    color:#d3d3d3 !important;
}
.btn_hover:hover{
    background: #3a464c;
    border-radius: 2px;
}
.btn_hover:hover small{
    color:#fff !important;
}
</style>