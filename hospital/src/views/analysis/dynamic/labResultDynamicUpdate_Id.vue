<template>
  <div class="add_main_page">
    <div class="bakteriya bg-white" >
        <div class="header text-center border-bottom py-2 header_bg_menu_name"  style="position:relative;">
            <h5 class="text-primary">{{main_name_title.name_title}}</h5>
            <mdb-btn  color="info" type="submit" class="p-0 update_btn" @click="labUpdate_show = true;"  p="r3 l3 t2 b2">
                <i class="fas fa-pen text-white mask waves-effect t m-0 pr-2" ></i>{{$t('Edit')}}
            </mdb-btn>
        </div>
        <div class="row mx-0 header_bg_patient py-2">
            
            <div class="col-4">
                <mdb-input :label="$t('patient list')" disabled size="sm" class="m-0 mt-1 bg-white" v-model="patient_name" outline/>
            </div>
            <div class="col-4">
                <mdb-input :label="$t('bornDate')" disabled size="sm" class="m-0 mt-1 bg-white" v-model="patient_born" outline/>
            </div>
            <div class="col-4">
                <mdb-input :label="$t('doctor_name')" size="sm" class="m-0 mt-1 bg-white" v-model="doctor_name" outline/>
                <small v-if="$v.doctor_name.$dirty && !$v.doctor_name.required" class="invalid-text  pl-5" style="position:absolute; bottom:-10px; left:-20px;"> 
                {{$t('select_item')}}
                </small>
            </div>
        </div>
        <loader v-if="loading"/>
        <div class="row mx-1 mt-4 border-bottom mb-2">
            <div class="col-1" v-show="main_name_title.name_29 != '+'">
                <div>
                    <small class="labTitle_small">Сокр.</small>
                </div>
            </div>
            <div class="col-3">
                <small class="labTitle_small">Показатель</small>
            </div>
            <div class="col-1" v-show="main_name_title.name_29 == '+'">
                <div>
                    <small class="labTitle_small">Сокр.</small>
                </div>
            </div>
            <div class=" px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}">
                <small class="labTitle_small">Результат</small>
            </div>
            <div class=" px-1" v-if="main_name_title.name_24 == '+'" :class="{'col-2' : main_name_title.name_24 == '+'}">
                <small class="labTitle_small">{{main_name_title.name_23}}</small>
            </div>
            <div class="col-3">
                <small class="labTitle_small">Норма</small>
            </div>
        </div>
        <div v-if="result_list.length" class="mx-1 row pb-3" >
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_1 != '' && get_lab_name_list[0].name_1 != ''">
                <h6>{{ get_lab_center_list[0].name_1 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_1 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_1" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_1 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_1" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_1 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_1" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_1 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[0].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_1 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[0].result1"  outline/>
                </div>
            </div>
            <div class="col-3"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_1 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_1 != ''" >
                    <small style="font-size:13px " v-html="get_lab_norma_list[0].name_1"></small>
                </div>
            </div>

            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_2 != '' && get_lab_name_list[0].name_2 != ''">
                <h6>{{ get_lab_center_list[0].name_2 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_2 != ''  && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_2" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_2 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_2" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_2 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_2" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_2 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[1].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_2 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[1].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_2 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_2 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_2"></small>
                </div>
            </div>

            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_3 != ''  && get_lab_name_list[0].name_3 != ''">
                <h6>{{ get_lab_center_list[0].name_3 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_3 != ''  && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_3" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_3 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_3" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_3 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_3" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_3 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[2].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_3 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[2].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_3 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_3 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_3"></small>
                </div>
            </div>

            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_4 != '' && get_lab_name_list[0].name_4 != ''">
                <h6>{{ get_lab_center_list[0].name_4 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_4 != ''  && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_4" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_4 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_4" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_4 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_4" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_4 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[3].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_4 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[3].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_4 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_4 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_4"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_5 != '' && get_lab_name_list[0].name_5 != ''">
                <h6>{{ get_lab_center_list[0].name_5 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_5 != ''  && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_5" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_5 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_5" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_5 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_5" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_5 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[4].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_5 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[4].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_5 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_5 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_5"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_6 != '' && get_lab_name_list[0].name_6 != ''">
                <h6>{{ get_lab_center_list[0].name_6 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_6 != ''  && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_6" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_6 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_6" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_6 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_6" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_6 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[5].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_6 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[5].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_6 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_6 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_6"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_7 != '' && get_lab_name_list[0].name_7 != ''">
                <h6>{{ get_lab_center_list[0].name_7 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_7 != ''  && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_7" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_7 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_7" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_7 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_7" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_7 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[6].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_7 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[6].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_7 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_7 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_7"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_8 != '' && get_lab_name_list[0].name_8 != ''">
                <h6>{{ get_lab_center_list[0].name_8 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_8 != ''  && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_8" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_8 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_8" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_8 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_8" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_8 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[7].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_8 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[7].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_8 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_8 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_8"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_9 != '' && get_lab_name_list[0].name_9 != ''">
                <h6>{{ get_lab_center_list[0].name_9 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_9 != ''  && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_9" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_9 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_9" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_9 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_9" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_9 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[8].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_9 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[8].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_9 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_9 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_9"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_10 != '' && get_lab_name_list[0].name_10 != ''">
                <h6>{{ get_lab_center_list[0].name_10 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_10 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_10" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_10 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_10" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_10 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_10" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_10 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[9].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_10 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[9].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_10 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_10 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_10"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_11 != '' && get_lab_name_list[0].name_11 != ''">
                <h6>{{ get_lab_center_list[0].name_11 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_11 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_11" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_11 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_11" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_11 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_11" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_11 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[10].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_11 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[10].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_11 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_11 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_11"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_12 != '' && get_lab_name_list[0].name_12 != ''">
                <h6>{{ get_lab_center_list[0].name_12 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_12 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_12" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_12 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_12" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_12 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_12" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_12 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[11].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_12 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[11].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_12 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_12 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_12"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_13 != '' && get_lab_name_list[0].name_13 != ''">
                <h6>{{ get_lab_center_list[0].name_13 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_13 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_13" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_13 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_13" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_13 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_13" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_13 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[12].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_13 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[12].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_13 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_13 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_13"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_14 != '' && get_lab_name_list[0].name_14 != ''">
                <h6>{{ get_lab_center_list[0].name_14 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_14 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_14" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_14 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_14" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_14 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_14" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_14 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[13].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_14 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[13].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_14 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_14 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_14"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_15 != '' && get_lab_name_list[0].name_15 != ''">
                <h6>{{ get_lab_center_list[0].name_15 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_15 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_15" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_15 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_15" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_15 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_15" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_15 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[14].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_15 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[14].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_15 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_15 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_15"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_16 != ''  && get_lab_name_list[0].name_16 != ''">
                <h6>{{ get_lab_center_list[0].name_16 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_16 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_16" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_16 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_16" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_16 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_16" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_16 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[15].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_16 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[15].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_16 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_16 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_16"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_17 != ''  && get_lab_name_list[0].name_17 != ''">
                <h6>{{ get_lab_center_list[0].name_17 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_17 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_17" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_17 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_17" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_17 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_17" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_17 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[16].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_17 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[16].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_17 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_17 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_17"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_18 != ''  && get_lab_name_list[0].name_18 != ''">
                <h6>{{ get_lab_center_list[0].name_18 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_18 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_18" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_18 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_18" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_18 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_18" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_18 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[17].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_18 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[17].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_18 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_18 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_18"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_19 != ''  && get_lab_name_list[0].name_19 != ''">
                <h6>{{ get_lab_center_list[0].name_19 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_19 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_19" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_19 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_19" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_19 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_19" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_19 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[18].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_19 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[18].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_19 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_19 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_19"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_20 != ''  && get_lab_name_list[0].name_20 != ''">
                <h6>{{ get_lab_center_list[0].name_20 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_20 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_20" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_20 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_20" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_20 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_20" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_20 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[19].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_20 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[19].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_20 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_20 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_20"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_21 != ''  && get_lab_name_list[0].name_21 != ''">
                <h6>{{ get_lab_center_list[0].name_21 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_21 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_21" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_21 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_21" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_21 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_21" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_21 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[20].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_21 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[20].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_21 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_21 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_21"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_22 != ''  && get_lab_name_list[0].name_22 != ''">
                <h6>{{ get_lab_center_list[0].name_22 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_22 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_22" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_22 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_22" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_22 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_22" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_22 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[21].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_22 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[21].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_22 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_22 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_22"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_23 != '' && get_lab_name_list[0].name_23 != ''">
                <h6>{{ get_lab_center_list[0].name_23 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_23 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_23" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_23 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_23" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_23 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_23" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_23 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[22].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_23 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[22].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_23 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_23 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_23"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_24 != '' && get_lab_name_list[0].name_24 != ''">
                <h6>{{ get_lab_center_list[0].name_24 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_24 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_24" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_24 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_24" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_24 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_24" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_24 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[23].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_24 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[23].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_24 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_24 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_24"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_25 != '' && get_lab_name_list[0].name_25 != ''">
                <h6>{{ get_lab_center_list[0].name_25 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_25 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_25" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_25 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_25" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_25 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_25" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_25 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[24].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_25 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[24].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_25 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_25 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_25"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_26 != '' && get_lab_name_list[0].name_26 != ''">
                <h6>{{ get_lab_center_list[0].name_26 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_26 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_26" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_26 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_26" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_26 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_26" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_26 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[25].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_26 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[25].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_26 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_26 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_26"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_27 != '' && get_lab_name_list[0].name_27 != ''">
                <h6>{{ get_lab_center_list[0].name_27 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_27 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_27" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_27 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_27" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_27 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_27" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_27 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[26].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_27 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[26].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_27 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_27 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_27"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_28 != '' && get_lab_name_list[0].name_28 != ''">
                <h6>{{ get_lab_center_list[0].name_28 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_28 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_28" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_28 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_28" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_28 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_28" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_28 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[27].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_28 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[27].result1"  outline/>
                </div>
            </div>
            
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_28 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_28 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_28"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_29 != '' && get_lab_name_list[0].name_29 != ''">
                <h6>{{ get_lab_center_list[0].name_29 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_29 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_29" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_29 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_29" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_29 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_29" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_29 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[28].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_29 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[28].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_29 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_29 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_29"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_30 != '' && get_lab_name_list[0].name_30 != ''">
                <h6>{{ get_lab_center_list[0].name_30 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_30 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_30" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_30 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_30" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_30 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_30" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_30 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[29].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_30 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[29].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_30 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_30 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_30"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_31 != '' && get_lab_name_list[0].name_31 != ''">
                <h6>{{ get_lab_center_list[0].name_31 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_31 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_31" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_31 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_31" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_31 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_31" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_31 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[30].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_31 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[30].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_31 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_31 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_31"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_32 != '' && get_lab_name_list[0].name_32 != ''">
                <h6>{{ get_lab_center_list[0].name_32 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_32 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_32" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_32 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_32" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_32 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_32" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_32 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[31].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_32 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[31].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_32 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_32 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_32"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_33 != '' && get_lab_name_list[0].name_33 != ''">
                <h6>{{ get_lab_center_list[0].name_33 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_33 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_33" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_33 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_33" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_33 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_33" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_33 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[32].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_33 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[32].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_33 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_33 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_33"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_34 != '' && get_lab_name_list[0].name_34 != ''">
                <h6>{{ get_lab_center_list[0].name_34 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_34 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_34" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_34 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_34" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_34 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_34" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_34 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[33].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_34 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[33].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_34 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_34 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_34"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_35 != '' && get_lab_name_list[0].name_35 != ''">
                <h6>{{ get_lab_center_list[0].name_35 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_35 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_35" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_35 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_35" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_35 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_35" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_35 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[34].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_35 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[34].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_35 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_35 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_35"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_36 != '' && get_lab_name_list[0].name_36 != ''">
                <h6>{{ get_lab_center_list[0].name_36 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_36 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_36" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_36 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_36" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_36 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_36" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_36 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[35].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_36 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[35].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_36 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_36 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_36"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_37 != '' && get_lab_name_list[0].name_37 != ''">
                <h6>{{ get_lab_center_list[0].name_37 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_37 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_37" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_37 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_37" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_37 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_37" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_37 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[36].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_37 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[36].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_37 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_37 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_37"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_38 != '' && get_lab_name_list[0].name_38 != ''">
                <h6>{{ get_lab_center_list[0].name_38 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_38 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_38" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_38 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_38" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_38 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_38" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_38 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[37].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_38 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[37].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_38 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_38 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_38"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_39 != '' && get_lab_name_list[0].name_39 != ''">
                <h6>{{ get_lab_center_list[0].name_39 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_39 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_39" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_39 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_39" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_39 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_39" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_39 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[38].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_39 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[38].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_39 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_39 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_39"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_40 != '' && get_lab_name_list[0].name_40 != ''">
                <h6>{{ get_lab_center_list[0].name_40 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_40 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_40" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_40 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_40" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_40 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_40" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_40 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[39].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_40 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[39].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_40 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_40 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_40"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_41 != '' && get_lab_name_list[0].name_41 != ''">
                <h6>{{ get_lab_center_list[0].name_41 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_41 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_41" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_41 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_41" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_41 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_41" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_41 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[40].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_41 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[40].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_41 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_41 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_41"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_42 != '' && get_lab_name_list[0].name_42 != ''">
                <h6>{{ get_lab_center_list[0].name_42 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_42 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_42" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_42 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_42" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_42 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_42" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_42 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[41].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_42 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[41].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_42 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_42 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_42"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_43 != '' && get_lab_name_list[0].name_43 != ''">
                <h6>{{ get_lab_center_list[0].name_43 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_43 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_43" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_43 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_43" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_43 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_43" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_43 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[42].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_43 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[42].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_43 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_43 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_43"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_44 != '' && get_lab_name_list[0].name_44 != ''">
                <h6>{{ get_lab_center_list[0].name_44 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_44 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_44" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_44 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_44" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_44 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_44" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_44 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[43].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_44 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[43].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_44 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_44 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_44"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_45 != '' && get_lab_name_list[0].name_45 != ''">
                <h6>{{ get_lab_center_list[0].name_45 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_45 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_45" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_45 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_45" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_45 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_45" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_45 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[44].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_45 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[44].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_45 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_45 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_45"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_46 != '' && get_lab_name_list[0].name_46 != ''">
                <h6>{{ get_lab_center_list[0].name_46 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_46 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_46" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_46 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_46" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_46 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_46" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_46 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[45].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_46 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[45].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_46 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_46 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_46"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_47 != '' && get_lab_name_list[0].name_47 != ''">
                <h6>{{ get_lab_center_list[0].name_47 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_47 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_47" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_47 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_47" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_47 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_47" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_47 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[46].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_47 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[46].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_47 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_47 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_47"></small>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_48 != '' && get_lab_name_list[0].name_48 != ''">
                <h6>{{ get_lab_center_list[0].name_48 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_48 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_48" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_48 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_48" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_48 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_48" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_48 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[47].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_48 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[47].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_48 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_48 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_48"></small>
                </div>
            </div>
            
            <!-- <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_49 != '' && get_lab_name_list[0].name_49 != ''">
                <h6>{{ get_lab_center_list[0].name_49 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_49 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_49" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_49 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_49" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_49 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_49" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_49 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[48].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_49 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[48].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_49 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_49 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_49"></small>
                </div>
            </div> -->
            <!-- <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_50 != '' && get_lab_name_list[0].name_50 != ''">
                <h6>{{ get_lab_center_list[0].name_50 }}</h6>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_50 != '' && main_name_title.name_29 != '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_firs_name_list[0].name_50" outline/>
                </div>
            </div>
            <div class="col-3" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_50 != ''">
                <mdb-input size="sm" disabled class="m-0 p-0 mb-3" v-model="get_lab_name_list[0].name_50" outline/>
            </div>
            <div class="col-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_50 != '' && main_name_title.name_29 == '+'">
                <div>
                    <mdb-input size="sm"  disabled class="m-0 p-0 mb-3" v-model="get_lab_second_name_list[0].name_50" outline/>
                </div>
            </div>
            <div class="px-1" :class="{'col-5' : main_name_title.name_24 != '+', 'col-3' : main_name_title.name_24 == '+'}" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_50 != ''">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_list[49].result"  outline/>
                </div>
            </div>
            <div class="px-1 col-2"  v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_50 != '' && main_name_title.name_24 == '+'">
                <div class="pos_relative">
                    <mdb-input size="sm" class="m-0 p-0 mb-3" v-model="result_first_list[49].result1"  outline/>
                </div>
            </div>
            <div class="col-3 " v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_50 != ''">
                <div class="normClass rounded px-2 border"  v-if="get_lab_norma_list[0].name_50 != ''" >
                    <small style="font-size:13px;" v-html="get_lab_norma_list[0].name_50"></small>
                </div>
            </div> -->
        </div>
        <div class="text-right mx-4 py-2 border-top">
            <mdb-btn color="primary" class="m-0 p-0"   p="r4 l4 t2 b2" @click="send" style="font-size:10px;">{{$t('Send')}}</mdb-btn>  
        </div>
    </div>
    <div class="dynamic_update" v-if="labUpdate_show">
        <div class="close_lab_update shadow " @click="labUpdate_show = false;">
            <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler  icon-tabler-x" width="15" height="15" viewBox="0 0 24 24" stroke-width="2.5" stroke="#fff" fill="none" stroke-linecap="round" stroke-linejoin="round">
            <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
            <line x1="18" y1="6" x2="6" y2="18" />
            <line x1="6" y1="6" x2="18" y2="18" />
            </svg>
        </div>
        <transition name="v-transition-animate">
            <labUpdate :main_id="main_id" @close="closeUpdateLab" @closed="labUpdate_show = false;"/>
        </transition>
        
    </div>
    <Toast ref="message"></Toast>
        <AlertError ref="msg"></AlertError>
    <massage_box :hide="modal_status" :detail_info="modal_info"
        :m_text="$t('Failed_to_add')" @to_hide_modal = "modal_status= false"/>
  </div>
</template>

<script>
import {mdbBtn, mdbInput} from "mdbvue";
import labUpdate from './labAdd_update'
import {mapActions, mapGetters} from 'vuex'
import { required } from 'vuelidate/lib/validators'
export default {
  components: {
    mdbBtn,mdbInput, labUpdate
  },
  validations: {
      patient_name : {required},
      doctor_name : {required}
    },
    props: {
       main_name_title:{
        type: Object,
        default(){
          return {}
        }
      },
      main_id:{
        type : Number,
        default : null
      },
      patient_id:{
        type : Number,
        default : null
      },
      patient_name:{
        type : String,
        default : ''
      },
      patient_born:{
        type : String,
        default : ''
      },
      lab_result_props:{
        type: Object,
        default(){
          return {}
        }
      },
      lab_pic_id: {
        type : Number,
        default : null
      }
    },
  data(){
    return {
      loading : false,
      modal_info : '',
      modal_status: false,
      numberEnter: null,
      name: '',
      doctor_name: localStorage.docName,
      id: this.$route.params.id,
      result_list: [],
      result_first_list: [],
      labUpdate_show: false,
      result_first_id: 0,
    }
  },
  computed:{
    ...mapGetters(['get_patient_client_list', 'get_lab_firs_name_list', 'get_lab_name_list', 'get_lab_second_name_list',
        'get_lab_norma_list', 'get_lab_edzm_list','get_lab_center_list', 'get_lab_result_list']),
    filteredList: function(){
      if(this.search)
      {
        return this.get_patient_client_list.filter((item)=>{
          return this.search.toLowerCase().split(' ').every(v => item.fio.toLowerCase().includes(v))
        })
      }else
      {
        return this.get_patient_client_list;
      }
      }
  },
  async mounted(){
    this.result_list = [];
    this.result_first_list = [];
    for(let i=0; i<50; i++){
      let obj_name = {
        result: ''
      }
      let obj_name1 = {
        result1: ''
      }
      this.result_list.push(obj_name);
      this.result_first_list.push(obj_name1);
    //   console.log(this.result_list)
    }
    this.fetch_patient_client();
    await this.fetch_lab_name_list(this.main_id);
    await this.fetch_lab_norma_list(this.main_id);
    await this.fetch_lab_center_list(this.main_id);
    await this.fetch_lab_firs_name_list(this.main_id);
    await this.fetch_lab_result_list(this.main_id);
    await this.fetch_lab_second_name_list(this.main_id);
    await this.fetch_lab_second_update(this.main_id);
    this.result_data_get();
    // console.log('this.get_lab_name_list')
    // console.log(this.main_name_title)
    // console.log(this.get_lab_name_list)
    // console.log(this.get_lab_norma_list)
    // console.log(this.get_lab_center_list)
  },
  methods:{
    ...mapActions(['fetch_patient_client', 'fetch_lab_name_list', 'fetch_lab_firs_name_list', 'fetch_lab_result_list',
    'fetch_lab_norma_list', 'fetch_lab_edzm_list', 'fetch_lab_center_list', 'fetch_lab_second_name_list']),
    async fetch_lab_second_update(main_id){
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + '/HospitalAnalizDynamicResultFirst/getPaginationByMainTableId?page=0&size=1&main_table_id=' + main_id)
          const data = await response.json();
          console.log('data name first result')
          if(data.items_list.length == 0)return;
          console.log(data.items_list[0])
          this.result_first_id = data.items_list[0].Id;
          this.result_first_list[0].result1 = data.items_list[0].name_1;
          this.result_first_list[1].result1 = data.items_list[0].name_2;
          this.result_first_list[2].result1 = data.items_list[0].name_3;
          this.result_first_list[3].result1 = data.items_list[0].name_4;
          this.result_first_list[4].result1 = data.items_list[0].name_5;
          this.result_first_list[5].result1 = data.items_list[0].name_6;
          this.result_first_list[6].result1 = data.items_list[0].name_7;
          this.result_first_list[7].result1 = data.items_list[0].name_8;
          this.result_first_list[8].result1 = data.items_list[0].name_9;
          this.result_first_list[9].result1 = data.items_list[0].name_10;
          this.result_first_list[10].result1 = data.items_list[0].name_11;
          this.result_first_list[11].result1 = data.items_list[0].name_12;
          this.result_first_list[12].result1 = data.items_list[0].name_13;
          this.result_first_list[13].result1 = data.items_list[0].name_14;
          this.result_first_list[14].result1 = data.items_list[0].name_15;
          this.result_first_list[15].result1 = data.items_list[0].name_16;
          this.result_first_list[16].result1 = data.items_list[0].name_17;
          this.result_first_list[17].result1 = data.items_list[0].name_18;
          this.result_first_list[18].result1 = data.items_list[0].name_19;
          this.result_first_list[19].result1 = data.items_list[0].name_20;
          this.result_first_list[20].result1 = data.items_list[0].name_21;
          this.result_first_list[21].result1 = data.items_list[0].name_22;
          this.result_first_list[22].result1 = data.items_list[0].name_23;
          this.result_first_list[23].result1 = data.items_list[0].name_24;
          this.result_first_list[24].result1 = data.items_list[0].name_25;
          this.result_first_list[25].result1 = data.items_list[0].name_26;
          this.result_first_list[26].result1 = data.items_list[0].name_27;
          this.result_first_list[27].result1 = data.items_list[0].name_28;
          this.result_first_list[28].result1 = data.items_list[0].name_29;
          this.result_first_list[29].result1 = data.items_list[0].name_30;
          this.result_first_list[30].result1 = data.items_list[0].name_31;
          this.result_first_list[31].result1 = data.items_list[0].name_32;
          this.result_first_list[32].result1 = data.items_list[0].name_33;
          this.result_first_list[33].result1 = data.items_list[0].name_34;
          this.result_first_list[34].result1 = data.items_list[0].name_35;
          this.result_first_list[35].result1 = data.items_list[0].name_36;
          this.result_first_list[36].result1 = data.items_list[0].name_37;
          this.result_first_list[37].result1 = data.items_list[0].name_38;
          this.result_first_list[38].result1 = data.items_list[0].name_39;
          this.result_first_list[39].result1 = data.items_list[0].name_40;
          this.result_first_list[40].result1 = data.items_list[0].name_41;
          this.result_first_list[41].result1 = data.items_list[0].name_42;
          this.result_first_list[42].result1 = data.items_list[0].name_43;
          this.result_first_list[43].result1 = data.items_list[0].name_44;
          this.result_first_list[44].result1 = data.items_list[0].name_45;
          this.result_first_list[45].result1 = data.items_list[0].name_46;
          this.result_first_list[46].result1 = data.items_list[0].name_47;
          this.result_first_list[47].result1 = data.items_list[0].name_48;
          this.result_first_list[48].result1 = data.items_list[0].name_49;
          this.result_first_list[49].result1 = data.items_list[0].name_50;
          this.loading = false;
        }
        catch{
            this.$refs.message.error('server_not_working')
          // console.log('catch')
        }
    },
    async closeUpdateLab(id_lab){
        // console.log(id_lab)
        this.labUpdate_show = false;
        // this.result_list = [];
        // for(let i=0; i<50; i++){
        // let obj_name = {
        //     result: ''
        // }
        // this.result_list.push(obj_name);
        //   console.log(this.result_list)
        // }
        await this.fetch_lab_name_list(id_lab);
        if(this.main_name_title.norma_status == true){
            await this.fetch_lab_norma_list(id_lab);
        }
        if(this.main_name_title.extra_status == true){
            await this.fetch_lab_center_list(id_lab);
        }
    },
    result_data_get(){
        this.result_list[0].result = this.lab_result_props.name_1;
        this.result_list[1].result = this.lab_result_props.name_2;
        this.result_list[2].result = this.lab_result_props.name_3;
        this.result_list[3].result = this.lab_result_props.name_4;
        this.result_list[4].result = this.lab_result_props.name_5;
        this.result_list[5].result = this.lab_result_props.name_6;
        this.result_list[6].result = this.lab_result_props.name_7;
        this.result_list[7].result = this.lab_result_props.name_8;
        this.result_list[8].result = this.lab_result_props.name_9;
        this.result_list[9].result = this.lab_result_props.name_10;
        this.result_list[10].result = this.lab_result_props.name_11;
        this.result_list[11].result = this.lab_result_props.name_12;
        this.result_list[12].result = this.lab_result_props.name_13;
        this.result_list[13].result = this.lab_result_props.name_14;
        this.result_list[14].result = this.lab_result_props.name_15;
        this.result_list[15].result = this.lab_result_props.name_16;
        this.result_list[16].result = this.lab_result_props.name_17;
        this.result_list[17].result = this.lab_result_props.name_18;
        this.result_list[18].result = this.lab_result_props.name_19;
        this.result_list[19].result = this.lab_result_props.name_20;
        this.result_list[20].result = this.lab_result_props.name_21;
        this.result_list[21].result = this.lab_result_props.name_22;
        this.result_list[22].result = this.lab_result_props.name_23;
        this.result_list[23].result = this.lab_result_props.name_24;
        this.result_list[24].result = this.lab_result_props.name_25;
        this.result_list[25].result = this.lab_result_props.name_26;
        this.result_list[26].result = this.lab_result_props.name_27;
        this.result_list[27].result = this.lab_result_props.name_28;
        this.result_list[28].result = this.lab_result_props.name_29;
        this.result_list[29].result = this.lab_result_props.name_30;
        this.result_list[30].result = this.lab_result_props.name_31;
        this.result_list[31].result = this.lab_result_props.name_32;
        this.result_list[32].result = this.lab_result_props.name_33;
        this.result_list[33].result = this.lab_result_props.name_34;
        this.result_list[34].result = this.lab_result_props.name_35;
        this.result_list[35].result = this.lab_result_props.name_36;
        this.result_list[36].result = this.lab_result_props.name_37;
        this.result_list[37].result = this.lab_result_props.name_38;
        this.result_list[38].result = this.lab_result_props.name_39;
        this.result_list[39].result = this.lab_result_props.name_40;
        this.result_list[40].result = this.lab_result_props.name_41;
        this.result_list[41].result = this.lab_result_props.name_42;
        this.result_list[42].result = this.lab_result_props.name_43;
        this.result_list[43].result = this.lab_result_props.name_44;
        this.result_list[44].result = this.lab_result_props.name_45;
        this.result_list[45].result = this.lab_result_props.name_46;
        this.result_list[46].result = this.lab_result_props.name_47;
        this.result_list[47].result = this.lab_result_props.name_48;
        this.result_list[48].result = this.lab_result_props.name_49;
        this.result_list[49].result = this.lab_result_props.name_50;
    },
    async submit(){
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + '/HospitalMarkSeviceTyesWithNumber/getFullInfoById?id=' + this.numberEnter)
        const data = await response.json()
        // console.log('data')
        // console.log(data)
        if(data.id){
        //   this.patient_name = data.patients.fio;
        //   this.patient_id = data.patients.id;
          this.loading = false;
          this.$refs.message.success('Added_successfully')
        }
        else{
          // this.$refs.msg.error('Error_successfully')
          this.loading = false;
          this.modal_info = data.detail + "    (" + data.routine + ")";
          this.modal_status = true;
          return false;
        }
      }
      catch{
        this.loading = false;
        this.modal_info = "Server no connect";
        this.modal_status = true;
      }
    },
    async sendFinish(){
      if(this.numberEnter){
        try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + '/HospitalMarkSeviceTyesWithNumber/getCheckedStatusAnaliz?id=' + this.numberEnter)
        const data = await response.json()
        // console.log('data')
        // console.log(data)
        if(data.id){
          this.loading = false;
          this.$refs.message.success('Added_successfully')
        }
        else{
          // this.$refs.msg.error('Error_successfully')
          this.loading = false;
          this.modal_info = data.detail + "    (" + data.routine + ")";
          this.modal_status = true;
          return false;
        }
      }
      catch{
        this.loading = false;
        this.modal_info = "Server no connect";
        this.modal_status = true;
      }
      }
    },
    cls_wnd(){
        this.result_list = [];
        this.result_first_list = [];
        for(let i=0; i<50; i++){
        let obj_name = {
            result: ''
        }
        let obj_name1 = {
            result1: ''
        }
        this.result_list.push(obj_name);
        this.result_first_list.push(obj_name1);
        //   console.log(this.result_list)
        }
        // this.patient_name = '';
        // this.patient_id = null;
        this.numberEnter = null;
    },
    async first_result(){
        try{
            const requestOptions = {
            method : "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify({
            "hospitalAnalizDynamicmainId": this.main_id,
            "name": this.patient_name,
            "name_title": this.main_name_title.name_title,
            "patientsId": this.patient_id,
            "extra1": this.doctor_name,
            "lab_name": this.main_name_title.lab_name,
            "lab_name_status": this.main_name_title.lab_name_status,
            "norma": this.main_name_title.norma,
            "norma_status": this.main_name_title.norma_status,
            "first_name": this.main_name_title.first_name,
            "firs_name_status": this.main_name_title.firs_name_status,
            "unit_measurment": this.main_name_title.unit_measurment,
            "unit_measurment_status": this.main_name_title.unit_measurment_status,
            "extra": this.main_name_title.extra,
            "extra_status": this.main_name_title.extra_status,
            "name_1": this.result_first_list[0].result1,
            "name_2": this.result_first_list[1].result1,
            "name_3": this.result_first_list[2].result1,
            "name_4": this.result_first_list[3].result1,
            "name_5": this.result_first_list[4].result1,
            "name_6": this.result_first_list[5].result1,
            "name_7": this.result_first_list[6].result1,
            "name_8": this.result_first_list[7].result1,
            "name_9": this.result_first_list[8].result1,
            "name_10": this.result_first_list[9].result1,
            "name_11": this.result_first_list[10].result1,
            "name_12": this.result_first_list[11].result1,
            "name_13": this.result_first_list[12].result1,
            "name_14": this.result_first_list[13].result1,
            "name_15": this.result_first_list[14].result1,
            "name_16": this.result_first_list[15].result1,
            "name_17": this.result_first_list[16].result1,
            "name_18": this.result_first_list[17].result1,
            "name_19": this.result_first_list[18].result1,
            "name_20": this.result_first_list[19].result1,
            "name_21": this.result_first_list[20].result1,
            "name_22": this.result_first_list[21].result1,
            "name_23": this.result_first_list[22].result1,
            "name_24": this.result_first_list[23].result1,
            "name_25": this.result_first_list[24].result1,
            "name_26": this.result_first_list[25].result1,
            "name_27": this.result_first_list[26].result1,
            "name_28": this.result_first_list[27].result1,
            "name_29": this.result_first_list[28].result1,
            "name_30": this.result_first_list[29].result1,
            "name_31": this.result_first_list[30].result1,
            "name_32": this.result_first_list[31].result1,
            "name_33": this.result_first_list[32].result1,
            "name_34": this.result_first_list[33].result1,
            "name_35": this.result_first_list[34].result1,
            "name_36": this.result_first_list[35].result1,
            "name_37": this.result_first_list[36].result1,
            "name_38": this.result_first_list[37].result1,
            "name_39": this.result_first_list[38].result1,
            "name_40": this.result_first_list[39].result1,
            "name_41": this.result_first_list[40].result1,
            "name_42": this.result_first_list[41].result1,
            "name_43": this.result_first_list[42].result1,
            "name_44": this.result_first_list[43].result1,
            "name_45": this.result_first_list[44].result1,
            "name_46": this.result_first_list[45].result1,
            "name_47": this.result_first_list[46].result1,
            "name_48": this.result_first_list[47].result1,
            "name_49": this.result_first_list[48].result1,
            "name_50": this.result_first_list[49].result1,
            "auth_id": localStorage.AuthId,
            "creator_id": localStorage.AuthId,
            "id": this.result_first_id,
            })
        };
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + '/HospitalAnalizDynamicResultFirst', requestOptions)
        const data = await response.json()
        console.log('data result titr')
        console.log(data)
    }
    catch{
        this.loading = false;
        this.modal_info = "Server no connect";
        this.modal_status = true;
    }
    },
    async send(){
        if(this.$v.$invalid )
        {
            this.$v.$touch();
            this.$refs.message.warning('please_fill')
            return false;
        }
        
      try{
        const requestOptions = {
          method : "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
          "hospitalAnalizDynamicmainId": this.main_id,
          "name": this.patient_name,
          "name_title": this.main_name_title.name_title,
          "patientsId": this.patient_id,
          "extra1": this.doctor_name,
          "lab_name": this.main_name_title.lab_name,
          "lab_name_status": this.main_name_title.lab_name_status,
          "norma": this.main_name_title.norma,
          "norma_status": this.main_name_title.norma_status,
          "first_name": this.main_name_title.first_name,
          "firs_name_status": this.main_name_title.firs_name_status,
          "unit_measurment": this.main_name_title.unit_measurment,
          "unit_measurment_status": this.main_name_title.unit_measurment_status,
          "extra": this.main_name_title.extra,
          "extra_status": this.main_name_title.extra_status,
          "name_1": this.result_list[0].result,
          "name_2": this.result_list[1].result,
          "name_3": this.result_list[2].result,
          "name_4": this.result_list[3].result,
          "name_5": this.result_list[4].result,
          "name_6": this.result_list[5].result,
          "name_7": this.result_list[6].result,
          "name_8": this.result_list[7].result,
          "name_9": this.result_list[8].result,
          "name_10": this.result_list[9].result,
          "name_11": this.result_list[10].result,
          "name_12": this.result_list[11].result,
          "name_13": this.result_list[12].result,
          "name_14": this.result_list[13].result,
          "name_15": this.result_list[14].result,
          "name_16": this.result_list[15].result,
          "name_17": this.result_list[16].result,
          "name_18": this.result_list[17].result,
          "name_19": this.result_list[18].result,
          "name_20": this.result_list[19].result,
          "name_21": this.result_list[20].result,
          "name_22": this.result_list[21].result,
          "name_23": this.result_list[22].result,
          "name_24": this.result_list[23].result,
          "name_25": this.result_list[24].result,
          "name_26": this.result_list[25].result,
          "name_27": this.result_list[26].result,
          "name_28": this.result_list[27].result,
          "name_29": this.result_list[28].result,
          "name_30": this.result_list[29].result,
          "name_31": this.result_list[30].result,
          "name_32": this.result_list[31].result,
          "name_33": this.result_list[32].result,
          "name_34": this.result_list[33].result,
          "name_35": this.result_list[34].result,
          "name_36": this.result_list[35].result,
          "name_37": this.result_list[36].result,
          "name_38": this.result_list[37].result,
          "name_39": this.result_list[38].result,
          "name_40": this.result_list[39].result,
          "name_41": this.result_list[40].result,
          "name_42": this.result_list[41].result,
          "name_43": this.result_list[42].result,
          "name_44": this.result_list[43].result,
          "name_45": this.result_list[44].result,
          "name_46": this.result_list[45].result,
          "name_47": this.result_list[46].result,
          "name_48": this.result_list[47].result,
          "name_49": this.result_list[48].result,
          "name_50": this.result_list[49].result,
          "auth_id": localStorage.AuthId,
          "creator_id": localStorage.AuthId,
          "id": this.lab_result_props.id,
          })
        };
        this.loading = true; // update bulayotgani uchun titr ga id quyish kerak
        const response = await fetch(this.$store.state.hostname + '/HospitalAnalizDynamicResults', requestOptions)
        const data = await response.json()
        // console.log('data addDynamic')
        // console.log(data)
        if(data.id){
            await this.first_result();
          await this.fetch_delete_lab_picture();
          this.cls_wnd()
          this.loading = false;
        //   this.$refs.message.success('Added_successfully')
          this.$router.push('/lab_print_id/' + data.id)
          return true;
        }
        else{
          // this.$refs.msg.error('Error_successfully')
          this.loading = false;
          this.modal_info = data.detail + "    (" + data.routine + ")";
          this.modal_status = true;
          return false;
        }
      }
      catch{
        this.loading = false;
        this.modal_info = "Server no connect";
        this.modal_status = true;
      }
    },
    async fetch_delete_lab_picture(){
        console.log('this.lab_pic_id')
        console.log(this.lab_pic_id)
        const requestOptions = {
            method : "delete",
        };
        const response = await fetch(this.$store.state.hostname + "/HospitalPatientAnalizHistory/" + this.lab_pic_id, requestOptions);
        const data = await response.text();

        if(response.status == 201 || response.status == 200)
        {
            console.log('response')
            console.log(response)
            this.$refs.message.success('Successfully_removed')

        }
        else{
            this.modal_info = data;
            this.modal_status = true;
        }
    },
  }
}
</script>

<style>
.pos_relative{
  position: relative;
}
.pos_absolute{
  position: absolute;
  top:37px;
  left:5px;
  color:rgb(45, 25, 170);
  /* clear: both; */
    display: inline-block;
    overflow: hidden;
    height: 15px;
    /* overflow-y: scroll; */
    /* white-space: nowrap; */
}
.update_btn{
    font-size: 9px; 
    margin: 2px 5px;
    position: absolute;
    right: 10px;
    top:10px;
}
.dynamic_update{
    position: fixed;
    top: 0; 
    bottom:0;
    left:0;
    right:0;
    overflow: scroll;
    height: 100vh;
    width:100%;
    background: #fff;
    z-index:2;
    padding: 10px 0px 0px 0px; 
}
.close_lab_update{
    position: fixed;
    top: 50px; 
    right: 10px;
    padding:2px 7px 3px 7px;
    cursor:pointer;
    background: rgb(255, 20, 110);
    border-radius: 50%;
    cursor:pointer;
}
.v-transition-animate-enter {
  opacity: 0;
  transform: translate(500px);
}
.v-transition-animate-enter-to {
  opacity: 1;
}
.v-transition-animate-enter-active {
  transition: all .5s ease;
}

.v-transition-animate-leave {
  transform: translate(0px);
  opacity: 1;
}
.v-transition-animate-leave-to {
  transform: translate(500px);
  opacity: 0;
}
.v-transition-animate-leave-active {
  transition: all .3s ease;
}
.header_bg_patient{
    background-image: radial-gradient( circle farthest-corner at 1.3% 2.8%,  rgba(239,249,249,1) 0%, rgb(219, 233, 255) 100.2% );
    box-shadow: rgba(0, 0, 0, 0.1) 0px 10px 15px -3px, rgba(0, 0, 0, 0.05) 0px 4px 6px -2px;
}.header_bg_menu_name{
    background-image: radial-gradient( circle farthest-corner at 1.3% 2.8%,  rgba(239,249,249,1) 0%, rgb(219, 233, 255) 100.2% );
}
.icon_scale{
    
    animation-name: example;
    animation-duration: 4s;
    animation-iteration-count: infinite;
}
@keyframes example {
  0%   {transform: scale(0.7);}
  20%  {transform: scale(0.8);}
  40%  {transform: scale(0.9);}
  60%  {transform: scale(0.8);}
  80%  {transform: scale(0.7);}
  100%  {transform: scale(0.6);}
  
}
.pos_absolute p{
    font-size: 12px !important;
    display: inline-block;
    overflow: hidden;
    height: 20px;
    z-index:1;
}
.normClass{
    height: 31px;
    overflow: hidden;
    overflow-y: scroll;
    padding-top: 5px;
}
.normClass:hover{
    width: 100%;
    height: 100%;
    z-index: 1;
    background: #fff;
    overflow: hidden;
    overflow-y: scroll;
    padding-top: 5px;
}
.labTitle_small{
    border-bottom: 2px solid #04A4D8;
    padding: 3px 15px;
}
.normClass p{
  margin:0;
  padding:0;
  font-size: 13.5px !important;
  font-weight: 400 !important;
}
</style>