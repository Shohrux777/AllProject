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
      <div class="lab_answer upHeightAllspirma320"  >
        <div class=" " style="display:flex; justify-content:center; flex-direction:column;">
          <h3 style="width:100%; text-align:center; font-weight:bold; " class="mb-5 headerTitle">Биохимический Анализ крови</h3>
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
                <th class="fs font-weight-bold py-2" >СИ бирлик</th>
              </tr>
            </thead>
            <tbody >
              
              <tr v-show="option.name_1 != null && option.name_1 != ''">
                <td class="fs">1</td>
                <td class="fs">Умумий оқсил</td>
                <td class="fs">{{option.name_1}}</td>
                <td class="fs"><p class="m-0 p-0 pb-1">{{simbol}}3 ёш 46-70  > 3 ёш 66 –85 </p>
                </td>
                <td class="fs">г/л</td>
              </tr>
              <tr v-show="option.name_2 != null && option.name_2 != ''">
                <td class="fs">2</td>
                <td class="fs">Альбумин</td>
                <td class="fs">{{option.name_2}}</td>
                <td class="fs">35 – 55</td>
                <td class="fs">г/л</td>
              </tr>
              <tr v-show="option.name_3 != null && option.name_3 != ''">
                <td class="fs">3</td>
                <td class="fs">Холестерин (умумий)</td>
                <td class="fs">{{option.name_3}}</td>
                <td class="fs">
                  <span>{{simbol}}</span> 5,2
                </td>
                <td class="fs">ммоль/л</td>
              </tr>
              <tr v-show="option.name_4 != null && option.name_4 != ''">
                <td class="fs">4</td>
                <td class="fs">Триглицеридлар</td>
                <td class="fs">{{option.name_4}}</td>
                <td class="fs"> <span>{{simbol}}</span> 2,28
                </td>
                <td class="fs">ммоль/л</td>
              </tr>
              <tr v-show="option.name_5 != null && option.name_5 != ''">
                <td class="fs">5</td>
                <td class="fs">Глюкоза</td>
                <td class="fs">{{option.name_5}}</td>
                <td class="fs">3,2 – 6,1
                </td>
                <td class="fs">ммоль/л</td>
              </tr>
              <tr v-show="option.name_6 != null && option.name_6 != ''">
                <td class="fs">6</td>
                <td class="fs">Мочевина</td>
                <td class="fs">{{option.name_6}}</td>
                <td class="fs">2,5 – 8,3
                </td>
                <td class="fs">ммоль/л</td>
              </tr>
              <tr v-show="option.name_7 != null && option.name_7 != ''">
                <td class="fs">7</td>
                <td class="fs">Креатинин</td>
                <td class="fs">{{option.name_7}}</td>
                <td class="fs"> Э: 44 – 115
                    А: 44 – 97
                </td>
                <td class="fs">мкмоль/л</td>
              </tr>
              <tr v-show="option.name_8 != null && option.name_8 != ''">
                <td class="fs">8</td>
                <td class="fs">Билирубин: Умумий</td>
                <td class="fs">{{option.name_8}}</td>
                <td class="fs"> 3,4 – 20,5
                </td>
                <td class="fs">мкмоль/л</td>
              </tr>
              <tr v-show="option.name_9 != null && option.name_9 != ''">
                <td class="fs">9</td>
                <td class="fs">Билирубин: Боғланган</td>
                <td class="fs">{{option.name_9}}</td>
                <td class="fs"> 0,86 – 5,3
                </td>
                <td class="fs">мкмоль/л</td>
              </tr>
              <tr v-show="option.name_10 != null && option.name_10 != ''">
                <td class="fs">10</td>
                <td class="fs">Билирубин: Эркин</td>
                <td class="fs">{{option.name_10}}</td>
                <td class="fs"> 1,7 – 17,1
                </td>
                <td class="fs">мкмоль/л</td>
              </tr>
              <tr v-show="option.name_11 != null && option.name_11 != ''">
                <td class="fs">11</td>
                <td class="fs">Аланинаминотрансфераза (АЛТ)</td>
                <td class="fs">{{option.name_11}}</td>
                <td class="fs"> {{simbol}} 40
                </td>
                <td class="fs">Ед/л</td>
              </tr>
              <tr v-show="option.name_12 != null && option.name_12 != ''">
                <td class="fs">12</td>
                <td class="fs">Аспартатаминотрансфераза (АСТ)</td>
                <td class="fs">{{option.name_12}}</td>
                <td class="fs"> {{simbol}} 35
                </td>
                <td class="fs">Ед/л</td>
              </tr>
              <tr v-show="option.name_13 != null && option.name_13 != ''">
                <td class="fs">13</td>
                <td class="fs">Лактатдегидрогеназа (ЛДГ)</td>
                <td class="fs">{{option.name_13}}</td>
                <td class="fs">
                  <p class="m-0 p-0 pb-1">225-450</p>
                </td>
                <td class="fs">Ед/л</td>
              </tr>
              <tr v-show="option.name_14 != null && option.name_14 != ''">
                <td class="fs">14</td>
                <td class="fs">Гаммаглутамилтрансфераза (ГГТ)</td>
                <td class="fs">{{option.name_14}}</td>
                <td class="fs"> 
                  <p class="m-0 p-0 pb-1">Э: 11-61</p>
                  <p class="m-0 p-0 pb-1">А: 9-39</p>
                </td>
                <td class="fs">Ед/л</td>
              </tr>
              <tr v-show="option.name_15 != null && option.name_15 != ''">
                <td class="fs">15</td>
                <td class="fs">α-амилаза</td>
                <td class="fs">{{option.name_15}}</td>
                <td class="fs"> 0 – 220
                </td>
                <td class="fs">Ед/л</td>
              </tr>
              <tr v-show="option.name_16 != null && option.name_16 != ''">
                <td class="fs">16</td>
                <td class="fs">Ишқорий фосфатаза (ИФ)</td>
                <td class="fs">{{option.name_16}}</td>
                <td class="fs"> {{simbol}}15 ёш  {{simbol}}644 15-17 ёш {{simbol}}483
                </td>
                <td class="fs">Ед/л</td>
              </tr>
              <tr v-show="option.name_17 != null && option.name_17 != ''">
                <td class="fs">17</td>
                <td class="fs">Калий</td>
                <td class="fs">{{option.name_17}}</td>
                <td class="fs"> 3,6 – 5,4
                </td>
                <td class="fs">ммоль/л</td>
              </tr>
              <tr v-show="option.name_18 != null && option.name_18 != ''">
                <td class="fs">18</td>
                <td class="fs">Натрий</td>
                <td class="fs">{{option.name_18}}</td>
                <td class="fs"> 135 - 150
                </td>
                <td class="fs">ммоль/л</td>
              </tr>
              <tr v-show="option.name_19 != null && option.name_19 != ''">
                <td class="fs">19</td>
                <td class="fs">Кальций</td>
                <td class="fs">{{option.name_19}}</td>
                <td class="fs"> 2,0 – 2,6
                </td>
                <td class="fs">ммоль/л</td>
              </tr>
              <tr v-show="option.name_20 != null && option.name_20 != ''">
                <td class="fs">20</td>
                <td class="fs">Темир</td>
                <td class="fs">{{option.name_20}}</td>
                <td class="fs"> 6,6-27
                </td>
                <td class="fs">мкмоль/л</td>
              </tr>
              <tr v-show="option.name_21 != null && option.name_21 != ''">
                <td class="fs">21</td>
                <td class="fs">Фосфор</td>
                <td class="fs">{{option.name_21}}</td>
                <td class="fs"> 4,0 – 7,0
                </td>
                <td class="fs">мг/дл</td>
              </tr>
              <tr v-show="option.name_22 != null && option.name_22 != ''">
                <td class="fs">22</td>
                <td class="fs">Хлор</td>
                <td class="fs">{{option.name_22}}</td>
                <td class="fs"> 98-106
                </td>
                <td class="fs">ммоль/л</td>
              </tr>
              <tr v-show="option.name_23 != null && option.name_23 != ''">
                <td class="fs">23</td>
                <td class="fs">Глюкоза (оч коринга)</td>
                <td class="fs">{{option.name_23}}</td>
                <td class="fs">3,2 – 6,1
                </td>
                <td class="fs">ммоль/л</td>
              </tr>
              <tr v-show="option.name_24 != null && option.name_24 != ''">
                <td class="fs">24</td>
                <td class="fs">Глюкоза  (тук коринга)</td>
                <td class="fs">{{option.name_24}}</td>
                <td class="fs">3,2 – 6,1
                </td>
                <td class="fs">ммоль/л</td>
              </tr>
              <!-- <tr>
                <td class="fs">23</td>
                <td class="fs">Хлор</td>
                <td class="fs">{{option.name_23}}</td>
                <td class="fs"> 98-106
                </td>
                <td class="fs"></td>
              </tr>
              <tr>
                <td class="fs">24</td>
                <td class="fs">Скорость Оседания Эритроцитов (COЭ)</td>
                <td class="fs">{{option.name_24}}</td>
                <td class="fs"> 
                  <p class="m-0 p-0 pb-1">М:0-10Мм/час</p>
                  <p class="m-0 p-0 pb-1">Ж: 0-15 Мм/час</p>
                </td>
                <td class="fs"></td>
              </tr> -->
              
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
      const response = await fetch(this.$store.state.hostname + '/HospitalAnaliz_6/getFullInfoById?id=' + this.id)
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
        "analiz_name": "Биохимический Анализ крови",
        "analiz_base_str": this.base64,
        "id": 0,
        "patientsId": this.option.patientsId,
        "sended_status":  true,
        "doctor_name": 'biox_krov_print',
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