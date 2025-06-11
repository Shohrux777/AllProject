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
          <div class="d-flex" style="cursor:pointer;">
            <div style="border-bottom:1px solid blue;" class="px-1 py-1 pb-2 mx-1" >
              <p class="m-0 " style="font-size: 13px; color:#293142;">Topshiriqlar<span class="badge rounded-pill" style="background:#F4AA79;">16</span></p>
            </div>
            <div  class="px-1 py-1 pb-2 mx-1" @click="$router.push('/bajarilgan')">
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
        <div class="card mt-2 px-3 pt-3 pb-1" style="cursor:pointer">
          <div>
            <div class="">
              <p class="m-0 " style="font-size: 14.5px; color:#293142;">ІІІ. Терговга қадар текширув, суриштирув ва дастлабки тергов соҳаларида:</p>
              <small class="mx-2" style="font-style:italic; font-size: 12.5px; color:#67748A;">
                Иш ўрганувчи мураббий ва (ёки) туман (шаҳар) прокурори
                раҳбарлигида камида бешта жиноят иши юзасидан мустақил равишда
                дастлабки терговни ўтказади.
              </small>
            </div>
          </div>
          <div class="mt-4">
            <div class="">
              <label for="summary" style="font-size: 12.5px;" class="ms-1 mb-0">Qisqacha mazmuni *</label>
              <textarea id="summary" class="p-2" style="font-size: 12px;" placeholder="Qisqacha mazmuni"></textarea>

              <div class="upload-container">
                <label for="file-upload" style="font-size: 12px;">
                  <img class="upload-icon" src="https://img.icons8.com/material-rounded/24/6a5acd/attach.png"/>
                  Yuklash
                </label>
                <!-- <input type="file" id="file-upload" /> -->
                <span style="font-size: 12px; color: #6b7280;" class="mb-2 ms-3">Iltimos, asoslovchi fayllarni biriktiring!</span>
              </div>
            </div>
          </div>

          <div class="d-flex justify-content-end mt-3 mb-1 border-top pt-3 pb-2">
            <div class="px-3  py-1" style="background: #4795EF; border-radius: 5px;" >
                <small style="font-size: 13px; color:white;">Saqlash</small>
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
  padding: 6px 30px 0px 30px !important;
  background: white;
}
.range_component{
  height: 10px;
  background: #ebebeb;
  border-radius: 7px;
}

.form-container {
      background-color: #fff;
      padding: 25px 30px;
      border-radius: 12px;
      box-shadow: 0 6px 20px rgba(0, 0, 0, 0.08);
      max-width: 700px;
      margin: auto;
    }

    label {
      display: block;
      font-weight: 600;
      margin-bottom: 10px;
      font-size: 15px;
      color: #333;
    }

    textarea {
      width: 100%;
      height: 120px;
      padding: 15px;
      border: 1px solid #d0d5dd;
      border-radius: 8px;
      font-size: 14px;
      resize: vertical;
      outline: none;
      transition: border-color 0.3s;
    }

    textarea:focus {
      border-color: #6366f1;
      box-shadow: 0 0 0 3px rgba(99, 102, 241, 0.2);
    }

    .upload-container {
      display: flex;
      align-items: center;
      margin-top: 15px;
    }

    .upload-container label {
      display: inline-flex;
      align-items: center;
      color: #6a5acd;
      cursor: pointer;
      font-size: 14px;
      gap: 6px;
    }

    .upload-container input {
      display: none;
    }

    .note {
      margin-left: 10px;
      font-size: 13px;
      color: #6b7280;
    }

    .upload-icon {
      width: 18px;
      height: 18px;
    }
</style>