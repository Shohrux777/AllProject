<template>
  <div class="hok_dashboard_main">
    <div class="dashboard_header">
        <p class="header_text p-0 m-0 pt-2">Edo.ijro.uz "Ijro intizomi idoralararo yagona elektron tizimi orqali berilgan topshiriqlar ijrosi to‘g‘risi"da</p>
        <p class="header_info p-0 m-0">
            MA’LUMOT
        </p>
        <div class="d-flex justify-content-end border-bottom mb-2">
            <span>{{day}} yil soat {{hour}} holatiga</span>
        </div>
    </div>
    <div class="row container-fluid px-5">
        <div class="col-6 dashboard_main_item_danger ">
            <div class="text-center">
                <span>Muddati o'tgan hujjatlar soni</span>
            </div>
            <div>
                <div class="user_ill text-left py-1 px-3">
                    <div  v-for="(item, index) in bajarilmaganList" :key="index" v-show="item.gr>0"  class="item px-3 user_topshiriq" >
                        <div>
                        <div class="d-flex ">
                            <div class=" user_photo">
                                <img :src="hostname1 + item.image_url" style="height:80px; width:100%; overflow: none; " class="img-fluid" alt="">
                            </div>
                            <div class="px-3 mt-1">
                                <p class="m-0 p-0" style="font-size: 23px;">{{item.ism}}</p>
                                <p class="m-0 p-0 px-1 rang" style="font-size:11.5px; margin-top:1px !important;">{{item.cardno}} </p>
                            </div>
                            <div class="topshiriq_soni">
                                <span class="qty_danger">{{item.gr}}</span>
                            </div>
                        </div>
                        </div>
                    </div>
                    
                </div>
            </div>
        </div>
        <div class="col-6 dashboard_main_item_info">
            <div>
                <div class="text-center">
                    <span class="px-4 py-1 bg_warning">Muddati bugun tugaydigan hujjatlar soni</span>
                </div>
                
            </div>
            <div>
                <div class="user_ill text-left py-1 px-3">
                    <div v-for="(item, i) in bajarilmoqdaList" v-show="parseInt(item.without_gr_id)>0" :key="i+100" class="item px-3 user_topshiriq" >
                        <div>
                        <div class="d-flex ">
                            <div class=" user_photo">
                                <img :src="hostname1 + item.image_url" style="height:80px; width:100%; overflow: none; " class="img-fluid" alt="">
                            </div>
                            <div class="px-3 mt-1">
                                <p class="m-0 p-0" style="font-size: 23px;">{{item.ism}}</p>
                                <p class="m-0 p-0 px-1 rang" style="font-size:11.5px; margin-top:1px !important;"> {{item.cardno}}</p>
                            </div>
                            <div class="topshiriq_soni">
                                <span class="qty_info">{{item.without_gr_id}}</span>
                            </div>
                        </div>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>
  </div>
</template>

<script>
import {mapActions, mapGetters} from 'vuex'
export default {
    data(){
          return{
              hostname1: this.$store.state.hostname1,
              day: '',
              hour:''
          }
      },
async mounted(){
        await this.fetch_user();
        if(this.get_user_list.rows[0].familiya){
            this.day = this.get_user_list.rows[0].familiya
         }
         if(this.get_user_list.rows[0].acc_name){
            this.hour = this.get_user_list.rows[0].acc_name
         }
        setInterval(() => {
		 this.fetch_user();
         console.clear();
         if(this.get_user_list.rows[0].familiya){
            this.day = this.get_user_list.rows[0].familiya
         }
         if(this.get_user_list.rows[0].acc_name){
            this.hour = this.get_user_list.rows[0].acc_name
         }
        }, 15000)
      },

      methods:{
        ...mapActions(['fetch_user']),
      },

      computed: {
      ...mapGetters(['get_user_list']),
        bajarilmaganList(){
            return this.get_user_list.rows.sort((a, b) => b.gr - a.gr );
        },
        bajarilmoqdaList(){
            return this.get_user_list.rows.sort((a, b) => b.without_gr_id - a.without_gr_id );
        }
      },
}
</script>

<style lang="scss">
.hok_dashboard_main{
    background: #fbfdff;
    height: 100vh;
}
.dashboard_header .header_text{
    text-align: center;
    font-size: 35px;
    margin-top: 10px;
    padding: 0px 15px ;
    color: rgb(60, 127, 253);
    font-weight: bold;
    font-family: 'Times New Roman', Times;
}
.dashboard_header .header_info{
    text-align: center;
    font-size: 32px;
    margin-top: 10px;
    padding: 0px 15px;
    color: rgb(60, 127, 253);
    font-weight: bold;
    font-family: 'Times New Roman', Times;

}
.dashboard_header span{
    font-size: 20px;
    margin-top: 10px;
    padding: 5px 7%;
    color: rgb(0, 0, 0);
}
.dashboard_main_item_danger span{
    color: red;
    font-size: 18px;
    font-weight: bold;
}
.dashboard_main_item_info span{
    color: rgb(70, 70, 70);
    font-size: 18px;
    font-weight: bold;
   
}
.bg_warning{
 background: rgb(252, 207, 84);
}
.user_ill{

      overflow: hidden;
      max-height: calc(100vh - 150px);
      width: 100%;
      // background-color: rgba(32, 32, 32,0.75);
      .item{
          .user_photo{
            display: flex;
            align-items: center;
            justify-content: center;
              background-color: #fff;
              border-radius: 10%;
              width: 75px;
              height: 75px;
              overflow: hidden;
          }
          width:100%;
          box-shadow: 2px 2px 8px rgb(224, 224, 224), -1px -1px 2px rgb(224, 224, 224);
          height: 80px;
          margin: 10px auto;
          display: flex;
          align-items: center;
          justify-content: space-between;
          background-color: #fff;
          transition: all 0.5s ease-in-out;
          .rang{
            color: rgb(100, 183, 255);
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
        .user_topshiriq{
            position: relative;
        }
        .user_topshiriq .topshiriq_soni{
            position: absolute;
            height: 100%;
            width: 70px;
            right: 0;
            display: flex;
            align-items: center;
            justify-content: center;
            .qty_danger{
                color: red;
                font-size: 35px;
            }
            .qty_info{
                color: rgb(65, 161, 190);
                font-size: 32px;
            }
        }
      
    }
</style>