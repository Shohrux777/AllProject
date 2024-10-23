export default {
    state: {
        unpay_patient_list: [],
        unpay_patient_list_today: [],
        unpay_patient_all_list: [],
        last_patient_list: [],
        service_pay_list: [],
        dolg_check_data: {
            list: [],
            pay_sum: 0,
            dolg: 0,
        },
        cashbox_patient_info_selected: {
            id: 0
        },
        summ: 0,
        skidka: 0,
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
            code: ''
        },
        cashbox_patient_info: {
            patient_name: '...',
            patient_id: 0,
        },
        dolg_patient_id_list: {
            list: [],
            summ: 0,
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
        },
        async fetch_dolg_patient_id_list(ctx,id){
            try{
                const res = await fetch(ctx.rootState.hostname + '/HospitalPatientDolgPaymentInfoes/getPaginationByPatientId?page=0&size=100&patient_id=' + id);
                const res_data = await res.json();
                ctx.commit('UpdateDolg_patient_id_list', res_data);
            }
            catch(error){
                console.log(error)
            }
        },
        async fetch_all_patient_unpayed_lists(ctx){
            try{
                const res_service = await fetch(ctx.rootState.hostname + '/Payments/getUnpaidPatientList');
                const res_data_service = await res_service.json();
                const res_startsionar = await fetch(ctx.rootState.hostname + '/HospitalBronRoomPayments/getPaginationNeedpayedSummNotZero?page=0&size=200');
                const res_data_startsionar = await res_startsionar.json();
                const res_dolg = await fetch(ctx.rootState.hostname + '/HospitalPatientDolgPaymentInfoes/getPaginationQarzdorlik?page=0&size=200');
                const res_data_dolg = await res_dolg.json();

                ctx.commit('UpdateAll_patient_unpayed_lists', {service: res_data_service, bron: res_data_startsionar, dolg: res_data_dolg});
            }
            catch(error){
                console.log('server error')
            }
            
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
            state.checkInfo.code = data.code
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
            state.unpay_patient_list_today = [];
            let today_date = new Date().toISOString().slice(0,10) + 'T00:00:35.000Z';
            for(let i=0; i<data.length; i++){
                if(new Date(today_date) < new Date(data[i].registratedDate)){
                    state.unpay_patient_list_today.push(data[i])
                }
            }
        },
        UpdateServicePay(state, data) {
            console.log('data strlik');
            console.log(data);
            state.service_pay_list = data;
            state.summ = 0;
            state.skidka = 0;
            for (let i = 0; i < data.length; i++) {
                state.summ += data[i].summ;
                state.skidka += data[i].discount_qty;
            }
        },
        UpdateDolg_patient_id_list(state, data){
            console.log('data dolg_items')
            console.log(data)
            console.log(state.dolg_patient_id_list)
            state.dolg_patient_id_list.summ = 0;
            state.dolg_patient_id_list.list = data.items_list;
            for(let i=0; i<data.items_list.length; i++){
                state.dolg_patient_id_list.summ += data.items_list[i].dolg_summ;
            }
        },
        Update_check_data(state, data) {
            console.log(data, 'bu checkga keladigan data')
            state.check_sum = 0;
            for (let i = 0; i < data.length; i++) {
                state.check_sum += data[i].summ;
            }
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
                    talon: 0,
                    doc_name: ''
                }
                check.talon = list[i].discount_real_qty;
                check.doc_name = list[i].discount_card_qty;
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
            state.cashSumm_Array = [];
            state.inArray = [];
            var list_data = [];
            list_data = data;
            var jump_sum = 0;
            console.log('list_data')
            console.log(list_data)
            for(let k=0; k<list_data.length; k++){
                state.inArray = [];
                jump_sum = 0;
                state.inArray.push(list_data[k]);
                jump_sum += (parseFloat(list_data[k].summ)*parseFloat(list_data[k].qty));
                for(let l=k+1; l<list_data.length; l++){
                    if(list_data[k].authorizationId == list_data[l].authorizationId){
                        state.inArray.push(list_data[l])
                        jump_sum += (parseFloat(list_data[l].summ)*parseFloat(list_data[l].qty));
                        list_data.splice(l, 1);
                        l--;
                    }
                }
                var objSumma = {
                    all_sum : 0,
                    cash: 0,
                    card: 0,
                }
                objSumma.all_sum = jump_sum;
                state.cashSumm_Array.push(objSumma);
                state.check_print_list.push(state.inArray);
                console.log('state.cashSumm_Array');
                console.log(state.cashSumm_Array);
            }
        },
        Update_cashbox_patient_info(state, data){
            state.cashbox_patient_info.patient_name = data.name
            state.cashbox_patient_info.patient_id = data.id
        },

        // service laboratory, qarzlar, xonalar tulov qilmaganlar bitta listda 
        UpdateAll_patient_unpayed_lists(state, data){
            console.log('all_date_show')
            console.log(data)
            state.unpay_patient_all_list = [];
            for(let i=0; i< data.bron.items_list.length; i++){
                let temp = {
                    fio: data.bron.items_list[i].reserved_name_1,
                    id: data.bron.items_list[i].PatientsId,
                }
                state.unpay_patient_all_list.push(temp)
            }
            for(let i=0; i< data.dolg.items_list.length; i++){
                let temp = {
                    fio: data.dolg.items_list[i].fio,
                    id: data.dolg.items_list[i].patient_id,
                }
                state.unpay_patient_all_list.push(temp)
            }
            for(let i=0; i< data.service.length; i++){
                let temp = {
                    fio: data.service[i].fio,
                    id: data.service[i].id,
                }
                state.unpay_patient_all_list.push(temp)
            }
            for(let i=0; i<state.unpay_patient_all_list.length; i++){
                for(let j=i+1; j<state.unpay_patient_all_list.length; j++){
                    if(state.unpay_patient_all_list[i].id == state.unpay_patient_all_list[j].id){
                        state.unpay_patient_all_list.splice(j,1);
                        j--;
                    }
                }
            }
            console.log(state.unpay_patient_all_list)

        },
        UpdateCashbox_patient_info_selected(state, data){
            state.cashbox_patient_info_selected = data
        },
        UpdateDolgCheck_data(state, data){
            state.dolg_check_data = data;
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
        get_skidka(state){
            return state.skidka.toFixed()
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
        },
        get_unpay_patient_list_today(state){
            return state.unpay_patient_list_today;
        },
        get_cashbox_patient_info(state){
            return state.cashbox_patient_info;
        },
        get_unpay_patient_all_list(state){
            return state.unpay_patient_all_list;
        },
        get_dolg_patient_id_list(state){
            return state.dolg_patient_id_list;
        },
        get_cashbox_patient_info_selected(state){
            return state.cashbox_patient_info_selected;
        },
        get_dolg_check_data(state){
            return state.dolg_check_data;
        }

    }
}