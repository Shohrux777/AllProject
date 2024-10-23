<template>
  <div class="send_main_page">
    <div class="send_page">
      <loader v-if="loading"/>
        <form v-on:submit.prevent="submit">
          <div class="d-flex bg-white">
            <div class="doctor_list_send_page bg-white  px-2">
              <small v-if="$v.doc_name.$dirty && user_id == null" class="text-danger pt-4 pl-5" >
                {{$t('select_item')}}
              </small>
              <div  v-for="(doc,i) in get_doctor_list_by_casher.rows" :key="i"
                @click="show_serv(i,doc.DocAuthId,doc.doctorAuth.users.id, doc.fio, doc.doctorAuth.users.position.name, doc.doctorAuth.users.image )"  
                class="item px-2" :class="{ 'activeUser' : active_el == i }"
              >
              <!-- @dblclick="show_service(doc.doctorAuth.users.id)" -->
                <div class="d-flex align-items-center user_text_boxs">
                  <div class="user_photo user_photo_back">
                    <img :src="doc.doctorAuth.users.image" style="overflow: none;" class="img-fluid" alt="">
                  </div>
                  <div class="px-3 doctor_fullname">
                    <p class="m-0 p-0" style="font-size:14px;">{{doc.fio}}</p>
                    <p class="m-0 rang" style="font-size:11.5px;">{{doc.doctorAuth.users.position.name}}</p>
                  </div>
                </div>
                <div class="d-flex align-items-center justify-content-center">
                  <div @click="show_service(doc.doctorAuth.users.id)" class="bg-success add_icon ">
                    <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-plus m-1" style="padding: 2px;" width="17" height="17" viewBox="0 0 24 24" stroke-width="2.5" stroke="#fff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                      <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                      <line x1="12" y1="5" x2="12" y2="19" />
                      <line x1="5" y1="12" x2="19" y2="12" />
                    </svg>
                  </div>
                </div>
              </div>
            </div>
            <div class="Information w-100">
              <div class="PatientInfo d-flex justify-content-center">
                <div class="w-100" @click="addpatient">
                  <div class="d-flex  w-100 " style="cursor:pointer;" >
                    <div class="text-center w-50">
                      <div class="patient_name_box border">
                        <h4 class="m-0 pt-2 pb-1 mr-3" style="font-weight: 550; ">{{get_patient_info.patient_name}} (ID:  {{ get_patient_info.patient_id }})</h4>
                      </div>
                      <div class="patient_info_box d-flex w-100">
                        <div class="w-50 border">
                          <small class="pb-1" style="font-weight: 100 !important;">
                            <span class=" text-primary">Тел:</span> 
                            {{get_patient_info.patient_tel}}
                          </small>
                        </div>
                        <div class="w-50 border">
                          <small class="pb-1 ml-2" >
                            <span class=" text-primary">Дата рож:</span> 
                            {{get_patient_info.patient_born}}</small>
                        </div>
                      </div>
                      <!-- <h5 class="m-0 pt-1 mr-3 text-danger" style="font-weight: 100 !important; font-size: 16px;" v-if="get_patient_info.patient_born"> -->
                      <small v-if="$v.patient_name.$dirty && patient_id == null" class="text-danger pt-4 pl-5">
                        {{$t('select_item')}}
                      </small>
                    </div>
                    <div class="text-center w-50">
                      <div class="patient_name_box border">
                        <h4 class="m-0 pt-2 pb-1 mr-3" style="font-weight: 550; ">Цена:  {{(summa + get_bron_list_patient_id.need_payed).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} 
                          <small style="font-size: 19px;" class="px-2">сум </small></h4>
                      </div>
                      <div class="patient_info_box d-flex w-100">
                        <div class="w-25 border">
                          <small class="pb-1" style="font-weight: 100 !important;">
                            <span class=" text-primary">Услуга: </span> 
                            {{ServiceTypesCount.length}}
                          </small>
                        </div>
                        <div class="w-25 border">
                          <small class="pb-1 ml-2"  >
                            <span class=" text-primary">Усл. сумма:</span> 
                            {{summaString}}</small>
                        </div>
                        <div class="w-25 border">
                          <small class="pb-1 ml-2"  >
                            <span class=" text-primary">Стац:</span> 
                            {{get_bron_list_patient_id.need_payed.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small>
                        </div>
                        <div class="w-25 border">
                          <small class="pb-1 ml-2"  >
                            <span class=" text-primary">Скидка:</span> 
                              {{discount_summaString}}
                            </small>
                        </div>
                        <!-- <div class="w-25 border bg-primary">
                          <small class="pb-1 ml-2"  >
                            <span class=" text-white">Добавить</span> 
                            </small>
                        </div> -->
                      </div>
                      <!-- <h5 class="m-0 pt-1 mr-3 text-danger" style="font-weight: 100 !important; font-size: 16px;" v-if="get_patient_info.patient_born"> -->
                      
                    </div>
                    <!-- <div class="border-left w-50" style="position:relative;">
                      <h4 class="m-0 pt-3 pl-4" style="font-weight: 550;">Цена:  {{summaString}} <small style="font-size: 19px;" class="px-2">сум </small></h4>
                      <h5 class="m-0 pt-1 pl-4 ml-1 text-danger text-left pb-1" style="font-size: 16px;">
                        <small class="pb-1" style="border-bottom: 1px dashed black;">Услуга: {{ServiceTypesCount.length}}</small>
                        <small class="pb-1 ml-4" style="border-bottom: 1px dashed black;">Скидка: {{ discount_summaString }}</small>
                      </h5>
                      
                    </div> -->
                  </div>
                </div>
              </div>
              <div :class="{'bg_choose_graduind': !get_patient_info.patient_id, 'bg_choosen_graduind': get_patient_info.patient_id}">
                    <!-- <mdb-btn style=" font-size:10px;" class="blue-gradient" @click="addpatient"  p="r4 l4 t2 b2">{{$t('add')}}</mdb-btn>   -->
                </div>
              <div class="DoctorInfo d-flex">
                <!-- <div style="width: 18%">
                  <div class="picture shadow border  picture_background zoom mt-5">
                      <img :src="img" style="height: 100%; overflow: none; " class="img-fluid " alt="">
                  </div>
                </div> -->
                <div class="info_doc px-4 my-1 mx-2" style="width: 100%">
                  <div class="border-bottom m-0">
                    <div class="d-flex justify-content-between">
                      <div class="d-flex align-items-center w-75">
                        <div class="mr-3">
                          <div class="picture m-0 mt-1 mb-1 shadow border rounded-circle  picture_background zoom ">
                              <img :src="img" style="height: 100%; overflow: none; " class="img-fluid rounded-circle" alt="">
                          </div>
                        </div>
                        <div>
                          <h4 class="mb-1 mt-1">{{doc_name}}</h4>
                          <!-- <p class="text-primary px-1 mb-1">{{doc_position}}</p> -->
                        </div>
                      </div>
                      <div class="w-50 d-flex align-items-end">
                        <mdb-input label="Скидка %" v-model="discount" type="number" class="m-0 p-0 " style="width:270px;" size="sm"  @input="discount_func"/>
                        <mdb-input label="Скидка сум" v-model="discount_summ" type="number" class="m-0 p-0 ml-3" style="width:270px;" size="sm"  @input="discount_summ_func"/>
                        <mdb-input label="Поиск услуга" v-model="search" ref="refSearchService" class="m-0 ml-3 p-0 w-100" size="sm"  @input="search_func"/>
                      </div>
                    </div>
                  </div>
                  <div class="service_list">
                    <div class="row " v-if="search == ''">
                      <div class="col-12 " v-if="serviceGroupList.length>1">
                          <div class=" group_list_service">
                            <div class="group_item_service" :class="{'activ_group_item': index == activ_group.index}"
                              v-for="(item, index) in serviceGroupList" :key="index" 
                              @click="selectActiveGroup(item, index)">
                              <p>{{ item.group_name }}</p>
                            </div>
                          </div>
                      </div>
                      <div v-for="(servic,i) in searchServicelist" :key="i"  class="col-4" v-show="activ_group.link == servic.according ">
                        <div class="d-flex align-items-center" >
                          <div class="d-flex mt-0 p-0" style="margin-top: -5px !important;" v-if="servic.serviceType.link_str == '1'">
                            <div @click="AddServ_link(servic,null,null)" style="font-size: 10px; width: 20px; height: 16px; background: #00C851; cursor: pointer;" 
                              class="d-flex rounded justify-content-center align-items-center">
                              <span class="text-white">+</span>
                            </div>
                            <input type="text" class="px-1 " :value="servic.qty" style="width: 20px; height: 16px; outline: none; 
                              border-left: none; border-right: none; border-top: 0.1px solid rgb(220, 220, 220);
                              border-bottom: 0.1px solid rgb(220, 220, 220);  font-size: 13px;">
                            <div @click="RemoveServ_link(servic,null,null)" style="font-size: 10px; width: 20px; height: 16px; background: #FF3547; cursor: pointer;" 
                              class="d-flex rounded justify-content-center align-items-center">
                              <span class="text-white">-</span>
                            </div>
                          </div>
                          <div v-else>
                            <input type="checkbox" style="width: 17px; height: 17px;" :value="servic.serviceType.id" v-model="checkedCategories[servic.serviceType.id]" :id="servic.serviceType.name" @click="check_service($event, servic.serviceType.name, servic.serviceType.price)" />
                          </div>
                          <div class="pb-2 px-2">
                            <label style="font-weight:500; font-size:13.5px" :class="{'text-primary': servic.qty >0}"  :for="servic.serviceType.name" class="m-0">{{servic.serviceType.name}}  ({{servic.serviceType.price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}})</label>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="row mt-1" v-else>
                      <div class="col-12" v-if="serviceGroupList.length>1">
                        <div class="d-flex group_list_service" >
                          <div class="group_item_service" :class="{'activ_group_item': index == activ_group.index}"
                            v-for="(item, index) in serviceGroupList" :key="index" 
                            @click="selectActiveGroup(item, index)">
                            <p>{{ item.group_name }}</p>
                          </div>
                        </div>
                      </div>
                      <div v-for="(servic,i) in searchServicelist" :key="i"  class="col-4" >
                        <div class="d-flex align-items-center" >
                          <div class="d-flex mt-0 p-0" style="margin-top: -5px !important;" v-if="servic.serviceType.link_str == '1'">
                            <div @click="AddServ_link(servic,null,null)" style="font-size: 10px; width: 20px; height: 16px; background: #00C851; cursor: pointer;" 
                              class="d-flex rounded justify-content-center align-items-center">
                              <span class="text-white">+</span>
                            </div>
                            <input type="text" class="px-1 " :value="servic.qty" style="width: 20px; height: 16px; outline: none; 
                              border-left: none; border-right: none; border-top: 0.1px solid rgb(220, 220, 220);
                              border-bottom: 0.1px solid rgb(220, 220, 220);  font-size: 13px;">
                            <div @click="RemoveServ_link(servic,null,null)" style="font-size: 10px; width: 20px; height: 16px; background: #FF3547; cursor: pointer;" 
                              class="d-flex rounded justify-content-center align-items-center">
                              <span class="text-white">-</span>
                            </div>
                          </div>
                          <div v-else>
                            <input type="checkbox" style="width: 17px; height: 17px;" :value="servic.serviceType.id" v-model="checkedCategories[servic.serviceType.id]" :id="servic.serviceType.name" @click="check_service($event, servic.serviceType.name, servic.serviceType.price)" />
                          </div>
                          <div class="pb-2 px-2">
                            <label style="font-weight:500; font-size:13.5px" :class="{'text-primary': servic.qty >0}"  :for="servic.serviceType.name" class="m-0">{{servic.serviceType.name}}  ({{servic.serviceType.price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}})</label>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div>
                  </div>
                  <small v-if="$v.check_name.$dirty && ServiceTypesCount.length == 0" class="text-danger pt-4 pl-2" >
                    {{$t('select_item')}}
                  </small>
                </div>
              </div>
              <div class="row  px-4  addention_service w-100" v-if="service_bahila_list_database.length != 0">
                <div v-for="(item,i) in service_bahila_list_database" :key="i" class="col-4 px-3">
                  <div style="" class="d-flex align-items-center justify-content-between">
                      <small style="font-size:13px;" class="one_line">{{item.service_name}}</small>
                      <div class="ml-2 d-flex mt-1">
                        <div @click="AddServ(item)" style="font-size: 10px; width: 30px; height: 22px; background: #00C851; cursor: pointer;" 
                          class="d-flex rounded justify-content-center align-items-center">
                          <span class="text-white">+</span>
                        </div>
                        <input type="text" class="px-1 " :value="item.qty" style="width: 30px; height: 22px; outline: none; 
                          border-left:none; border-right:none; border-top: 0.1px solid rgb(220, 220, 220);
                          border-bottom: 0.1px solid rgb(220, 220, 220); font-size: 13px;">
                        <div @click="RemoveServ(item)" style="font-size: 10px; width: 30px; height: 22px; background: #FF3547; cursor: pointer;" 
                          class="d-flex rounded justify-content-center align-items-center">
                          <span class="text-white">-</span>
                        </div>
                      </div>
                  </div>
                  </div>
              </div>


              <div class=" m-3 mt-2" :class="{'TablePatientDocIdSendNotBahila' : service_bahila_list_database.length == 0, 'TablePatientDocIdSend': service_bahila_list_database.length > 0}">
                <table class="myTable">
                  <thead >
                    <tr class="header ">
                      <th width="50">№</th>
                      <th>{{$t('FIO')}}</th>
                      <th>{{$t('otchrit')}}</th>
                      <th>{{$t('unregistrDate')}}</th>
                      <th>{{$t('bornDate')}}</th>
                      <th>{{$t('phoneNumber')}}</th>
                      <th width="100">{{$t('Action')}}</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr v-for="(row,rowIndex) in ocherd_list" :key="rowIndex">
                      <td> <small style="font-size:11.5px;">{{rowIndex+1}}</small> </td>
                      <td @click="update_client_info(row)"> <small class="text-primary" style="font-size:11.5px; cursor:pointer;">{{row.patients.FIO}}</small> </td>
                      <td> <small style="font-size:11.5px; font-weight: bold;">
                        {{row.ochred_name_aout_genereted}}</small> 
                      </td>
                      <td> <small style="font-size:11.5px;">{{row.created_date_time.slice(0,10)}}</small><small>{{row.created_date_time.slice(11,19)}}</small></td>
                      <td> <small style="font-size:11.5px;">{{row.patients.BornDate.slice(0,10)}}</small> </td>
                      <td> <small style="font-size:11.5px;">{{row.patients.PhoneNumber}}</small> </td>
                      <td class="text-center">
                        <i class="fas fa-eye editIcon  mask waves-effect text-warning m-0 pr-2" disabled @click="getPatientIdService(row.PatientsId, row.patients.FIO, row.patients)" :data-row="rowIndex"></i>
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div>
          <div style="position: absolute; bottom: 7px; right: 15px;">
            <!-- <mdb-btn style=" font-size:10px;" class="green-gradient" @click="add_tomorrow_ochret"  p="r4 l4 t2 b2">Завтра {{$t('otchrit')}}</mdb-btn>  
            <mdb-btn style=" font-size:10px;" class="blue-gradient" @click="infopatient"  p="r4 l4 t2 b2">{{$t('info')}}</mdb-btn>   -->
            <!-- <mdb-btn color="secondary" @click="blankaShow" style=" font-size:10px;"   p="r5 l5 t2 b2">{{$t('blanka')}}</mdb-btn>   -->
            <!-- <mdb-btn color="warning" :disabled="user_id == null" @click="$router.push('/otcheritList/' + user_id)" style=" font-size:10px;" p="r4 l4 t2 b2">{{$t('otchrit')}}</mdb-btn>   -->
            <mdb-btn color="warning" @click="$router.push('/result_lab')" style=" font-size:10px;" p="r4 l4 t2 b2">{{$t('result')}}</mdb-btn>  
            
            <mdb-btn  color="primary" type="submit" style=" font-size:10px;"  p="r4 l4 t2 b2">{{$t('Send')}}</mdb-btn>
          </div>
        </form>
        <ModalUser :show="show" :title="$t('Choose client')" @close="show = false" width="72%">
          <template v-slot:body>
            <clientAdd  @close="show = false"/>
          </template>
        </ModalUser>

        <ModalUser  :show="service_show" headerbackColor="info" closeColor="white" titlecolor="white" :title="$t('Choose service type')" @close="service_show = false" width="80%">
          <template v-slot:body>
            <div class="d-flex justify-content-between align-items-center">
              <div>
                <mdb-input label="Поиск" v-model="search_service_doctor_add" type="text" class="m-0 p-0 " style="width:320px;" size="sm"/>
              </div>
              <div>
                <mdb-btn  color="secondary" @click="service_doc_change_activ = !service_doc_change_activ" style="font-size:10px"  p="r4 l4 t2 b2">
                  <mdb-icon icon="undo-alt"/>
                  {{$t('change')}}
                </mdb-btn>
              </div>
            </div>
            <div style="min-height: 300px;" v-if="service_doc_change_activ">
              <div class="row px-1">
                <div class="col-4" v-for="(item,i) in filteredList" :key="i">
                  <div :class="{'bg-info': item.status, 'text-white': item.status}" style="height: 55px; overflow: hidden; overflow-y: auto" @click="choosenServiceType({data: item, index: i})" class="wrap_chip  border rounded d-flex my-2 px-3 text-center justify-content-center">
                    <p class="my-1"> {{item.name}} ({{item.groupSer}})</p>
                  </div>
                </div>
              </div>
            </div>
            <div style="min-height: 300px;" v-else>
              <div class="row px-1">
                <div class="col-4" v-for="(item,i) in get_user_service_list" :key="i">
                  <div  style="height: 55px; overflow: hidden; overflow-y: auto" class="wrap_chip  border rounded d-flex my-2 px-3 text-center justify-content-center align-items-center">
                    <p class="my-1"> {{item.serviceType.name}} ({{item.serviceType.price}})</p>
                    <i class="fas fa-trash delIcon mask waves-effect m-0 pl-1"  v-on:click="deleteDoc_service(item.serviceType.id)"></i>
                  </div>
                </div>
              </div>
            </div>
            <div class="text-right py-2 border-top bg-white" style="position: sticky; bottom: -20px;">
              <mdb-btn  color="danger" @click="service_show = false" style="font-size:10px"  p="r4 l4 t2 b2">{{$t('close')}}</mdb-btn>  
              <mdb-btn  color="primary" @click="add_services" style="font-size:10px"  p="r4 l4 t2 b2">Добавить услуги</mdb-btn>  
            </div>
          </template>
        </ModalUser>

          <mdb-modal  :show="show_service_patient_id"  @close="show_service_patient_id = false" size="lg" light>
            <mdb-modal-body>
              <div class="d-flex justify-content-between border-bottom">
                <h4>{{patientNmaeService}}</h4>
                <mdb-btn class="py-2 px-4" style="font-size: 11px;" @click="ocherd_add_service()" color="primary"> + Добавить услуги</mdb-btn>
              </div>
              <div class="px-3 row mt-2">
                <div v-for="(servic,i) in show_patientId_ServiceList" :key="i" 
                  class="col-12 border-bottom d-flex justify-content-between " :class="{'bg-payed': servic.finishPayment}">
                  <div class="" >
                    <div class="py-2 px-2 d-flex w-100">
                      <span class="m-0 p-0 mr-3" style="margin-top: -2px !important">{{i+1}} ) </span>
                      <label style="font-weight:500;" class="m-0">{{servic.serviceName}} ({{servic.summ}})</label>
                    </div>
                  </div>
                  <div style="width: 40px;" class="d-flex justify-content-center align-items-center" v-if="!servic.finishPayment">
                    <i class="fas fa-trash delIcon mask waves-effect m-0 pl-2" disabled @click="delServiceNopayed(servic, i)"></i>
                  </div>
                </div>
              </div>
            </mdb-modal-body>
          </mdb-modal>
          <mdb-modal :show="service_unpayed_show" @close="service_unpayed_show = false" size="sm" class="text-center" danger>
            <mdb-modal-header center :close="false">
              <p class="heading">{{$t('Are_you_sure')}}</p>
            </mdb-modal-header>
            <mdb-modal-body>
              <mdb-icon icon="times" size="4x" class="animated rotateIn"/>
            </mdb-modal-body>
            <mdb-modal-footer center>
              <mdb-btn outline="danger" @click="promiseService">{{$t('Yes')}}</mdb-btn>
              <mdb-btn color="danger" @click="service_unpayed_show = false">{{$t('No')}}</mdb-btn>
            </mdb-modal-footer>
          </mdb-modal>

          <div v-if="get_patient_info.patient_id" class="d-flex justify-content-center align-items-center  shadow" 
          :class="{'InfoIcoFixedOpenInfo': showInfoPatientID, 'InfoIcoFixedInfo': !showInfoPatientID}"  @click="show_patient_info">
            <mdb-icon icon="info" class="text-white ml-1" style="font-size: 17px;" />
          </div>
          <div :class="{'InfoIcoFixedOpenInfoPatient': showInfoPatientID, 'infoPatientIdNo': !showInfoPatientID}">
            <patient_info_id :status_info="showInfoPatientID_status" ref="patient_info_id" :patientId="get_patient_info.patient_id" @closed="showSending=false"/>
          </div>

          <!-- Startsionar registratsiya qilish knopkasi -->
          <div v-if="get_patient_info.patient_id" class="d-flex justify-content-center align-items-center shadow Bron_patient_added_btn" 
            @click="show_startsionar_add_patient = !show_startsionar_add_patient">
            <mdb-icon icon="bed" class="text-white ml-1" style="font-size: 17px;" />
          </div>


          <ModalUser :show="patient_update_ochred" title="Изминит пациент" @close="patient_update_ochred = false" width="72%">
            <template v-slot:body>
              <client_add_update @close="close_patient_data_id" :option="patient_data_id" ref="patient_update_by_id"/>
              <!-- <clientAdd  @close="show_startsionar_add_patient = false"/> -->
            </template>
          </ModalUser>
    </div>
    <Toast ref="message"></Toast>
    <AlertError ref="msg"></AlertError>
      <massage_box :hide="modal_status" :detail_info="modal_info"
  :m_text="$t('Failed_to_add')" @to_hide_modal="modal_status= false"/>
  </div>
</template>

<script>
import {mdbBtn,  mdbModal, mdbModalBody,mdbInput, mdbModalHeader, mdbModalFooter, mdbIcon} from "mdbvue";
import {mapActions, mapGetters, mapMutations} from 'vuex'
import ModalUser from '../../components/modal.vue'
import { required } from 'vuelidate/lib/validators'
import clientAdd from "../../components/new_prog_add/client_add"
import client_add_update from "../../components/new_prog_add/client_add_update"
import patient_info_id from './patient_info_id.vue'

export default {
  components: {
    mdbBtn, ModalUser, clientAdd, mdbModal, mdbModalBody, mdbInput,
    mdbModalHeader, mdbModalFooter, mdbIcon, patient_info_id, client_add_update
  },
  validations: {
      doc_name: {required},
      check_name: {required},
      patient_name: {required},
    },
  data(){
    return{
      modal_info: '',
      modal_status: false,

      show: false,
      service_show: false,
      show_surov: false,
      patientNmaeService: '',
      patientsInfoFull: {},
      show_patientId_ServiceList: [],
      show_startsionar_add_patient: false,
      show_otchert: false,

       

      img: '',
      doc_name: '',
      doc_position: '',
      active_el: -1,
      user_id: null,
      auth_id: null,
      checkedCategories: [],
      ServiceTypesCount: [],
      OcheredServiceList: [],
      searchServicelist: [],
      check_name: '',
      patient_name: '',
      patient_id: null,
      loading: false,
      service_bahila_list_database: [],
      summa: 0,
      summaString: '0',
      discount_summa: 0,
      discount_summ: 0,
      discount_summaString: '0',
      show_service_patient_id: false,

      genereted: '',
      check_ochret_status: false,
      search: '',

      service_unpayed_id: null,
      service_unpayed_name: '',
      service_unpayed_index: null,
      service_unpayed_show: false,
      serviceListGroup: [],
      serviceListGroupItem: [],

      serviceGroupList: [],
      activ_group: {
        link: '',
        index: 0,
      },
      discount: 0,

      ocherd_list: [],

      showInfoPatientID: false,
      showInfoPatientID_status: true,
      showSending: false,
      search_service_doctor_add: '',
      service_doc_change_activ: true,
      connect_doc_service_list: [],
      tomorrow_date: new Date(),
      genereted_tomorrow: '',

      patient_update_ochred: false,
      patient_data_id: {},
    }
  },
   async mounted(){
    console.log('send_docga kirdi')
    console.log(localStorage.hos_id)
    await this.fetch_bahila_service_type_group()
     console.log(this.get_bahila_service_type_group.rows)
     this.service_bahila_list_database = this.get_bahila_service_type_group.rows.map(item => {
        return {
            id: item.id.toString(),
            service_id: item.serviceType.id,
            service_name: item.serviceType.name,
            service_price: item.serviceType.price,
            qty: 0,
        }
      })
    
    this.fetch_get_doctor_list(localStorage.AuthId)
    this.fetch_auth_list();
    this.fetch_service_type();
    await this.fetch_contragent();
  },

  watch: {
    get_patient_info(){
      this.patient_name = this.get_patient_info.patient_name
      this.patient_id = this.get_patient_info.patient_id
    }
  },
  computed: {
    ...mapGetters(['auth_user_list','get_service_patientId','get_serviceChosenList',
    'get_contragent_list', 'get_bahila_service_type_group', 'get_service_no_chosen_list',
    'get_user_service_list', 'get_patient_info', 'get_patient_list_doc_id', 'get_doctor_list_by_casher',
    'get_bron_list_patient_id']),
    // get_service_no_chosen_list     groupSer
    filteredList: function(){
        if(this.search_service_doctor_add)
        {
          return this.get_service_no_chosen_list.filter((item)=>{
            return this.search_service_doctor_add.toLowerCase().split(' ').every(v => item.name.toLowerCase().includes(v)) || this.search_service_doctor_add.toLowerCase().split(' ').every(v => item.groupSer.toLowerCase().includes(v))
          })
        }else
        {
          return this.get_service_no_chosen_list;
        }
      }
  },
  methods:{
    ...mapActions(['fetch_auth_list','fetch_service_patientId', 'fetch_patient_client','fetch_contragent' ,'fetch_bahila_service_type_group', 'fetch_service_type', 'fetch_add_service_to_user', 'fetch_users_service_list', 'fetch_get_patient_list_Doc_Id','fetch_get_doctor_list']),
    ...mapMutations(['Users_service_list','update_patient_list','bron_patient_summ_nol', 'ochred_add_check', 'ochred_add_service', 'choosenServiceType', 'choose_patient_client']),
    async update_client_info(item){
      console.log('update');
      console.log(item);
      this.patient_update_ochred = true;
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + '/Patients/' + item.PatientsId)
        const data = await response.json()
        this.loading = false;
        console.log(data);
        this.patient_data_id = data;
        if(response.status == 200 || response.status == 201){
          this.$refs.patient_update_by_id.update(data);
        }
      }
      catch(error){
        console.log(error)
      }
    },
    async close_patient_data_id(){
      this.patient_update_ochred = false;
      await this.fetch_ocherd_list();
    },
    async deleteDoc_service(serv_id){
      console.log(this.user_id)
      console.log(serv_id)
      for(let i=0; i<this.connect_doc_service_list.length; i++){
        if(this.connect_doc_service_list[i].serviceTypeId == serv_id){
          console.log(this.connect_doc_service_list[i])
          await this.delete_connect_doc_service(this.connect_doc_service_list[i].id);
          break;
        }
      }
    },
    async fetch_connect_doc_serv_list(){
      try{
        const response = await fetch(this.$store.state.hostname + "/ServiceTypeDetails/getServiceTypeDetailByUserId?UserId=" + this.user_id);
        const data = await response.json();
        if(response.status == 200 || response.status == 201){
          console.log('connect data')
          console.log(data)
          this.connect_doc_service_list = data;
        }
      }
      catch(error){
        console.log(error)
      } 
    },
    async delete_connect_doc_service(id){
      try{
        const requestOptions = {
          method : "delete",
        };
        const response = await fetch(this.$store.state.hostname + "/ServiceTypeDetails/" + id, requestOptions);
        if(response.status == 200 || response.status == 201){
          this.$refs.message.success('Successfully_removed')
          await this.fetch_users_service_list(this.user_id)
        }
      }
      catch(error){
        console.log(error);
      }
    },
    search_func(){
      // this.get_user_service_list = this.searchServicelist;
      this.renderFunc(this.get_user_service_list)
        if(this.search != ''){
          let userSearchList = [];
          for(let i=0; i<this.searchServicelist.length;i++){
            if(this.searchServicelist[i].serviceType.name.toLowerCase().includes(this.search.toLowerCase()) || this.searchServicelist[i].serviceType.price.toString().toLowerCase().includes(this.search.toLowerCase())){
              userSearchList.push(this.searchServicelist[i])
            }
          }
          this.searchServicelist = userSearchList;
        }
        else{
          this.renderFunc(this.get_user_service_list)
        }
    },
    show_patient_info(){
      this.showInfoPatientID = !this.showInfoPatientID;
      // this.$refs.patient_info_id.fetch_patient_drugs();
    },
    discount_func(){
      console.log(this.discount);
      if(this.discount != null && this.discount != ''){
        this.discount_summa = parseInt((this.summa * this.discount)/100);
        this.discount_summaString = this.discount_summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
      }
      else{
        this.discount_summa = 0;
        this.discount_summaString = this.discount_summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
      }
    },
    discount_summ_func(){
      if(this.discount_summ != null && this.discount_summ != ''){
        this.discount = this.discount_summ * 100 / this.summa;
        this.discount_func();
      }
      else{
        this.discount = 0;
        this.discount_summa = 0;
        this.discount_summaString = this.discount_summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
        this.discount_func();
      }
    },
    
    async show_serv(i,authId, UserId, name, position, img){
      this.active_el = i
      this.auth_id = authId;
      this.user_id = UserId;
      this.img = img;
      this.doc_name = name;
      this.doc_position = position;
      this.activ_group.link = '';
      this.activ_group.index = 0;
      await this.fetch_users_service_list(UserId);
      // this.searchServicelist = ;
      console.log('this.get_patient_list_doc_id')
      console.log(this.get_user_service_list[0].serviceType.link_str1)

      this.renderFunc(this.get_user_service_list)
      this.search = '';
      this.$refs.refSearchService.focus();
      // this.fetch_get_patient_list_Doc_Id(authId)
      await this.fetch_ocherd_list();
      console.log(this.get_patient_list_doc_id)
      // this.checkedCategories = []
      // this.ServiceTypesCount = []
      console.log(localStorage.AuthId)
    },
    async selectActiveGroup(data, i){
      console.log(data, i)
      this.activ_group.link = data.link_str1;
      this.activ_group.index = i;
      this.$refs.refSearchService.focus();
    },
    async renderFunc(data){
      this.serviceGroupList = [
        {
          index: 0,
          link_str1: '',
          group_name: 'По умолчанию'
        }
      ];
      for(let i=0; i<data.length; i++){
        var serQty_count = 0;
        for(let j=0; j<this.serviceGroupList.length; j++){
          if(data[i].serviceType.link_str1 == this.serviceGroupList[j].link_str1){
            serQty_count ++;
          }
        }
        if(serQty_count == 0){
          let temp = {
            index: 0,
            link_str1: data[i].serviceType.link_str1,
            group_name: data[i].serviceType.link_str2
          }
          this.serviceGroupList.push(temp)
        }
      }
      console.log('this.serviceGroupList')
      console.log(this.serviceGroupList)
      this.searchServicelist = data.map(item => {
        return {
            id: item.id,
            serviceTypeId: item.serviceTypeId,
            serviceType: item.serviceType,
            users: item.users,
            usersId: item.usersId,
            activeStatus: item.activeStatus,
            according: item.serviceType.link_str1,
            qty: 0,
        }
      });
      for(let i = 0; i < this.ServiceTypesCount.length; i++){
        for(let j = 0; j < this.searchServicelist.length; j++){
          if(this.ServiceTypesCount[i].id == this.searchServicelist[j].serviceType.id){
            this.searchServicelist[j].qty++;
          }
        }
      }
              // if(this.ServiceTypesCount[j].id == data.serviceType.id){
    },
    async show_service(userId){
      this.service_show = true;
      this.user_id = userId;
      await this.fetch_users_service_list(userId)
      this.renderFunc(this.get_user_service_list)
      this.search = '';
      this.search_service_doctor_add = '';
      this.Users_service_list()
      await this.fetch_connect_doc_serv_list()
    },
    delServiceNopayed(data, index){
      console.log(data)
      console.log(index)
      this.service_unpayed_id = data.id;
      this.service_unpayed_name = data.serviceType.name;
      this.service_unpayed_index = index;
      this.service_unpayed_show = true;
    },
    async promiseService(){
      const requestOptions = {
        method : "delete",
      };
      const response = await fetch(this.$store.state.hostname + "/Payments/" + this.service_unpayed_id, requestOptions);
      const data = await response.json();
      console.log(data)
      if(data.id)
      {
        this.$refs.message.success('Successfully_removed')
        this.show_patientId_ServiceList.splice(this.service_unpayed_index, 1);
        this.service_unpayed_show = false;
      }
      },
    async getPatientIdService(id, fio, patients){
      console.log(id)
      this.patientNmaeService = fio;
      this.patientsInfoFull = patients;
      const response = await fetch(this.$store.state.hostname + "/Payments/getPatientPayedServiceByPatientIdListByDoctrAuthId?PatientId=" + id + '&doctirAuthId=' + this.auth_id);
      const data = await response.json();
      console.log(' info service')
      console.log(data)
      this.show_patientId_ServiceList = data;
      // console.log()
      this.show_service_patient_id = true;
    },
    async getService_type(data){
      var a = {
        user_id: null,
        service_id: null,
      };
      a.user_id = this.user_id;
      a.service_id = data.id;
      await this.fetch_add_service_to_user(a)
      this.fetch_users_service_list(this.user_id)
      this.renderFunc(this.get_user_service_list)
      this.search = '';
      this.service_show = false
    },
    async ocherd_add_service(){
      this.show_service_patient_id = false;
      let patient_info = {
          reason: '',
          contragent_id: this.get_contragent_list.rows[0].id,
          contragent_name: this.get_contragent_list.rows[0].name,
          contragent_tel: '',
          number_into_id: null,
          patient_type_id: null,
          patient_born: this.patientsInfoFull.BornDate.slice(0,10),
          patient_id: this.patientsInfoFull.Id,
          patient_name: this.patientNmaeService,
          patient_tel: this.patientsInfoFull.PhoneNumber
        }
        this.choose_patient_client(patient_info)
    },
    async add_services(){
      console.log('get_serviceChosenList')
      console.log(this.get_serviceChosenList)
      for(let i=0; i<this.get_serviceChosenList.length; i++){
        var alfa = {
          user_id: null,
          service_id: null,
        };
        alfa.user_id = this.user_id;
        alfa.service_id = this.get_serviceChosenList[i].id;
        await this.fetch_add_service_to_user(alfa)
      }
      this.service_show = false;
      this.fetch_users_service_list(this.user_id)
      this.renderFunc(this.get_user_service_list)
      this.search = '';
    },
    addpatient(){
      this.show = true;
      this.fetch_patient_client();
      this.$root.$refs.clientAdd.rootMounted();
    },
    infopatient(){
      this.$router.push('/infoPatient')
      console.log('infoletsGo')
    },
    AddServ(data){
      console.log(data) 
      for(let i=0; i<this.get_bahila_service_type_group.rows.length; i++){
        if(this.get_bahila_service_type_group.rows[i].id == data.id){
          this.service_bahila_list_database[i].qty ++;
            var t = {
              id: '',
              name: '',
              price: null,
              authId: this.auth_id,
              user_Id: this.user_id,
              doc_name: this.doc_name,
              talon: 0,
            }
            
            t.id = this.service_bahila_list_database[i].service_id.toString()
            t.name = this.service_bahila_list_database[i].service_name
            t.price = this.service_bahila_list_database[i].service_price
            this.ServiceTypesCount.push(t);
            this.summa = 0
            this.summaString = this.summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
            for(let a=0; a<this.ServiceTypesCount.length; a++){
              this.summa += this.ServiceTypesCount[a].price
              this.summaString = this.summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
            }
            if(this.discount != null && this.discount != ''){
              this.discount_summa = parseInt((this.summa * this.discount)/100);
              this.discount_summaString = this.discount_summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
            }
          return
        }
      }
    },
    AddServ_link(data, indx1, indx2){
      if(indx1 != null && indx2 != null){
        this.serviceListGroupItem[indx1][indx2].qty ++;
      }
      this.$refs.refSearchService.focus();
      console.log(data) 
      for(let i=0; i<this.searchServicelist.length; i++){
        if(this.searchServicelist[i].id == data.id){
          this.searchServicelist[i].qty ++;
            var t = {
              id: '',
              name: '',
              price: null,
              authId: this.auth_id,
              user_Id: this.user_id,
              doc_name: this.doc_name,
              talon: 0,
            }
            t.id = this.searchServicelist[i].serviceType.id.toString();
            t.name = this.searchServicelist[i].serviceType.name;
            t.price = this.searchServicelist[i].serviceType.price;
            this.ServiceTypesCount.push(t);
            this.summa = 0;
            this.summaString = this.summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
            for(let a=0; a<this.ServiceTypesCount.length; a++){
              this.summa += this.ServiceTypesCount[a].price
              this.summaString = this.summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
            }
            if(this.discount != null && this.discount != ''){
              this.discount_summa = parseInt((this.summa * this.discount)/100);
              this.discount_summaString = this.discount_summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
            }
          return
        }
      }
    },
    RemoveServ(data){
      // console.log(data)
      for(let i=0; i<this.get_bahila_service_type_group.rows.length; i++){
        if(this.get_bahila_service_type_group.rows[i].id == data.id){
          if(this.service_bahila_list_database[i].qty != 0){
            this.service_bahila_list_database[i].qty --;

            for(let j = 0; j < this.ServiceTypesCount.length; j++){
              if(this.ServiceTypesCount[j].id == data.service_id){
                this.ServiceTypesCount.splice(j, 1);
                this.summa = 0
                this.summaString = this.summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
                for(let a=0; a<this.ServiceTypesCount.length; a++){
                  this.summa += this.ServiceTypesCount[a].price
                  this.summaString = this.summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
                }
                if(this.discount != null && this.discount != ''){
                  this.discount_summa = parseInt((this.summa * this.discount)/100);
                  this.discount_summaString = this.discount_summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
                }
                return;
              }
            }

            return
          }
        }
      }
    },
    RemoveServ_link(data, indx1, indx2){
      if(indx1 != null && indx2 != null ){
        if(this.serviceListGroupItem[indx1][indx2].qty != 0){
          this.serviceListGroupItem[indx1][indx2].qty --;
        }
      }
      console.log(data)
      this.$refs.refSearchService.focus();
      for(let i=0; i<this.searchServicelist.length; i++){
        if(this.searchServicelist[i].id == data.id){
          if(this.searchServicelist[i].qty != 0){
            this.searchServicelist[i].qty --;
            for(let j = 0; j < this.ServiceTypesCount.length; j++){
              if(this.ServiceTypesCount[j].id == data.serviceType.id){
                this.ServiceTypesCount.splice(j, 1);
                this.summa = 0
                this.summaString = this.summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
                for(let a=0; a<this.ServiceTypesCount.length; a++){
                  this.summa += this.ServiceTypesCount[a].price
                  this.summaString = this.summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
                }
                if(this.discount != null && this.discount != ''){
                  this.discount_summa = parseInt((this.summa * this.discount)/100);
                  this.discount_summaString = this.discount_summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
                }
                return;
              }
            }

            return
          }
        }
      }
    },
    check_service: function(e, serv_name, narx) {
      this.serviceCheck = false;
      if (e.target.checked) {
        console.log(e.target.value)
        console.log(serv_name)
        let s = {
          id: e.target.value,
          name: serv_name,
          price: narx,
          authId: this.auth_id,
          user_Id: this.user_id,
          doc_name: this.doc_name,
          talon: 0,
        }
        this.ServiceTypesCount.push(s);
        this.summa = 0
        this.summaString = this.summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
        for(let a=0; a<this.ServiceTypesCount.length; a++){
          this.summa += this.ServiceTypesCount[a].price
          this.summaString = this.summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
        }
        if(this.discount != null && this.discount != ''){
          this.discount_summa = parseInt((this.summa * this.discount)/100);
          this.discount_summaString = this.discount_summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
        }
      }
      else{
        // console.log(e.target.value)
        for(let i=0; i<this.ServiceTypesCount.length; i++){
          if(e.target.value == this.ServiceTypesCount[i].id){
            this.ServiceTypesCount.splice(i, 1);
            this.summa = 0
            this.summaString = this.summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
            for(let a=0; a<this.ServiceTypesCount.length; a++){
              this.summa += this.ServiceTypesCount[a].price
              this.summaString = this.summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
            }
            if(this.discount != null && this.discount != ''){
              this.discount_summa = parseInt((this.summa * this.discount)/100);
              this.discount_summaString = this.discount_summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
            }
            return
          }
        }
      }
      console.log(this.ServiceTypesCount);
      this.$refs.refSearchService.focus();
    },
    blankaShow(){
      this.show_surov = true;
      localStorage.Summa = this.summa;
    },
    async submit(){
      if(this.discount == null || this.discount == ''){
        this.discount = 0;
      }
      // this.ochred_add_service(this.ServiceTypesCount)
      this.patient_name = this.get_patient_info.patient_name
      this.patient_id = this.get_patient_info.patient_id
      if(this.ServiceTypesCount.length > 0){
        this.loading = true;
        this.check_name = this.ServiceTypesCount[0].name
        this.OcheredServiceList = []
        this.OcheredServiceList.push(this.ServiceTypesCount[0])
        for(let i=0; i<this.ServiceTypesCount.length; i++){
          var sN=0
          for(let j=0; j<this.OcheredServiceList.length; j++){
            if(this.ServiceTypesCount[i].authId == this.OcheredServiceList[j].authId){
              sN++;
            }
          }
          if(sN == 0){
            this.OcheredServiceList.push(this.ServiceTypesCount[i])
          }
        }
        
        for(let k=0; k<this.OcheredServiceList.length; k++){
          await this.check_ochred(this.get_patient_info.patient_id, this.OcheredServiceList[k].user_Id, this.OcheredServiceList[k].authId, k)
        }
        console.log('this.OcheredServiceList')
        console.log(this.OcheredServiceList)
      }
      else{
        this.loading = true;
        this.check_name = '';
      }
      if( this.$v.$invalid || this.user_id == null )
        {
          this.loading = false;
          this.$v.$touch();
          this.$refs.message.warning('please_fill')
          return;
        }

        console.log('this.ServiceTypesCount')
        console.log(this.ServiceTypesCount)

        var jsonData = []
      for(let i=0; i<this.ServiceTypesCount.length; i++){
        let tempSumm = this.ServiceTypesCount[i].price;
        let tempDiscount = this.discount;
        let tempDiscountSumm = 0;
        var a = {
          serviceTypeId: null,
          contragentId: null,
          serviceName: '',
          patientsId: null,
          summ: 0,
          name: '',
          patientName: '',
          authorizationId: null,
          creatorAuthId: null,
          discount_qty: 0,
          discount_persantage_qty: 0,
          discount_card_qty: 'none',
          discount_real_qty: 0,
          analiz_hos_id: 0,
        }
        if(this.discount != 0){
          tempDiscountSumm = parseInt((this.ServiceTypesCount[i].price * this.discount)/100);
          tempSumm = this.ServiceTypesCount[i].price - tempDiscountSumm;
        }
        a.discount_card_qty = this.ServiceTypesCount[i].doc_name
        a.discount_real_qty = parseInt(this.ServiceTypesCount[i].talon)
        a.discount_qty = tempDiscountSumm;
        a.discount_persantage_qty = tempDiscount;
        a.serviceTypeId = this.ServiceTypesCount[i].id;
        a.contragentId = this.get_patient_info.contragent_id;
        a.serviceName =  this.ServiceTypesCount[i].name;
        a.patientsId =  this.get_patient_info.patient_id;
        a.summ = tempSumm;
        a.name = this.get_patient_info.reason;
        a.patientName = localStorage.docName;
        a.authorizationId = this.ServiceTypesCount[i].authId; // this.auth_id
        a.creatorAuthId = localStorage.AuthId;
        a.analiz_hos_id = localStorage.hos_id;
        jsonData.push(a);
      }
      console.log(jsonData)
      const requestOptions = {
        method: "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify(jsonData)
      };
      console.log(requestOptions)
      this.loading = true;
      const response = await fetch(this.$store.state.hostname + "/Payments/addPayments", requestOptions);
      const data = await response.json();
      console.log('data payments')
      console.log(data)
      if(data[0].id){
        for(let l=0; l<this.OcheredServiceList.length; l++){
          if(this.OcheredServiceList[l].status == true){
            await this.ochred(this.get_patient_info.patient_id, this.OcheredServiceList[l].user_Id, this.OcheredServiceList[l].authId, this.OcheredServiceList[l].generete)
          }
        }
        this.loading = false;
        this.$refs.message.success('Added_successfully')
        this.ServiceTypesCount = [];
        this.checkedCategories = [];
        this.OcheredServiceList = [];
        this.summa = 0;
        this.summaString = this.summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
        this.balni
        this.update_patient_list();
        this.renderFunc(this.get_user_service_list)
        this.search = '';
        this.discount = 0;
        this.discount_summa = 0;
        this.discount_summaString = this.discount_summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
        this.fetch_get_patient_list_Doc_Id(this.auth_id)
        this.service_bahila_list_database = [];
        this.bron_patient_summ_nol();
        await this.fetch_ocherd_list();
        this.service_bahila_list_database = this.get_bahila_service_type_group.rows.map(item => {
              return {
                  id: item.id,
                  service_id: item.serviceType.id,
                  service_name: item.serviceType.name,
                  service_price: item.serviceType.price,
                  qty: 0,
              }
            })
        // this.auth_id = null;
        // this.active_el = -1;
        // this.user_id = null;
        // this.doc_name = '';
        // this.doc_position = '';
      }
      else{
        this.loading = false;
        this.$refs.msg.error('Error_successfully')
      }
      
    },
    async ochred(patient_id,doc_id, auth_id, generete){
      console.log('prosto ocheredga kirdi')
      const requestOptions = {
        method : "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
          "patientsId" : patient_id,
          "ochred_name_aout_genereted": generete,
          "id" : 0,
          "usersId": doc_id,
          "authorizationId": auth_id
        })
      };
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/HospitalOchreds", requestOptions);
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          const data = await response.json();
          console.log(data, 'jsonini oldim')
          let import_data = {
            doc_name : data.users.fio,
            client_name: data.patients.fio,
            born_date: data.patients.bornDate.slice(0,10),
            phone_number: data.patients.phoneNumber,
            number: data.ochred_name_aout_genereted
          }
          this.ochred_add_check(import_data)

          // this.show_otchert = true;
          // this.$refs.message.success('Added_successfully')
          return true;
        }
        else{
          const data = await response.text();
          this.modal_info = data;
          this.modal_status = true;
          return false;
        }
      }
      catch{
        this.loading = false;
        this.modal_info = this.$i18n.t('network_ne_connect'); 
        this.modal_status = true;
      }
    },
    async fetchGenereted(doc_id){
      let newdate = new Date().toISOString().slice(0,10);
      console.log(newdate)
      let b_date  = newdate + 'T00:00:35.000Z';
      let e_date  = newdate + 'T23:59:35.000Z';
      console.log(b_date)
      console.log(e_date)
      console.log(this.user_id)
      try{
        // this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/HospitalOchreds/getPaginationByDoktorIdAndDatesOxirgisiniOlish?user_id=" + doc_id + '&b_date=' + b_date + '&e_date='+ e_date);
        // this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          const data = await response.json();
          console.log(data ,  'sadasd data open Generete ga utdi')
          if(data.items_list.length == 0){
            this.genereted = '1'
          }
          else{
          console.log(data ,  'else Generete ga utdi')

            let test = parseInt(data.items_list[0].ochred_name_aout_genereted) + 1;
            this.genereted = test.toString();
            console.log(this.genereted)
          }
          // this.$refs.message.success('Added_successfully')
          return true;
        }
        else{
          const data = await response.text();
          this.modal_info = data;
          this.modal_status = true;
          return false;
        }
      }
      catch{
        this.loading = false;
        this.modal_info = this.$i18n.t('network_ne_connect'); 
        this.modal_status = true;
      }
    },
    async check_ochred(patient_id,doc_id, auth_id, index){
      this.check_ochret_status = false;
      console.log(auth_id)
      let newdate = new Date().toISOString().slice(0,10);
      console.log(newdate)
      let b_date  = newdate + 'T00:00:35.000Z';
      let e_date  = newdate + 'T23:59:35.000Z';
      try{
        // this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/HospitalOchreds/getPaginationByDoktorIdAndPatientIdDates?page=0&size=100&user_id=" + doc_id + '&patient_id=' + patient_id + '&b_date=' + b_date + '&e_date=' + e_date);
        // this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          const data = await response.json();
          console.log(data, 'jsonini ckeck ochret oldim')
          if(data.items_list.length == 0){
            console.log('add_new')
            await this.fetchGenereted(doc_id);
            this.OcheredServiceList[index].status = true;
            this.OcheredServiceList[index].generete = this.genereted;
            this.check_ochret_status = true;
          }
          else{
            this.genereted = data.items_list[0].ochred_name_aout_genereted;
            this.OcheredServiceList[index].status = false;
            this.OcheredServiceList[index].generete = this.genereted;
            console.log(data, 'check_Ochredda_else_ichiga kirdi')
            let import_data = {
              doc_name : data.items_list[0].users.FIO,
              client_name: data.items_list[0].patients.FIO,
              born_date: data.items_list[0].patients.BornDate.slice(0,10),
              phone_number: data.items_list[0].patients.PhoneNumber,
              number: data.items_list[0].ochred_name_aout_genereted
            }
            this.ochred_add_check(import_data)

            // this.show_otchert = true;
            // this.$refs.message.success('Added_successfully')
          }
          console.log('this.genereted')
          console.log(this.genereted)
          for(let i=0; i<this.ServiceTypesCount.length; i++){
            if(this.ServiceTypesCount[i].authId == auth_id){
              this.ServiceTypesCount[i].talon = this.genereted;
            }
          }
          return true;
        }
        else{
          const data = await response.text();
          this.modal_info = data;
          this.modal_status = true;
          return false;
        }
      }
      catch{
        this.loading = false;
        this.modal_info = this.$i18n.t('network_ne_connect'); 
        this.modal_status = true;
      }
    },
    async fetch_ocherd_list(){
      let newdate = new Date().toISOString().slice(0,10);
        console.log(newdate)
        let b_date  = newdate + 'T00:00:35.000Z';
        let e_date  = newdate + 'T23:59:35.000Z';
        console.log(b_date)
        console.log(e_date)
        console.log(this.user_id)
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/HospitalOchreds/getPaginationByDoktorIdAndDates?page=0&size=1000&user_id=" + this.user_id + '&b_date=' + b_date + '&e_date='+ e_date);
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            console.log(data ,  'sadasd data open')

            this.ocherd_list = data.items_list;
            
            // this.$refs.message.success('Added_successfully')
            return true;
          }
          else{
            const data = await response.text();
            this.modal_info = data;
            this.modal_status = true;
            return false;
          }
        }
        catch{
          this.loading = false;
          this.modal_info = this.$i18n.t('network_ne_connect'); 
          this.modal_status = true;
        }
    },
    async add_tomorrow_ochret(){
      this.tomorrow_date = new Date();
      this.tomorrow_date.setDate(this.tomorrow_date.getDate() + 1);
      console.log(this.tomorrow_date);
      await this.fetchGenereted_tomorrow(this.user_id);
      console.log(this.genereted_tomorrow);
      console.log(this.get_patient_info.patient_id);
      let newdate_tomorrow = this.tomorrow_date.toISOString().slice(0,10);
      let tomorrow_ochred = newdate_tomorrow + 'T10:00:35.000Z'
      await this.ochred_tomorrow(this.get_patient_info.patient_id,this.user_id, this.auth_id, this.genereted_tomorrow, tomorrow_ochred)
    },

    async fetchGenereted_tomorrow(doc_id){
      let newdate = this.tomorrow_date.toISOString().slice(0,10);
      console.log(newdate)
      let b_date  = newdate + 'T00:00:35.000Z';
      let e_date  = newdate + 'T23:59:35.000Z';
      console.log(b_date)
      console.log(e_date)
      console.log(this.user_id)
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/HospitalOchreds/getPaginationByDoktorIdAndDatesOxirgisiniOlish?user_id=" + doc_id + '&b_date=' + b_date + '&e_date='+ e_date);
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          const data = await response.json();
          // console.log(data ,  'sadasd data open Generete ga utdi')
          if(data.items_list.length == 0){
            this.genereted_tomorrow = '1'
          }
          else{
            console.log(data ,  'else Generete ga utdi')
            let test = parseInt(data.items_list[0].ochred_name_aout_genereted) + 1;
            this.genereted_tomorrow = test.toString();
            console.log(this.genereted)
          }
          // this.$refs.message.success('Added_successfully')
          return true;
        }
        else{
          this.loading = false;
          const data = await response.text();
          this.modal_info = data;
          this.modal_status = true;
          return false;
        }
      }
      catch{
        this.loading = false;
        this.modal_info = this.$i18n.t('network_ne_connect'); 
        this.modal_status = true;
      }
    },
    async ochred_tomorrow(patient_id,doc_id, auth_id, generete, tomorrow){
      console.log('prosto ocheredga kirdi')
      console.log(tomorrow)
      const requestOptions = {
        method : "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
          "patientsId" : patient_id,
          "ochred_name_aout_genereted": generete,
          "reg_date_ochred": tomorrow,
          "id" : 0,
          "usersId": doc_id,
          "authorizationId": auth_id
        })
      };
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/HospitalOchreds", requestOptions);
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          const data = await response.json();
          this.$refs.message.success('Added_successfully')
          console.log(data)
          return true;
        }
        else{
          const data = await response.text();
          this.modal_info = data;
          this.modal_status = true;
          return false;
        }
      }
      catch{
        this.loading = false;
        this.modal_info = this.$i18n.t('network_ne_connect'); 
        this.modal_status = true;
      }
    },
    

  },
 

}
</script>

<style lang="scss"> 
*{
  font-family: "Montserrat";
}
.send_page{
  height: calc(100vh - 50px);
  overflow: hidden;
}
.doctor_list_send_page{
  width: 28%;
  height: calc(100vh - 50px);
  overflow: hidden;
  overflow-y: auto;
  border-right: 1px solid rgb(78, 0, 63);
}
.Information{ 
  width: 72%;
}
.Information .PatientInfo {
  min-height: 73px;
  position: relative;
  // border-bottom: 1px solid rgb(155, 155, 155);
  box-shadow: 0 2px 8px rgb(187, 187, 187);
}
.doctor_list_send_page .item{
  .user_text_boxs{
    width: 100% !important;
    overflow: hidden;
  }
      .user_photo{
          background-color: #fff;
          border-radius: 50%;
          width: 45px !important;
          height: 45px !important;
          overflow: hidden;
          

        // img{
        //   border-radius: 50%;
        //   overflow: hidden;          
        // }
      }
      .doctor_fullname{
        width: calc(100% - 45px);
        overflow: hidden;

      }
      .doctor_fullname p{
        text-overflow: ellipsis;
        overflow: hidden;
        white-space: nowrap;
      }
      width:100%;
      box-shadow: 2px 2px 8px rgb(224, 224, 224), -1px -1px 2px rgb(224, 224, 224);
      height: 62px;
      margin: 5px auto;
      display: flex;
      align-items: center;
      justify-content: space-between;
      // background-color: #fff;
      transition: all 0.5s ease-in-out;
      .rang{
        color: rgb(66, 167, 255);
        font-weight: bold;
      }
      &:hover{
        cursor: pointer;
        box-shadow: 2px 2px 5px rgb(224, 224, 224);
        background-color: rgb(215, 242, 255);
        transform: translate(6px, 0px);
        transition: all 0.1s ease-in-out;
      }
    }
  .doctor_list_send_page .add_icon{
    border-radius: 50%;
    &:hover{
      box-shadow: 2px 2px 12px rgb(197, 197, 197), -1px -1px 10px rgb(224, 224, 224);
    }
  }
  .doctor_list_send_page .user_photo_back{
    background-image: url('../../assets/doc_icon.jpg');
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
}
.doctor_list_send_page .activeUser{
  cursor: pointer;
  box-shadow: 2px 2px 5px rgb(224, 224, 224);
  background-color: rgb(179, 230, 255);
  transform: translate(6px, 0px);
  transition: all 0.1s ease-in-out;
}
.wrap_chip{
  box-shadow: 0.5px 0.5px 1px rgb(225, 225, 225), -1px -1px 5px rgb(224, 224, 224);
  font-size: 13.5px;
  cursor: pointer;
  &:hover{
    // box-shadow: 2px 2px 10px rgb(193, 181, 251), -1px -1px 3px rgb(224, 224, 224);
    // border-bottom: 1px solid green;
    background-color: rgb(145, 231, 255);
  }
}
.picture_background{
  background-image: url('../../assets/doc_list_icon.jpg');
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
}
.addention_service{
  width: 100%;
  height: 60px;
  overflow: hidden;
  overflow-y: scroll;
}
.DoctorInfo{
    .picture{
      width: 50px;
      height: 50px;
      margin: 5px;
      border-radius: 5px;
      // box-shadow: 2px 2px 2px rgb(224, 224, 224), -1px -1px 3px rgb(224, 224, 224);
      background-color: #fff;
    }
    .info_doc{
      margin: 20px;
    }
  }
  .service_list{
    height: calc(100vh - 400px);
    overflow: hidden;
    overflow-y: auto;
  }
  .TablePatientDocIdSend{
    height: 185px;
    overflow: hidden;
    overflow-y: auto;
    // border: 1px solid #ddd;
  }
  .TablePatientDocIdSendNotBahila{
    height: 170px;
    overflow: hidden;
    overflow-y: auto;
    margin: 0 !important;
    margin-right: 16px !important;
    margin-left: 16px !important;
    // border: 1px solid #ddd;
  }
  .myTable {
  /* border-collapse: collapse; */
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  // border: 1px solid #ddd;
  font-size: 18px;
  max-height:80px; overflow-x:auto
}
.myTable th{
  font-weight: 600;
  font-size:12px;
}
.myTable td{
  font-size:13px;
}
.myTable th, .myTable td {
  text-align: left;
  padding: 10px;
}

.myTable tr {
  border-bottom: 1px solid rgb(240, 240, 240);
}

.editIcon{
  color:#01b348;
  font-size: 13px;

}
.editIcon:hover{
color: #000;
}

.delIcon:hover{
color: #000;
}
.delIcon{color: rgb(251, 70, 70);
  font-size: 13px;
}
.one_line{
  text-overflow: ellipsis; 
  overflow: hidden; 
  white-space: nowrap;
}
.bg-payed{
  background: #22ff7b;
}
.inputMinMax{
  width: 20px; height: 16px; 
  outline: none; 
  border-left: none; 
  border-right: none; 
  border-top: 0.1px solid rgb(220, 220, 220);
  border-bottom: 0.1px solid rgb(220, 220, 220);  
  font-size: 13px;
}
.InfoIcoFixedOpenInfo{
  transition: 0.5s;
  position:fixed;
  z-index: 11111;
  right:50%; 
  top:135px; 
  width: 40px; 
  height:40px; 
  cursor:pointer;
  background-image: linear-gradient( 83.2deg,  rgba(150,93,233,1) 10.8%, rgba(99,88,238,1) 94.3% );
  border-top-left-radius:5px; 
  border-bottom-left-radius:5px;
}
.InfoIcoFixedInfo{
  transition: 0.5s;
  position:fixed;
  z-index: 11111;
  right:0; 
  top:135px; 
  width: 40px; 
  height:40px; 
  cursor:pointer;
  background-image: linear-gradient( 83.2deg,  rgba(150,93,233,1) 10.8%, rgba(99,88,238,1) 94.3% );
  border-top-left-radius:5px; 
  border-bottom-left-radius:5px;
}
.Bron_patient_added_btn{
  transition: 0.5s;
  position:fixed;
  z-index: 11111;
  right:0; 
  top:180px;
  width: 40px;
  height:40px;
  cursor:pointer;
  background: linear-gradient(to right, rgb(242, 112, 156), rgb(255, 148, 114));
  border-top-left-radius:5px; 
  border-bottom-left-radius:5px;
}
.InfoIcoFixedOpenInfoPatient{
  transition: 0.5s;
  position:fixed;
  z-index: 11111;
  right:0%; 
  top:0; 
  width: 50%; 
  height:100vh; 
  cursor:pointer;
  background: white;
  border-top-left-radius:5px; 
  border-bottom-left-radius:5px;
}
.infoPatientId{
  position: fixed;
  width: 70%;
  height: 100vh;
  right:0%;
  background: white;
  z-index: 11111111111;
  transition: 0.5s;
  overflow: hidden;
  overflow-y: scroll;
}
.infoPatientIdNo{
  transition: 0.5s;
  position: fixed;
  width: 100%;
  height: 100vh;
  right:-100%;
  background: white;
  z-index: 1111111111;
}
.bg_choose_graduind{
  width: 100%;
  height: 3px;
  background: linear-gradient(25deg,#d64c7f,#ee4758 50%);
}
.bg_choosen_graduind{
  width: 100%;
  height: 3px;
  background-image: radial-gradient( circle farthest-corner at 10% 20%,  rgba(14,174,87,1) 0%, rgba(12,116,117,1) 90% );
}
.group_list_service{
  display: flex;
  flex-wrap:wrap;
  // width: 90% !important;
  margin-bottom:5px;
}
.group_list_service .group_item_service{
  border-radius: 4px;
  margin-right: 10px;
  background: #22ff7b;
  cursor: pointer;
  margin-top: 5px;  
}
.group_list_service .group_item_service p{
  font-size: 14px;
  padding: 4px 9px;
  margin: 0;
  text-overflow: ellipsis;
  width: 100%;
}
.activ_group_item{
  background: #25a1ff !important;
  color:#fff;
}
.delIcon{color: rgb(251, 70, 70);
  font-size: 13px;
}
</style>