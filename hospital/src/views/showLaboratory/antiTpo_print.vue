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
          <h3 style="width:100%; text-align:center; font-weight:bold; " class="mb-5 headerTitle">Кондаги гормонларни текшириш</h3>
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
              
              <tr>
                <td class="fs">1</td>
                <td class="fs">Тиретроп гормон /ТТГ/</td>
                <td class="fs">{{option.name_1}}</td>
                <td class="fs">0,35-5,1  мкМЕ/мл
                </td>
              </tr>
              <tr>
                <td class="fs">2</td>
                <td class="fs">ДЕАС (Дегидроепиандростерон)</td>
                <td class="fs">{{option.name_2}}</td>
                <td class="fs">Мужчины : 15-498  мкг/дл
                  Женшины : 32-400мкг/дл
                </td>
              </tr>
              <tr>
                <td class="fs">3</td>
                <td class="fs">Общий трийодтиронин/Т3/  </td>
                <td class="fs">{{option.name_3}}</td>
                <td class="fs">
                  0,58-1,62 нг/мл
                </td>
              </tr>
              <!-- <tr>
                <td class="fs">4</td>
                <td class="fs">Трийодтиронин свободный /Т3/</td>
                <td class="fs">{{option.svabodniy_t3}}</td>
                <td class="fs">
                </td>
              </tr> -->
              <!-- <tr>
                <td class="fs">5</td>
                <td class="fs">Тироксин /Т4/ </td>
                <td class="fs">{{option.t_4}}</td>
                <td class="fs">4,7-12,4 mkg/dl
                </td>
              </tr> -->
              <tr>
                <td class="fs">4</td>
                <td class="fs">Тироксин /Т4 свободного/</td>
                <td class="fs">{{option.name_4}}</td>
                <td class="fs"> 0,5-1,4 нг/мл
                </td>
              </tr>
              <tr>
                <td class="fs">5</td>
                <td class="fs">Тестостерон</td>
                <td class="fs">{{option.name_5}}</td>
                <td class="fs">Мужчины :2,27-9,76 нг/мл
                  Женщины  ≤1,23 нг/мл
                </td>
              </tr>
              <tr>
                <td class="fs">6</td>
                <td class="fs">Эстрадиол </td>
                <td class="fs">{{option.name_6}}</td>
                <td class="fs"><p class="fs m-0">Мужчины  28-84 пг/мл </p>
                    <p class="fs m-0">Женщины:фолликулярная20-138 пг/мл</p>
                    <p class="fs m-0">овуляция 100-440 пг/мл</p>
                    <p class="fs m-0">созревания желтова тела 31-317 пг/мл</p>
                    <p class="fs m-0">постменопауза 25-84</p>
                </td>
              </tr>
              <tr>
                <td class="fs">7</td>
                <td class="fs">Пролактин</td>
                <td class="fs">{{option.name_7}}</td>
                <td class="fs">   Мужчины:  3,0-16,5 нг/мл
                  Женщины: в пре-менопауза 3,8-30,7нг/мл
                  В пост-менопауза 3,2-24,9 нг/мл
                </td>
              </tr>
              <tr>
                <td class="fs">8</td>
                <td class="fs">ФСГ</td>
                <td class="fs">{{option.name_8}}</td>
                <td class="fs">Мужчины:  1,5-12,4  мМЕ/мл
                  Женщины : фолликулярная 3,5-12,5 мМЕ/мл
                  Период овуляция  4,7-21,5 мМЕ/мл
                  Лютенизирующая ф 1,7-7,7 мМЕ/мл
                  постменопауза 25,8-134,8 мМЕ/мл
                </td>
              </tr>
              <tr>
                <td class="fs">9</td>
                <td class="fs">ЛГ</td>
                <td class="fs">{{option.name_9}}</td>
                <td class="fs">Мужчины:  1,4-7,7 мМЕ/мл
                    Женщины : фолликулярная  1,9-11,6 мМЕ/мл
                    созревания желтова тела 0,8-10,5 мМЕ/мл
                    перед овуляция 12,9-105,2 мМЕ/мл
                    пост менопауза 6,64-66,4 мМЕ/мл

                </td>
              </tr>
              <tr>
                <td class="fs">10</td>
                <td class="fs">Anti-TPO </td>
                <td class="fs">{{option.name_10}}</td>
                <td class="fs"> 9 - ME/мл 
                </td>
              </tr>
              <tr>
                <td class="fs">11</td>
                <td class="fs">Прогестерон</td>
                <td class="fs">{{option.name_11}}</td>
                <td class="fs"> Мужчины:  0,1-2,1 нг/мл
                  Женщины :не беременные :  
                  фолликулярная 0,2- 1,6 нг/мл
                  овуляция 0,3-2,1 нг/мл
                  созревания желтова тела 1,8-22,5 нг/мл
                  постменопауза :≤1,05 нг/мл
                  беременость ж : 
                  првый триместр 3,9-60 нг/мл 
                  второй беременности 15,4-60 нг/мл 
                </td>
              </tr>
              
            </tbody>
          </table>
        </div>
        
      </div>
    </div>
    <div class="d-flex justify-content-center">
      <div v-show="false" style="width:83%; text-align:right; position:fixed; bottom:10px; right:108px;" v-if="font_size == false">
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
    }
  },
  async created(){
    if(this.id>0){
      this.loading =true;
      const response = await fetch(this.$store.state.hostname + '/HospitalAnaliz_2/getFullInfoById?id=' + this.id)
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
        "analiz_name": "Кондаги гормонлар",
        "analiz_base_str": this.base64,
        "id": 0,
        "patientsId": this.option.patientsId,
        "sended_status":  true,
        "doctor_name": 'antiTpo_print',
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
.fs{
  font-size: 27.5px;
}


.headerTitle{
  font-size:50px;
}
.headerTitle1{
  font-size:45px;
}





</style>