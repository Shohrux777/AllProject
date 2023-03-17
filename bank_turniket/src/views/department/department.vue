<template>
  <div class="deparment_page">
    <navbar :title = "$t('otdel')" @add="addDept"/>
    <div class="mainpage">
        <div class="main_table ">
            <MDBTable class="align-middle mb-0 bg-white">
                <thead class="table_header">
                    <tr>
                        <th v-for="(column, i) in get_deparment_list.columns" :key="i">{{$t(column)}}</th>
                    </tr>
                </thead>
                <tbody>
                <tr v-for="(row, index) in get_deparment_list.rows" :key="index">
                    <td>
                        {{ row.id }}
                    </td>
                    <td>
                        {{ row.name }}
                    </td>
                    <!-- <td>{{row.code}}</td> -->
                </tr>
                </tbody>
            </MDBTable>
        </div>
    </div>
    <MDBModal
        id="exampleModal"
        tabindex="-1"
        labelledby="exampleModalLabel"
        v-model="exampleModal"
        staticBackdrop
        size="lg"
    >
        <MDBModalHeader style="background: #475a65;" class="text-white px-3 py-2">
            <MDBModalTitle id="exampleModalLabel"> {{$t('otdel')}} </MDBModalTitle>
        </MDBModalHeader>
        <MDBModalBody>
            <dept_add @close="exampleModal = false"></dept_add>
        </MDBModalBody>
        
    </MDBModal>
  </div>
</template>

<script>
  import {
    MDBTable,
    MDBBtn,
    MDBBadge,
    MDBIcon,
    MDBModal,
    MDBModalHeader,
    MDBModalTitle,
    MDBModalBody,
  } from 'mdb-vue-ui-kit';
  import { ref } from 'vue';
  import navbar from '@/components/navbar.vue'
  import dept_add from './department_add.vue'
  import {mapActions, mapGetters} from 'vuex'
  export default {
    setup() {
      const exampleModal = ref(false);
      return {
        exampleModal,
      };
    },
    components: {
        MDBTable,
        MDBBtn,
        MDBBadge,
        MDBIcon,
        MDBModal,
        MDBModalHeader,
        MDBModalTitle,
        MDBModalBody,
        navbar,
        dept_add
    },
    data(){
        return{
            show_dept: false,
        }
    },
    async mounted(){
        await this.fetch_Department();
        console.log(this.get_deparment_list)
        console.log('this.get_deparment_list')
    },
    computed: mapGetters(['get_deparment_list']),
    methods:{
        ...mapActions(['fetch_Department']),
        addDept(){
            console.log('dept')
            this.show_dept = true;
            this.exampleModal = true;
        }
    }

  };
</script>

<style>
.mainpage{
    height: calc(100vh - 85px);
    overflow-y: scroll;
    padding:25px 15px;
}
.main_table{
    padding: 15px;
    box-shadow: rgba(0, 0, 0, 0.1) 0px 4px 12px;
    border-radius: 5px;
}
.table_header{
    /* position:sticky; 
    top:-25px;
    z-index: 2; */
    background:#e2e2e2;
    
}
th,td{
    padding: 5px !important;
    font-size: 12.5px;
}
</style>