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
      <div class="lab_answer upHeightAllspirma270"  >
        <div class=" " style="display:flex; justify-content:center; flex-direction:column;">
          <h3 style="width:100%; text-align:center; font-weight:bold; " class="mb-4 headerTitle2">ОБЩИЙ АНАЛИЗ МОЧИ</h3>
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
              <!-- <tr>
                <th class="fs" style="width:45%;"><span class="font-weight-bold">ПРИБОР:</span> Mindray СL 900 i </th>
                <th class="fs"></th>
              </tr> -->
            </thead>
          </table>
          <table border class="allWidthLabTable" style="margin-top:35px;">
            <thead>
              <tr>
                <th class="fs font-weight-bold py-2" >№</th>
                <th class="fs font-weight-bold py-2" >ИССЛЕДОВАНИЕ</th>
                <th class="fs font-weight-bold py-2" >РЕЗУЛЬТАТ</th>
                <th class="fs font-weight-bold py-2" >НОРМА</th>
                <!-- <th class="fs font-weight-bold py-2" >СИ бирлик</th> -->
              </tr>
            </thead>
            <tbody >
              
              <tr>
                <td class="fs">1</td>
                <td class="fs">Количество</td>
                <td class="fs">{{option.name_1}}</td>
                <td class="fs"><p class="m-0 p-0 pb-1"> </p></td>
              </tr>
              <tr>
                <td class="fs">2</td>
                <td class="fs">Прозрачность </td>
                <td class="fs">{{option.name_2}}</td>
                <td class="fs">Прозрачность</td>
              </tr>
              <tr>
                <td class="fs">3</td>
                <td class="fs">Цвет</td>
                <td class="fs">{{option.name_3}}</td>
                <td class="fs">
                  солом -желт.
                </td>
              </tr>
              <tr>
                <td class="fs">4</td>
                <td class="fs">Относительная плотность</td>
                <td class="fs">{{option.name_4}}</td>
                <td class="fs"> 1000-1030
                </td>
              </tr>
              <tr>
                <td class="fs">5</td>
                <td class="fs">РеакцияPh</td>
                <td class="fs">{{option.name_5}}</td>
                <td class="fs">5,0 - 9,0
                </td>
              </tr>
              <tr>
                <td class="fs">6</td>
                <td class="fs">Глюкоза</td>
                <td class="fs">{{option.name_6}}</td>
                <td class="fs">2,5 – 8,3
                </td>
              </tr>
              <tr>
                <td class="fs">7</td>
                <td class="fs">Нитриты</td>
                <td class="fs">{{option.name_7}}</td>
                <td class="fs"> abs
                </td>
              </tr>
              <tr>
                <td class="fs">8</td>
                <td class="fs">Белок</td>
                <td class="fs">{{option.name_8}}</td>
                <td class="fs"> abs
                </td>
              </tr>
              <tr>
                <td class="fs">9</td>
                <td class="fs">Кетоновые тела</td>
                <td class="fs">{{option.name_9}}</td>
                <td class="fs"> abs
                </td>
              </tr>
              <tr >
                <td class="fs">10</td>
                <td class="fs">Уробилиноген</td>
                <td class="fs">{{option.name_10}}</td>
                <td class="fs"> abs
                </td>
              </tr>
              <tr >
                <td class="fs">11</td>
                <td class="fs">Билирубин</td>
                <td class="fs">{{option.name_11}}</td>
                <td class="fs"> abs
                </td>
              </tr>
              <tr >
                <td class="fs">12</td>
                <td class="fs">Эпителий:плоский</td>
                <td class="fs">{{option.name_12}}</td>
                <td class="fs"> 2 - 3 в поле зрения
                </td>
              </tr>
              <tr >
                <td class="fs">13</td>
                <td class="fs">переходной </td>
                <td class="fs">{{option.name_13}}</td>
                <td class="fs">
                  <p class="m-0 p-0 pb-1">abs</p>
                </td>
              </tr>
              <tr >
                <td class="fs">14</td>
                <td class="fs">почечный</td>
                <td class="fs">{{option.name_14}}</td>
                <td class="fs"> 
                  <p class="m-0 p-0 pb-1">abs</p>
                </td>
              </tr>
              <tr >
                <td class="fs">15</td>
                <td class="fs">Лейкоциты </td>
                <td class="fs">{{option.name_15}}</td>
                <td class="fs">
                  <p class="m-0 p-0 pb-1">Муж. 0 - 2 в поле зрения</p>
                  <p class="m-0 p-0 pb-1">Жен. 1 - 2 в поле зрения</p>
                </td>
              </tr>
              <tr >
                <td class="fs">16</td>
                <td class="fs">Эритроциты:неизмененные </td>
                <td class="fs">{{option.name_16}}</td>
                <td class="fs"> 0-1
                </td>
              </tr>
              <tr >
                <td class="fs">17</td>
                <td class="fs">измененные</td>
                <td class="fs">{{option.name_17}}</td>
                <td class="fs"> abs
                </td>
              </tr>
              <tr >
                <td class="fs">18</td>
                <td class="fs">Цилиндры:гиалиновые </td>
                <td class="fs">{{option.name_18}}</td>
                <td class="fs">abs
                </td>
              </tr>
              <tr >
                <td class="fs">19</td>
                <td class="fs">зернистые</td>
                <td class="fs">{{option.name_19}}</td>
                <td class="fs"> abs
                </td>
              </tr>
              <tr >
                <td class="fs">20</td>
                <td class="fs">восковидные</td>
                <td class="fs">{{option.name_20}}</td>
                <td class="fs"> abs
                </td>
              </tr>
              <tr >
                <td class="fs">21</td>
                <td class="fs">эпителиальные</td>
                <td class="fs">{{option.name_21}}</td>
                <td class="fs"> abs
                </td>
              </tr>
              <tr >
                <td class="fs">22</td>
                <td class="fs">оксалаты</td>
                <td class="fs">{{option.name_22}}</td>
                <td class="fs"> abs
                </td>
              </tr>
              <tr >
                <td class="fs">23</td>
                <td class="fs">эритроцитарные</td>
                <td class="fs">{{option.name_23}}</td>
                <td class="fs"> abs
                </td>
              </tr>
              <tr >
                <td class="fs">24</td>
                <td class="fs">лейкоцитарные</td>
                <td class="fs">{{option.name_24}}</td>
                <td class="fs"> abs
                </td>
              </tr>
              <tr >
                <td class="fs">25</td>
                <td class="fs">Крист мочевой кислоты</td>
                <td class="fs">{{option.name_25}}</td>
                <td class="fs"> abs
                </td>
              </tr>
              <tr >
                <td class="fs">26</td>
                <td class="fs">Неорганич. осадок:ураты</td>
                <td class="fs">{{option.name_26}}</td>
                <td class="fs"> abs
                </td>
              </tr>
              <tr >
                <td class="fs">27</td>
                <td class="fs">аморфные фосфаты </td>
                <td class="fs">{{option.name_27}}</td>
                <td class="fs"> abs
                </td>
              </tr>
              <tr >
                <td class="fs">28</td>
                <td class="fs">мочекислый аммоний</td>
                <td class="fs">{{option.name_28}}</td>
                <td class="fs"> abs
                </td>
              </tr>
              <tr >
                <td class="fs">29</td>
                <td class="fs">трипельфосфат</td>
                <td class="fs">{{option.name_29}}</td>
                <td class="fs"> abs
                </td>
              </tr>
              <tr >
                <td class="fs">30</td>
                <td class="fs">Слизь</td>
                <td class="fs">{{option.name_30}}</td>
                <td class="fs"> abs
                </td>
              </tr>
              <tr >
                <td class="fs">31</td>
                <td class="fs">Бактерии</td>
                <td class="fs">{{option.name_31}}</td>
                <td class="fs"> abs
                </td>
              </tr>
              <tr >
                <td class="fs">32</td>
                <td class="fs">Соли</td>
                <td class="fs">{{option.name_32}}</td>
                <td class="fs"> abs
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
      const response = await fetch(this.$store.state.hostname + '/HospitalAnaliz_18/getFullInfoById?id=' + this.id)
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
        "analiz_name": "ОБЩИЙ АНАЛИЗ МОЧИ",
        "analiz_base_str": this.base64,
        "id": 0,
        "patientsId": this.option.patientsId,
        "sended_status":  true,
        "doctor_name": 'obshey_mochi_print',
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
.headerTitle2{
  font-size:30px;
}





</style>