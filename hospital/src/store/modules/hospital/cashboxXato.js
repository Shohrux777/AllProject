export default {
    state: {
        unpay_patient_list: [],
        service_pay_list: [],
        summ: 0,
        inArray: [],
        cashSumm_Array: [
            {
                all_sum: 0,
            }
        ],
        check_print_list: [
        ],
        check_sum: 0,
        debit_sum:0,
        pay_debit: {
            debit: 0,
            pay: 0,
            card: false,
            patient: '',
            date: '',
        },
        checkInfo: {
            checkid: 0,
            cash: 0,
            card: 0,
        }
    },
    actions: {
        async fetch_unpayed_patient(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/Payments/getUnpaidPatientList');
            const res_data = await res.json();
            // console.log(ctx.rootState.hostname);
            ctx.commit('UpdateUnpayedPatient', res_data);
        },
        async fetch_service_pay_list(ctx, id) {
            const res = await fetch(ctx.rootState.hostname + '/Payments/getPatientPayedServiceByPatientIdListForPayment?PatientId=' + id);
            const res_data = await res.json();
            // console.log(ctx.rootState.hostname);
            ctx.commit('UpdateServicePay', res_data);
        }
    },
    mutations: {
        updateDebit(state, data){
            state.debit_sum = data
        },
        UpdatecheckInfo(state, data){
            state.checkInfo.checkId = data.check
            state.checkInfo.cash = data.cash
            state.checkInfo.card = data.card
        },
        UpdatePay_debit(state, data){
            var time = new Date()
            time = time.toISOString().slice(0, 10);
            state.pay_debit.debit = data.debit
            state.pay_debit.pay = data.pay
            state.pay_debit.card = data.card
            state.pay_debit.patient = data.patient
            state.pay_debit.date = time

        },
        UpdateUnpayedPatient(state, data) {
            state.unpay_patient_list = data;
        },
        UpdateServicePay(state, data) {
            console.log(data);
            state.service_pay_list = data;
            state.summ = 0;
            for (let i = 0; i < data.length; i++) {
                state.summ += data[i].summ
            }
        },
        Update_check_data(state, data) {
            state.check_sum = 0;
            state.cashSumm_Array = [];
            var jump_sum = 0;
            for (let i = 0; i < data.length; i++) {
                 state.check_sum+= data[i].summ;
                 jump_sum += parseFloat(data[i].summ);
            }
            var objSumma = {
                all_sum : 0,
                cash: 0,
                card: 0,
            }
            objSumma.all_sum = jump_sum;
            state.cashSumm_Array.push(objSumma);
            var list = [];
            list = data;
            var IntoArray = [];
            
            for (let i = 0; i < list.length; i++) {
                var check = {
                    ServicePayId: null,
                    serviceName: "",
                    summ: 0,
                    qty: 1,
                    authorizationId: null,
                }
                check.ServicePayId = list[i].serviceTypeId;
                check.serviceName = list[i].serviceName;
                check.summ = list[i].summ;
                check.authorizationId = list[i].authorizationId;

                for (let j = i + 1; j < list.length; j++) {
                    if (list[i].serviceTypeId == list[j].serviceTypeId) {
                        check.qty += 1;
                        list.splice(j, 1);
                        j--;
                    }
                }
                IntoArray.push(check);
            }
            this.commit("callInboxInCheck", IntoArray)
            // state.check_print_list = data

        },
        callInboxInCheck(state,data){
            state.check_print_list = [];
            // var cash_tmof =  state.checkInfo.cash
            // var card_tmof =  state.checkInfo.card
            state.inArray = [];
            var list_data = [];
            list_data = data;
            
            for(let k=0; k<list_data.length; k++){
                state.inArray = [];
                state.inArray.push(list_data[k]);
                for(let l=k+1; l<list_data.length; l++){
                    if(list_data[k].authorizationId == list_data[l].authorizationId){
                        state.inArray.push(list_data[l])
                        list_data.splice(l, 1);
                        l--;
                    }
                }
                // if(jump_sum <= cash_tmof){
                //     objSumma.cash = jump_sum;
                //     cash_tmof = cash_tmof - jump_sum;
                // }
                state.check_print_list.push(state.inArray);
                console.log('state.cashSumm_Array');
                console.log(state.cashSumm_Array);
            }
        }

    },
    getters: {
        get_unpay_patient_list(state) {
            return state.unpay_patient_list;
        },
        get_service_pay_list(state) {
            return state.service_pay_list;
        },
        summa(state) {
            return state.summ
        },
        get_check_sum(state) {
            return state.check_sum
        },
        get_check_print_list(state) {
            return state.check_print_list;
        },
        debit_sum(state) {
            return state.debit_sum;
        },
        pay_debit(state) {
            return state.pay_debit;
        },
        checkInfo(state) {
            return state.checkInfo;
        },
        get_cashSumm_Array(state){
            return state.cashSumm_Array
        }

    }
}