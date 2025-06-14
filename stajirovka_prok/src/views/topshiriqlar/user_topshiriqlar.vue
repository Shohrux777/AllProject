<template>
  <div class="deparment_page">
    <navbar :title = "$t('topshiriqlar')" @add="addDept" :added_status = "added_status"/>
    <div class="example">
      <div class="example_topshiriqlar">
        <div class="card mt-2 px-3 pt-2 pb-1 w-100" style="cursor:pointer" v-for="(row) in get_task_list.rows" :key="row.id">
          <div>
            <div class="d-flex justify-content-between">
              <p class="m-0 " style="font-size: 15px; color:#293142;">{{ row.name }}</p>
              <!-- <small class="mx-2" style="font-style:italic; font-size: 11px; color:#67748A;">Last activ: 22 Aprel 2025</small> -->
            </div>
            <div class="d-flex justify-content-between">
              <small class="mx-2" style="font-style:italic; font-size: 12px; color:#67748A;">{{ row.note }}</small>
              <p class="m-0 text-success" style="font-size: 14px; font-weight:bold">3 / <span class="text-primary">{{ row.count }}</span></p>
            </div>
          </div>
          <div class="w-100" style="height: 6px;" >
            <TotalTask :totalTasks="row.count" :doneCount="2"  :pendingCount="3"/>
          </div>
          <div class="d-flex justify-content-end mt-2 mb-1" @click="$router.push('/answers_userID/' + row.id)">
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
import TotalTask from '@/components/totalTask.vue';
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
        TotalTask,
       
    },
    data(){
        return{
            show_dept: false,
            added_status:false,
        }
    },
    async mounted(){
        await this.fetch_task();
        console.log(this.get_task_list)
        // console.log('this.get_task_list')
    },
    computed: mapGetters(['get_task_list']),
    methods:{
        ...mapActions(['fetch_task']),
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