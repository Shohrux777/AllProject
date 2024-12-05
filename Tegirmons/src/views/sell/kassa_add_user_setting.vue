<template>
  <div class="container-fluid">
    <div class="title_name">

    </div>
    <div class="main_kassa_setting_connect mt-2 mb-2">
      <div class="row">
        <div class="col-4" v-for="(item,i) in sortNameList" :key="i">
          <div class=" rounded kassa_connect_item mt-3" :style="{'background': colors[i] || '#ff5733'}">
            <h5 class="kassa_name_sty">{{item.name}}</h5>
            <h5 class="kassa_name_sty_user text-right" v-if="item.text_1">{{item.text_1}}
              <span @click="select_kassa_item(item)">
                <mdb-icon  fas class="ml-2" style="cursor:pointer" icon="user-edit"></mdb-icon>
              </span>
              <span @click="del_kassa_user(item)">
                <mdb-icon  fas class="ml-2" style="cursor:pointer" icon="user-alt-slash"></mdb-icon>
              </span>
            </h5>
            <h5 @click="select_kassa_item(item)" class="kassa_name_sty_none text-right" v-else>Foydalanuvchi topilmadi.
              <mdb-icon  fas class="ml-2" style="cursor:pointer"  icon="user-plus"></mdb-icon>
            </h5>
          </div>
        </div>
      </div>
    </div>

    <modal-train  :show="add_user_status" headerbackColor="info"  titlecolor="black" :title="select_kassa.name" 
      @close="add_user_status = false" width="40%">
        <template v-slot:body>
          <div class="pt-2 pb-1">
            <div class="px-3" style="position:relative;">
              <input class="m-0 px-2 form-control" v-model="search" 
                 group type="input" validate error="wrong" success="right"
                 style="height:30px; font-size: 12.5px;" placeholder="Поиск сотрудник"/>
            </div>
            <div class="user_kassa_connect_list py-1 border-bottom  px-3" >
              <div  v-for="(item,index) in filteredList" :key="index" class="item px-3" 
                @click="getBemorId(index,item)" :class="{'activeUser' : active_bemor == index }">
                <div>
                  <div class="d-flex">
                    <div class="user_photo">
                      <img src="../../assets/client.png" style="height: 45px; overflow: none;" class="img-fluid" alt="">
                    </div>
                    <div class="px-3">
                      <p class="m-0 p-0 font-weight-bold" style="font-size: 12px;">{{item.fio}}</p>
                      <p class="m-0 mt-1 rang" style="font-size:10px;">Год: <span v-if="item.born_date" class="px-2">
                        {{ item.born_date.slice(8,10) + '-' + item.born_date.slice(5,7) + '-' + item.born_date.slice(0,4)}}</span> 
                        Тел: <span class="px-2">{{item.phone_number}}</span></p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class=" text-right">
              <mdb-btn @click="add_user_status = false;" class="main_button_cancel mt-2" size="sm" p="r4 l4 t2 b2" style="font-size: 8px;">
                {{$t('cancel')}}</mdb-btn>
              <mdb-btn @click="submit_add_user_kassa" class="main_button_add mt-2" m="r3" size="sm" p="r4 l4 t2 b2" style="font-size: 8px;">
              <mdb-icon icon="plus" class="mr-2"/>{{$t('add')}}</mdb-btn>
            </div>
          </div>
        </template>
    </modal-train>

    <massage_box
      :hide="modal_status"
      :detail_info="modal_info"
      :m_text="$t('Failed_to_add')"
      @to_hide_modal="modal_status = false"
    />
    <Toast ref="message"></Toast>
    <Alert ref="alert"></Alert> 
  </div>
</template>

<script>
import {mapActions, mapGetters} from 'vuex'
import {mdbInput, mdbBtn, mdbIcon } from 'mdbvue'
export default {
  components: {
    mdbInput, mdbBtn, mdbIcon
  }, 
  data(){
    return{
      modal_info: "",
      modal_status: false,

      add_user_status: false,
      colors: ['#ff5733', '#33ff83', '#ac33ff', '#339fff', '#ff3393', '#ff9233', '#9033ff','#ff5733', '#33ff83', '#ac33ff', '#339fff'],
      search: '',
      active_bemor : -1,
      select_user_name: '',
      select_user_id: null,
      select_kassa: {},
    }
  },
  async mounted(){
    await this.fetchKassa();
    // await this.fetchUser();


  },
  computed:{
    ...mapGetters(['allKassa', 'allUser']),
    filteredList: function(){
        if(this.search)
        {
          return this.allUser.rows.filter((item)=>{
            return this.search.toLowerCase().split(' ').every(v => item.fio.toLowerCase().includes(v))
          })
        }else
        {
          return this.allUser.rows;
        }
      },
      sortNameList(){
        let key = 'name'
        function compare(a, b) {
          if (a[key] < b[key])
            return -1;
          if (a[key] > b[key])
            return 1;
          return 0;
        }
        return this.allKassa.rows.sort(compare);
      },
    },
  methods:{
    ...mapActions(['fetchKassa', 'fetchUser']),
    async refresh(){
      this.status_kassa = [];
      await this.fetchKassa();
      await this.fetchUser();
      this.active_bemor = -1;
      this.select_user_name = '';
      this.select_user_id = null;
      this.select_kassa = {};
      this.add_user_status =false;

    },
    async select_kassa_item(data){
      await this.fetchUser();
      console.log('hiy')
      this.select_kassa = data;
      this.active_bemor = -1;
      this.select_user_name = '';
      this.select_user_id = null;
      this.add_user_status = true;
      this.search = '';
    },
    async del_kassa_user(data){
      const requestOptions = {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          name: data.name,
          text_1: '',
          num_1: data.num_1,
          tegirmonUserid: null,
          auth_user_creator_id: localStorage.AuthId,
          id: data.id,
        }),
      };
      try {
        this.loading = true;
        const response = await fetch(
          this.$store.state.hostname + "/TegirmonKassa",
          requestOptions
        );
        const data = await response.text();
        this.loading = false;
        if (response.status == 201 || response.status == 200) {
          this.add_user_status = false;
          await this.fetchKassa();
          this.$refs.message.success("Added_successfully");
          return true;
        } else {
          this.modal_info = data;
          this.modal_status = true;
          return false;
        }
      } catch {
        this.loading = false;
        this.modal_info = this.$i18n.t("network_ne_connect");
        this.modal_status = true;
      }
    },
    async submit_add_user_kassa(){
      if(this.select_user_name == '')return;

      console.log('this.allKassa');
      console.log(this.allKassa.rows);
      let count = 0;
      for(let i=0; i<this.allKassa.rows.length; i++){
        if(this.select_user_id == this.allKassa.rows[i].tegirmonUserid){
          count ++;
        }
      }
      if(count>0){
        this.$refs.alert.error('Bu foydalanuvchi boshqa kassaga qushilgan !');
        return;
      }

      const requestOptions = {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          name: this.select_kassa.name,
          text_1: this.select_user_name,
          num_1: this.select_kassa.num_1,
          tegirmonUserid: this.select_user_id,
          auth_user_creator_id: localStorage.AuthId,
          id: this.select_kassa.id,
        }),
      };
      try {
        this.loading = true;
        const response = await fetch(
          this.$store.state.hostname + "/TegirmonKassa",
          requestOptions
        );
        const data = await response.text();
        this.loading = false;
        if (response.status == 201 || response.status == 200) {
          this.add_user_status = false;
          await this.fetchKassa();
          this.$refs.message.success("Added_successfully");
          return true;
        } else {
          this.modal_info = data;
          this.modal_status = true;
          return false;
        }
      } catch {
        this.loading = false;
        this.modal_info = this.$i18n.t("network_ne_connect");
        this.modal_status = true;
      }
    },

    async getBemorId(index, data){
      console.log(data);
      this.active_bemor = index;
      this.select_user_name = data.fio;
      this.select_user_id = data.id;
    },
  }
}
</script>

<style lang="scss" scoped>
.kassa_connect_item{
  padding: 7px 10px;
}
.kassa_name_sty{
  font-style: italic;
  font-size: 15px;
  padding: 0;
  margin: 0;
  color:white;
}
.kassa_name_sty_user{
  font-style: italic;
  font-size: 13px;
  padding: 0;
  margin: 0;
  color:white;
}
.kassa_name_sty_none{
  font-style: italic;
  font-size: 12px;
  padding: 0;
  margin: 0;
  color:white;
}
.user_kassa_connect_list{
  overflow: hidden;
 overflow-y: scroll;
 height: 50vh;
 width: 100%;
 // background-color: rgba(32, 32, 32,0.75);
 .item{
     .user_photo{
        background-color: inherit;
         border-radius: 50%;
         width: 45px;
         height: 45px;
         overflow: hidden;
     }
     width:100%;
     box-shadow: 2px 2px 8px rgb(224, 224, 224), -1px -1px 2px rgb(224, 224, 224);
     height: 62px;
     margin: 5px auto;
     display: flex;
     align-items: center;
     justify-content: space-between;
     // background-color: #fff;
     transition: all 0.5s ease-in-out;
     .rang{
       color: rgb(67, 67, 127);
       font-weight: bold;
     }
     &:hover{
       cursor: pointer;
       box-shadow: 2px 2px 5px rgb(224, 224, 224);
       background-color: rgb(215, 242, 255);
       transform: translate(6px, 0px);
       transition: all 0.1s ease-in-out;
     }
   }
 
}
.activeUser{
 cursor: pointer;
 box-shadow: 2px 2px 5px rgb(224, 224, 224);
 background-color: rgb(179, 230, 255);
 transform: translate(6px, 0px);
 transition: all 0.1s ease-in-out;
}
</style>