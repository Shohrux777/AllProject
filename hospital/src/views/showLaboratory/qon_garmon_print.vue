<template>
  <div>
    <div class="rv_lab allWidthLab" id="table" style="position:relative;">
      <input type="text" style="position:absolute; top: -500px; outline:none;border:none;" ref="print" @keyup.enter="send">
      <div class="img " style="text-align:center;" v-show="false">
        <img id="image" width="100%" height="25%" alt="">
      </div>
      <div class="img " style="text-align:center;" >
        <img src="../../assets/lab1.png" width="100%" alt="">
      </div>
      <loader v-if="loading" />
      <div class="lab_answer upHeightAll"  >
        <div class=" " style="display:flex; justify-content:center; flex-direction:column;">
          <h3 style="width:100%; text-align:center; font-weight:bold; " class="mb-5 headerTitle">Иммуноферментный анализ крови на гормоны</h3>
          <table border class="allWidthLabTable">
            <thead >
              <tr>
                <th class="fs" style="width:45%;" ><span class="font-weight-bold">ПАЦИЕНТ:</span>  {{option.patient_name}}</th>
                <th class="fs"><span class="font-weight-bold">ВРАЧ:</span>  {{option.doctor_name}}</th>
              </tr>
              <tr>
                <th class="fs" style="width:45%;"><span class="font-weight-bold">ВОЗРАСТ:</span> {{borndate}} </th>
                <th class="fs"><span class="font-weight-bold">ДАТА:</span> {{date}}</th>
              </tr>
              <tr>
                <th class="fs" style="width:45%;"><span class="font-weight-bold">ПРИБОР:</span> Mindray СL 900 i </th>
                <th class="fs"></th>
              </tr>
            </thead>
          </table>
          <table border class="allWidthLabTable" style="margin-top:35px;">
            <thead>
              <tr>
                <th class="fs font-weight-bold py-2" >№</th>
                <th class="fs font-weight-bold py-2" >ИССЛЕДОВАНИЕ</th>
                <th class="fs font-weight-bold py-2" >РЕЗУЛЬТАТ</th>
                <th class="fs font-weight-bold py-2" >НОРМА</th>
              </tr>
            </thead>
            <tbody >
              
              <tr v-show="option.name_1 != null && option.name_1 != ''">
                <td class="fs">1</td>
                <td class="fs">Тиреотропный гормон (ТТГ)</td>
                <td class="fs">{{option.name_1}}</td>
                <td class="fs"><p class="m-0 p-0 pb-1">0.32 - 6.58</p>
                  <!-- <p class="m-0 p-0 pb-1">берем I-II трим 240 - 500 мг/дл</p>
                  <p class="m-0 p-0 pb-1">берем III трим 370 - 600 мг/дл</p> -->
                </td>
              </tr>
              <tr v-show="option.name_2 != null && option.name_2 != ''">
                <td class="fs">2</td>
                <td class="fs">Трийодтиронин свободный(Т3)</td>
                <td class="fs">{{option.name_2}}</td>
                <td class="fs">1.03 - 4.03
                </td>
              </tr>
              <tr v-show="option.name_3 != null && option.name_3 != ''">
                <td class="fs">3</td>
                <td class="fs">Тироксин свободный (Т4 свободный)</td>
                <td class="fs">{{option.name_3}}</td>
                <td class="fs">
                  9.0 - 22.0
                </td>
              </tr>
              <tr v-show="option.name_4 != null && option.name_4 != ''">
                <td class="fs">4</td>
                <td class="fs">Антитела к тиреоглобулину(АТ-ТГ)</td>
                <td class="fs">{{option.name_4}}</td>
                <td class="fs"> 0-100 ME/Ml
                </td>
              </tr>
              <tr v-show="option.name_5 != null && option.name_5 != ''">
                <td class="fs">5</td>
                <td class="fs">Фолликулостимулирующий гормон(ФСГ)</td>
                <td class="fs">{{option.name_5}}</td>
                <td class="fs">
                  <p class="m-0 p-0 pb-1">Дети до 11лет: 0,1-4 mIU/mL</p>
                  <p class="m-0 p-0 pb-1">Мужчины: 1-14 mIU/mL</p>
                  <p class="m-0 p-0 pb-1">Жен. Фолликулярная фаза: 3-12 mIU/mL</p>
                  <p class="m-0 p-0 pb-1">Лютеиновая фаза: 2-12 mIU/mL</p>
                  <p class="m-0 p-0 pb-1">Менопауза: 35-151 mIU/mL</p>
                  <p class="m-0 p-0 pb-1">Средина цикла: 8-22 mIU/mL</p>
                </td>
              </tr>
              <tr v-show="option.name_6 != null && option.name_6 != ''">
                <td class="fs">6</td>
                <td class="fs">Лютеинизирующий гормон(ЛГ) </td>
                <td class="fs">{{option.name_6}}</td>
                <td class="fs">
                  <p class="m-0 p-0 pb-1">Дети до 11 лет: 0,1-5 mIU/mL</p>
                  <p class="m-0 p-0 pb-1">Мужчины: 1.1-25 mIU/mL</p>
                  <p class="m-0 p-0 pb-1">Жен. Фолликулярная фаза: 1.2-12.5 mIU/mL</p>
                  <p class="m-0 p-0 pb-1">Овуляция: 12-82 mIU/mL</p>
                  <p class="m-0 p-0 pb-1">Лютеиновая: фаза 0.4-19mIU/mL</p>
                  <p class="m-0 p-0 pb-1">Менопауза: 14-48 mIU/mL</p>
                </td>
              </tr>
              <tr v-show="option.name_7 != null && option.name_7 != ''">
                <td class="fs">7</td>
                <td class="fs">Пролактин</td>
                <td class="fs">{{option.name_7}}</td>
                <td class="fs">
                  <p class="m-0 p-0 pb-1">1,3 - 18,1 мг-мл </p>
                  <!-- <p class="m-0 p-0 pb-1">Мужчины: 1.3 – 16.0</p> -->
                </td>
              </tr>
              <tr v-show="option.name_8 != null && option.name_8 != ''">
                <td class="fs">8</td>
                <td class="fs">Тестостерон</td>
                <td class="fs">{{option.name_8}}</td>
                <td class="fs">
                  <p class="m-0 p-0 pb-1">Женщины: {{simbol}} 1.0 ng/mL</p>
                  <p class="m-0 p-0 pb-1">Мужчины: 2.2-10.5 ng/mL</p>
                </td>
              </tr>
              <tr v-show="option.name_9 != null && option.name_9 != ''">
                <td class="fs">9</td>
                <td class="fs">Тироид - Анти - Тпо</td>
                <td class="fs">{{option.name_9}}</td>
                <td class="fs">
                  <p class="m-0 p-0 pb-1">Мужчины: 0-30 МЕ/мл</p>
                  <p class="m-0 p-0 pb-1">Женщины: 0-30 МЕ/мл</p>
                  <p class="m-0 p-0 pb-1">Женщины старше:  лет 0-50 МЕ/мл</p>
                </td>
              </tr>
              <tr v-show="option.name_10 != null && option.name_10 != ''">
                <td class="fs">10</td>
                <td class="fs">Инсулин</td>
                <td class="fs">{{option.name_10}}</td>
                <td class="fs">
                  <p class="m-0 p-0 pb-1">2.0 - 25.0 мкМЕ/мл</p>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
        
      </div>
    </div>
    <div class="d-flex justify-content-center">
      <div style="width:83%; text-align:right; position:fixed; bottom:10px; right:108px;" v-if="font_size == false">
        <mdb-btn color="success px-4 py-1" style="font-size:15px;" type="submit" @click="printed"><mdb-icon icon="print" /></mdb-btn>
        <mdb-btn color="primary px-4 py-1" style="font-size:15px;" v-if="option.sended_status==false" type="submit" @click="send"><mdb-icon color="white"  fab icon="telegram-plane" /></mdb-btn>
      </div>
    </div>
  </div>
</template>

<script>
import html2canvas from "html2canvas"
// import jsPDF from "jspdf"
// import domtoimage from "dom-to-image";
import { mdbBtn, mdbIcon} from 'mdbvue';

export default {
 async mounted() {
    this.$refs.print.focus();
  },
   components: {
      mdbBtn, mdbIcon
    },
  data(){
    return{
      option: {},
      id: this.$route.params.id,
      base64: '',
      loading: false,
      date: '',
      font_size: true,
      borndate: '',
      simbol: '<'
    }
  },
  async created(){
    if(this.id>0){
      this.loading =true;
      const response = await fetch(this.$store.state.hostname + '/HospitalAnaliz_5/getFullInfoById?id=' + this.id)
      const data = await response.json();
      this.loading = false;
      console.log(data)
      this.option = data;
      this.borndate = data.patients.bornDate.slice(0,4);
      this.date = this.option.created_date_time.slice(0,10)

      await this.sendPdfTeleg();
    }
  },
  methods: {
    printed(){
      window.print();
    },
    async sendPdfTeleg(){
        

        // this.loading = true;
        this.$nextTick(() => {
             // captureId is the page that needs to be downloaded
        html2canvas(document.querySelector('#table')).then(canvas => {
            // console.log(canvas.toDataURL())
            var img = document.querySelector("#image");
            img.src = canvas.toDataURL();
            this.font_size = false;
            img.onload = () => {
              console.log('dasdasdas')
              // this.send()
            };
            // this.loading = false;

        })
      })

        
     
      // console.log(doc)
    },
    async send(){
      var baseImg = document.querySelector("#image");
      this.base64 =await baseImg.src
      // console.log(this.base64)
      const requestOptions = {
        method : "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
        "analiz_name": "Иммуноферментный анализ крови на гормоны",
        "analiz_base_str": this.base64,
        "id": 0,
        "patientsId": this.option.patientsId,
        "sended_status":  true,
        "doctor_name": 'qon_garmon_print',
        "creator_doctor_name": localStorage.docName
        })
      };
      this.loading = true;
      const response = await fetch(this.$store.state.hostname + '/HospitalPatientAnalizHistory', requestOptions)
      const data = await response.json()
      console.log(data)
      if(data.id){
        this.$router.back()

      }
      else{
        // this.$refs.msg.error('Error_successfully')
        this.loading = false;
        // this.modal_info = data.detail + "    (" + data.routine + ")";
        // this.modal_status = true;
        return false;
      }
    }

  },
}
</script>

<style >
@import url(../../style/style.css);
th, td{
  padding: 2px 8px;
}
.headerTitle{
  font-size:50px;
}
.headerTitle1{
  font-size:45px;
}





</style>