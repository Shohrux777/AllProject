<template>
    <div v-if="show_page">
      <div class="rv_lab allWidthLab" id="table" style="position:relative;">
        <input type="text" style="position:absolute; top: -500px; outline:none;border:none;" ref="print" @keyup.enter="send">
        <div class="img " style="text-align:center;" v-show="false">
          <img id="image" width="100%" height="25%" alt="">
        </div>
        <div class="qr-code_absolute" v-show="false">
          <qr-code :text="lab_url" :size="size" error-level="L"></qr-code>
        </div>
        <div class="img " style="text-align:center;" >
          <img src="../../../assets/lab1.png" width="100%" alt="">
        </div>
        <loader v-if="loading"/>
        <div class="lab_answer upHeightAllspirma320" >
          <div class=" " style="display:flex; justify-content:center; flex-direction:column;">
            <table border class="allWidthLabTable" >
              <thead >
                <tr>
                  <th class="fs bg_headertable text-center" style="width:350px;" ><span class="font-weight-bold">ПАЦИЕНТ</span></th>
                  <th class="fs text-center">{{patient_name}}</th>
                  <th class="fs text-center bg_headertable" style="width:200px;" rowspan="3"> Результаты анализов <p>ID: {{patient_id}}</p> </th>
                </tr>
                <tr>
                  <th class="fs bg_headertable text-center" style="width:350px;"><span class="font-weight-bold">ВОЗРАСТ</span>  </th>
                  <th class="fs text-center">{{borndate}}</th>
                </tr>
                <tr>
                  <th class="fs bg_headertable text-center" style="width:350px;"><span class="font-weight-bold">ДАТА</span> </th>
                  <th class="fs text-center"> {{date}}</th>
                </tr>
                <!-- <tr>
                  <th v-if="option.hospitalAnalizDynamicmain.name_1" class="fs" style="width:45%;"><span class="font-weight-bold">ПРИБОР:</span> {{option.hospitalAnalizDynamicmain.name_1}} </th>
                  <th v-if="option.hospitalAnalizDynamicmain.name_3" class="fs" style="width:45%;"><span class="font-weight-bold">Паспорт</span> {{option.hospitalAnalizDynamicmain.name_3}} </th>
                </tr> -->
              </thead>
            </table>

            <!-- <table v-if="option.hospitalAnalizDynamicmain.name_25" border class="allWidthLabTable" style="margin-top:40px;">
              <thead>
                <tr>
                  <th v-if="option.hospitalAnalizDynamicmain.name_5" class="fs" style="width:45%;"> {{option.hospitalAnalizDynamicmain.name_5}} </th>
                  <th v-if="option.hospitalAnalizDynamicmain.name_6" class="fs" > {{option.hospitalAnalizDynamicmain.name_6}} </th>
                </tr>
                <tr>
                  <th v-if="option.hospitalAnalizDynamicmain.name_7" class="fs" style="width:45%;"> {{option.hospitalAnalizDynamicmain.name_7}} </th>
                  <th v-if="option.hospitalAnalizDynamicmain.name_8" class="fs" > {{option.hospitalAnalizDynamicmain.name_8}} </th>
                </tr>
                <tr>
                  <th v-if="option.hospitalAnalizDynamicmain.name_9" class="fs" style="width:45%;"> {{option.hospitalAnalizDynamicmain.name_9}} </th>
                  <th v-if="option.hospitalAnalizDynamicmain.name_10" class="fs" > {{option.hospitalAnalizDynamicmain.name_10}} </th>
                </tr>
                <tr>
                  <th v-if="option.hospitalAnalizDynamicmain.name_11" class="fs" style="width:45%;"> {{option.hospitalAnalizDynamicmain.name_11}} </th>
                  <th v-if="option.hospitalAnalizDynamicmain.name_12" class="fs" > {{option.hospitalAnalizDynamicmain.name_12}} </th>
                </tr>
              </thead>
            </table> -->
            <h5 style="width:100%; text-align:center; font-weight:bold; " class="mb-2 mt-3 headerTitle">{{ option.name_title }}</h5>
            <p v-if="option.hospitalAnalizDynamicmain.name_1" style="margin-top:15px;" class="m-0 p-0 text-right"><span ></span> {{option.hospitalAnalizDynamicmain.name_1}} </p>
            <table border class="allWidthLabTable" style="margin-top:0px;">
              <thead>
                <tr>
                  <!-- <th class="fs font-weight-bold py-2" style="width:40px;">№</th> -->
                  <th class="fs font-weight-bold py-2" v-if="option.firs_name_status">{{ option.first_name }}</th>
                  <th class="fs font-weight-bold py-2" v-if="option.lab_name_status">{{ option.lab_name }}</th>
                  <th class="fs font-weight-bold py-2" v-if="option.hospitalAnalizDynamicmain.name_2">{{ option.hospitalAnalizDynamicmain.name_2 }}</th>
                  <th class="fs font-weight-bold py-2" v-else>Результат</th>
                  <th class="fs font-weight-bold py-2" v-if="option.norma_status">{{ option.norma }}</th>
                  <th class="fs font-weight-bold py-2" v-if="option.unit_measurment_status">{{ option.unit_measurment }}</th>
                </tr>
              </thead>
              <tbody>
                <tr v-if="option.name_1 && get_lab_center_list[0].name_1">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_1}}</th>
                </tr>
                <tr v-show="option.name_1 != null && option.name_1 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_1}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_1 }}</td>
                  <td class="fs">{{option.name_1}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_1"></td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_1}} </td>
                </tr>
                
                <tr v-if="option.name_2 && get_lab_center_list[0].name_2">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_2}}</th>
                </tr>
                <tr v-show="option.name_2 != null && option.name_2 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_2}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_2 }}</td>
                  <td class="fs">{{option.name_2}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_2"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_2}} </td>
                </tr>
                
                <tr v-if="option.name_3 && get_lab_center_list[0].name_3">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_3}}</th>
                </tr>
                <tr v-show="option.name_3 != null && option.name_3 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_3}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_3 }}</td>
                  <td class="fs">{{option.name_3}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_3"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_3}} </td>
                </tr>
                
                <tr v-if="option.name_4 && get_lab_center_list[0].name_4">
                    <th class="fs text-center bgCenter font-weight-bold" colspan="6">{{get_lab_center_list[0].name_4}}</th>
                </tr>
                <tr v-show="option.name_4 != null && option.name_4 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_4}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_4 }}</td>
                  <td class="fs">{{option.name_4}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_4"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_4}} </td>
                </tr>
                
                <tr v-if="option.name_5 && get_lab_center_list[0].name_5">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_5}}</th>
                </tr>
                <tr v-show="option.name_5 != null && option.name_5 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_5}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_5 }}</td>
                  <td class="fs">{{option.name_5}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_5"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_5}} </td>
                </tr>
                
                <tr v-if="option.name_6 && get_lab_center_list[0].name_6">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_6}}</th>
                </tr>
                <tr v-show="option.name_6 != null && option.name_6 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_6}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_6 }}</td>
                  <td class="fs">{{option.name_6}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_6"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_6}} </td>
                </tr>
                
                <tr v-if="option.name_7 && get_lab_center_list[0].name_7">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_7}}</th>
                </tr>
                <tr v-show="option.name_7 != null && option.name_7 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_7}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_7 }}</td>
                  <td class="fs">{{option.name_7}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_7"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_7}} </td>
                </tr>
                
                <tr v-if="option.name_8 && get_lab_center_list[0].name_8">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_8}}</th>
                </tr>
                <tr v-show="option.name_8 != null && option.name_8 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_8}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_8 }}</td>
                  <td class="fs">{{option.name_8}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_8"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_8}} </td>
                </tr>
                
                <tr v-if="option.name_9 && get_lab_center_list[0].name_9">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_9}}</th>
                </tr>
                <tr v-show="option.name_9 != null && option.name_9 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_9}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_9 }}</td>
                  <td class="fs">{{option.name_9}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_9"></td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_9}} </td>
                </tr>
                
                <tr v-if="option.name_10 && get_lab_center_list[0].name_10">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_10}}</th>
                </tr>
                <tr v-show="option.name_10 != null && option.name_10 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_10}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_10 }}</td>
                  <td class="fs">{{option.name_10}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_10"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_10}} </td>
                </tr>
                
                <tr v-if="option.name_11 && get_lab_center_list[0].name_11">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_11}}</th>
                </tr>
                <tr v-show="option.name_11 != null && option.name_11 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_11}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_11 }}</td>
                  <td class="fs">{{option.name_11}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_11"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_11}} </td>
                </tr>
                
                <tr v-if="option.name_12 && get_lab_center_list[0].name_12">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_12}}</th>
                </tr>
                <tr v-show="option.name_12 != null && option.name_12 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_12}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_12 }}</td>
                  <td class="fs">{{option.name_12}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_12"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_12}} </td>
                </tr>
                
                <tr v-if="option.name_13 && get_lab_center_list[0].name_13">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_13}}</th>
                </tr>
                <tr v-show="option.name_13 != null && option.name_13 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_13}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_13 }}</td>
                  <td class="fs">{{option.name_13}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_13"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_13}} </td>
                </tr>
                
                <tr v-if="option.name_14 && get_lab_center_list[0].name_14">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_14}}</th>
                </tr>
                <tr v-show="option.name_14 != null && option.name_14 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_14}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_14 }}</td>
                  <td class="fs">{{option.name_14}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_14"></td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_14}} </td>
                </tr>
                
                <tr v-if="option.name_15 && get_lab_center_list[0].name_15">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_15}}</th>
                </tr>
                <tr v-show="option.name_15 != null && option.name_15 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_15}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_15 }}</td>
                  <td class="fs">{{option.name_15}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_15"></td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_15}} </td>
                </tr>
                
                <tr v-if="option.name_16 && get_lab_center_list[0].name_16">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_16}}</th>
                </tr>
                <tr v-show="option.name_16 != null && option.name_16 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_16}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_16 }}</td>
                  <td class="fs">{{option.name_16}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_16"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_16}} </td>
                </tr>
                
                <tr v-if="option.name_17 && get_lab_center_list[0].name_17">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_17}}</th>
                </tr>
                <tr v-show="option.name_17 != null && option.name_17 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_17}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_17 }}</td>
                  <td class="fs">{{option.name_17}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_17"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_17}} </td>
                </tr>
                
                <tr v-if="option.name_18 && get_lab_center_list[0].name_18">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_18}}</th>
                </tr>
                <tr v-show="option.name_18 != null && option.name_18 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_18}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_18 }}</td>
                  <td class="fs">{{option.name_18}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_18"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_18}} </td>
                </tr>
                
                <tr v-if="option.name_19 && get_lab_center_list[0].name_19">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_19}}</th>
                </tr>
                <tr v-show="option.name_19 != null && option.name_19 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_19}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_19 }}</td>
                  <td class="fs">{{option.name_19}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_19"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_19}} </td>
                </tr>
                
                <tr v-if="option.name_20 && get_lab_center_list[0].name_20">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_20}}</th>
                </tr>
                <tr v-show="option.name_20 != null && option.name_20 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_20}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_20 }}</td>
                  <td class="fs">{{option.name_20}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_20"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_20}} </td>
                </tr>
                
                <tr v-if="option.name_21 && get_lab_center_list[0].name_21">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_21}}</th>
                </tr>
                <tr v-show="option.name_21 != null && option.name_21 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_21}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_21 }}</td>
                  <td class="fs">{{option.name_21}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_21"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_21}} </td>
                </tr>
                
                <tr v-if="option.name_22 && get_lab_center_list[0].name_22">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_22}}</th>
                </tr>
                <tr v-show="option.name_22 != null && option.name_22 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_22}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_22 }}</td>
                  <td class="fs">{{option.name_22}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_22"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_22}} </td>
                </tr>
                
                <tr v-if="option.name_23 && get_lab_center_list[0].name_23">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_23}}</th>
                </tr>
                <tr v-show="option.name_23 != null && option.name_23 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_23}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_23 }}</td>
                  <td class="fs">{{option.name_23}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_23"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_23}} </td>
                </tr>
                
                <tr v-if="option.name_24 && get_lab_center_list[0].name_24">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_24}}</th>
                </tr>
                <tr v-show="option.name_24 != null && option.name_24 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_24}}</td>
                  <td class="fs" v-if="option.lab_name_status" v-html="get_lab_name_list[0].name_24"></td>
                  <td class="fs">{{option.name_24}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_24"></td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_24}}</td>
                </tr>
                
                <tr v-if="option.name_25 && get_lab_center_list[0].name_25">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_25}}</th>
                </tr>
                <tr v-show="option.name_25 != null && option.name_25 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_25}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_25 }}</td>
                  <td class="fs">{{option.name_25}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_25"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_25}} </td>
                </tr>
                
                <tr v-if="option.name_26 && get_lab_center_list[0].name_26">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_26}}</th>
                </tr>
                <tr v-show="option.name_26 != null && option.name_26 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_26}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_26 }}</td>
                  <td class="fs">{{option.name_26}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_26"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_26}} </td>
                </tr>
                
                <tr v-if="option.name_27 && get_lab_center_list[0].name_27">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_27}}</th>
                </tr>
                <tr v-show="option.name_27 != null && option.name_27 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_27}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_27 }}</td>
                  <td class="fs">{{option.name_27}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_27"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_27}} </td>
                </tr>
                
                <tr v-if="option.name_28 && get_lab_center_list[0].name_28">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_28}}</th>
                </tr>
                <tr v-show="option.name_28 != null && option.name_28 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_28}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_28 }}</td>
                  <td class="fs">{{option.name_28}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_28"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_28}} </td>
                </tr>
                
                <tr v-if="option.name_29 && get_lab_center_list[0].name_29">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_29}}</th>
                </tr>
                <tr v-show="option.name_29 != null && option.name_29 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_29}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_29 }}</td>
                  <td class="fs">{{option.name_29}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_29"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_29}} </td>
                </tr>
                
                <tr v-if="option.name_30 && get_lab_center_list[0].name_30">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_30}}</th>
                </tr>
                <tr v-show="option.name_30 != null && option.name_30 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_30}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_30 }}</td>
                  <td class="fs">{{option.name_30}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_30"></td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_30}} </td>
                </tr>
                
                <tr v-if="option.name_31 && get_lab_center_list[0].name_31">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_31}}</th>
                </tr>
                <tr v-show="option.name_31 != null && option.name_31 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_31}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_31 }}</td>
                  <td class="fs">{{option.name_31}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_31"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_31}} </td>
                </tr>
                
                <tr v-if="option.name_32 && get_lab_center_list[0].name_32">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_32}}</th>
                </tr>
                <tr v-show="option.name_32 != null && option.name_32 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_32}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_32 }}</td>
                  <td class="fs">{{option.name_32}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_32"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_32}} </td>
                </tr>
                
                <tr v-if="option.name_33 && get_lab_center_list[0].name_33">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_33}}</th>
                </tr>
                <tr v-show="option.name_33 != null && option.name_33 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_33}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_33 }}</td>
                  <td class="fs">{{option.name_33}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_33"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_33}} </td>
                </tr>
                
                <tr v-if="option.name_34 && get_lab_center_list[0].name_34">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_34}}</th>
                </tr>
                <tr v-show="option.name_34 != null && option.name_34 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_34}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_34 }}</td>
                  <td class="fs">{{option.name_34}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_34"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_34}} </td>
                </tr>
                
                <tr v-if="option.name_35 && get_lab_center_list[0].name_35">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_35}}</th>
                </tr>
                <tr v-show="option.name_35 != null && option.name_35 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_35}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_35 }}</td>
                  <td class="fs">{{option.name_35}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_35"></td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_35}} </td>
                </tr>
                
                <tr v-if="option.name_36 && get_lab_center_list[0].name_36">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_36}}</th>
                </tr>
                <tr v-show="option.name_36 != null && option.name_36 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_36}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_36 }}</td>
                  <td class="fs">{{option.name_36}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_36"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_36}} </td>
                </tr>
                
                <tr v-if="option.name_37 && get_lab_center_list[0].name_37">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_37}}</th>
                </tr>
                <tr v-show="option.name_37 != null && option.name_37 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_37}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_37 }}</td>
                  <td class="fs">{{option.name_37}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_37"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_37}} </td>
                </tr>
                
                <tr v-if="option.name_38 && get_lab_center_list[0].name_38">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_38}}</th>
                </tr>
                <tr v-show="option.name_38 != null && option.name_38 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_38}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_38 }}</td>
                  <td class="fs">{{option.name_38}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_38"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_38}} </td>
                </tr>
                
                <tr v-if="option.name_39 && get_lab_center_list[0].name_39">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_39}}</th>
                </tr>
                <tr v-show="option.name_39 != null && option.name_39 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_39}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_39 }}</td>
                  <td class="fs">{{option.name_39}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_39"></td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_39}} </td>
                </tr>
                
                <tr v-if="option.name_40 && get_lab_center_list[0].name_40">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_40}}</th>
                </tr>
                <tr v-show="option.name_40 != null && option.name_40 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_40}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_40 }}</td>
                  <td class="fs">{{option.name_40}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_40"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_40}} </td>
                </tr>
                
                <tr v-if="option.name_41 && get_lab_center_list[0].name_41">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_41}}</th>
                </tr>
                <tr v-show="option.name_41 != null && option.name_41 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_41}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_41 }}</td>
                  <td class="fs">{{option.name_41}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_41"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_41}} </td>
                </tr>
                
                <tr v-if="option.name_42 && get_lab_center_list[0].name_42">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_42}}</th>
                </tr>
                <tr v-show="option.name_42 != null && option.name_42 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_42}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_42 }}</td>
                  <td class="fs">{{option.name_42}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_42"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_42}} </td>
                </tr>
                
                <tr v-if="option.name_43 && get_lab_center_list[0].name_43">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_43}}</th>
                </tr>
                <tr v-show="option.name_43 != null && option.name_43 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_43}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_43 }}</td>
                  <td class="fs">{{option.name_43}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_43"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_43}} </td>
                </tr>
                
                <tr v-if="option.name_44 && get_lab_center_list[0].name_44">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_44}}</th>
                </tr>
                <tr v-show="option.name_44 != null && option.name_44 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_44}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_44 }}</td>
                  <td class="fs">{{option.name_44}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_44"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_44}} </td>
                </tr>
                
                <tr v-if="option.name_45 && get_lab_center_list[0].name_45">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_45}}</th>
                </tr>
                <tr v-show="option.name_45 != null && option.name_45 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_45}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_45 }}</td>
                  <td class="fs">{{option.name_45}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_45"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_45}} </td>
                </tr>
                
                <tr v-if="option.name_46 && get_lab_center_list[0].name_46">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_46}}</th>
                </tr>
                <tr v-show="option.name_46 != null && option.name_46 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_46}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_46 }}</td>
                  <td class="fs">{{option.name_46}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_46"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_46}} </td>
                </tr>
                
                <tr v-if="option.name_47 && get_lab_center_list[0].name_47">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_47}}</th>
                </tr>
                <tr v-show="option.name_47 != null && option.name_47 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_47}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_47 }}</td>
                  <td class="fs">{{option.name_47}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_47"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_47}} </td>
                </tr>
                
                <tr v-if="option.name_48 && get_lab_center_list[0].name_48">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_48}}</th>
                </tr>
                <tr v-show="option.name_48 != null && option.name_48 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_48}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_48 }}</td>
                  <td class="fs">{{option.name_48}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_48"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_48}} </td>
                </tr>
                
                <tr v-if="option.name_49 && get_lab_center_list[0].name_49">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_49}}</th>
                </tr>
                <tr v-show="option.name_49 != null && option.name_49 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_49}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_49 }}</td>
                  <td class="fs">{{option.name_49}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_49"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_49}} </td>
                </tr>

                <tr v-if="option.name_50 && get_lab_center_list[0].name_50">
                    <th colspan="6" class="fs text-center bgCenter font-weight-bold">{{get_lab_center_list[0].name_50}}</th>
                </tr>
                <tr v-show="option.name_50 != null && option.name_50 != ''">
                  <td class="fs" v-if="option.firs_name_status">{{ get_lab_firs_name_list[0].name_50}}</td>
                  <td class="fs" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_50 }}</td>
                  <td class="fs">{{option.name_50}}</td>
                  <td class="fs" v-if="option.norma_status" v-html="get_lab_norma_list[0].name_50"> </td>
                  <td class="fs" v-if="option.unit_measurment_status">{{get_lab_edzm_list[0].name_50}} </td>
                </tr>
              </tbody>
            </table>
            <p v-if="option.hospitalAnalizDynamicmain.name_4" class="font-weight-bold mt-3 " style="font-size:24px;"> {{option.hospitalAnalizDynamicmain.name_4}} </p>
            <p v-if="option.hospitalAnalizDynamicmain.name_13" class="font-weight-bold mt-3" style="font-size:24px;"> {{option.hospitalAnalizDynamicmain.name_13}} </p>
            <p v-if="option.hospitalAnalizDynamicmain.name_14" class="font-weight-bold mt-3 " style="font-size:24px;"> {{option.hospitalAnalizDynamicmain.name_14}}: <span class="ml-4">{{option.extra1}}</span> </p>
          
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
  import {mapActions, mapGetters} from 'vuex'
  export default {
   async mounted() {
    //   this.$refs.print.focus();
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
        simbol: '<',
        patient_name: '',
        show_page: false,
        lab_url: this.$store.state.hostname2 + 'hospital_lab_result/'+ this.$route.params.id,
        size: 170,
        patient_id: null,
      }
    },
    computed:{
        ...mapGetters(['get_lab_firs_name_list', 'get_lab_name_list', 'get_lab_norma_list', 'get_lab_edzm_list','get_lab_center_list'])
    },
    async created(){
      if(this.id>0){
        this.loading =true;
        this.show_page = false;
        const response = await fetch(this.$store.state.hostname + '/HospitalAnalizDynamicResults/' + this.$route.params.id)
        const data = await response.json();
        this.loading = false;
        console.log(data)
        console.log('data')
        this.option = data;
        this.borndate = data.patients.bornDate.slice(0,4);
        this.date = this.option.create_date.slice(0,10);
        this.patient_name = this.option.patients.fio;
        this.patient_id = this.option.patients.id;
        if(data.firs_name_status){
            await this.fetch_lab_firs_name_list(this.option.hospitalAnalizDynamicmainId);
            console.log(this.lab_firs_name_list)
        }
        if(data.lab_name_status){
            await this.fetch_lab_name_list(this.option.hospitalAnalizDynamicmainId);
        }
        if(data.norma_status){
        await this.fetch_lab_norma_list(this.option.hospitalAnalizDynamicmainId);
            
        }
        if(data.unit_measurment_status){
        await this.fetch_lab_edzm_list(this.option.hospitalAnalizDynamicmainId);
            
        }
        if(data.extra_status){  
            await this.fetch_lab_center_list(this.option.hospitalAnalizDynamicmainId);
        }
        this.show_page = true;
        // console.log(this.get_lab_name_list)
        // console.log(this.get_lab_norma_list)
  
        await this.sendPdfTeleg();
      }
    },
    methods: {
        ...mapActions(['fetch_lab_firs_name_list', 'fetch_lab_name_list','fetch_lab_norma_list','fetch_lab_edzm_list',
    'fetch_lab_center_list']),
      printed(){
        window.print();
      },
      async sendPdfTeleg(){
          
          // this.loading = true;
          this.$nextTick(() => {
               // captureId is the page that needs to be downloaded
          html2canvas(document.querySelector('#table')).then(canvas => {
            //   console.log(canvas.toDataURL())
              var img = document.querySelector("#image");
              img.src = canvas.toDataURL();
              this.font_size = false;
              img.onload = () => {
                // console.log('dasdasdas')
                window.print();
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
          "analiz_name": this.option.name_title,
          "analiz_base_str": this.base64,
          "id": 0,
          "patientsId": this.option.patientsId,
          "sended_status":  true,
          "doctor_name": this.option.id.toString(),
          "creator_doctor_name": localStorage.docName
          })
        };
        this.loading = true;

        const response = await fetch(this.$store.state.hostname + '/HospitalPatientAnalizHistory', requestOptions)
        const data = await response.json()
        if(data.id){
            this.loading = false;
            this.$router.push('/laboratory_list/' + this.option.hospitalAnalizDynamicmainId);
        }
        else{
            // localStorage.labBack = true;
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
  @import url(../../../style/style.css);
  th, td{
    padding: 2px 8px;
  }
  .fs{
    font-size: 25px !important;
  }
  
  
  .headerTitle{
    font-size:35px;
  }
  .headerTitle1{
    font-size:35px;
  }
  p{
    margin:0;
    padding:0;
    font-size: 22.5px;
  }
  @media print {
    body {-webkit-print-color-adjust: exact;}
  }
  .qr-code_absolute{
    position: absolute;
    top: 104px;
    left: 790px;
  }
  
  
  
  </style>