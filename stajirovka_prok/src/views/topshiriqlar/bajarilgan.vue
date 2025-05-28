<template>
  <div class="deparment_page">
    <navbar :title = "$t('topshiriqlar')" @add="addDept" :added_status = "added_status"/>
    <div class="example">
      <div class="example_cont">
        <div class="w-100 d-flex justify-content-between">
          <div>
            <p class="m-0 pt-1" style="font-size: 14px;">
              ІІІ. Терговга қадар текширув, суриштирув ва дастлабки тергов соҳаларида:
            </p>
          </div>
          <div class="d-flex">
            <div  class="px-1 py-1 pb-2 mx-1" @click="$router.push('/answers_userID')">
              <p class="m-0 " style="font-size: 13px; color:#293142;">Topshiriqlar<span class="badge rounded-pill" style="background:#F4AA79;">16</span></p>
            </div>
            <div style="border-bottom:1px solid blue;" class="px-1 py-1 pb-2 mx-1">
              <p class="m-0 " style="font-size: 13px; color:#293142;">Bajarilgan<span class="badge rounded-pill " style="background:#4EC264;">3</span></p>
            </div>
            <div  class="px-1 py-1 pb-2 mx-1">
              <p class="m-0 " style="font-size: 13px; color:#293142;">Tasdiqlash<span class="badge rounded-pill" style="background:#6968EC;">1</span></p>
            </div>
            <div  class="px-1 py-1 pb-2 mx-1">
              <p class="m-0 " style="font-size: 13px; color:#293142;">Bajarilmoqda<span class="badge rounded-pill" style="background:#5082F2;">12</span></p>
            </div>
          </div>
          
        </div>
      </div>
      <div class="example_topshiriqlar">

        <div class="mt-4 ">
          <p class="m-0 " style="font-style:italic; font-size: 15px; color:#757580;">Bajarilgan</p>
        </div>  
        <div class="card px-3 mt-1 pt-2 pb-1 mb-5" style="cursor:pointer">
          <div>
            <div class="d-flex justify-content-between">
              <p class="m-0 " style="font-size: 14.5px; color:#293142;">ІІІ. Терговга қадар текширув, суриштирув ва дастлабки тергов соҳаларида:</p>
              <!-- <small class="mx-2" style="font-style:italic; font-size: 11px; color:#67748A;">Last activ: 17 May 2025</small> -->
            </div>
          </div>
          <div class="d-flex justify-content-between  mt-4 mb-1">
            <div>
              <div class="px-3  " style="background: #4795EF; border-radius: 15px;" >
                <small style=" font-size: 12px; color:white;">Berilgan javob</small>
              </div>
              <small class="px-2" style=" font-size: 12px; color:#67748A;">22 may 2025 16:21</small>
            </div>
            
            <div class="" style="text-align: right;">
              <p class="m-0" style=" font-size: 13px; color:black;">Yusupov Baxrom Uktamovich</p>
              <p class="m-0" style=" font-size: 12px; color:#67748A;">Sam. viloyat Payariq tuman</p>
            </div>
          </div>

          <div class="border-start px-3 mt-3">
            <p class="m-0"  style=" font-size: 13px;">
              Хонадонга борган ҳолда фуыаро Умаров Ботир билан якка тартибда суҳбат ўтказилди
            </p>
          </div>

          <div class="mt-3">
            <div class="row">
              <div class="col-6">
                <div class="file-card w-100">
                  <div class="file-icon">
                    <span style="font-size: 14px;">PDF</span>
                    </div>
                  <div class="file-info">
                    <div class="file-name">Doc 22-0....pdf</div>
                    <div class="file-meta">0,42 мб<br>22 май 2025 16:20</div>
                  </div>
                  <div class="file-actions">
                    <i class="fa-regular fa-eye"></i>
                    <i class="far fa-circle-down"></i>
                  </div>
                </div>
              </div>
              <div class="col-6">
                <div class="file-card w-100">
                  <div class="file-icon bg-warning">
                    <span style="font-size: 14px;">JPG</span>
                    </div>
                  <div class="file-info">
                    <div class="file-name">photo_20....jpg</div>
                    <div class="file-meta">0,42 мб<br>22 май 2025 16:20</div>
                  </div>
                  <div class="file-actions">
                    <i class="fa-regular fa-eye"></i>
                    <i class="far fa-circle-down"></i>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <hr>
          <div class="d-flex justify-content-between  mt-4 mb-1">
            <div>
              <div class="px-3 bg-success" style="border-radius: 15px;" >
                <small style=" font-size: 12px; color:white;">Qabul qilindi</small>
              </div>
              <small class="px-2" style=" font-size: 12px; color:#67748A;">24 may 2025 10:01</small>
            </div>
            
            <div class="" style="text-align: right;">
              <p class="m-0" style=" font-size: 13px; color:black;">Akbarov Elmurod Asatovich</p>
              <p class="m-0" style=" font-size: 12px; color:#67748A;">Kadr bo'limi boshlig'i</p>
            </div>
          </div>

          <div class="border-start px-3 mt-2 mb-4">
            <p class="m-0"  style=" font-size: 13px;">
              Berilgan javobni qabul qilishdagi izoh.
            </p>
          </div>
        </div>


        <div class="card px-3 pt-2 pb-1 mb-5" style="cursor:pointer">
          <div>
            <div class="d-flex justify-content-between">
              <p class="m-0 " style="font-size: 14.5px; color:#293142;">ІІІ. Терговга қадар текширув, суриштирув ва дастлабки тергов соҳаларида:</p>
              <!-- <small class="mx-2" style="font-style:italic; font-size: 11px; color:#67748A;">Last activ: 17 May 2025</small> -->
            </div>
          </div>
          <div class="d-flex justify-content-between  mt-4 mb-1">
            <div>
              <div class="px-3  " style="background: #4795EF; border-radius: 15px;" >
                <small style=" font-size: 12px; color:white;">Berilgan javob</small>
              </div>
              <small class="px-2" style=" font-size: 12px; color:#67748A;">22 may 2025 16:21</small>
            </div>
            
            <div class="" style="text-align: right;">
              <p class="m-0" style=" font-size: 13px; color:black;">Yusupov Baxrom Uktamovich</p>
              <p class="m-0" style=" font-size: 12px; color:#67748A;">Sam. viloyat Payariq tuman</p>
            </div>
          </div>

          <div class="border-start px-3 mt-3">
            <p class="m-0"  style=" font-size: 13px;">
              Хонадонга борган ҳолда фуыаро Умаров Ботир билан якка тартибда суҳбат ўтказилди
            </p>
          </div>

          <div class="mt-3">
            <div class="row">
              <div class="col-6">
                <div class="file-card w-100">
                  <div class="file-icon">
                    <span style="font-size: 14px;">PDF</span>
                    </div>
                  <div class="file-info">
                    <div class="file-name">Doc 22-0....pdf</div>
                    <div class="file-meta">0,42 мб<br>22 май 2025 16:20</div>
                  </div>
                  <div class="file-actions">
                    <i class="fa-regular fa-eye"></i>
                    <i class="far fa-circle-down"></i>
                  </div>
                </div>
              </div>
              <div class="col-6">
                <div class="file-card w-100">
                  <div class="file-icon bg-warning">
                    <span style="font-size: 14px;">JPG</span>
                    </div>
                  <div class="file-info">
                    <div class="file-name">photo_20....jpg</div>
                    <div class="file-meta">0,42 мб<br>22 май 2025 16:20</div>
                  </div>
                  <div class="file-actions">
                    <i class="fa-regular fa-eye"></i>
                    <i class="far fa-circle-down"></i>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <hr>
          <div class="d-flex justify-content-between  mt-4 mb-1 ">
            <div>
              <div class="px-3 bg-success" style="border-radius: 15px;" >
                <small style=" font-size: 12px; color:white;">Qabul qilindi</small>
              </div>
              <small class="px-2" style=" font-size: 12px; color:#67748A;">24 may 2025 10:01</small>
            </div>
            
            <div class="" style="text-align: right;">
              <p class="m-0" style=" font-size: 13px; color:black;">Akbarov Elmurod Asatovich</p>
              <p class="m-0" style=" font-size: 12px; color:#67748A;">Kadr bo'limi boshlig'i</p>
            </div>
          </div>

          <div class="border-start px-3 mt-2 mb-4">
            <p class="m-0"  style=" font-size: 13px;">
              Berilgan javobni qabul qilishdagi izoh.
            </p>
          </div>
        </div>


        <div class="card px-3 pt-2 pb-1 mb-5" style="cursor:pointer">
          <div>
            <div class="d-flex justify-content-between">
              <p class="m-0 " style="font-size: 14.5px; color:#293142;">ІІІ. Терговга қадар текширув, суриштирув ва дастлабки тергов соҳаларида:</p>
              <!-- <small class="mx-2" style="font-style:italic; font-size: 11px; color:#67748A;">Last activ: 17 May 2025</small> -->
            </div>
          </div>
          <div class="d-flex justify-content-between  mt-4 mb-1">
            <div>
              <div class="px-3  " style="background: #4795EF; border-radius: 15px;" >
                <small style=" font-size: 12px; color:white;">Berilgan javob</small>
              </div>
              <small class="px-2" style=" font-size: 12px; color:#67748A;">22 may 2025 16:21</small>
            </div>
            
            <div class="" style="text-align: right;">
              <p class="m-0" style=" font-size: 13px; color:black;">Yusupov Baxrom Uktamovich</p>
              <p class="m-0" style=" font-size: 12px; color:#67748A;">Sam. viloyat Payariq tuman</p>
            </div>
          </div>

          <div class="border-start px-3 mt-3">
            <p class="m-0"  style=" font-size: 13px;">
              Хонадонга борган ҳолда фуыаро Умаров Ботир билан якка тартибда суҳбат ўтказилди
            </p>
          </div>

          <div class="mt-3">
            <div class="row">
              <div class="col-6">
                <div class="file-card w-100">
                  <div class="file-icon">
                    <span style="font-size: 14px;">PDF</span>
                    </div>
                  <div class="file-info">
                    <div class="file-name">Doc 22-0....pdf</div>
                    <div class="file-meta">0,42 мб<br>22 май 2025 16:20</div>
                  </div>
                  <div class="file-actions">
                    <i class="fa-regular fa-eye"></i>
                    <i class="far fa-circle-down"></i>
                  </div>
                </div>
              </div>
              <div class="col-6">
                <div class="file-card w-100">
                  <div class="file-icon bg-warning">
                    <span style="font-size: 14px;">JPG</span>
                    </div>
                  <div class="file-info">
                    <div class="file-name">photo_20....jpg</div>
                    <div class="file-meta">0,42 мб<br>22 май 2025 16:20</div>
                  </div>
                  <div class="file-actions">
                    <i class="fa-regular fa-eye"></i>
                    <i class="far fa-circle-down"></i>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <hr>
          <div class="d-flex justify-content-between  mt-4 mb-1 ">
            <div>
              <div class="px-3 bg-success" style="border-radius: 15px;" >
                <small style=" font-size: 12px; color:white;">Qabul qilindi</small>
              </div>
              <small class="px-2" style=" font-size: 12px; color:#67748A;">24 may 2025 10:01</small>
            </div>
            
            <div class="" style="text-align: right;">
              <p class="m-0" style=" font-size: 13px; color:black;">Akbarov Elmurod Asatovich</p>
              <p class="m-0" style=" font-size: 12px; color:#67748A;">Kadr bo'limi boshlig'i</p>
            </div>
          </div>

          <div class="border-start px-3 mt-2 mb-4">
            <p class="m-0"  style=" font-size: 13px;">
              Berilgan javobni qabul qilishdagi izoh.
            </p>
          </div>
        </div>
      </div>
    </div>
    
  </div>
</template>

<script>
  import {
    MDBBtn,
    MDBBadge,
    MDBIcon,
    
  } from 'mdb-vue-ui-kit';
  import { ref } from 'vue';
  import navbar from '@/components/navbar.vue'
  import {mapActions, mapGetters} from 'vuex'
  export default {
    setup() {
      const exampleModal = ref(false);
      return {
        exampleModal,
      };
    },
    components: {
        MDBBtn,
        MDBBadge,
        MDBIcon,
        navbar,
       
    },
    data(){
        return{
            show_dept: false,
            added_status:false,
        }
    },
    async mounted(){
        await this.fetch_Salary();
        console.log(this.get_salary_list)
        console.log('this.get_salary_list')
    },
    computed: mapGetters(['get_salary_list']),
    methods:{
        ...mapActions(['fetch_Salary']),
        addDept(){
            console.log('dept')
            this.show_dept = true;
            this.exampleModal = true;
        },
        selectData(data){
          console.log('select_date')
          console.log(data.userid)
          this.select_data = data
        },
        async deleteFunc(){
            try{
                console.log('deleteFunc')
                const requestOptions = {
                    method : "delete",
                };
                const response = await fetch(this.$store.state.hostname + "/SkudOyliks/" + this.select_data.id, requestOptions);
                const data = await response.json();
                console.log('data')
                console.log(data)
                console.log(response)
                if(response.status == 200 || response.status == 201){
                    await this.fetch_Salary();
                }
            }
            catch(error){
                console.log(error)
            }
          },
          async editFunc(){
            this.show_dept = true;
            this.exampleModal = true;
            console.log('editFunc')
          }
    }

  };
</script>

<style>
.example{
    height: calc(100vh - 85px);
    overflow-y: scroll;
    background: #F3F5F9;
}
.example_topshiriqlar{
  padding: 15px 20px !important;
}
.example_cont{
  padding: 6px 30px 0px 30px !important;
  background: white;
}
.range_component{
  height: 10px;
  background: #ebebeb;
  border-radius: 7px;
}

 .file-card {
      display: flex;
      align-items: center;
      background: #fff;
      border: 1px solid #e6e6e6;
      border-radius: 8px;
      padding: 10px 14px;
      width: 420px;
      box-shadow: 0 2px 5px rgba(199, 199, 199, 0.05);
    }

    .file-icon {
      background: #d32f2f;
      color: white;
      font-weight: bold;
      width: 55px;
      height: 55px;
      display: flex;
      align-items: center;
      justify-content: center;
      border-radius: 7px;
      margin-right: 15px;
    }

    .file-info {
      flex: 1;
    }

    .file-name {
      font-weight: 600;
      font-size: 12.5px;
    }

    .file-meta {
      font-size: 12px;
      color: #666;
    }

    .file-actions {
      display: flex;
      gap: 12px;
      margin-left: 10px;
    }

    .file-actions i {
      color: #7a7a7a;
      cursor: pointer;
      transition: color 0.3s;
      font-size: 18px;
    }

    .file-actions i:hover {
      color: #000;
    }
</style>