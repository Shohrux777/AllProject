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
        <div 
            class="access_item d-flex justify-content-between py-2 border-bottom">
            <span>Асосий касса</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.main_kassa" type="checkbox" class="custom-control-input " id="number12" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number12"></label>
            </div>
        </div>
        <div 
            class="access_item d-flex justify-content-between py-2 border-bottom">
            <span>Сотув касса</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.sotuv_kassa" type="checkbox" class="custom-control-input " id="number13" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number13"></label>
            </div>
        </div>
        <div 
            class="access_item d-flex justify-content-between py-2 border-bottom">
            <span>Кассага ходимларни ўлаш</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.kassa_setting" type="checkbox" class="custom-control-input " id="number14" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number14"></label>
            </div>
        </div>
        <div 
            class="access_item d-flex justify-content-between py-2 border-bottom">
            <span>Счета</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.hisoblar" type="checkbox" class="custom-control-input " id="number15" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number15"></label>
            </div>
        </div>
        <div 
            class="access_item d-flex justify-content-between py-2 border-bottom">
            <span>Склад</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.sklad" type="checkbox" class="custom-control-input " id="number17" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number17"></label>
            </div>
        </div>
        <div 
            class="access_item d-flex justify-content-between py-2 border-bottom">
            <span>Список должников</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.debt" type="checkbox" class="custom-control-input " id="number18" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number18"></label>
            </div>
        </div>
        <div 
            class="access_item d-flex justify-content-between py-2 border-bottom">
            <span>Оптом савдо</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.optom" type="checkbox" class="custom-control-input " id="number20" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number20"></label>
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
                main_kassa: false,
                sotuv_kassa: false,
                kassa_setting: false,
                hisoblar: false,
                debt: false,
                optom: false,
                sklad: false,
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
                this.access_item.main_kassa = data.status_2;
                this.access_item.sotuv_kassa = data.status_3;
                this.access_item.kassa_setting = data.status_4;
                this.access_item.hisoblar = data.status_5;
                if(data.num_1 > 0){
                    this.access_item.debt = true;
                }
                if(data.num_2 > 0){
                    this.access_item.sklad = true;
                }
                if(data.num_3 > 0){
                    this.access_item.optom = true;
                }
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
            let num_1 = 0;
            let num_2 = 0;
            let num_3 = 0;
            if(this.access_item.debt == true){
                num_1 = 1;
            }
            if(this.access_item.sklad == true){
                num_2 = 1;
            }
            if(this.access_item.optom == true){
                num_3 = 1;
            }
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
                    "status_1": this.access_item.used_product_report, // xodimlarga prixod rasxod otchoti
                    "status_2": this.access_item.main_kassa,
                    "status_3": this.access_item.sotuv_kassa,
                    "status_4": this.access_item.kassa_setting,
                    "status_5": this.access_item.hisoblar,
                    "num_1": num_1,
                    "num_2": num_2,
                    "num_3": num_3,
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