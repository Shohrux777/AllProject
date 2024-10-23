<template>
  <div class="allWidthLabTable mt-3 mb-5">
    <div class="text-center mb-2" v-if="option.lab_name_status && option.name_1">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_1}}</small>
        <small class="fs_without_table px-2">{{option.name_1}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_2">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_2}}</small>
        <small class="fs_without_table px-2">{{option.name_2}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_3">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_3}}</small>
        <small class="fs_without_table px-2">{{option.name_3}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_4">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_4}}</small>
        <small class="fs_without_table px-2">{{option.name_4}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_5">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_5}}</small>
        <small class="fs_without_table px-2">{{option.name_5}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_6">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_6}}</small>
        <small class="fs_without_table px-2">{{option.name_6}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_7">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_7}}</small>
        <small class="fs_without_table px-2">{{option.name_7}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_8">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_8}}</small>
        <small class="fs_without_table px-2">{{option.name_8}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_9">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_9}}</small>
        <small class="fs_without_table px-2">{{option.name_9}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_10">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_10}}</small>
        <small class="fs_without_table px-2">{{option.name_10}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_11">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_11}}</small>
        <small class="fs_without_table px-2">{{option.name_11}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_12">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_12}}</small>
        <small class="fs_without_table px-2">{{option.name_12}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_13">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_13}}</small>
        <small class="fs_without_table px-2">{{option.name_13}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_14">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_14}}</small>
        <small class="fs_without_table px-2">{{option.name_14}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_15">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_15}}</small>
        <small class="fs_without_table px-2">{{option.name_15}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_16">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_16}}</small>
        <small class="fs_without_table px-2">{{option.name_16}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_17">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_17}}</small>
        <small class="fs_without_table px-2">{{option.name_17}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_18">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_18}}</small>
        <small class="fs_without_table px-2">{{option.name_18}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_19">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_19}}</small>
        <small class="fs_without_table px-2">{{option.name_19}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_20">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_20}}</small>
        <small class="fs_without_table px-2">{{option.name_20}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_21">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_21}}</small>
        <small class="fs_without_table px-2">{{option.name_21}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_22">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_22}}</small>
        <small class="fs_without_table px-2">{{option.name_22}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_23">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_23}}</small>
        <small class="fs_without_table px-2">{{option.name_23}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_24">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_24}}</small>
        <small class="fs_without_table px-2">{{option.name_24}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_25">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_25}}</small>
        <small class="fs_without_table px-2">{{option.name_25}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_26">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_26}}</small>
        <small class="fs_without_table px-2">{{option.name_26}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_27">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_27}}</small>
        <small class="fs_without_table px-2">{{option.name_27}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_28">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_28}}</small>
        <small class="fs_without_table px-2">{{option.name_28}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_29">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_29}}</small>
        <small class="fs_without_table px-2">{{option.name_29}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_30">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_30}}</small>
        <small class="fs_without_table px-2">{{option.name_30}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_31">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_31}}</small>
        <small class="fs_without_table px-2">{{option.name_31}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_32">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_32}}</small>
        <small class="fs_without_table px-2">{{option.name_32}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_33">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_33}}</small>
        <small class="fs_without_table px-2">{{option.name_33}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_34">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_34}}</small>
        <small class="fs_without_table px-2">{{option.name_34}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_35">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_35}}</small>
        <small class="fs_without_table px-2">{{option.name_35}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_36">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_36}}</small>
        <small class="fs_without_table px-2">{{option.name_36}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_37">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_37}}</small>
        <small class="fs_without_table px-2">{{option.name_37}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_38">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_38}}</small>
        <small class="fs_without_table px-2">{{option.name_38}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_39">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_39}}</small>
        <small class="fs_without_table px-2">{{option.name_39}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_40">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_40}}</small>
        <small class="fs_without_table px-2">{{option.name_40}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_41">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_41}}</small>
        <small class="fs_without_table px-2">{{option.name_41}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_42">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_42}}</small>
        <small class="fs_without_table px-2">{{option.name_42}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_43">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_43}}</small>
        <small class="fs_without_table px-2">{{option.name_43}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_44">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_44}}</small>
        <small class="fs_without_table px-2">{{option.name_44}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_45">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_45}}</small>
        <small class="fs_without_table px-2">{{option.name_45}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_46">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_46}}</small>
        <small class="fs_without_table px-2">{{option.name_46}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_47">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_47}}</small>
        <small class="fs_without_table px-2">{{option.name_47}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_48">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_48}}</small>
        <small class="fs_without_table px-2">{{option.name_48}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_49">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_49}}</small>
        <small class="fs_without_table px-2">{{option.name_49}}</small>
      </div>
    </div>
    <div class="text-center mb-2" v-if="option.lab_name_status  && option.name_50">
      <div>
        <small class="fs_without_table" v-if="option.lab_name_status">{{ get_lab_name_list[0].name_50}}</small>
        <small class="fs_without_table px-2">{{option.name_50}}</small>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props:{
    get_lab_name_list:
      {
        type: Array,
        default(){
          return []
        }
      },
      option:{
        type: Object,
        default(){
          return {}
        }
      }
  }
}
</script>

<style>
.fs_without_table{
  font-size: 32px !important;
}
</style>