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
          <h3 style="width:100%; text-align:center; font-weight:bold;" class="mb-4 headerTitle2">МАЗОК НА СТЕПЕНЬ ЧИСТОТЫ</h3>
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
                <th class="fs font-weight-bold py-2 " rowspan="2" >№</th>
                <th class="fs font-weight-bold py-2" rowspan="2">ПОКАЗАТЕЛИ</th>
                <th class="fs font-weight-bold text-center py-2" colspan="3" >ОТДЕЛЯЕМОЕ ИЗ:</th>
                <!-- <th class="fs font-weight-bold py-2" >СИ бирлик</th> -->
              </tr>
              <tr>
                <th class="fs font-weight-bold text-center py-2">V</th>
                <th class="fs font-weight-bold text-center py-2">C</th>
                <th class="fs font-weight-bold text-center py-2">U</th>
              </tr>
            </thead>
            <tbody>
              
              <tr>
                <td class="fs">1</td>
                <td class="fs">Микрофлора</td>
                <td class="fs">{{option.name_1}}</td>
                <td class="fs">{{option.name_2}}</td>
                <td class="fs">{{option.name_3}}</td>
                <!-- <td class="fs"><p class="m-0 p-0 pb-1"></p></td> -->
              </tr>
              <tr>
                <td class="fs">2</td>
                <td class="fs">Эпителий </td>
                <td class="fs">{{option.name_4}}</td>
                <td class="fs">{{option.name_5}}</td>
                <td class="fs">{{option.name_6}}</td>
                <!-- <td class="fs">Прозрачность</td> -->
              </tr>
              <tr>
                <td class="fs">3</td>
                <td class="fs">Лейкоциты</td>
                <td class="fs">{{option.name_7}}</td>
                <td class="fs">{{option.name_8}}</td>
                <td class="fs">{{option.name_9}}</td>
                <!-- <td class="fs">
                  солом -желт.
                </td> -->
              </tr>
              <tr>
                <td class="fs">4</td>
                <td class="fs">Эритроциты</td>
                <td class="fs">{{option.name_10}}</td>
                <td class="fs">{{option.name_11}}</td>
                <td class="fs">{{option.name_12}}</td>
                <!-- <td class="fs"> 1000-1030
                </td> -->
              </tr>
              <tr>
                <td class="fs">5</td>
                <td class="fs">Гонококки</td>
                <td class="fs">{{option.name_13}}</td>
                <td class="fs">{{option.name_14}}</td>
                <td class="fs">{{option.name_15}}</td>
                <!-- <td class="fs">5,0 - 9,0
                </td> -->
              </tr>
              <tr>
                <td class="fs">6</td>
                <td class="fs">Трихомонады</td>
                <td class="fs">{{option.name_16}}</td>
                <td class="fs">{{option.name_17}}</td>
                <td class="fs">{{option.name_18}}</td>
                <!-- <td class="fs">2,5 – 8,3
                </td> -->
              </tr>
              <tr>
                <td class="fs">7</td>
                <td class="fs">Ключевые клетки</td>
                <td class="fs">{{option.name_19}}</td>
                <td class="fs">{{option.name_20}}</td>
                <td class="fs">{{option.name_21}}</td>
                <!-- <td class="fs"> abs
                </td> -->
              </tr>
              <tr>
                <td class="fs">8</td>
                <td class="fs">Лептотрекс</td>
                <td class="fs">{{option.name_22}}</td>
                <td class="fs">{{option.name_23}}</td>
                <td class="fs">{{option.name_24}}</td>
                <!-- <td class="fs"> abs
                </td> -->
              </tr>
              <tr>
                <td class="fs">9</td>
                <td class="fs">Дрожжи</td>
                <td class="fs">{{option.name_25}}</td>
                <td class="fs">{{option.name_26}}</td>
                <td class="fs">{{option.name_27}}</td>
                <!-- <td class="fs"> abs
                </td> -->
              </tr>
              <tr >
                <td class="fs">10</td>
                <td class="fs">Слизь</td>
                <td class="fs">{{option.name_28}}</td>
                <td class="fs">{{option.name_29}}</td>
                <td class="fs">{{option.name_30}}</td>
                <!-- <td class="fs"> ± (норм)
                </td> -->
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
      const response = await fetch(this.$store.state.hostname + '/HospitalAnaliz_19/getFullInfoById?id=' + this.id)
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
              this.send()
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
        "analiz_name": "МАЗОК НА СТЕПЕНЬ ЧИСТОТЫ",
        "analiz_base_str": this.base64,
        "id": 0,
        "patientsId": this.option.patientsId,
        "sended_status":  true,
        "doctor_name": 'mazok_step_print',
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