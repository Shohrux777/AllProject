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
      <div class="lab_answer upHeight" >
        <div class=" " style="display:flex; justify-content:center; flex-direction:column;">
          <h3 style="width:100%; text-align:center; font-weight: bold;" class="mb-5 headerTitle">Анализ витамин – Д</h3>
          <table border class="allWidthLabTable">
            <thead >
              <tr>
                <th class="fs" style="width:45%;"  ><span class="font-weight-bold ">ПАЦИЕНТ:</span>  {{option.patient_name}}</th>
                <th class="fs"  ><span class="font-weight-bold ">ВРАЧ:</span>  {{option.doctor_name}}</th>
              </tr>
              <tr>
                <th class="fs"   style="width:45%;"><span class="font-weight-bold ">ВОЗРАСТ:</span> {{borndate}} </th>
                <th class="fs"  ><span class="font-weight-bold ">ДАТА:</span> {{date}}</th>
              </tr>
              <tr>
                <th class="fs" style="width:45%;"><span class="font-weight-bold">ПРИБОР:</span> Mindray СL 900 i </th>
                <th class="fs"> </th>
              </tr>
            </thead>
          </table>
          <table border class="allWidthLabTable" style="margin-top:35px;">
            <thead>
              <tr>
                <th class="font-weight-bold fs">ИССЛЕДОВАНИЕ</th>
                <th class="fs"  ><span class="font-weight-bold ">Результат</span></th>
                <th class="fs" style="width:30%;"><span class="font-weight-bold py-2">Норма</span></th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td class="fs">Витамин – Д</td>
                <td class="fs">{{option.name_32}}</td>
                <td class="fs" >Низкий : <span>{{spr}}</span>  20 нг/мл  Недостаточный :  20-30 нг/мл  Достаточный : > 30 нг/мл</td>
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
      patient_name: 'Hakim hamid ogli',
      id: this.$route.params.id,
      base64: '',
      loading: false,
      date: '',
      font_size: true,
      test: 0,
      borndate: '',
      spr: '<'
      
     
    }
  },
  async created(){
    if(this.id>0){
      this.loading =true;
      const response = await fetch(this.$store.state.hostname + '/HospitalAnaliz_1/getFullInfoById?id=' + this.id)
      const data = await response.json();
      this.loading = false;
      console.log(data)
      this.option = data;
      this.borndate = this.option.patients.bornDate.slice(0,4);
      this.date = this.option.created_date_time.slice(0,10)

      await this.sendPdfTeleg();
      this.font_size = false
    }
  },
  methods: {
    printed(){
      window.print();
    },
    async sendPdfTeleg(){
      console.log('ishladi')


          // await domtoimage
          // .toPng(this.$refs.pdfID)
          // .then(function(dataUrl) {
          //   var img = document.querySelector("#image");
          //   img.src = dataUrl;
          // console.log(dataUrl)

          // const doc = new jsPDF({
          //   orientation: 'p',
          //   unit: 'mm',
          //   format: 'a3',
          //   putOnlyUsedFonts:true
          // });
          // doc.addImage(img, "JPEG", 1, 1);
          // const date = new Date();
          // const filename =
          //   "timechart_" +
          //   date.getFullYear() +
          //   ("0" + (date.getMonth() + 1)).slice(-2) +
          //   ("0" + date.getDate()).slice(-2) +
          //   ("0" + date.getHours()).slice(-2) +
          //   ("0" + date.getMinutes()).slice(-2) +
          //   ("0" + date.getSeconds()).slice(-2) +
          //   ".pdf";
          // doc.save(filename);

          // })
          // .catch(function(error) {
          //   console.error("oops, something went wrong!", error);
          // });

        

        // this.loading = true;
        await this.$nextTick(() => {
             // captureId is the page that needs to be downloaded
         html2canvas(document.querySelector('#table')).then(canvas => {
            console.log(canvas.toDataURL())
            var img = document.querySelector("#image");
            img.src = canvas.toDataURL();
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
      this.base64 = await baseImg.src
      console.log(this.base64)
      const requestOptions = {
        method : "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
        "analiz_name": "Анализ витамин – Д",
        "analiz_base_str": this.base64,
        "id": 0,
        "patientsId": this.option.patientsId,
        "sended_status":  true,
        "doctor_name": "vitD_print",
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
  font-size: 29px;
}

.headerTitle{
  font-size:50px;
}

</style>