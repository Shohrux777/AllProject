<template>
  <div class="bytimeCheckInOut">
    <loader v-if="loading"/>
      <div class="navbar_Nav_Edit d-flex justify-content-between"  container>
          <div class="d-flex align-items-center">
              <span to="#"> <span class="text-white">Kirish Chiqish1</span> </span>
          </div>
          <div>
              <div  class="excel d-flex  align-items-center">
                  <i class="fas fa-filter text-white"></i>
                  <div class="px-2"> 
                      <MDBInput type="text" v-model="search" size="sm" label="Search" style="color:white !important;"/>
                  </div>
                  <div class="px-2 btn_hover"> 
                    <download-excel
                      :data   = "filteredList"
                      :fields = "json_fields"
                      name="Зарплата отчета.xls">
                      <small class="text-white" style="font-size: 12px;">
                          <MDBIcon  icon="file-excel" />
                          EXCEL
                      </small>
                    </download-excel>
                      
                  </div>
                  <div class="px-2 btn_hover"> 
                      <small class="text-white" style="font-size: 12px;">
                          <MDBIcon  icon="file-pdf"  far/>
                          PDF
                      </small>
                  </div>
              </div>
          </div>
      </div>

      <div class="mainpage ">
        <div class="main_table">
          <div class="date_time_header d-flex mt-2 mb-0">
            <div style="width: 20%;" class="px-2">
              <MDBInput v-model="Start_time" type="date" size="sm" label="e_date" />
            </div>
            <div style="width: 20%;" class="px-2">
              <MDBInput v-model="End_time" type="date" size="sm" label="e_date" />
            </div>
            
            <div style="width: 20%;" class="px-2">
              <MDBBtn style="font-size: 9px;" @click="submit" color="primary">OK</MDBBtn>
              <MDBBtn style="font-size: 9px;" @click="refresh" color="info">Refresh</MDBBtn>
            </div>
            <div style="width: 60%;" class="px-0 d-flex" >
              <div style="width:300px;">
                <erpSelectDoor
                  :options = "get_deparment_list.rows"
                  @select="sub_door_select"
                  :selected="dept_name"
                  :label="$t('otdel')"
                  class="m-0 p-0"
                  style="margin-top: -30px !important;"
                />
              </div>
              <MDBSwitch
                label="Почасовая"
                v-model="switch1"
              />
              <MDBSwitch
                label="Дневная"
                v-model="switch2"
              />
              
            </div>
          </div>
          <div class="all_summa px-2" style="margin-top:-12px;">
            <small class="fw-bold mr-2">Общий сумма:</small>
            <small style="margin-left:10px;" class="mr-5">{{all_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small>

            <small class="px-5"></small>

            <small class="fw-bold">Рабочы дни:</small>
            <small style="margin-left:10px;">{{working_day_count}}</small>
          </div>
          
          <div class="mt-3">
            <MDBTable class="align-middle mb-0 bg-white">
                <thead class="bg-light">
                  <tr>
                    <th width="150">Стаж работы 
                        <span style="position:relative;">
                            <span @click="sortedWorkersAsc()"><MDBIcon icon="angle-up"  class="px-1 up_down_icon"  style="position:absolute; font-size: 11px; top:-2px; cursor:pointer;"/></span>
                            <span @click="sortedWorkers()"><MDBIcon icon="angle-down"  class="px-1 up_down_icon" style="position:absolute; font-size: 11px; bottom:-4px; cursor:pointer;"/></span>
                          </span>
                      </th>
                      <th width="300">{{$t('FIO')}}
                        <span style="position:relative;">
                            <span @click="sortedArrayAsc('ism')"><MDBIcon icon="angle-up"  class="px-1 up_down_icon"  style="position:absolute; font-size: 11px; top:-2px; cursor:pointer;"/></span>
                            <span @click="sortedArray('ism')"><MDBIcon icon="angle-down"  class="px-1 up_down_icon" style="position:absolute; font-size: 11px; bottom:-4px; cursor:pointer;"/></span>
                          </span>
                      </th>
                      <th width="100">User ID
                        <span style="position:relative;">
                          <span @click="sortedArrayAsc('userid')"><MDBIcon icon="angle-up"  class="px-1 up_down_icon"  style="position:absolute; font-size: 11px; top:-2px; cursor:pointer;"/></span>
                          <span @click="sortedArray('userid')"><MDBIcon icon="angle-down"  class="px-1 up_down_icon" style="position:absolute; font-size: 11px; bottom:-4px; cursor:pointer;"/></span>
                        </span>
                      </th>
                      <th width="150">{{$t('time_work')}}
                        <span style="position:relative;">
                          <span @click="sortedArrayAsc('worked_hours_itm')"><MDBIcon icon="angle-up"  class="px-1 up_down_icon"  style="position:absolute; font-size: 11px; top:-2px; cursor:pointer;"/></span>
                          <span @click="sortedArray('worked_hours_itm')"><MDBIcon icon="angle-down"  class="px-1 up_down_icon" style="position:absolute; font-size: 11px; bottom:-4px; cursor:pointer;"/></span>
                        </span>
                      </th>
                      <th>{{$t('day')}}
                        <span style="position:relative;">
                          <span @click="sortedArrayAsc('day')"><MDBIcon icon="angle-up"  class="px-1 up_down_icon"  style="position:absolute; font-size: 11px; top:-2px; cursor:pointer;"/></span>
                          <span @click="sortedArray('day')"><MDBIcon icon="angle-down"  class="px-1 up_down_icon" style="position:absolute; font-size: 11px; bottom:-4px; cursor:pointer;"/></span>
                        </span>
                      </th>
                      <th>День
                        <span style="position:relative;">
                          <span @click="sortedArrayAsc('day_count')"><MDBIcon icon="angle-up"  class="px-1 up_down_icon"  style="position:absolute; font-size: 11px; top:-2px; cursor:pointer;"/></span>
                          <span @click="sortedArray('day_count')"><MDBIcon icon="angle-down"  class="px-1 up_down_icon" style="position:absolute; font-size: 11px; bottom:-4px; cursor:pointer;"/></span>
                        </span>
                      </th>
                      <th>Расходь
                        <span style="position:relative;">
                          <span @click="sortedArrayAsc('rasxod')"><MDBIcon icon="angle-up"  class="px-1 up_down_icon"  style="position:absolute; font-size: 11px; top:-2px; cursor:pointer;"/></span>
                          <span @click="sortedArray('rasxod')"><MDBIcon icon="angle-down"  class="px-1 up_down_icon" style="position:absolute; font-size: 11px; bottom:-4px; cursor:pointer;"/></span>
                        </span>
                      </th>
                      <th>{{$t('type_salary')}}
                        <span style="position:relative;">
                          <span @click="sortedArrayAsc('oylik_nomi')"><MDBIcon icon="angle-up"  class="px-1 up_down_icon"  style="position:absolute; font-size: 11px; top:-2px; cursor:pointer;"/></span>
                          <span @click="sortedArray('oylik_nomi')"><MDBIcon icon="angle-down"  class="px-1 up_down_icon" style="position:absolute; font-size: 11px; bottom:-4px; cursor:pointer;"/></span>
                        </span>
                      </th>
                      <th>{{$t('salary')}}
                        <span style="position:relative;">
                          <span @click="sortedArrayAsc('ishlagan_puli')"><MDBIcon icon="angle-up"  class="px-1 up_down_icon"  style="position:absolute; font-size: 11px; top:-2px; cursor:pointer;"/></span>
                          <span @click="sortedArray('ishlagan_puli')"><MDBIcon icon="angle-down"  class="px-1 up_down_icon" style="position:absolute; font-size: 11px; bottom:-4px; cursor:pointer;"/></span>
                        </span>
                      </th>
                      <th width="140">{{$t('Action')}}</th>
                  </tr>
                </thead>
                <tbody>
                <tr v-for="(row, index) in filteredList" :key="index" 
                :class="{'alert-danger': row.vaqt_flag == 3, 'alert-warning': row.vaqt_flag == 0, 'alert-success': row.vaqt_flag == 1,}">
                    <td style="cursor: pointer;" @click="change_kelgan_vaqt(row.userid)">{{ row.kelganidan_beri }}</td>
                    <td>
                    <div class="d-flex align-items-center">
                        <div class="ms-3">
                        <p class="fw-bold mb-1" v-if="row.ism != null">{{ row.ism }}</p>
                        <p class="text-muted mb-0" v-if="row.familiya != null">{{ row.familiya }}</p>
                        </div>
                    </div>
                    </td>
                    <td style="width:60px;">
                      {{ row.userid }}
                    </td>
                    <!-- <td v-if="row.ishlagan_vaqtlar_ls.length>0"><small v-for="(item, i) in row.ishlagan_vaqtlar_ls" :key="i" style="display: block;">{{ item }}</small></td>
                    <td v-else class="text-danger">---</td> -->
                    
                    <td :class="{'text-danger': row.worked_hours_itm == '---'}">{{row.worked_hours_itm}}</td>
                    <td>{{ row.day }}</td>
                    <td class="text-info" style="cursor:pointer" @click="func_show_user_add_status(row)">{{ row.day_count }}</td>
                    <td class="text-danger" style="cursor:pointer" @click="func_show_user_rasxod_status(row)">{{ row.rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</td>
                    <td>{{row.oylik_nomi}}</td>
                    <td v-if="row.without_gr_id == 2" @click="func_show_add_rasxod(row.userid, row.ism, index,row.ishlagan_puli)" 
                    :class="{'text-success': row.ishlagan_puli != 0, 'text-danger': row.ishlagan_puli<0}" 
                    style="cursor:pointer" >
                      {{row.ishlagan_puli.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                    </td>
                    <td v-else @click="func_show_add_rasxod(row.userid, row.ism, index, row.ishlagan_puli)" 
                    :class="{'text-success': row.ishlagan_puli != 0, 'text-danger': row.ishlagan_puli<0}"
                    style="cursor:pointer">
                      {{row.ishlagan_puli.toFixed(0).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                    </td>
                    <!-- <td>{{row.ishlagan_puli}}</td> -->
                    <td class="text-center">
                      <span class="mx-1 action_info" style="display: inline-block;" @click="func_show_add_note(row.userid, row.ism, index)">
                        <i v-if="row.user_note != 1" class="fas fa-sticky-note"  style="cursor: pointer;"></i>
                        <i v-else class="fas fa-check text-success" style="cursor: pointer;"></i>
                        <small>Заметка</small>
                      </span>
                      <span class="mx-1 action_info" style="display: inline-block;" @click="func_add_avto_rasxod(row.userid, row.ism, index)">
                        <i class="fas fa-bolt" data-fa-transform="shrink-8 up-4 right-6" style="cursor: pointer;"></i>
                        <small>Авто расход</small>
                      </span>
                      <span class="mx-1 action_info" style="display: inline-block;" @click="func_show_add_days(row.userid, row.ism, index)">
                        <i class="fa-solid fa-hourglass-start text-info" style="cursor: pointer;"></i>
                        <small>День</small>
                      </span>
                      <span class="mx-1 action_info" style="display: inline-block;" @click="func_show_add_rasxods(row.userid, row.ism, index)">
                        <i class="fa-regular fa-registered text-danger" style="cursor: pointer;"></i>
                        <small>Расходь</small>
                      </span>
                      <span class="mx-1" style="display: inline-block;"><i class="fas fa-arrow-alt-circle-left text-primary " @click="show_user_details_report(row.userid)" style="cursor: pointer;"></i></span>
                      <span class="mx-1" style="display: inline-block;"><i class="fas fa-eye text-success" @click="show_user_report(row.userid)" style="cursor: pointer;"></i></span>
                    </td>
                </tr>
                </tbody>
              </MDBTable>
          </div>
        </div>
      </div>
      <MDBModal
          id="exampleModal"
          tabindex="-1"
          labelledby="exampleModalLabel"
          v-model="exampleModal"
          size="lg"
      >
          <MDBModalHeader style="background: #475a65;" class="text-white px-3 py-2">
              <MDBModalTitle id="exampleModalLabel"> {{$t('door_add')}} </MDBModalTitle>
          </MDBModalHeader>
          <MDBModalBody>
              <div class="userWorkTime">
                <MDBTable class="align-middle mb-0 bg-white">
                  <thead class="bg-light">
                    <tr>
                        <th>№</th>
                        <th>{{$t('FIO')}}
                        </th>
                        <th width="100">User ID
                        </th>
                        <th width="150">{{$t('time_work')}}</th>
                        <th>{{$t('date')}}</th>
                    </tr>
                  </thead>
                  <tbody>
                  <tr v-for="(row, index) in userWorkTime" :key="index" >
                      <td >{{index+1}}</td>
                      <td>
                      <div class="d-flex align-items-center">
                          <div class="ms-3">
                          <p class="fw-bold mb-1" v-if="row.ism != null">{{ row.ism }}</p>
                          <p class="text-muted mb-0" v-if="row.familiya != null">{{ row.familiya }}</p>
                        </div>
                      </div>
                      </td>
                      <td style="width:60px;">
                        {{ row.userid }}
                      </td>
                      <!-- <td v-if="row.ishlagan_vaqtlar_ls.length>0"><small v-for="(item, i) in row.ishlagan_vaqtlar_ls" :key="i" style="display: block;">{{ item }}</small></td>
                      <td v-else class="text-danger">---</td> -->
                      
                      <td>{{row.worked_hours_itm_str}}</td>

                      <td>
                        {{row.kun}}
                      </td>
                  </tr>
                  </tbody>
                </MDBTable>
              </div>
          </MDBModalBody>
      </MDBModal>

      <MDBModal
          id="exampleModal"
          tabindex="-1"
          labelledby="exampleModalLabel"
          v-model="exampleModal1"
          size="xl"
      >
          <MDBModalHeader style="background: #475a65;" class="text-white px-3 py-2">
              <MDBModalTitle id="exampleModalLabel"> {{$t('door_add')}} </MDBModalTitle>
          </MDBModalHeader>
          <MDBModalBody>
              <div class="userWorkTime" v-for="(item,i) in list_of_in_out" :key="i">
                <div class="my-2 px-4 alert-danger" >
                  <span>{{ item.kun }}</span>
                  <span class="mx-3"> Ishlagan vaqti: <span class="mx-2 text-success" >{{ userWorkTime[i].worked_hours_itm_str }}</span></span>
                </div>
                <MDBTable class="align-middle mb-0 bg-white">
                  <thead class="bg-light">
                    <tr>
                        <th>№</th>
                        <th>{{$t('FIO')}}
                        </th>
                        <th width="100">User ID
                        </th>
                        <th>{{$t('checktype')}}</th>
                        <th>{{$t('door_name')}}</th>
                        <th>{{$t('date')}}</th>
                        <th>{{$t('time')}}</th>
                    </tr>
                  </thead>
                  <tbody>
                  <tr v-for="(row, index) in item.list" :key="index+1000" >
                      <td >{{index+1}}</td>
                      <td>
                      <div class="d-flex align-items-center">
                          <div class="ms-3">
                          <p class="fw-bold mb-1" v-if="row.fio != null">{{ row.fio }}</p>
                          <p class="text-muted mb-0" v-if="row.userinfo.familiya != null">{{ row.userinfo.familiya }}</p>
                        </div>
                      </div>
                      </td>
                      <td style="width:60px;">
                        {{ row.userid }}
                      </td>
                      <td>
                        <MDBBadge v-if="row.checktype == 'I' || row.checktype == 'K'" badge="success" pill class="d-inline">Входить</MDBBadge>
                        <MDBBadge v-else badge="warning" pill class="d-inline">Выход</MDBBadge>
                      </td>
                      <td>{{row.door_name}}</td>

                      <td>
                        {{row.sana.slice(0,10)}}
                      </td>
                      <td>
                        {{row.checktime}}
                      </td>
                  </tr>
                  </tbody>
                </MDBTable>
              </div>
          </MDBModalBody>
      </MDBModal>
      <MDBModal
          id="exampleModal1"
          tabindex="1"
          labelledby="exampleModalLabel1"
          v-model="show_add_day"
      >
          <MDBModalHeader style="background: #475a65;" class="text-white px-3 py-2">
              <MDBModalTitle id="exampleModalLabel1"> {{$t('day_add')}} </MDBModalTitle>
          </MDBModalHeader>
          <MDBModalBody>
            <div class="px-3 row">
              <div class="col-12 mt-2 mb-3">
                <MDBInput v-model="day_time" type="date" size="sm" label="Дата" />
              </div>
              <div class="col-12 mb-3">
                <div class="d-flex">
                  <div class="add_num_day1 border rounded" :class="{'bg-success text-white': day_num == 1}" @click="func_day_plus">
                    <span>+1</span>
                  </div>
                  <div class="add_num_day2 border rounded" :class="{'bg-danger text-white': day_num == -1}" @click="func_day_minus"
                    style="margin-left: 20px !important;">
                    <span class="ml-4">-1</span>
                  </div>
                  <div style="width:100px;">
                    <input type="number" class="mx-4 border rounded form-control" style="height:33px;" v-model="day_num">
                  </div>
                </div>
              </div>
              <div class="col-12 mb-3">
                <MDBInput v-model="day_info" type="text" size="sm" label="Примечания" />
              </div>
              <div class="col-12 mb-3 border-top pt-1">
                <div class="d-flex justify-content-end">
                  <MDBBtn style="font-size: 9px;" @click="submit_day_info" color="success">Сохранить</MDBBtn>
                </div>
              </div>
            </div>
          </MDBModalBody>
      </MDBModal>
      <MDBModal
          id="exampleModal7"
          tabindex="2"
          labelledby="exampleModalLabel7"
          v-model="show_checkinout"
      > 
          <MDBModalHeader style="background: #475a65;" class="text-white px-3 py-2">
              <MDBModalTitle id="exampleModalLabel"> Xodimni birinchi kelgan vaqtini kiritish </MDBModalTitle>
          </MDBModalHeader>
          <MDBModalBody>
            <div class="px-3 row">
              <div class="col-12 mt-2 mb-3">
                <MDBInput v-model="day_time" type="date" size="sm" label="Дата" />
              </div>
              <div class="col-12 mb-3 border-top pt-1">
                <div class="d-flex justify-content-end">
                  <MDBBtn style="font-size: 9px;" @click="submit_checkinout" color="success">Сохранить</MDBBtn>
                </div>
              </div>
            </div>
          </MDBModalBody>
      </MDBModal>

      <MDBModal
          id="exampleModal2"
          tabindex="2"
          labelledby="exampleModalLabel2"
          v-model="show_add_rasxod"
      > 
          <MDBModalHeader style="background: #475a65;" class="text-white px-3 py-2">
              <MDBModalTitle id="exampleModalLabel"> {{$t('rasxod_zarplata')}} </MDBModalTitle>
          </MDBModalHeader>
          <MDBModalBody>
            <div class="px-3 row">
              <div class="col-12 mt-2 mb-3">
                <MDBInput v-model="day_time" type="date" size="sm" label="Дата" />
              </div>
              <div class="col-12 mb-3" style="position:relative;">
                <!-- <MDBInput v-model="rasxod_summa" type="number" size="sm" label="Расходь сумма" /> -->
                <input type="text" v-model="rasxod_summaString"  @keyup="funcCash($event.target.value)"  
                class="form-control  mt-2 text-right pr-2" style="border:1px solid #BDBDBD; outline:none;font-size:13px; height:30px;" >
                <small style="position:absolute; background:white; padding: 0px 3px; z-index: 55; top:-2px; left:21px; font-size:11.5px; " class="testing">
                  Расходь сумма
                </small> 
              </div>
              <div class="col-12 mb-3" style="position:relative;">
                <!-- <MDBInput v-model="rasxod_summa" type="number" size="sm" label="Расходь сумма" /> -->
                <input type="text" v-model="zaplata_summaString"  @keyup="funcCashZaplata($event.target.value)"  
                class="form-control  mt-2 text-right pr-2" style="border:1px solid #BDBDBD; outline:none;font-size:13px; height:30px;" >
                <small style="position:absolute; background:white; padding: 0px 3px; z-index: 55; top:-2px; left:21px; font-size:11.5px; " class="testing">
                  Зарплата сумма
                </small> 
              </div>
              <div class="col-12 mb-3">
                <MDBInput v-model="day_info" type="text" size="sm" label="Примечания" />
              </div>
              <div class="col-12 mb-3 border-top pt-1">
                <div class="d-flex justify-content-end">
                  <MDBBtn style="font-size: 9px;" @click="submit_rasxod_info" color="success">Сохранить</MDBBtn>
                </div>
              </div>
            </div>
          </MDBModalBody>
      </MDBModal>

      <MDBModal
          id="exampleModal5"
          tabindex="2"
          labelledby="exampleModalLabel5"
          v-model="show_avto_rasxod"
      > 
          <MDBModalHeader style="background: #475a65;" class="text-white px-3 py-2">
              <MDBModalTitle id="exampleModalLabel"> Авто расходь </MDBModalTitle>
          </MDBModalHeader>
          <MDBModalBody>
            <div class="px-3 row">
              <div class="col-12 mb-3" style="position:relative;">
                <!-- <MDBInput v-model="rasxod_summa" type="number" size="sm" label="Расходь сумма" /> -->
                <input type="number" v-model="rasxod_day"    
                class="form-control  mt-2 text-right pr-2" style="border:1px solid #BDBDBD; outline:none;font-size:13px; height:30px;" >
                <small style="position:absolute; background:white; padding: 0px 3px; z-index: 55; top:-2px; left:21px; font-size:11.5px; " class="testing">
                  День
                </small> 
              </div>
              <div class="col-12 mb-3" style="position:relative;">
                <!-- <MDBInput v-model="rasxod_summa" type="number" size="sm" label="Расходь сумма" /> -->
                <input type="text" v-model="rasxod_summaString"  @keyup="funcCash($event.target.value)"  
                class="form-control  mt-2 text-right pr-2" style="border:1px solid #BDBDBD; outline:none;font-size:13px; height:30px;" >
                <small style="position:absolute; background:white; padding: 0px 3px; z-index: 55; top:-2px; left:21px; font-size:11.5px; " class="testing">
                  Авто расходь сумма
                </small> 
              </div>
              
              <div class="col-12 mb-3">
                <MDBInput v-model="day_info" type="text" size="sm" label="Примечания" />
              </div>
              <div class="col-12 mb-3 border-top border-bottom pt-1 pb-1">
                <div class="d-flex justify-content-end">
                  <MDBBtn style="font-size: 9px;" @click="submit_avto_rasxod" color="success">Сохранить</MDBBtn>
                </div>
              </div>

              <div class="col-12 mt-3 ">
                <div class="userWorkTime">
                  <MDBTable class="align-middle mb-0 bg-white">
                    <thead class="bg-light">
                      <tr>
                          <th>{{$t('FIO')}}
                          </th>
                          <th width="50">День</th>
                          <th>Cумма</th>
                          <th>{{$t('note')}}</th>
                          <th>{{$t('date')}}</th>
                      </tr>
                    </thead>
                    <tbody>
                    <tr v-for="(row, index) in user_avto_rasxod_list" :key="index" >
                        <td>
                          {{row.user_name}}
                        </td>
                        <td >
                          {{ row.day }}
                        </td>
                        <td >
                          {{ row.sum }}
                        </td>
                        <td>
                          {{row.note}}
                        </td>
                        <td>
                          {{row.created_date_time.slice(0,10)}}
                        </td>
                    </tr>
                    </tbody>
                  </MDBTable>
                </div>
              </div>
            </div>
          </MDBModalBody>
      </MDBModal>

      <MDBModal
          id="exampleModal6"
          tabindex="2"
          labelledby="exampleModalLabel6"
          v-model="show_user_note"
      > 
          <MDBModalHeader style="background: #475a65;" class="text-white px-3 py-2">
              <MDBModalTitle id="exampleModalLabel"> Заметка </MDBModalTitle>
          </MDBModalHeader>
          <MDBModalBody>
            <div class="px-3 row">
              
              <div class="col-12 mb-3">
                <MDBInput v-model="day_info" type="text" size="sm" label="Заметка" />
              </div>
              <div class="col-12 mb-3 border-top border-bottom pt-1 pb-1">
                <div class="d-flex justify-content-end">
                  <MDBBtn style="font-size: 9px;" @click="submit_user_note" color="success">Сохранить</MDBBtn>
                </div>
              </div>

              <div class="col-12 mt-3 ">
                <div class="userWorkTime">
                  <MDBTable class="align-middle mb-0 bg-white">
                    <thead class="bg-light">
                      <tr>
                          <th>{{$t('FIO')}}
                          </th>
                          <th>{{$t('note')}}</th>
                          <th>{{$t('date')}}</th>
                      </tr>
                    </thead>
                    <tbody>
                    <tr v-for="(row, index) in user_note_list" :key="index">
                        <td>
                          {{row.user_name}}
                        </td>
                        <td>
                          {{row.note}}
                        </td>
                        <td>
                          {{row.created_date_time.slice(0,10)}}
                        </td>
                    </tr>
                    </tbody>
                  </MDBTable>
                </div>
              </div>
            </div>
          </MDBModalBody>
      </MDBModal>

      <MDBModal
          id="exampleModal"
          tabindex="-1"
          labelledby="exampleModalLabel"
          v-model="show_user_add_status"
          size="lg"
      >
          <MDBModalHeader style="background: #475a65;" class="text-white px-3 py-2">
              <MDBModalTitle id="exampleModalLabel"> Добавить день</MDBModalTitle>
          </MDBModalHeader>
          <MDBModalBody>
              <div class="userWorkTime">
                <MDBTable class="align-middle mb-0 bg-white">
                  <thead class="bg-light">
                    <tr>
                        <th>№</th>
                        <th>{{$t('FIO')}}
                        </th>
                        <th width="100">User ID
                        </th>
                        <th width="150">День</th>
                        <th>{{$t('note')}}</th>
                        <th>{{$t('date')}}</th>
                    </tr>
                  </thead>
                  <tbody>
                  <tr v-for="(row, index) in show_user_add_data" :key="index" >
                      <td >{{index+1}}</td>
                      <td>
                      <div class="d-flex align-items-center">
                          <div class="ms-3">
                          <p class="fw-bold mb-1" v-if="row.user_name != null">{{ row.user_name }}</p>
                        </div>
                      </div>
                      </td>
                      <td style="width:60px;">
                        {{ row.userid }}
                      </td>
                      <!-- <td v-if="row.ishlagan_vaqtlar_ls.length>0"><small v-for="(item, i) in row.ishlagan_vaqtlar_ls" :key="i" style="display: block;">{{ item }}</small></td>
                      <td v-else class="text-danger">---</td> -->
                      
                      <td>{{row.count}}</td>

                      <td>
                        {{row.note}}
                      </td>
                      <td>
                        {{row.reg_date.slice(0,10)}}
                      </td>
                  </tr>
                  </tbody>
                </MDBTable>
              </div>
          </MDBModalBody>
      </MDBModal>
      <MDBModal
          id="exampleModal"
          tabindex="-1"
          labelledby="exampleModalLabel"
          v-model="show_user_rasxod_status"
          size="lg"
      >
          <MDBModalHeader style="background: #475a65;" class="text-white px-3 py-2">
              <MDBModalTitle id="exampleModalLabel"> Расходь </MDBModalTitle>
          </MDBModalHeader>
          <MDBModalBody>
              <div class="userWorkTime">
                <MDBTable class="align-middle mb-0 bg-white">
                  <thead class="bg-light">
                    <tr>
                        <th>№</th>
                        <th>{{$t('FIO')}}
                        </th>
                        <th width="100">User ID
                        </th>
                        <th width="150">Расходь</th>
                        <th>Статус</th>
                        <th>{{$t('note')}}</th>
                        <th>{{$t('date')}}</th>
                    </tr>
                  </thead>
                  <tbody>
                  <tr v-for="(row, index) in show_user_rasxod_data" :key="index" >
                      <td >{{index+1}}</td>
                      <td>
                      <div class="d-flex align-items-center">
                          <div class="ms-3">
                          <p class="fw-bold mb-1" v-if="row.user_name != null">{{ row.user_name }}</p>
                        </div>
                      </div>
                      </td>
                      <td style="width:60px;">
                        {{ row.userid }}
                      </td>                      
                      <td>{{row.count}}</td>
                      <td v-if="row.num_1 == 0" class="text "><MDBBadge badge="danger" pill>Расходь сумма</MDBBadge></td>
                      <td v-else class="text "><MDBBadge badge="success" pill>Зарплата сумма</MDBBadge></td>
                      <td>
                        {{row.note}}
                      </td>
                      <td>
                        {{row.reg_date.slice(0,10)}}
                      </td>
                  </tr>
                  </tbody>
                </MDBTable>
              </div>
          </MDBModalBody>
      </MDBModal>
  </div>
</template>

<script>
import {
  MDBIcon,MDBInput, MDBBtn, MDBTable, MDBBadge,
  MDBModal,
  MDBModalHeader,
  MDBModalTitle,
  MDBModalBody,
  MDBSwitch 
} from 'mdb-vue-ui-kit';
import { ref } from 'vue';
import erpSelectDoor from '@/components/erpSelectAdd.vue'
import {mapActions, mapGetters} from 'vuex'
import Loader from '@/components/loader.vue';
import downloadExcel from "vue-json-excel3";

export default {
  setup() {
    const exampleModal = ref(false);
    const exampleModal1 = ref(false);
    const show_add_day = ref(false);
    const show_add_rasxod = ref(false);
    const switch1 = ref(false);
    const switch2 = ref(false);
    const show_user_add_status = ref(false);
    const show_user_rasxod_status = ref(false);
    return {
      exampleModal,
      exampleModal1,
      switch1,
      switch2,
      show_add_day,
      show_add_rasxod,
      show_user_add_status,
      show_user_rasxod_status
    };
  },
  components: {
      MDBIcon,
      MDBInput,
      MDBBtn,
      MDBTable,
      MDBBadge,
      MDBModal,
      MDBModalHeader,
      MDBModalTitle,
      MDBModalBody,
      erpSelectDoor,
      Loader,
      downloadExcel,
      MDBSwitch
  },
  
  data(){
    return{
      loading: false,
      dept_name: '',
      dept_id: null,
      Start_time: null,
      End_time: null,
      day_time: null,
      day_num: 0,
      day_info: '',
      user_id: 0,
      user_name: '',
      item_index: -1,
      all_summa: 0,
      show_user_add_data: [],
      show_user_rasxod_data: [],


      rasxod_summa : null,
      rasxod_summaString: '',
      zaplata_summa : null,
      zaplata_summaString: '',
      zaplata_status: 0,
      reportList: [],
      main_reportList: [],
      user_add_del_list: [],
      user_rasxod_list: [],
      userWorkTime: [],
      show_avto_rasxod: false,
      user_avto_rasxod_list: [],
      rasxod_day: null,
      show_user_note: false,
      user_note_list: [],
      checkinout_data: {},
      show_checkinout: false,

      search: '',
      json_fields: {
        'ФИО': 'ism',
        'User ID': 'userid',
        'Раб. время': 'ishlagan_vaqtlar_ls',
        "Дни": 'day',
        "День": 'day_count',
        "Расход": 'rasxod',
        'Тип зарплаты': 'oylik_nomi',
        'Зарплата' : 'ishlagan_puli'
      },
      list_of_in_out: [],
      working_day_count: 0,
      sortKey :  '',
      sortDir : '',
    }
  },
  computed: {
    ...mapGetters(['get_door_list', 'get_deparment_list', 'get_user_add_note_list', 'get_user_rasxod_list']),
    
    filteredList: function(){
      let items = this.reportList;
      this.all_summa = 0;
      if(this.search)
      {
        items = items.filter( item => {
          return this.search.toLowerCase().split(' ').every(v => item.ism.toLowerCase().includes(v))
        })
        this.all_summa = items.reduce((accumulator ,item) => {
          return accumulator += parseFloat(item.ishlagan_puli);
        }, 0)
      }
      if(this.dept_id != null){
        items = items.filter( item => {
          return item.departid == this.dept_id;
        })
        this.all_summa = items.reduce((accumulator ,item) => {
          return accumulator += parseFloat(item.ishlagan_puli);
        }, 0)
      }
      if(this.switch1 && !this.switch2){
        let soatbay = 0;
        if(this.switch1){
          soatbay = 1;
        }
        items = items.filter( item => {
          return item.without_gr_id == soatbay;
        })
        this.all_summa = items.reduce((accumulator ,item) => {
          return accumulator += parseFloat(item.ishlagan_puli);
        }, 0)
      }
      else if(this.switch2 && !this.switch1){
        let kunbay = 0;
        if(this.switch2){
          kunbay = 2;
        }
        items = items.filter( item => {
          return item.without_gr_id == kunbay;
        })
        this.all_summa = items.reduce((accumulator ,item) => {
          return accumulator += parseFloat(item.ishlagan_puli);
        }, 0)
      }
      else if(this.switch2 && this.switch1){
        items = items.filter( item => {
          return item.without_gr_id == 1 || item.without_gr_id == 2;
        })
        this.all_summa = items.reduce((accumulator ,item) => {
          return accumulator += parseFloat(item.ishlagan_puli);
        }, 0)
      }
      if(this.search == '' && this.dept_id == null && this.switch1 == false && this.switch2 == false)
      {
        items = this.reportList;
        this.all_summa = items.reduce((accumulator ,item) => {
          return accumulator += parseFloat(item.ishlagan_puli);
        }, 0)
      }
      if (this.sortKey) {
        items.sort((a, b) => {
          if (a[this.sortKey] < b[this.sortKey]) return this.sortDir === 'asc' ? -1 : 1;
          if (a[this.sortKey] > b[this.sortKey]) return this.sortDir === 'asc' ? 1 : -1;
          return 0;
        });
      }
      return items;
    },

  },
  async mounted(){
    let time1 = new Date();
    this.Start_time = time1.toISOString().slice(0,10); 
    this.End_time = time1.toISOString().slice(0,10);
    this.day_time = time1.toISOString().slice(0,10);
    // await this.fetchFindSundayInMonth();
    await this.fetch_Department();

    let start = this.Start_time;
    let end = this.End_time;
    await this.submit();
    console.log(start)
    console.log(end)
  },
  methods:{
    ...mapActions([ 'fetch_Door', 'fetch_Department', 'fetch_user_add_note_list', 'fetch_user_rasxod_list']),

    
    async fetchFindSundayInMonth(){
      const daysInMonth = (year, month) => new Date(year, month, 0).getDate();
      var d = new Date(this.Start_time);
      var getTot = daysInMonth(d.getFullYear(), d.getMonth()+1); // Bunda 11 chi oy bulsa 11 deb yozilishi kerak ekan
      
      var sun = new Array();
      for(var i=1;i<=getTot;i++){    //looping through days in month
          var newDate = new Date(d.getFullYear(),d.getMonth(),i)
          // console.log(newDate)
          if(newDate.getDay()==0){   //if Sunday
              sun.push(i);
          }
      }
      this.working_day_count = parseInt(getTot) - parseInt(sun.length);
      console.log('sun');
      console.log(sun);
      console.log(this.working_day_count);
    },
    funcCash(n){
        this.zaplata_summa = null;
        this.zaplata_summaString = '';
        this.zaplata_status = 0;
        // this.rasxod_summaString = ''; 
        // n = n[n.length-1];
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }

       this.rasxod_summaString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.rasxod_summaString.length; i++){
        if(this.rasxod_summaString[i] != ' '){
          temp += this.rasxod_summaString[i];
        }
       }
       console.log(temp)
       if(temp != ''){
        this.rasxod_summa = parseFloat(temp);
       }
       console.log(this.rasxod_summa)

      
      
    },
    funcCashZaplata(n){
        this.rasxod_summa = null;
        this.rasxod_summaString = '';
        this.zaplata_status = 1;
        // n = n[n.length-1];
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }

       this.zaplata_summaString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.zaplata_summaString.length; i++){
        if(this.zaplata_summaString[i] != ' '){
          temp += this.zaplata_summaString[i];
        }
       }
       console.log(temp)
       if(temp != ''){
        this.zaplata_summa = parseFloat(temp);
       }
       console.log(this.zaplata_summa)

      
      
    },
    func_show_user_add_status(data){
      console.log(data)
      this.show_user_add_data = data.day_list;
      this.show_user_add_status = true;

    },
    func_show_user_rasxod_status(data){
      console.log(data)
      this.show_user_rasxod_data = data.rasxod_list;
      this.show_user_rasxod_status = true;
    },
    func_show_add_days(user_id, user_name, index){
      this.show_add_day = true;
      let time1 = new Date();
      this.day_time = time1.toISOString().slice(0,10);
      this.day_num = 0;
      this.day_info = '';
      console.log(user_id);
      this.user_id = user_id;
      this.user_name = user_name;
      this.item_index = index;
    },
    func_show_add_rasxods(user_id, user_name, index){
      this.show_add_rasxod = true;
      let time1 = new Date();
      this.day_time = time1.toISOString().slice(0,10);
      this.rasxod_summa = null;
      this.zaplata_summa = null;
      this.day_info = '';
      console.log(user_id);
      this.user_id = user_id;
      this.user_name = user_name;
      this.item_index = index;
      this.rasxod_summaString = '';
      this.zaplata_summaString = '';
    },
    func_show_add_rasxod(user_id, user_name, index,summa){
      this.show_add_rasxod = true;
      let time1 = new Date();
      this.day_time = time1.toISOString().slice(0,10);
      this.rasxod_summa = 0;
      this.zaplata_summa = summa;
      this.day_info = '';
      console.log(user_id);
      this.user_id = user_id;
      this.user_name = user_name;
      this.item_index = index;
      this.rasxod_summaString = '';
      this.zaplata_summaString = summa.toFixed(0).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
    },
    async change_kelgan_vaqt(userId){
      try{
        const response = await fetch(this.$store.state.hostname + "/SkudMyCheckinouts/getPaginationGetByUserID?page=0&size=1&user_id=" + userId);
        const data = await response.json();
        if(response.status == 200 || response.status == 201){
          this.show_checkinout = true;
          if(data.items_list.length>0){
            this.checkinout_data = data.items_list[0];
            this.day_time = this.checkinout_data.sana.slice(0,10);
          }
          else{
            let time1 = new Date();
            this.day_time = time1.toISOString().slice(0,10);
            this.checkinout_data = {
              userid: userId,
              sana: '',
              checktime: "10:55:40",
              checktype: "K",
              door_name: "Face",
              code: 0,
            }
          }
        }
      }
      catch(error){
        this.loading = false;
        console.log(error)
      }
    },
    async submit_checkinout(){
      try{
        const requestOptions = {
          method: "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            "userid" : this.checkinout_data.userid,
            "sana" : this.day_time + 'T00:00:00',
            "checktime" : this.checkinout_data.checktime,
            "checktype": this.checkinout_data.checktype,
            "door_name": this.checkinout_data.door_name,
            "code": this.checkinout_data.code
            })
          };
          const response = await fetch(this.$store.state.hostname + '/SkudMyCheckinouts', requestOptions);
          // const data = await response.json();
          if(response.status == 200 || response.status == 201){
            this.day_time = null;
            this.show_checkinout = false;
          }
      }
      catch(error){
        console.log(error)
      }
    },
    async func_add_avto_rasxod(user_id, user_name, index){
      this.show_avto_rasxod = true;
      this.user_id = user_id;
      this.user_name = user_name;
      this.item_index = index;
      await this.fetch_avto_rasxod_list(user_id);
    },
    async fetch_avto_rasxod_list(userId){
      try{
        const response = await fetch(this.$store.state.hostname + "/SkudUserAvtoRasxod/getUserAddDayUser_ID?user_id=" + userId);
        const data = await response.json();
        if(response.status == 200 || response.status == 201){
          this.user_avto_rasxod_list = data;
        }
      }
      catch(error){
        this.loading = false;
        console.log(error)
      }
    },

    async submit_avto_rasxod(){
      if(!this.rasxod_summa || !this.rasxod_day)return;
      try{
        const requestOptions = {
          method: "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            "user_name" : this.user_name,
            "userid" : this.user_id,
            "day" : this.rasxod_day,
            "sum": this.rasxod_summa,
            "note": this.day_info
            })
          };
          const response = await fetch(this.$store.state.hostname + '/SkudUserAvtoRasxod', requestOptions);
          // const data = await response.json();
          if(response.status == 200 || response.status == 201){
            this.show_avto_rasxod = false;
            this.rasxod_day = null,
            this.rasxod_summa = 0,
            this.rasxod_summaString = '';
            this.day_info = ''
          }
      }
      catch(error){
        console.log(error)
      }
      console.log('rasxod_submit')
    },

    async func_show_add_note(user_id, user_name, index){
      this.show_user_note = true;
      this.user_id = user_id;
      this.user_name = user_name;
      this.item_index = index;
      await this.fetch_note_list(user_id);
    },

    async fetch_note_list(userId){
      try{
        const response = await fetch(this.$store.state.hostname + "/SkudUserNote/getUserAddDayUser_ID?user_id=" + userId);
        const data = await response.json();
        if(response.status == 200 || response.status == 201){
          this.user_note_list = data;
        }
      }
      catch(error){
        this.loading = false;
        console.log(error)
      }
    },

    async submit_user_note(){
      if(!this.day_info)return;
      try{
        const requestOptions = {
          method: "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            "user_name" : this.user_name,
            "userid" : this.user_id,
            "note": this.day_info
            })
          };
          const response = await fetch(this.$store.state.hostname + '/SkudUserNote', requestOptions);
          // const data = await response.json();
          if(response.status == 200 || response.status == 201){
            this.show_user_note = false;
            this.day_info = ''
          }
      }
      catch(error){
        console.log(error)
      }
      console.log('rasxod_submit')
    },


    async submit_day_info(){
      if(this.day_num == null || this.day_num == 0)return;
      try{
        const requestOptions = {
          method: "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            "user_name" : this.user_name,
            "userid" : this.user_id,
            "reg_date" : this.day_time,
            "count": this.day_num,
            "note": this.day_info
            })
          };
          const response = await fetch(this.$store.state.hostname + '/SkudUserDayAdd', requestOptions);
          const data = await response.json();
          if(response.status == 200 || response.status == 201){
            this.show_add_day = false;
            if(this.Start_time<= this.day_time && this.End_time>= this.day_time){
              for(let i=0; i<this.reportList.length; i++){
                if(data.userid == this.reportList[i].userid){
                  this.reportList[i].day_count += data.count;
                  this.reportList[i].day_list.push(data);
                  this.reportList[i].ishlagan_puli = parseFloat(this.reportList[i].ishlagan_puli) + ((parseFloat(this.reportList[i].oylik_haqqi) / parseFloat(this.working_day_count)).toFixed() * parseFloat(data.count)); 
                  break;  
                }
              }
            }
          }
      }
      catch(error){
        console.log(error)
      }
      console.log('day_submit')
    },
    async submit_rasxod_info(){
      if(!this.rasxod_summa && !this.zaplata_summa)return;
      let rasxod_zaplata_sum = 0;
      if(this.zaplata_status == 0){
        rasxod_zaplata_sum = this.rasxod_summa;
      }
      else if(this.zaplata_status == 1){
        rasxod_zaplata_sum = this.zaplata_summa;
      }
      try{
        const requestOptions = {
          method: "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            "user_name" : this.user_name,
            "userid" : this.user_id,
            "reg_date" : this.day_time,
            "count": rasxod_zaplata_sum,
            "num_1": this.zaplata_status,
            "note": this.day_info
            })
          };
          const response = await fetch(this.$store.state.hostname + '/SkudUserRasxod', requestOptions);
          const data = await response.json();
          if(response.status == 200 || response.status == 201){
            this.show_add_rasxod = false;
            if(this.Start_time<= this.day_time && this.End_time>= this.day_time){
              for(let i=0; i<this.reportList.length; i++){
                if(data.userid == this.reportList[i].userid){
                  this.reportList[i].rasxod += data.count;
                  this.reportList[i].rasxod_list.push(data);
                  this.reportList[i].ishlagan_puli = parseFloat(this.reportList[i].ishlagan_puli) - parseFloat(data.count);
                  break;  
                }
              }
                          
            }
          }
      }
      catch(error){
        console.log(error)
      }
      console.log('rasxod_submit')
    },
    func_day_plus(){
      this.day_num = 1;
    },
    func_day_minus(){
      this.day_num = -1;
    },
    sub_door_select(option){
      // console.log(option)
      this.dept_name = option.name;
      this.dept_id = option.id;
    },
    async show_user_details_report(userId){
      // let start = this.Start_time ;
      console.log('hiy')
      this.list_of_in_out = [];
      this.userWorkTime = [];
      let start_date = new Date(this.Start_time)
      let end_date = new Date(this.End_time)
      console.log(start_date.toISOString().slice(0,10))
      // console.log(end_date)
      await this.fetch_user_details_input(userId, start_date.toISOString().slice(0,10))
      await this.fetch_user_work_use(userId, start_date.toISOString().slice(0,10))
      while(start_date < end_date){
        var next_date = start_date.setDate(start_date.getDate() + 1);
        console.log('next_date');
        console.log(next_date);
        start_date = new Date(next_date); 
        console.log(start_date);
        await this.fetch_user_details_input(userId, start_date.toISOString().slice(0,10))
        await this.fetch_user_work_use(userId, start_date.toISOString().slice(0,10))
      }
      // await this.fetch_user_work_use(userId)
      console.log('this.list_of_in_out')
      console.log(this.list_of_in_out)
      console.log(this.userWorkTime)
      this.exampleModal1 = true;

    },
    async fetch_user_details_input(id, date){
      let start = date ;
      // let end = date;
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/SkudMyUserinfoes/getPaginationBy0404Qarovul?page=0&size=1000&date_b=" + start + '&user_id=' + id);
        const data = await response.json();
        this.loading = false;
        console.log('userIdReport');
        console.log(data.items_list);
        if(response.status == 200 || response.status == 201){
          let temp_kun = {
            kun: date,
            list: data.items_list
          }
          this.list_of_in_out.push(temp_kun);
        }
      }
      catch(error){
        this.loading = false;
        console.log(error)
      }
    },

    async show_user_report(userId){
      // let start = this.Start_time ;
      console.log('hiy')
      this.userWorkTime = [];
      let start_date = new Date(this.Start_time)
      let end_date = new Date(this.End_time)
      console.log(start_date.toISOString().slice(0,10))
      // console.log(end_date)
      await this.fetch_user_work_use(userId, start_date.toISOString().slice(0,10))
      while(start_date < end_date){
        var next_date = start_date.setDate(start_date.getDate() + 1);
        console.log('next_date');
        console.log(next_date);
        start_date = new Date(next_date); 
        console.log(start_date);
        await this.fetch_user_work_use(userId, start_date.toISOString().slice(0,10))
      }
      // await this.fetch_user_work_use(userId)
      console.log('this.userWorkTime')
      console.log(this.userWorkTime)
      this.exampleModal = true;

    },
    async fetch_user_work_use(id, date){
      let start = date ;
      let end = date;
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/SkudMyUserinfoes/getReportWithoutSmenaPaginationByAllUserAndOylik0404QarovulWithUserId?page=0&size=1000&begin_date=" + start + '&end_date=' + end + '&user_id=' + id);
        const data = await response.json();
        this.loading = false;
        console.log('userIdReport');
        console.log(data.items_list);
        if(response.status == 200 || response.status == 201){
          this.userWorkTime.push(data.items_list[0])
        }
        
        
      }
      catch(error){
        this.loading = false;
        console.log(error)
      }
    },
    async submit(){
      await this.fetchFindSundayInMonth();
      this.all_summa = 0;
      this.dept_name = '';
      this.dept_id = null;
      this.switch1 = false;
      this.switch2 = false;
      let start = this.Start_time;
      let end = this.End_time;
      console.log(start)
      console.log(end)
      console.log('submit')
      await this.fetch_user_add_note_list({start: start, end: end});
      this.user_add_del_list = this.get_user_add_note_list;
      await this.fetch_user_rasxod_list({start: start, end: end});
      this.user_rasxod_list = this.get_user_rasxod_list;
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/SkudMyUserinfoes/getReportWithoutSmenaPaginationByAllUserAndOylik0404Qorovulxona?page=0&size=1000&begin_date=" + start + '&end_date=' + end);
        const data = await response.json();
        this.loading = false;
        this.reportList = [];
        console.log('data itemlist') 
        console.log(data) 
        for(let i=0; i<data.items_list.length; i++){
          let b_day = 0;
          for(let j=0; j<data.items_list[i].ishlagan_vaqtlar_ls.length; j++){
            if(data.items_list[i].ishlagan_vaqtlar_ls[j] != '00:00:00'){
              b_day ++;
            }
          }
          let a = {
            ism: data.items_list[i].ism,
            familiya: data.items_list[i].familiya,
            userid: data.items_list[i].userid,
            ishlagan_vaqtlar_ls: data.items_list[i].ishlagan_vaqtlar_ls,
            // day: data.items_list[i].ishlagan_vaqtlar_ls,
            day: b_day,
            kun: data.items_list[i].kun,
            ishlagan_puli: data.items_list[i].ishlagan_puli,
            oylik_nomi: data.items_list[i].oylik_nomi,
            worked_hours_itm: '',
            departid: data.items_list[i].departid,
            without_gr_id: data.items_list[i].without_gr_id,
            oylik_haqqi: 0,
            rasxod: 0,
            rasxod_list: [],
            day_count: 0,
            day_list: [],
            vaqt_flag: data.items_list[i].vaqt_flag,
            kelganidan_beri: data.items_list[i].kelganidan_beri,
            user_note: data.items_list[i].user_note,
          }
          if(data.items_list[i].oylik_nomi != ''){
            a.oylik_haqqi = data.items_list[i].skudOylik.value;
          }
          if(data.items_list[i].without_gr_id == 2 && data.items_list[i].oylik_haqqi != 0){
            a.ishlagan_puli = ((parseFloat(a.oylik_haqqi)/parseFloat(this.working_day_count)).toFixed() * a.day);
            // a.ishlagan_puli = a.ishlagan_puli.toFixed();
          }
          a.worked_hours_itm = this.timeConvert(data.items_list[i].worked_hours_itm)
          this.reportList.push(a);
        }
        this.main_reportList = this.reportList;
        await this.fetch_check_user_find();
        // this.reportList = data.items_list;
      }
      catch(error){
        this.loading = false;
        console.log(error)
      }
    },
    async fetch_check_user_find(){
      for(let i=0; i<this.reportList.length; i++){
        for(let j=0; j<this.user_add_del_list.length; j++){
          if(this.reportList[i].userid == this.user_add_del_list[j].userid){
            this.reportList[i].day_count += this.user_add_del_list[j].count;
            this.reportList[i].day_list.push(this.user_add_del_list[j]);
            this.reportList[i].ishlagan_puli = parseFloat(this.reportList[i].ishlagan_puli) + ((parseFloat(this.reportList[i].oylik_haqqi) / parseFloat(this.working_day_count)).toFixed() * parseFloat(this.user_add_del_list[j].count));            
            this.user_add_del_list.splice(j,1);
            j--;
          }
        }
        this.user_rasxod_list
        for(let k=0; k<this.user_rasxod_list.length; k++){
          if(this.reportList[i].userid == this.user_rasxod_list[k].userid){
            this.reportList[i].rasxod += this.user_rasxod_list[k].count;
            this.reportList[i].rasxod_list.push(this.user_rasxod_list[k]);
            this.reportList[i].ishlagan_puli = parseFloat(this.reportList[i].ishlagan_puli) - parseFloat(this.user_rasxod_list[k].count);            
            this.user_rasxod_list.splice(k,1);
            k--;
          }
        }
        this.all_summa += parseFloat(this.reportList[i].ishlagan_puli);
      }  
      console.log(this.all_summa)
    },
    timeConvert(n) {
      if(n==0) return '---'
      var hours = (n);
      var rhours = Math.floor(hours);
      var minutes = (hours - rhours) * 60;
      var rminutes = Math.floor(minutes);
      var secund = (minutes - rminutes) * 60;
      var rsecund = Math.round(secund);
      if(rhours<10){
        rhours = '0' + rhours.toString()
      }
      if(rminutes<10){
        rminutes = '0' + rminutes.toString()
      }
      if(rsecund<10){
        rsecund = '0' + rsecund.toString()
      }
      return rhours + ":" + rminutes + ":" + rsecund;
    },
    async refresh(){
      console.log('refresh')
      this.dept_name = '';
      this.dept_id = null;
      this.switch1 = false;
      this.switch2 = false;
    },
    sortedArrayAsc(key){
        // function compare(a, b) {
        //   if (a[key] < b[key])
        //     return -1;
        //   if (a[key] > b[key])
        //     return 1;
        //   return 0;
        // }
        // this.filteredList.sort(compare);
        this.sortKey = key;
        this.sortDir = 'asc';
    },
    sortedArray(key){
        // function compare(a, b) {
        //   if (a[key] > b[key])
        //     return -1;
        //   if (a[key] < b[key])
        //     return 1;
        //   return 0;
        // }

        // this.filteredList.sort(compare);
        this.sortKey = key;
        this.sortDir = 'desc';
    },
    toSeconds(str) {
      // Masalan: "0 yil 7 kun 4 soat"
      if(str){
        const match = str.match(/(\d+)\s*yil\s*(\d+)\s*kun\s*(\d+)\s*soat/);
        if (!match) return 0;

        const years = parseInt(match[1]);
        const days = parseInt(match[2]);
        const hours = parseInt(match[3]);

        return years * 365 * 24 * 3600 + days * 24 * 3600 + hours * 3600;
      }
      
    },

    sortedWorkersAsc(){
      console.log('jkasdkasdjas')
      this.filteredList.sort((a, b) => {
        const aSeconds = this.toSeconds(a.kelganidan_beri);
        const bSeconds = this.toSeconds(b.kelganidan_beri);

        return bSeconds - aSeconds; // eng ko‘p ishlagan oldin
      });
    },
    sortedWorkers(){
      this.filteredList.sort((a, b) => {
      const aSeconds = this.toSeconds(a.kelganidan_beri);
      const bSeconds = this.toSeconds(b.kelganidan_beri);

      return aSeconds - bSeconds; // eng kam ishlagan oldin
    });
    },
  },
  
}
</script>

<style lang="scss" scoped>
.navbar_Nav_Edit{
  background: #475a65;
  padding: 7px 12px;
}
.btn_hover{
  cursor:pointer;
}
.btn_hover small{
  color:#d3d3d3 !important;
}
.btn_hover:hover{
  background: #3a464c;
  border-radius: 2px;
}
.btn_hover:hover small{
  color:#fff !important;
}
.mainpage{
    height: calc(100vh - 90px);
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
.action_info{
  position: relative;
}
.action_info small{
  position: absolute;
  width:100px;
  bottom:-15px;
  left: 0;
  border-radius: 3px;
  padding: 0px 3px;
  background: rgb(229, 229, 229);
  display: none;
}
.action_info:hover small{
  position: absolute;
  bottom:-20px;
  left: 0;
  border-radius: 3px;
  padding: 0px 3px;
  background: rgb(229, 229, 229);
  display: inline-block;
}
.add_num_day1{
  background: #fff;
  cursor:pointer;
  width: 50px;
  display: flex;
  justify-content:center;
  &:hover{
    background: rgb(128, 255, 128);
  }
}
.add_num_day2{
  background: #fff;
  cursor:pointer;
  width: 50px;
  display: flex;
  justify-content:center;
  &:hover{
    background: rgb(252, 120, 120);
  }
}
</style>