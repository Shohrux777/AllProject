<template>
  <div class="order_info py-2">
    <div class="container-fluid">
      <div class="row">
        <div class="col-8">
          <div class="main_order_info card">
            <div class="client_info">
              <div class="user_info_selected px-3">
                <h5 class="font-weight-bold" style="font-size: 14.5px;">{{order_data.client_name}}</h5>
                <p class="m-0 "><span class=" pr-1">ID:</span>  {{order_data.tegirmonOrderClientid}}</p>
                <!-- <p class="m-0"><span class=" pr-1">Номер паспорта:</span>  {{client_info.passport_number}}</p>
                <p class="m-0 "><span class=" pr-1">Дата рождения:</span>  {{born_date}}</p> -->
                <p class="m-0 "><span class=" pr-1">Тел:</span>  99 777 22 47</p>
              </div>
            </div>
            
          </div>
        </div>
        <div class="col-4">
          <div class="order_payment card">
            wsd
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data(){
    return{
      order_data: {},
    }
  },
  methods:{
    async fetchMounted(order){
      this.order_data = order;
    }
  }
}
</script>

<style lang="scss" scoped>
.user_photo{
  background-color: #fff;
  border-radius: 50%;
  width: 45px;
  height: 45px;
  overflow: hidden;
}
.user_info_selected p{
  color: #595961;
  font-size: 14px;
}
</style>