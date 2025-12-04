<template>
  <div class="user_access">
    <loader v-if="loading" />
    <div v-else class="access card user-access-card">
      <div class="user-access-header d-flex justify-content-between align-items-center mb-2">
        <div>
          <small class="user-access-title">
            {{ user_name }}
          </small>
        </div>
        <span class="user-access-subtitle">
          {{$t('access_user')}}
        </span>
      </div>

      <div
        class="access_item d-flex justify-content-between py-2 border-bottom"
      >
            <span>Продукт</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.product" type="checkbox" class="custom-control-input " id="number1" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number1"></label>
            </div>
      </div>
      <div
        class="access_item d-flex justify-content-between py-2 border-bottom"
      >
            <span>Изменить продукт</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.product_edit" type="checkbox" class="custom-control-input " id="number2" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number2"></label>
            </div>
      </div>
      <div
        class="access_item d-flex justify-content-between py-2 border-bottom"
      >
            <span>Приходь</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.buy" type="checkbox" class="custom-control-input " id="number3" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number3"></label>
            </div>
      </div>
      <div
        class="access_item d-flex justify-content-between py-2 border-bottom"
      >
            <span>Получить продукт</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.product_olish" type="checkbox" class="custom-control-input " id="number4" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number4"></label>
            </div>
      </div>
      <div
        class="access_item d-flex justify-content-between py-2 border-bottom"
      >
            <span>Захира</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.zaxira" type="checkbox" class="custom-control-input " id="number5" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number5"></label>
            </div>
      </div>
      <div
        class="access_item d-flex justify-content-between py-2 border-bottom"
      >
            <span>Тароз</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.taroz" type="checkbox" class="custom-control-input " id="number6" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number6"></label>
            </div>
      </div>
      <div
        class="access_item d-flex justify-content-between py-2 border-bottom"
      >
            <span>Тароз список</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.taroz_list" type="checkbox" class="custom-control-input " id="number7" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number7"></label>
            </div>
      </div>
      <div
        class="access_item d-flex justify-content-between py-2 border-bottom"
      >
            <span>Катта тарози</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.big_tarozi" type="checkbox" class="custom-control-input " id="number8" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number8"></label>
            </div>
      </div>
      <div
        class="access_item d-flex justify-content-between py-2 border-bottom"
      >
            <span>Продажа</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.sell" type="checkbox" class="custom-control-input " id="number9" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number9"></label>
            </div>
      </div>
      <div
        class="access_item d-flex justify-content-between py-2 border-bottom"
      >
            <span>Остатка</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.ostatka" type="checkbox" class="custom-control-input " id="number10" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number10"></label>
            </div>
      </div>
      <div
        class="access_item d-flex justify-content-between py-2 border-bottom"
      >
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
      <div v-if="access_item.hisoblar">
        <div
          v-for="(item,i) in hisoblar_list"
          :key="i"
          class="access_item d-flex justify-content-between py-1 border-bottom"
        >
          <span class="ml-2 access-sub-item-name">{{item.name}}</span>
          <div class="custom-control custom-switch">
            <input v-model="item.status" type="checkbox" class="custom-control-input " :id="'number256' + item.id" checked>
            <label  style="cursor:pointer;" class="custom-control-label " :for="'number256' +item.id"></label>
          </div>
        </div>
      </div>
      <div
        class="access_item d-flex justify-content-between py-2 border-bottom"
      >
            <span>Склад</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.sklad" type="checkbox" class="custom-control-input " id="number17" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number17"></label>
            </div>
      </div>
      <div v-if="access_item.sklad">
        <div
          v-for="(item,i) in sklad_list"
          :key="i"
          class="access_item d-flex justify-content-between py-1 border-bottom"
        >
          <span class="ml-2 access-sub-item-name">{{item.name}}</span>
          <div class="custom-control custom-switch">
            <input v-model="item.status" type="checkbox" class="custom-control-input " :id="'number175' + item.id" checked>
            <label  style="cursor:pointer;" class="custom-control-label " :for="'number175' +item.id"></label>
          </div>
        </div>
      </div>

      <div
        class="access_item d-flex justify-content-between py-2 border-bottom"
      >
            <span>Список должников</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.debt" type="checkbox" class="custom-control-input " id="number18" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number18"></label>
            </div>
      </div>
      <div
        class="access_item d-flex justify-content-between py-2 border-bottom"
      >
            <span>Оптом савдо</span>
            <div class="custom-control custom-switch">
                <input v-model="access_item.optom" type="checkbox" class="custom-control-input " id="number20" checked>
                <label  style="cursor:pointer;" class="custom-control-label " for="number20"></label>
            </div>
      </div>
      <div v-if="access_item.optom">
        <div class="access_item d-flex justify-content-between py-1 border-bottom">
          <span class="ml-2 access-sub-item-name">📈 Статистика</span>
          <div class="custom-control custom-switch">
            <input v-model="access_item.optom_statis" type="checkbox" class="custom-control-input " :id="'number7892'" checked>
            <label  style="cursor:pointer;" class="custom-control-label " :for="'number7892'"></label>
          </div>
        </div>
        <div class="access_item d-flex justify-content-between py-1 border-bottom">
          <span class="ml-2 access-sub-item-name">🪙 Платежи</span>
          <div class="custom-control custom-switch">
            <input v-model="access_item.optom_pay" type="checkbox" class="custom-control-input " :id="'number7893'" checked>
            <label  style="cursor:pointer;" class="custom-control-label " :for="'number7893'"></label>
          </div>
        </div>
        <div class="access_item d-flex justify-content-between py-1 border-bottom">
          <span class="ml-2 access-sub-item-name">🚚 Статистика погрузки</span>
          <div class="custom-control custom-switch">
            <input v-model="access_item.optom_load" type="checkbox" class="custom-control-input " :id="'number7894'" checked>
            <label  style="cursor:pointer;" class="custom-control-label " :for="'number7894'"></label>
          </div>
        </div>
      </div>
      <div class="d-flex justify-content-end mt-3 pt-2">
        <mdb-btn
          class="user-access-save-btn"
          @click="saveAccess"
          p="r4 l4 t2 b2"
        >
          {{$t('save')}}
        </mdb-btn>
      </div>
    </div>
    <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_add')" @to_hide_modal="modal_status= false"/>

      <Toast ref="message"></Toast>
  </div>
</template>

<script>
import {mapActions,mapGetters} from 'vuex'
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
                optom_statis: false,
                optom_pay: false,
                optom_load: false,

            },
            id: 0,
            sklad_list: [],
            userSkladAccessList: [],

            hisoblar_list: [],
            userHisobAccessList: [],

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
                this.access_item.optom_statis = data.optom_statis;
                this.access_item.optom_pay = data.optom_pay;
                this.access_item.optom_load = data.optom_load;
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
                await this.fetchUserSkladAccess()
                await this.fetchUserHisobAccess()
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
  computed: mapGetters(['allSklad', 'allHisob']),
    methods: {
        ...mapActions(['fetchSklad', 'fetchHisob']),
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
                    "optom_statis": this.access_item.optom_statis,
                    "optom_pay": this.access_item.optom_pay,
                    "optom_load": this.access_item.optom_load,
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
                console.log(data)
                if(this.access_item.sklad == true){
                    await this.deleteTegirmonUserSkladAccessByUserId();
                    await this.addUserSkladAccess();
                }
                if(this.access_item.hisoblar == true){
                    await this.deleteTegirmonUserHisoblarAccessByUserId();
                    await this.addUserHisoblarAccess();
                }
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
        },
        async addUserSkladAccess(){
            for(let i=0; i<this.sklad_list.length; i++){
                if(this.sklad_list[i].status == true){
                    await this.add_fetch_user_access(this.sklad_list[i].id);
                }
            }
        },
        async add_fetch_user_access(sklad_id){
            const requestOptions = {
                method : "POST",
                headers: { "Content-Type" : "application/json" },
                body: JSON.stringify({
                    "tegirmonSkladId": sklad_id,
                    "tegirmonUserid": this.user_id,
                })
            };
            try{
                const response = await fetch(this.$store.state.hostname + "/TegirmonUserSkladAccess", requestOptions);
                console.log('response', response)
            }
            catch(error){
                console.log(error)
            }
        },
        async deleteTegirmonUserSkladAccessByUserId()
        {
            const requestOptions = {
                method : "delete",
            };
            try{
                const response = await fetch(this.$store.state.hostname + "/TegirmonUserSkladAccess/deleteTegirmonUserSkladAccessByUserId?user_id=" + this.user_id, requestOptions);
                console.log(response);
            }
            catch(error){
                console.log(error);
            }
            // const data = await response.text();
        },
        async fetchUserSkladAccess(){
            this.sklad_list = [];
            await this.fetchSklad();
            console.log(this.allSklad.rows);
            for(let i=0; i<this.allSklad.rows.length; i++){
                let temp = {
                    name: this.allSklad.rows[i].name,
                    id: this.allSklad.rows[i].id,
                    status: false,
                    user_id: this.user_id,
                }
                this.sklad_list.push(temp);
            }
            await this.fetchUserSkladAccessUserId();

            for(let j=0; j<this.userSkladAccessList.length; j++){
                for(let k=0; k<this.sklad_list.length; k++){
                    if(this.userSkladAccessList[j].tegirmonSkladId == this.sklad_list[k].id){
                        this.sklad_list[k].status = true;
                    }
                }
            }

        },
        async fetchUserSkladAccessUserId(){
            this.userSkladAccessList = [];
            try{
                const res = await fetch(this.$store.state.hostname + '/TegirmonUserSkladAccess/getTegirmonUserSkladAccessUserId?user_id=' + this.user_id);
                const data = await res.json();
                console.log('this is by id')
                if(res.status == 200 || res.status == 201){
                    this.userSkladAccessList = data;
                }
            }
            catch(error){
                this.userSkladAccessList = [];
                console.log(error)
            }
        },




        async addUserHisoblarAccess(){
            for(let i=0; i<this.hisoblar_list.length; i++){
                if(this.hisoblar_list[i].status == true){
                    await this.add_fetch_user_hisob_access(this.hisoblar_list[i].id);
                }
            }
        },
        async add_fetch_user_hisob_access(hisob_id){
            const requestOptions = {
                method : "POST",
                headers: { "Content-Type" : "application/json" },
                body: JSON.stringify({
                    "tegirmonHisoblarId": hisob_id,
                    "tegirmonUserid": this.user_id,
                })
            };
            try{
                const response = await fetch(this.$store.state.hostname + "/TegirmonUserHisobAccess", requestOptions);
                console.log('response', response)
            }
            catch(error){
                console.log(error)
            }
        },
        async deleteTegirmonUserHisoblarAccessByUserId()
        {
            const requestOptions = {
                method : "delete",
            };
            try{
                const response = await fetch(this.$store.state.hostname + "/TegirmonUserHisobAccess/deleteTegirmonUserHisobAccessByUserId?user_id=" + this.user_id, requestOptions);
                console.log(response);
            }
            catch(error){
                console.log(error);
            }
            // const data = await response.text();
        },

        async fetchUserHisobAccess(){
            this.hisoblar_list = [];
            await this.fetchHisob();
            console.log(this.allHisob.rows);
            for(let i=0; i<this.allHisob.rows.length; i++){
                let temp = {
                    name: this.allHisob.rows[i].name,
                    id: this.allHisob.rows[i].id,
                    status: false,
                    user_id: this.user_id,
                }
                this.hisoblar_list.push(temp);
            }
            await this.fetchUserHisobAccessUserId();

            for(let j=0; j<this.userHisobAccessList.length; j++){
                for(let k=0; k<this.hisoblar_list.length; k++){
                    if(this.userHisobAccessList[j].tegirmonHisoblarId == this.hisoblar_list[k].id){
                        this.hisoblar_list[k].status = true;
                    }
                }
            }

        },
        async fetchUserHisobAccessUserId(){
            this.userHisobAccessList = [];
            try{
                const res = await fetch(this.$store.state.hostname + '/TegirmonUserHisobAccess/getTegirmonUserHisobAccessUserId?user_id=' + this.user_id);
                const data = await res.json();
                console.log('this is by id')
                if(res.status == 200 || res.status == 201){
                    this.userHisobAccessList = data;
                }
            }
            catch(error){
                this.userHisobAccessList = [];
                console.log(error)
            }
        },
        
        
    },
}
</script>

<style lang="scss" scoped>
.user_access {
  padding: 0px;
}

.user-access-card {
  border-radius: 10px;
  border: none;
  padding: 17px 12px;
  background: #ffffff;
  box-shadow: rgba(60, 64, 67, 0.3) 0px 1px 2px 0px,
              rgba(60, 64, 67, 0.15) 0px 1px 3px 1px;
  max-height: 80vh;
  overflow-y: auto;
}

.user-access-header {
  border-bottom: 1px dashed #e0e6f1;
  padding-bottom: 4px;
}

.user-access-title {
  font-size: 13px;
  font-weight: 600;
  padding: 2px 8px;
  border-radius: 999px;
  background: #e3f2fd;
  color: #1e88e5;
}

.user-access-subtitle {
  font-size: 11px;
  color: #78909c;
}

.access_item {
  font-size: 12px;
  color: #37474f;
}

.access_item span {
  display: inline-block;
}

.access_item:hover {
  background: #f5f9ff;
  cursor: pointer;
}

.access-sub-item-name {
  font-size: 11.5px;
}

.user-access-save-btn {
  font-size: 11px !important;
  padding: 4px 14px !important;
  border-radius: 16px;
  background-image: linear-gradient(90deg, #43a047 0%, #66bb6a 100%) !important;
  color: #ffffff !important;
  border: none;
  box-shadow: 0 2px 6px rgba(67, 160, 71, 0.4);
}

.user-access-save-btn:hover {
  filter: brightness(1.05);
  box-shadow: 0 3px 8px rgba(67, 160, 71, 0.55);
}
</style>