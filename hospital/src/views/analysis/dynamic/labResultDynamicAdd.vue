<template>
  <div class="add_main_page">
    <div class="bakteriya bg-white" style="min-height: calc(100vh - 55px);">
        <div class="header text-center border-bottom py-2 header_bg_menu_name"  style="position:relative;">
            <h4 class="text-primary">{{main_name_title.name_title}}</h4>
            <mdb-btn  color="info" type="submit" class="p-0 update_btn" @click="labUpdate_show = true;"  p="r3 l3 t2 b2">
                <i class="fas fa-pen text-white mask waves-effect t m-0 pr-2" ></i>{{$t('Edit')}}
            </mdb-btn>
        </div>
        <div class="row mx-0 header_bg_patient py-2">
            <div class="col-4">
            <form v-on:submit.prevent="submit">
                <div class="d-flex " style="height:40px;">
                    <mdb-input :label="$t('number')" size="sm" class="m-0 ml-4 w-100 mt-1 bg-white" v-model="numberEnter" outline/>
                    <mdb-btn  color="primary" type="submit" class=" p-0" style="font-size: 9.8px; margin: 4px 5px; width:100px;"   p="r3 l3 t0 b0">{{$t('enter')}}</mdb-btn>
                </div>
            </form>
            </div>
            <div class="col-4">
                <lineSelect
                class="mt-1 ml-0"
                :options="filteredList" 
                :searchshow="true"
                @select="selectPatient"
                :selected="patient_name"
                :label="$t('patient list')"
            />
            <small v-if="$v.patient_name.$dirty && patient_id == null" class="invalid-text pt-4 pl-4" >
                {{$t('select_item')}}
            </small>
            </div>
            <div class="col-4">
                <mdb-input :label="$t('doctor_name')" size="sm" class="m-0 mt-1 bg-white" v-model="doctor_name" outline/>
                <small v-if="$v.doctor_name.$dirty && !$v.doctor_name.required" class="invalid-text  pl-5" style="position:absolute; bottom:-10px; left:-20px;"> 
                {{$t('select_item')}}
                </small>
            </div>
        </div>
        <loader v-if="loading"/>
        <div v-if="result_list.length" class="mx-1 row mt-4" >
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_1 != '' && get_lab_name_list[0].name_1 != ''">
                <h6>{{ get_lab_center_list[0].name_1 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_1 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[0].result" :label="get_lab_name_list[0].name_1"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_1 != ''" v-html="get_lab_norma_list[0].name_1"></small>
                    </div>
                </div>
            </div>

            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_2 != '' && get_lab_name_list[0].name_2 != ''">
                <h6>{{ get_lab_center_list[0].name_2 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_2 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[1].result" :label="get_lab_name_list[0].name_2"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_2 != ''" v-html="get_lab_norma_list[0].name_2"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_3 != ''  && get_lab_name_list[0].name_3 != ''">
                <h6>{{ get_lab_center_list[0].name_3 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_3 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[2].result" :label="get_lab_name_list[0].name_3"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_3 != ''" v-html="get_lab_norma_list[0].name_3"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_4 != '' && get_lab_name_list[0].name_4 != ''">
                <h6>{{ get_lab_center_list[0].name_4 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_4 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[3].result" :label="get_lab_name_list[0].name_4"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_4 != ''" v-html="get_lab_norma_list[0].name_4"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_5 != '' && get_lab_name_list[0].name_5 != ''">
                <h6>{{ get_lab_center_list[0].name_5 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_5 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[4].result" :label="get_lab_name_list[0].name_5"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_5 != ''" v-html="get_lab_norma_list[0].name_5"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_6 != '' && get_lab_name_list[0].name_6 != ''">
                <h6>{{ get_lab_center_list[0].name_6 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_6 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[5].result" :label="get_lab_name_list[0].name_6"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_6 != ''" v-html="get_lab_norma_list[0].name_6"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_7 != '' && get_lab_name_list[0].name_7 != ''">
                <h6>{{ get_lab_center_list[0].name_7 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_7 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[6].result" :label="get_lab_name_list[0].name_7"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_7 != ''" v-html="get_lab_norma_list[0].name_7"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_8 != '' && get_lab_name_list[0].name_8 != ''">
                <h6>{{ get_lab_center_list[0].name_8 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_8 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[7].result" :label="get_lab_name_list[0].name_8"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_8 != ''" v-html="get_lab_norma_list[0].name_8"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_9 != '' && get_lab_name_list[0].name_9 != ''">
                <h6>{{ get_lab_center_list[0].name_9 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_9 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[8].result" :label="get_lab_name_list[0].name_9"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_9 != ''" v-html="get_lab_norma_list[0].name_9"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_10 != '' && get_lab_name_list[0].name_10 != ''">
                <h6>{{ get_lab_center_list[0].name_10 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_10 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[9].result" :label="get_lab_name_list[0].name_10"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_10 != ''" v-html="get_lab_norma_list[0].name_10"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_11 != '' && get_lab_name_list[0].name_11 != ''">
                <h6>{{ get_lab_center_list[0].name_11 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_11 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[10].result" :label="get_lab_name_list[0].name_11"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_11 != ''" v-html="get_lab_norma_list[0].name_11"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_12 != '' && get_lab_name_list[0].name_12 != ''">
                <h6>{{ get_lab_center_list[0].name_12 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_12 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[11].result" :label="get_lab_name_list[0].name_12"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_12 != ''" v-html="get_lab_norma_list[0].name_12"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_13 != '' && get_lab_name_list[0].name_13 != ''">
                <h6>{{ get_lab_center_list[0].name_13 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_13 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[12].result" :label="get_lab_name_list[0].name_13"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_13 != ''" v-html="get_lab_norma_list[0].name_13"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_14 != '' && get_lab_name_list[0].name_14 != ''">
                <h6>{{ get_lab_center_list[0].name_14 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_14 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[13].result" :label="get_lab_name_list[0].name_14"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_14 != ''" v-html="get_lab_norma_list[0].name_14"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_15 != '' && get_lab_name_list[0].name_15 != ''">
                <h6>{{ get_lab_center_list[0].name_15 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_15 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[14].result" :label="get_lab_name_list[0].name_15"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_15 != ''" v-html="get_lab_norma_list[0].name_15"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_16 != ''  && get_lab_name_list[0].name_16 != ''">
                <h6>{{ get_lab_center_list[0].name_16 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_16 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[15].result" :label="get_lab_name_list[0].name_16"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_16 != ''" v-html="get_lab_norma_list[0].name_16"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_17 != ''  && get_lab_name_list[0].name_17 != ''">
                <h6>{{ get_lab_center_list[0].name_17 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_17 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[16].result" :label="get_lab_name_list[0].name_17"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_17 != ''" v-html="get_lab_norma_list[0].name_17"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_18 != ''  && get_lab_name_list[0].name_18 != ''">
                <h6>{{ get_lab_center_list[0].name_18 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_18 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[17].result" :label="get_lab_name_list[0].name_18"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_18 != ''" v-html="get_lab_norma_list[0].name_18"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_19 != ''  && get_lab_name_list[0].name_19 != ''">
                <h6>{{ get_lab_center_list[0].name_19 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_19 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[18].result" :label="get_lab_name_list[0].name_19"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_19 != ''" v-html="get_lab_norma_list[0].name_19"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_20 != ''  && get_lab_name_list[0].name_20 != ''">
                <h6>{{ get_lab_center_list[0].name_20 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_20 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[19].result" :label="get_lab_name_list[0].name_20"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_20 != ''" v-html="get_lab_norma_list[0].name_20"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_21 != ''  && get_lab_name_list[0].name_21 != ''">
                <h6>{{ get_lab_center_list[0].name_21 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_21 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[20].result" :label="get_lab_name_list[0].name_21"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_21 != ''" v-html="get_lab_norma_list[0].name_21"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_22 != ''  && get_lab_name_list[0].name_22 != ''">
                <h6>{{ get_lab_center_list[0].name_22 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_22 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[21].result" :label="get_lab_name_list[0].name_22"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_22 != ''" v-html="get_lab_norma_list[0].name_22"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_23 != '' && get_lab_name_list[0].name_23 != ''">
                <h6>{{ get_lab_center_list[0].name_23 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_23 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[22].result" :label="get_lab_name_list[0].name_23"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_23 != ''" v-html="get_lab_norma_list[0].name_23"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_24 != '' && get_lab_name_list[0].name_24 != ''">
                <h6>{{ get_lab_center_list[0].name_24 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_24 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[23].result" :label="get_lab_name_list[0].name_24"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_24 != ''" v-html="get_lab_norma_list[0].name_24"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_25 != '' && get_lab_name_list[0].name_25 != ''">
                <h6>{{ get_lab_center_list[0].name_25 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_25 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[24].result" :label="get_lab_name_list[0].name_25"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_25 != ''" v-html="get_lab_norma_list[0].name_25"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_26 != '' && get_lab_name_list[0].name_26 != ''">
                <h6>{{ get_lab_center_list[0].name_26 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_26 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[25].result" :label="get_lab_name_list[0].name_26"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_26 != ''" v-html="get_lab_norma_list[0].name_26"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_27 != '' && get_lab_name_list[0].name_27 != ''">
                <h6>{{ get_lab_center_list[0].name_27 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_27 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[26].result" :label="get_lab_name_list[0].name_27"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_27 != ''" v-html="get_lab_norma_list[0].name_27"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_28 != '' && get_lab_name_list[0].name_28 != ''">
                <h6>{{ get_lab_center_list[0].name_28 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_28 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[27].result" :label="get_lab_name_list[0].name_28"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_28 != ''" v-html="get_lab_norma_list[0].name_28"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_29 != '' && get_lab_name_list[0].name_29 != ''">
                <h6>{{ get_lab_center_list[0].name_29 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_29 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[28].result" :label="get_lab_name_list[0].name_29"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_29 != ''" v-html="get_lab_norma_list[0].name_29 "></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_30 != '' && get_lab_name_list[0].name_30 != ''">
                <h6>{{ get_lab_center_list[0].name_30 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_30 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[29].result" :label="get_lab_name_list[0].name_30"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_30 != ''" v-html="get_lab_norma_list[0].name_30"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_31 != '' && get_lab_name_list[0].name_31 != ''">
                <h6>{{ get_lab_center_list[0].name_31 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_31 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[30].result" :label="get_lab_name_list[0].name_31"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_31 != ''" v-html="get_lab_norma_list[0].name_31"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_32 != '' && get_lab_name_list[0].name_32 != ''">
                <h6>{{ get_lab_center_list[0].name_32 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_32 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[31].result" :label="get_lab_name_list[0].name_32"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_32 != ''" v-html="get_lab_norma_list[0].name_32"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_33 != '' && get_lab_name_list[0].name_33 != ''">
                <h6>{{ get_lab_center_list[0].name_33 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_33 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[32].result" :label="get_lab_name_list[0].name_33"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_33 != ''" v-html="get_lab_norma_list[0].name_33"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_34 != '' && get_lab_name_list[0].name_34 != ''">
                <h6>{{ get_lab_center_list[0].name_34 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_34 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[33].result" :label="get_lab_name_list[0].name_34"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_34 != ''" v-html="get_lab_norma_list[0].name_34"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_35 != '' && get_lab_name_list[0].name_35 != ''">
                <h6>{{ get_lab_center_list[0].name_35 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_35 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[34].result" :label="get_lab_name_list[0].name_35"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_35 != ''" v-html="get_lab_norma_list[0].name_35"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_36 != '' && get_lab_name_list[0].name_36 != ''">
                <h6>{{ get_lab_center_list[0].name_36 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_36 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[35].result" :label="get_lab_name_list[0].name_36"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_36 != ''" v-html="get_lab_norma_list[0].name_36"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_37 != '' && get_lab_name_list[0].name_37 != ''">
                <h6>{{ get_lab_center_list[0].name_37 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_37 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[36].result" :label="get_lab_name_list[0].name_37"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_37 != ''" v-html="get_lab_norma_list[0].name_37"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_38 != '' && get_lab_name_list[0].name_38 != ''">
                <h6>{{ get_lab_center_list[0].name_38 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_38 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[37].result" :label="get_lab_name_list[0].name_38"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_38 != ''" v-html="get_lab_norma_list[0].name_38"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_39 != '' && get_lab_name_list[0].name_39 != ''">
                <h6>{{ get_lab_center_list[0].name_39 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_39 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[38].result" :label="get_lab_name_list[0].name_39"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_39 != ''" v-html=" get_lab_norma_list[0].name_39"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_40 != '' && get_lab_name_list[0].name_40 != ''">
                <h6>{{ get_lab_center_list[0].name_40 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_40 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[39].result" :label="get_lab_name_list[0].name_40"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_40 != ''" v-html="get_lab_norma_list[0].name_40"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_41 != '' && get_lab_name_list[0].name_41 != ''">
                <h6>{{ get_lab_center_list[0].name_41 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_41 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[40].result" :label="get_lab_name_list[0].name_41"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_41 != ''" v-html=" get_lab_norma_list[0].name_41 "></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_42 != '' && get_lab_name_list[0].name_42 != ''">
                <h6>{{ get_lab_center_list[0].name_42 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_42 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[41].result" :label="get_lab_name_list[0].name_42"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_42 != ''" v-html="get_lab_norma_list[0].name_42"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_43 != '' && get_lab_name_list[0].name_43 != ''">
                <h6>{{ get_lab_center_list[0].name_43 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_43 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[42].result" :label="get_lab_name_list[0].name_43"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_43 != ''" v-html="get_lab_norma_list[0].name_43"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_44 != '' && get_lab_name_list[0].name_44 != ''">
                <h6>{{ get_lab_center_list[0].name_44 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_44 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[43].result" :label="get_lab_name_list[0].name_44"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_44 != ''" v-html="get_lab_norma_list[0].name_44"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_45 != '' && get_lab_name_list[0].name_45 != ''">
                <h6>{{ get_lab_center_list[0].name_45 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_45 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[44].result" :label="get_lab_name_list[0].name_45"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_45 != ''" v-html="get_lab_norma_list[0].name_45"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_46 != '' && get_lab_name_list[0].name_46 != ''">
                <h6>{{ get_lab_center_list[0].name_46 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_46 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[45].result" :label="get_lab_name_list[0].name_46"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_46 != ''" v-html="get_lab_norma_list[0].name_46"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_47 != '' && get_lab_name_list[0].name_47 != ''">
                <h6>{{ get_lab_center_list[0].name_47 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_47 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[46].result" :label="get_lab_name_list[0].name_47"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_47 != ''" v-html="get_lab_norma_list[0].name_47"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_48 != '' && get_lab_name_list[0].name_48 != ''">
                <h6>{{ get_lab_center_list[0].name_48 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_48 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[47].result" :label="get_lab_name_list[0].name_48"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_48 != ''" v-html="get_lab_norma_list[0].name_48"></small>
                    </div>
                </div>
            </div>
            
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_49 != '' && get_lab_name_list[0].name_49 != ''">
                <h6>{{ get_lab_center_list[0].name_49 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_49 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[48].result" :label="get_lab_name_list[0].name_49"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_49 != ''" v-html="get_lab_norma_list[0].name_49"></small>
                    </div>
                </div>
            </div>
            <div class="col-12 text-center m-0 p-0" v-if="main_name_title.extra_status && get_lab_center_list[0].name_50 != '' && get_lab_name_list[0].name_50 != ''">
                <h6>{{ get_lab_center_list[0].name_50 }}</h6>
            </div>
            <div class="col-4 px-1" v-if="main_name_title.lab_name_status && get_lab_name_list[0].name_50 != ''">
                <div class="pos_relative">
                    <mdb-input class="m-0 p-0 mb-4" v-model="result_list[49].result" :label="get_lab_name_list[0].name_50"  outline/>
                    <div class="pos_absolute" v-if="main_name_title.norma_status">
                        <small v-if="get_lab_norma_list[0].name_50 != ''" v-html="get_lab_norma_list[0].name_50"></small>
                    </div>
                </div>
            </div>
        </div>
        <div class="text-right mx-4 py-2 border-top">
        <mdb-btn color="primary" class="m-0 p-0"   p="r4 l4 t2 b2" @click="send" style="font-size:10px;">{{$t('Send')}}</mdb-btn>  
        </div>
    </div>
    <div class="dynamic_update" v-if="labUpdate_show">
        <div class="close_lab_update shadow " @click="labUpdate_show = false;">
            <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon_scale icon-tabler-x" width="25" height="25" viewBox="0 0 24 24" stroke-width="2.5" stroke="#fff" fill="none" stroke-linecap="round" stroke-linejoin="round">
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
import lineSelect from "@/components/hospital/cashUserSelectForAnalysis.vue";
export default {
  components: {
    mdbBtn,mdbInput,lineSelect, labUpdate
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
      }
    },
  data(){
    return {
      loading : false,
      modal_info : '',
      modal_status: false,
      numberEnter: null,
      patient_name: '',
      patient_id: null,
      name: '',
      doctor_name: localStorage.docName,
      id: this.$route.params.id,
      result_list: [],
      labUpdate_show: false,
    }
  },
  computed:{
    ...mapGetters(['get_patient_client_list', 'get_lab_firs_name_list', 'get_lab_name_list', 'get_lab_norma_list', 
        'get_lab_edzm_list','get_lab_center_list', 'get_client_info']),
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
    for(let i=0; i<50; i++){
      let obj_name = {
        result: ''
      }
      this.result_list.push(obj_name);
    //   console.log(this.result_list)
    }
    this.fetch_patient_client();
    await this.fetch_lab_name_list(this.main_id);
    if(this.main_name_title.norma_status == true){
        await this.fetch_lab_norma_list(this.main_id);
    }
    if(this.main_name_title.extra_status == true){
        await this.fetch_lab_center_list(this.main_id);
    }
    
    // console.log('this.get_lab_name_list')
    // console.log(this.main_name_title)
    // console.log(this.get_lab_name_list)
    // console.log(this.get_lab_norma_list)
    // console.log(this.get_lab_center_list)
  },
  methods:{
    ...mapActions(['fetch_patient_client', 'fetch_lab_name_list', 'fetch_lab_firs_name_list',
         'fetch_lab_norma_list', 'fetch_lab_edzm_list', 'fetch_lab_center_list', 'fetch_client_info']),
    selectPatient(option){
    //   console.log(option)
      this.patient_name = option.data.fio;
      this.patient_id = option.data.id;
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
    async submit(){
        try{
            await this.fetch_client_info(this.numberEnter);
            this.$refs.message.success('Added_successfully')
            if(this.get_client_info.fio){
                this.patient_name = this.get_client_info.fio;
                this.patient_id = this.get_client_info.id;
            }
            else{this.$refs.message.success('not_found')}
        }
        catch{
            this.$refs.message.success('not_found')
        }
    },
    // async submit(){
    //   try{
    //     this.loading = true;
    //     const response = await fetch(this.$store.state.hostname + '/HospitalMarkSeviceTyesWithNumber/getFullInfoById?id=' + this.numberEnter)
    //     const data = await response.json()
    //     // console.log('data')
    //     // console.log(data)
    //     if(data.id){
    //       this.patient_name = data.patients.fio;
    //       this.patient_id = data.patients.id;
    //       this.loading = false;
    //       this.$refs.message.success('Added_successfully')
    //     }
    //     else{
    //       // this.$refs.msg.error('Error_successfully')
    //       this.loading = false;
    //       this.modal_info = data.detail + "    (" + data.routine + ")";
    //       this.modal_status = true;
    //       return false;
    //     }
    //   }
    //   catch{
    //     this.loading = false;
    //     this.modal_info = "Server no connect";
    //     this.modal_status = true;
    //   }
    // },
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
        for(let i=0; i<50; i++){
        let obj_name = {
            result: ''
        }
        this.result_list.push(obj_name);
        //   console.log(this.result_list)
        }
        this.patient_name = '';
        this.patient_id = null;
        this.numberEnter = null;
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
          "id": 0,
          })
        };
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + '/HospitalAnalizDynamicResults', requestOptions)
        const data = await response.json()
        // console.log('data addDynamic')
        // console.log(data)
        if(data.id){
          this.cls_wnd()
          this.sendFinish()
          this.loading = false;
          this.$refs.message.success('Added_successfully')
          this.$router.push('/lab_print/' + data.id)
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
    }
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
    height: 10px;
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
    padding: 50px 30px 30px 30px; 
}
.close_lab_update{
    position: fixed;
    top: 50px; 
    right: 10px;
    padding:6px;
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
}
</style>