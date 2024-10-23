<template>
  <div class="user_access">
    <loader v-if="loading"/>
    <div v-else class="access">
        <div class="d-flex justify-content-end mb-2 ">
            <small class="border-bottom px-2 font-weight-bold" style="font-size: 12px;">{{user_name}}</small>
        </div>
        <div 
            class="access_item d-flex justify-content-between py-2 border-bottom">
            <span>Продукт</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.product" type="checkbox" class="custom-control-input " id="number1" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number1"></label>
            </div>
        </div>
        <div 
            class="access_item d-flex justify-content-between py-2 border-bottom">
            <span>Изменить продукт</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.product_edit" type="checkbox" class="custom-control-input " id="number2" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number2"></label>
            </div>
        </div>
        <div 
            class="access_item d-flex justify-content-between py-2 border-bottom">
            <span>Приходь</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.buy" type="checkbox" class="custom-control-input " id="number3" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number3"></label>
            </div>
        </div>
        <div 
            class="access_item d-flex justify-content-between py-2 border-bottom">
            <span>Получить продукт</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.product_olish" type="checkbox" class="custom-control-input " id="number4" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number4"></label>
            </div>
        </div>
        <div 
            class="access_item d-flex justify-content-between py-2 border-bottom">
            <span>Захира</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.zaxira" type="checkbox" class="custom-control-input " id="number5" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number5"></label>
            </div>
        </div>
        <div 
            class="access_item d-flex justify-content-between py-2 border-bottom">
            <span>Тароз</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.taroz" type="checkbox" class="custom-control-input " id="number6" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number6"></label>
            </div>
        </div>
        <div 
            class="access_item d-flex justify-content-between py-2 border-bottom">
            <span>Тароз список</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.taroz_list" type="checkbox" class="custom-control-input " id="number7" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number7"></label>
            </div>
        </div>
        <div 
            class="access_item d-flex justify-content-between py-2 border-bottom">
            <span>Катта тарози</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.big_tarozi" type="checkbox" class="custom-control-input " id="number8" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number8"></label>
            </div>
        </div>
        <div 
            class="access_item d-flex justify-content-between py-2 border-bottom">
            <span>Продажа</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.sell" type="checkbox" class="custom-control-input " id="number9" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number9"></label>
            </div>
        </div>
        <div 
            class="access_item d-flex justify-content-between py-2 border-bottom">
            <span>Остатка</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.ostatka" type="checkbox" class="custom-control-input " id="number10" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number10"></label>
            </div>
        </div>
        <div 
            class="access_item d-flex justify-content-between py-2 border-bottom">
            <span>Использованные продукты</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.used_product_report" type="checkbox" class="custom-control-input " id="number11" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number11"></label>
            </div>
        </div>
        <div class="d-flex justify-content-end mt-4">
            <mdb-btn color="success" style="font-size: 10.5px"
            @click="saveAccess"
            p="r4 l4 t2 b2">
            {{$t('save')}}</mdb-btn>
        </div>
    </div>
    <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_add')" @to_hide_modal="modal_status= false"/>

      <Toast ref="message"></Toast>
  </div>
</template>

<script>
import { mdbBtn, } from "mdbvue"
export default {
    data() {
        return {
            modal_info: '',
            modal_status: false,
            loading: false,
            access_item : {
                product: false,
                product_edit: false,
                buy: false,
                product_olish: false,
                zaxira: false,
                taroz: false,
                taroz_list: false,
                big_tarozi: false,
                sell: false,
                ostatka: false,
                used_product_report: false,
            },
            id: 0,
        }
    },
    components: {
        mdbBtn
    },
    async created()
    {
      if(this.user_id > 0)
      {
        try{
            const res = await fetch(this.$store.state.hostname + '/TegirmonUserAccess/getTegirmonUserAccessUserId?user_id=' + this.user_id);
            const data = await res.json();
            console.log('this is by id')
            if(res.status == 200 || res.status == 201){
                console.log(data)
                this.access_item.product = data.product;
                this.access_item.product_edit = data.product_edit;
                this.access_item.buy = data.buy;
                this.access_item.product_olish = data.product_olish;
                this.access_item.zaxira = data.zaxira;
                this.access_item.taroz = data.taroz;
                this.access_item.taroz_list = data.taroz_list;
                this.access_item.big_tarozi = data.big_tarozi;
                this.access_item.sell = data.sell;
                this.access_item.ostatka = data.ostatka;
                this.access_item.used_product_report = data.status_1;

                this.id = data.id;
            }
        }
        catch(error){
            console.log(error)
        }
      }

    },
    props: {
        user_id: {
            type: Number,
            default: 0 
        },
        user_name: {
            type: String,
            default: ""
        },
    },
    methods: {
        async saveAccess() {
            const requestOptions = {
                method : "POST",
                headers: { "Content-Type" : "application/json" },
                body: JSON.stringify({
                    "product" : this.access_item.product,
                    "product_edit": this.access_item.product_edit,
                    "buy": this.access_item.buy,
                    "product_olish": this.access_item.product_olish,
                    "zaxira": this.access_item.zaxira,
                    "taroz": this.access_item.taroz,
                    "taroz_list": this.access_item.taroz_list,
                    "big_tarozi": this.access_item.big_tarozi,
                    "sell": this.access_item.sell,
                    "ostatka": this.access_item.ostatka,
                    "status_1": this.access_item.used_product_report,
                    "tegirmonUserid": this.user_id,
                    "id" : this.id,
                })
            };
            try{
            this.loading = true;
            const response = await fetch(this.$store.state.hostname + "/TegirmonUserAccess", requestOptions);
            const data = await response.text();
            this.loading = false;
            if(response.status == 201 || response.status == 200)
            {
                this.$refs.message.success('Added_successfully')
                this.$emit("close")
                return true;
            }
            else{
                this.modal_info = data;
                this.modal_status = true;
                return false;
            }
            }
            catch{
                this.loading = false;
                this.modal_info = this.$i18n.t('network_ne_connect'); 
                this.modal_status = true;
            }
        }
    },
}
</script>

<style>

</style>