<template>
  <div class="deparment_page">
    <navbar :title = "$t('topshiriqlar')" @add="addDept" :added_status = "added_status"/>
    <div class="example">
      <div class="example_topshiriqlar">
        <div class="card mt-2 px-3 pt-2 pb-1" style="cursor:pointer">
          <div>
            <div class="d-flex justify-content-between">
              <p class="m-0 " style="font-size: 15px; color:#293142;">1. Прокуратура органларида ишларни ташкиллаштириш соҳасида:</p>
              <small class="mx-2" style="font-style:italic; font-size: 11px; color:#67748A;">Last activ: 17 May 2025</small>
            </div>
          </div>
          <div class="d-flex justify-content-end mt-2 mb-1">
            <div class="px-4 rounded alert-info">
              <small style=" font-size: 12px; color:#67748A; font-weight:600;">Batafsil</small>
            </div>
          </div>
        </div>

        <div class="card mt-2 px-3 pt-2 pb-1 w-100" style="cursor:pointer">
          <div>
            <div class="d-flex justify-content-between">
              <p class="m-0 " style="font-size: 15px; color:#293142;">ІІ. Қонунчилик ижроси устидан назорат соҳасида:</p>
              <small class="mx-2" style="font-style:italic; font-size: 11px; color:#67748A;">Last activ: 22 Aprel 2025</small>
            </div>
            <div class="d-flex justify-content-between">
              <small class="mx-2" style="font-style:italic; font-size: 12px; color:#67748A;">Topshiriq yuzasidan kamida o'n beshta ish yuklash va bitta elektiv o'quv kurs.</small>
              <p class="m-0 text-success" style="font-size: 14px; font-weight:bold">3 / <span class="text-primary">16</span></p>
            </div>
          </div>
          <div class="d-flex w-100 bg-secondary" style="height: 6px;">
            <div class="border bg-success" style="width: 6.25%;"></div>
            <div class="border bg-success" style="width: 6.25%;"></div>
            <div class="border bg-success" style="width: 6.25%;"></div>
            <div class="border bg-warning" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
          </div>
          <div class="d-flex justify-content-end mt-2 mb-1" @click="$router.push('/answers_userID')">
            <div class="px-4 rounded alert-info" >
              <small style=" font-size: 12px; color:#67748A; font-weight:600;">Batafsil</small>
            </div>
          </div>
        </div>

        <div class="card mt-2 px-3 pt-2 pb-1 w-100" style="cursor:pointer">
          <div>
            <div class="d-flex justify-content-between">
              <p class="m-0 " style="font-size: 15px; color:#293142;">ІІІ. Терговга қадар текширув, суриштирув ва дастлабки тергов соҳаларида:</p>
              <small class="mx-2" style="font-style:italic; font-size: 11px; color:#67748A;">Last activ: 22 Aprel 2025</small>
            </div>
            <div class="d-flex justify-content-between">
              <small class="mx-2" style="font-style:italic; font-size: 12px; color:#67748A;">Topshiriq yuzasidan kamida o'n beshta ish yuklash va bitta elektiv o'quv kurs.</small>
              <p class="m-0 text-success" style="font-size: 14px; font-weight:bold">2 / <span class="text-primary">16</span></p>
            </div>
          </div>
          <div class="d-flex w-100 bg-secondary" style="height: 6px;">
            <div class="border bg-success" style="width: 6.25%;"></div>
            <div class="border bg-success" style="width: 6.25%;"></div>
            <div class="border bg-warning" style="width: 6.25%;"></div>
            <div class="border bg-warning" style="width: 6.25%;"></div>
            <div class="border bg-warning" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
          </div>
          <div class="d-flex justify-content-end mt-2 mb-1"  @click="$router.push('/answers_userID')">
            <div class="px-4 rounded alert-info" >
              <small style=" font-size: 12px; color:#67748A; font-weight:600;">Batafsil</small>
            </div>
          </div>
        </div>

        <div class="card mt-2 px-3 pt-2 pb-1 w-100" style="cursor:pointer">
          <div>
            <div class="d-flex justify-content-between">
              <p class="m-0 " style="font-size: 15px; color:#293142;">IV. Ички ишлар органларида қонунлар ижроси устидан назорат соҳасида:</p>
              <small class="mx-2" style="font-style:italic; font-size: 11px; color:#67748A;">Last activ: 22 Aprel 2025</small>
            </div>
            <div class="d-flex justify-content-between">
              <small class="mx-2" style="font-style:italic; font-size: 12px; color:#67748A;">Topshiriq yuzasidan kamida beshta ish yuklash va bitta elektiv o'quv kurs.</small>
              <p class="m-0 text-success" style="font-size: 14px; font-weight:bold">2 / <span class="text-primary">6</span></p>
            </div>
          </div>
          <div class="d-flex w-100 bg-secondary" style="height: 6px;">
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>

          </div>
          <div class="d-flex justify-content-end mt-2 mb-1">
            <div class="px-4 rounded alert-info" >
              <small style=" font-size: 12px; color:#67748A; font-weight:600;">Batafsil</small>
            </div>
          </div>
        </div>

        <div class="card mt-2 px-3 pt-2 pb-1 w-100" style="cursor:pointer">
          <div>
            <div class="d-flex justify-content-between">
              <p class="m-0 " style="font-size: 15px; color:#293142;">V. Судларда ишлар кўрилишида прокурор ваколатини таъминлаш соҳасида:</p>
              <small class="mx-2" style="font-style:italic; font-size: 11px; color:#67748A;">Last activ: 22 Aprel 2025</small>
            </div>
              <div class="d-flex justify-content-between">
              <small class="mx-2" style="font-style:italic; font-size: 12px; color:#67748A;">Topshiriq yuzasidan kamida beshta ish yuklash va bitta elektiv o'quv kurs.</small>
              <p class="m-0 text-success" style="font-size: 14px; font-weight:bold">2 / <span class="text-primary">6</span></p>
            </div>
          </div>
          <div class="d-flex w-100 bg-secondary" style="height: 6px;">
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>

          </div>
          <div class="d-flex justify-content-end mt-2 mb-1">
            <div class="px-4 rounded alert-info" >
              <small style=" font-size: 12px; color:#67748A; font-weight:600;">Batafsil</small>
            </div>
          </div>
        </div>

        <div class="card mt-2 px-3 pt-2 pb-1 w-100" style="cursor:pointer">
          <div>
            <div class="d-flex justify-content-between">
              <p class="m-0 " style="font-size: 15px; color:#293142;">VI. Суд ҳужжатлари ва бошқа органлар ҳужжатларини ижро этишда, қамоққа олинганларни сақлашда ва жиноий жазоларнинг ижро этилишида қонунларга риоя этилиши устидан назорат соҳасида:</p>
              <small class="mx-2" style="font-style:italic; font-size: 11px; color:#67748A;">Last activ: 22 Aprel 2025</small>
            </div>
              <div class="d-flex justify-content-between">
              <small class="mx-2" style="font-style:italic; font-size: 12px; color:#67748A;">Topshiriq yuzasidan kamida beshta ish yuklash va bitta elektiv o'quv kurs.</small>
              <p class="m-0 text-success" style="font-size: 14px; font-weight:bold">2 / <span class="text-primary">12</span></p>
            </div>
          </div>
          <div class="d-flex w-100 bg-secondary" style="height: 6px;">
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-warning" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>

          </div>
          <div class="d-flex justify-content-end mt-2 mb-1">
            <div class="px-4 rounded alert-info" >
              <small style=" font-size: 12px; color:#67748A; font-weight:600;">Batafsil</small>
            </div>
          </div>
        </div>

        <div class="card mt-2 px-3 pt-2 pb-1 w-100" style="cursor:pointer">
          <div>
            <div class="d-flex justify-content-between">
              <p class="m-0 " style="font-size: 15px; color:#293142;">VII. Жисмоний ва юридик шахсларнинг мурожаатларини кўриб чиқиш соҳасида:</p>
              <small class="mx-2" style="font-style:italic; font-size: 11px; color:#67748A;">Last activ: 22 Aprel 2025</small>
            </div>
              <div class="d-flex justify-content-between">
              <small class="mx-2" style="font-style:italic; font-size: 12px; color:#67748A;">Topshiriq yuzasidan kamida beshta ish yuklash va bitta elektiv o'quv kurs.</small>
              <p class="m-0 text-success" style="font-size: 14px; font-weight:bold">1 / <span class="text-primary">10</span></p>
            </div>
          </div>
          <div class="d-flex w-100 bg-secondary" style="height: 6px;">
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>

          </div>
          <div class="d-flex justify-content-end mt-2 mb-1">
            <div class="px-4 rounded alert-info" >
              <small style=" font-size: 12px; color:#67748A; font-weight:600;">Batafsil</small>
            </div>
          </div>
        </div>

        <div class="card mt-2 px-3 pt-2 pb-1 w-100" style="cursor:pointer">
          <div>
            <div class="d-flex justify-content-between">
              <p class="m-0 " style="font-size: 15px; color:#293142;">VIII. Қонунчиликни тарғиб қилиш, жамиятда ҳуқуқий саводхонликни ошириш бўйича:</p>
              <small class="mx-2" style="font-style:italic; font-size: 11px; color:#67748A;">Last activ: 22 Aprel 2025</small>
            </div>
              <div class="d-flex justify-content-between">
              <small class="mx-2" style="font-style:italic; font-size: 12px; color:#67748A;">Topshiriq yuzasidan kamida beshta ish yuklash va bitta elektiv o'quv kurs.</small>
              <p class="m-0 text-success" style="font-size: 14px; font-weight:bold">8 / <span class="text-primary">10</span></p>
            </div>
          </div>
          <div class="d-flex w-100 bg-secondary" style="height: 5px;">
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-warning" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>

          </div>
          <div class="d-flex justify-content-end mt-2 mb-1">
            <div class="px-4 rounded alert-info" >
              <small style=" font-size: 12px; color:#67748A; font-weight:600;">Batafsil</small>
            </div>
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
  padding: 10px 20px !important;
  background: white;
}
.range_component{
  height: 10px;
  background: #ebebeb;
  border-radius: 7px;
}
</style>