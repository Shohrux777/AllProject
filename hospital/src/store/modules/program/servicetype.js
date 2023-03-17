export default {
    state: {
        service_type: {
            rows: [],
            columns: ['name', 'price', 'persantage', 'contragentPrice', 'groupSer', 'activeStatus'],
            col: []
        },
        user_service_type: [],
        user_service_list: [],
        service_no_chosen_list: [],
        serviceChosenList: [],
        service_medicine: null,
        service_medicine_list: {
            rows: [],
            columns: ['product', 'qty', 'activeStatus'],
            col: []
        },
        bonus_sum_cont_list: {
            rows: [],
            columns: ['service_name', 'bonusSumm', 'activeStatus'],
            col: []
        },
        bahila_service_type_group: {
            rows: [],
            columns: ['service_name', 'bonusSumm', 'activeStatus'],
            col: []
        }
    },
    actions: {
        async fetch_service_type(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/ServiceTypes');
            const res_data = await res.json();
            // console.log(ctx.rootState.hostname);
            ctx.commit('UpdateService_type', res_data);
        },
        async fetch_add_service_to_user(ctx, option) {
            const response = await fetch(ctx.rootState.hostname + '/ServiceTypeDetails/addServiceTypeDetail?UserId=' + option.user_id +
                '&ServiceTypeId=' + option.service_id);
            const data = await response.json()
            ctx.commit('UpdateUser_service_type', data);
        },
        async fetch_users_service_list(ctx, userId) {
            const response = await fetch(ctx.rootState.hostname + '/ServiceTypeDetails/getServiceTypeDetailByUserId?UserId=' + userId);
            const data = await response.json()
            ctx.commit('UpdateUser_service_list', data);
        },
        async fetch_service_medicine_list(ctx, Id) {
            const response = await fetch(ctx.rootState.hostname + '/HospitalServiceRecipes/getHospitalServiceByServiceTypeId?serviceTypeId=' + Id);
            const data = await response.json()
            ctx.commit('Update_service_medicine_list', data);
        },
        async fetch_bonus_sum_cont_list(ctx, Id) {
            const response = await fetch(ctx.rootState.hostname + '/ContragentServiceTypeBonusAdditanalies/getAdditanilyBonusByContragentId?contragentId=' + Id);
            const data = await response.json()
            ctx.commit('Update_bonus_sum_cont_list', data);
        },
        async fetch_bahila_service_type_group(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/HospitelRequiredServiceTypesAllPatcientsAndNotPatcients');
            const res_data = await res.json();
            // console.log(ctx.rootState.hostname);
            ctx.commit('Update_bahila_service_type_group', res_data);
        }
    },
    mutations: {
        UpdateService_type(state, data) {
            state.service_type.rows = data.map(item => {
                return {
                    id: item.id,
                    name: item.name,
                    price: item.price,
                    persantage: item.persantage,
                    contragentPrice: item.contragentPrice,
                    groupSer: item.hospitalServiceTypeGroup.name,
                    groupId: item.hospitalServiceTypeGroup.id,
                    calculateWithPersentage: item.calculateWithPersentage,
                    paymentable: item.paymentable,
                    link_str: item.link_str,
                    link_str1: item.link_str1,
                    link_str2: item.link_str2,
                    link_str3: item.link_str3,
                    link_str4: item.link_str4,
                    link_str5: item.link_str5,
                    activeStatus: item.activeStatus
                }
            });
        },
        Update_bonus_sum_cont_list(state, data) {
            console.log('dasdasdad asdasd')
            console.log(data)
            state.bonus_sum_cont_list.rows = data.map(item => {
                return {
                    id: item.id,
                    bonusSumm: item.bonusSumm,
                    service_name: item.serviceType.name,
                    service_id: item.serviceType.id,
                    activeStatus: item.serviceType.activeStatus
                }
            });
        },
        Update_bahila_service_type_group(state, data) {
            // console.log('bahila');
            // console.log(data);
            state.bahila_service_type_group.rows = data;
        },
        UpdateUser_service_type(state, data) {
            console.log(data);
            state.user_service_type = data
        },
        Update_service_medicine_list(state, data) {
            console.log(data);
            state.service_medicine_list.rows = data.map(item => {
                return {
                    id: item.id,
                    product: item.marketProduct.name,
                    qty: item.qty,
                    activeStatus: item.activeStatus
                }
            })
        },
        UpdateUser_service_list(state, data) {
            console.log('user fanat');
            console.log(data);
            state.user_service_list = data

        },
        serviceIdForMedice(state, data) {
            state.service_medicine = data
        },
        Users_service_list(state) {
            state.service_no_chosen_list = []
            state.serviceChosenList = []
            for (let i = 0; i < state.service_type.rows.length; i++) {
                let a = 0;
                for (let j = 0; j < state.user_service_list.length; j++) {
                    if (state.service_type.rows[i].id == state.user_service_list[j].serviceType.id) {
                        a++
                    }
                }
                if (a == 0) {
                    console.log(state.service_type.rows[i])
                    let bag = {
                        groupSer : state.service_type.rows[i].groupSer,
                        id : state.service_type.rows[i].id,
                        name : state.service_type.rows[i].name,
                        status : false
                    }
                    console.log(bag)
                    state.service_no_chosen_list.push(bag);
                    a = 0;
                } else {
                    a = 0;
                }
            }
        },
        choosenServiceType(state, option){
            // console.log(option)
            if(option.data.status == false){
                console.log('add')
                state.service_no_chosen_list[option.index].status = true;
                state.serviceChosenList.push(state.service_no_chosen_list[option.index])
            }
            else if(option.data.status == true){
                state.service_no_chosen_list[option.index].status = false;
                for(let i=0; i<state.serviceChosenList.length;i++){
                    if(state.serviceChosenList[i].id == option.data.id){
                        state.serviceChosenList.splice(i,1);
                    }
                }
            }
            console.log(state.serviceChosenList)
            console.log(state.service_no_chosen_list)


        },
        service_type_row_delete(state, index) {
            state.service_type.rows.splice(parseInt(index), 1);
        },
        hos_bonus_cont_delete_service(state, index) {
            state.bonus_sum_cont_list.rows.splice(parseInt(index), 1);
        },
        service_medicine_list_delete(state, index) {
            state.service_medicine_list.rows.splice(parseInt(index), 1);
        },

    },
    getters: {
        get_service_type(state) {
            return state.service_type;
        },
        get_user_service_type(state) {
            return state.user_service_type;
        },
        get_user_service_list(state) {
            return state.user_service_list;
        },
        get_service_no_chosen_list(state) {
            return state.service_no_chosen_list;
        },
        get_service_medicine(state) {
            return state.service_medicine;
        },
        get_service_medicine_list(state) {
            return state.service_medicine_list;
        },
        get_bonus_sum_cont_list(state) {
            return state.bonus_sum_cont_list;
        },
        get_bahila_service_type_group(state) {
            return state.bahila_service_type_group;
        },
        get_serviceChosenList(state) {
            return state.serviceChosenList;
        }


    }
}