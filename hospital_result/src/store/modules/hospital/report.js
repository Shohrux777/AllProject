export default {
    state: {
        report_by_data_contragent: {
            rows: [],
            columns: ['contragent_name', 'patient_name', 'service_name', 'service_price', 'payDate', 'payed', 'finish'],
            col: []
        },
        report_by_data_time: {
            rows: [],
            columns: ['contragent_name', 'patient_name', 'service_name', 'service_price', 'paymentInCard', 'paymentInCash', 'payed', 'finish'],
            col: []
        },
        report_by_data: [],
        report_qty_summ: {
            qty: 0,
            summ: 0,
        },
        report_by_time_card_cash: {
            card: 0,
            cash: 0,
            qty: 0,
            summ: 0,
            discount_qty: 0,
            vozvrat: 0,
            dolg: 0,
        },
        report_by_time_card_cash_bydoctorInfo: {
            card: 0,
            cash: 0,
            qty: 0,
            summ: 0,
            rows: [],
            users: 0,
            vozvrat: 0,
            discount_qty: 0,
        },
        report_operatsion_service: {
            card: 0,
            cash: 0,
            summ: 0,
            discount_qty:0,
            qty:0,
            vozvrat: 0,
            rows:[]
        },
        report_by_cont_card_cash: {
            card: 0,
            cash: 0,
        },
        daily_report_list: [],
        service_bron_data: [],
        report_by_data_time_excel: [],
        check_payment_list: [],
        payment_debit_pay_list: [],
        daily_report_list_kassir: [],
        report_by_data_time_kassir: {
            rows: [],
            columns: ['contragent_name', 'patient_name', 'service_name', 'service_price', 'paymentInCard', 'paymentInCash', 'payed', 'finish'],
            col: []
        },
        report_by_time_card_cash_kassir: {
            card: 0,
            cash: 0,
            qty: 0,
            summ: 0,
            discount_qty: 0,
        },
    },
    actions: {
        async fetch_report_by_data_cont(ctx, a) {
            const res = await fetch(ctx.rootState.hostname + '/Payments/getPaymentsListByContragentAndDateTime?ContragentId=' + a.contId +
                '&beginDate=' + a.time1 + '&endDate=' + a.time2);
            const res_data = await res.json();
            ctx.commit('Updatereport_by_data_contragent', res_data);
        },
        async fetch_report_by_data_time(ctx, a) {
            const res = await fetch(ctx.rootState.hostname + '/Payments/getPaymentsListByContragentAndDateTime?ContragentId=' + a.contId +
                '&beginDate=' + a.time1 + '&endDate=' + a.time2);
            const res_data = await res.json();
            ctx.commit('Updatereport_by_data_time', res_data);
        },
        async fetch_report_by_data_time_debit_pay(ctx, a) {
            const res = await fetch(ctx.rootState.hostname + '/Payments/getPaymentsListByContragentAndDateTime?ContragentId=' + a.contId +
                '&beginDate=' + a.time1 + '&endDate=' + a.time2);
            const res_data = await res.json();
            ctx.commit('Updatereport_by_data_time_debit_pay', res_data);
        },
        async fetch_report_by_data_time_check(ctx, a) {
            const res = await fetch(ctx.rootState.hostname + '/Payments/getPaymentsListByContragentAndDateTime?ContragentId=' + 0 +
                '&beginDate=' + a.time1 + '&endDate=' + a.time2);
            const res_data = await res.json();
            ctx.commit('Updatereport_by_data_time_check', res_data);
        },
        async fetch_report_by_data_time_excel(ctx, a) {
            const res = await fetch(ctx.rootState.hostname + '/Payments/getPaymentsListByContragentAndDateTime?ContragentId=' + a.contId +
                '&beginDate=' + a.time1 + '&endDate=' + a.time2);
            const res_data = await res.json();
            ctx.commit('Updatereport_by_data_time_excel', res_data);
        },
        async fetch_report_by_data_time_service_bron(ctx, a) {
            const res = await fetch(ctx.rootState.hostname + '/Payments/getPaymentsListByContragentAndDateTime?ContragentId=' + a.contId +
                '&beginDate=' + a.time1 + '&endDate=' + a.time2);
            const res_data = await res.json();
            ctx.commit('Updatereport_by_data_time_service_bron', res_data);
        },
        async fetch_report_by_data(ctx, a) {
            const res = await fetch(ctx.rootState.hostname + '/HospitalContragentDebitPaymentReports/getContragentPaymentsByDate?begindate=' + a.time1 + '&endDate=' + a.time2);
            const res_data = await res.json();
            ctx.commit('Updatereport_by_data', res_data);
        },
        async fetch_report_by_data_time_by_doctorinfo(ctx, a) {
            const res = await fetch(ctx.rootState.hostname + '/Payments/getPaginationByDateTimeAndDoctorAuthIdAll?page=0&size=100000&begin_date=' + a.time1 + '&end_date=' + a.time2 + '&auth_id=' + a.auth_id);
            const res_data = await res.json();
            ctx.commit('Updatereport_by_data_time_by_doctorinfo', res_data);
        },
        async fetch_report_by_kassir_data_time(ctx, a) {
            const res = await fetch(ctx.rootState.hostname + '/Payments/getPaymentsListByKassirAndDateTime?payed_auth_id=' + a.contId +
                '&beginDate=' + a.time1 + '&endDate=' + a.time2);
            const res_data = await res.json();
            ctx.commit('Updatereport_by_kassir_data_time', res_data);
        },

    },
    mutations: {
        Updatereport_by_data(state, data) {
            console.log('data iployeee');
            console.log(data);
            state.report_by_data = data;
        },
        Updatereport_by_data_contragent(state, data) {
            state.report_qty_summ.summ = data.reduce((total, item) => {
                return total + item.summ
            }, 0)
            state.report_by_cont_card_cash.card = data.reduce((total, item) => {
                return total + item.paymentInCard
            }, 0)
            state.report_by_cont_card_cash.cash = data.reduce((total, item) => {
                return total + item.paymentInCash
            }, 0)
            state.report_qty_summ.qty = data.length;
            console.log(data)
            state.report_by_data_contragent.rows = data.map(item => {
                return {
                    id: item.id,
                    contragent_name: item.contragent.name,
                    patient_name: item.patients.fio,
                    service_name: item.serviceName,
                    service_price: item.summ,
                    payDate: item.payedDate.slice(0, 10),
                    payed: item.finishPayment,
                    paymentInCard: item.paymentInCard,
                    paymentInCash: item.paymentInCash,
                    finish: item.finish,
                    service_Group: item.serviceType.hospitalServiceTypeGroup.name
                }
            });
        },
        dibet_delite_cont(state, index) {
            state.report_by_data.splice(parseInt(index), 1);
        },
        Updatereport_by_data_time_check(state,data){
            console.log(data)
            var check_list_temp = [];
            check_list_temp = data.filter(person => person.finishPayment);
            console.log(check_list_temp)
            state.check_payment_list = [];
            for(let i=0; i<check_list_temp.length; i++){
                var list_temp = [];
                list_temp.push(check_list_temp[i]);
                for(let j=i+1; j<check_list_temp.length; j++){
                    if(check_list_temp[i].patientsId == check_list_temp[j].patientsId){
                        list_temp.push(check_list_temp[j]);
                        check_list_temp.splice(j,1);
                        j--;
                    }
                    else{
                        break;
                    }
                }
                state.check_payment_list.push(list_temp)
            }
            console.log('state.check_payment_list')
            console.log(state.check_payment_list)
        },  
        Updatereport_by_data_time_by_doctorinfo(state, data){
            console.log('data')
            console.log(data)
            state.report_by_time_card_cash_bydoctorInfo.card = 0;
            state.report_by_time_card_cash_bydoctorInfo.cash = 0;
            state.report_by_time_card_cash_bydoctorInfo.qty = 0;
            state.report_by_time_card_cash_bydoctorInfo.summ = 0;
            state.report_by_time_card_cash_bydoctorInfo.discount_qty = 0;
            state.report_by_time_card_cash_bydoctorInfo.users = 0;
            state.report_by_time_card_cash_bydoctorInfo.vozvrat = 0;
            state.report_by_time_card_cash_bydoctorInfo.rows = [];
            // state.report_by_time_card_cash_bydoctorInfo.rows = data.items_list;

            state.report_operatsion_service.card = 0;
            state.report_operatsion_service.cash = 0;
            state.report_operatsion_service.summ = 0;
            state.report_operatsion_service.discount_qty = 0;
            state.report_operatsion_service.qty = 0;
            state.report_operatsion_service.vozvrat = 0;
            state.report_operatsion_service.rows = [];

            for(let i=0; i<data.items_list.length; i++){
                if(data.items_list[i].serviceType.link_str2 != 'операция '){
                    state.report_by_time_card_cash_bydoctorInfo.card += data.items_list[i].PaymentInCard
                    state.report_by_time_card_cash_bydoctorInfo.cash += data.items_list[i].PaymentInCash
                    state.report_by_time_card_cash_bydoctorInfo.summ += data.items_list[i].Summ
                    state.report_by_time_card_cash_bydoctorInfo.discount_qty += data.items_list[i].discount_qty
                    state.report_by_time_card_cash_bydoctorInfo.qty ++;
                    state.report_by_time_card_cash_bydoctorInfo.rows.push(data.items_list[i]);
                    if(data.items_list[i].pay_num1>0){
                        state.report_by_time_card_cash_bydoctorInfo.vozvrat += parseFloat(data.items_list[i].pay_num1)
                        state.report_by_time_card_cash_bydoctorInfo.qty --;
                    }
                }
                else{
                    state.report_operatsion_service.card += data.items_list[i].PaymentInCard;
                    state.report_operatsion_service.cash += data.items_list[i].PaymentInCash;
                    state.report_operatsion_service.summ += data.items_list[i].Summ;
                    state.report_operatsion_service.discount_qty += data.items_list[i].discount_qty;
                    state.report_operatsion_service.qty ++;
                    state.report_operatsion_service.rows.push(data.items_list[i]);
                    if(data.items_list[i].pay_num1>0){
                        state.report_operatsion_service.vozvrat += parseFloat(data.items_list[i].pay_num1)
                        state.report_operatsion_service.qty --;
                    }
                }
            }
            console.log(state.report_by_time_card_cash_bydoctorInfo)

            // state.report_by_time_card_cash_bydoctorInfo.card = data.items_list.reduce((total, item) => {
            //     return total + item.PaymentInCard
            // }, 0)
            // state.report_by_time_card_cash_bydoctorInfo.cash = data.items_list.reduce((total, item) => {
            //     return total + item.PaymentInCash
            // }, 0)
            // state.report_by_time_card_cash_bydoctorInfo.summ = data.items_list.reduce((total, item) => {
            //     return total + item.Summ
            // }, 0)
            // state.report_by_time_card_cash_bydoctorInfo.discount_qty = data.items_list.reduce((total, item) => {
            //     return total + item.discount_qty
            // }, 0)
            let patientList = [];

            patientList = data.items_list.map(item => {
                return item.PatientsId
            });
            for(let i=0; i<patientList.length; i++){
                for (let j = i + 1; j < patientList.length; j++) {
                    if (patientList[i] == patientList[j]) {
                        patientList.splice(j, 1);
                        j--;
                    }
                }
            }
            console.log(patientList)
            state.report_by_time_card_cash_bydoctorInfo.users = patientList.length;
            
        },
        Updatereport_by_data_time_debit_pay(state,data){
            console.log('data time check item')
            console.log(data)
            state.payment_debit_pay_list = [];
            for(let i=0; i<data.length; i++){
                if(data[i].dolg_status){
                    state.payment_debit_pay_list.push(data[i])
                }
            }
            console.log(state.payment_debit_pay_list)
        },
        Updatereport_by_data_time(state, data) {
            console.log('fetch get')
            console.log()
            console.log(data)
            state.report_by_time_card_cash.card = data.reduce((total, item) => {
                return total + item.paymentInCard
            }, 0)
            state.report_by_time_card_cash.cash = data.reduce((total, item) => {
                return total + item.paymentInCash
            }, 0)
            state.report_by_time_card_cash.summ = data.reduce((total, item) => {
                return total + item.summ
            }, 0)
            state.report_by_time_card_cash.discount_qty = data.reduce((total, item) => {
                return total + item.discount_qty
            }, 0)
            state.report_by_time_card_cash.qty = data.length;
            // console.log(state.report_by_time_card_cash)
            state.report_by_time_card_cash.vozvrat = 0;
            state.report_by_time_card_cash.dolg = 0;
            for(let j=0; j<data.length; j++){
                if(data[j].pay_num1>0){
                    state.report_by_time_card_cash.vozvrat += parseFloat(data[j].pay_num1);
                }
                else if(data[j].dolg_summ>0){
                    state.report_by_time_card_cash.dolg += parseFloat(data[j].dolg_summ);
                }
            }
            console.log(' qani nima chiqarkan')
            console.log(state.report_by_time_card_cash)
            state.report_by_data_time.rows = data.map(item => {
                return {
                    id: item.id,
                    contragent_name: item.contragent.name,
                    patient_name: item.patients.fio,
                    service_name: item.serviceName,
                    service_price: item.summ,
                    paymentInCard: item.paymentInCard,
                    paymentInCash: item.paymentInCash,
                    payed: item.finishPayment,
                    finish: item.finish,
                    serviceGroup: item.serviceType.hospitalServiceTypeGroup.name,
                    regisdate: item.registratedDate,
                    discount_persantage_qty: item.discount_persantage_qty,
                    discount_card_qty: item.discount_card_qty,
                    discount_qty: item.discount_qty,
                    dolg_summ: item.dolg_summ,
                    dolg_status: item.dolg_status,
                    serviceTypeId: item.serviceType.id,
                    payedDate: item.payedDate,
                    authorizationId: item.authorizationId,
                    contragentId: item.contragentId,
                    pay_num1: item.pay_num1,
                    pay_status: item.pay_status,
                }
            });
            console.log('state.report_by_data_time.rows')
            console.log(state.report_by_data_time.rows)

            var list = [];
            list = data
            console.log(list)
            state.daily_report_list = [];
            for (let i = 0; i < list.length; i++) {
                var daily = {
                    service_group: '',
                    cash: 0,
                    card: 0,
                    date: null,
                    qty: 0,
                }
                if (list[i].finishPayment == true) {
                    daily.qty++;
                }
                daily.service_group = list[i].serviceType.hospitalServiceTypeGroup.name;
                daily.cash = list[i].paymentInCash;
                daily.card = list[i].paymentInCard;
                daily.date = list[i].registratedDate.slice(0, 10);
                console.log('list[i].serviceTypeId')
                console.log(daily.service_group)

                for (let j = i + 1; j < list.length; j++) {
                    if (daily.service_group == list[j].serviceType.hospitalServiceTypeGroup.name) {
                        if (list[j].finishPayment == true) {
                            daily.qty++;
                        }
                        daily.cash += list[j].paymentInCash;
                        daily.card += list[j].paymentInCard;
                        list.splice(j, 1);
                        j--;
                    }
                }
                state.daily_report_list.push(daily);
            }
            // console.log(state.daily_report_list)
        },
        Updatereport_by_data_time_excel(state, data) {
            state.report_by_data_time_excel = data.map(item => {
                return {
                    id: item.id,
                    contragent_name: item.contragent.name,
                    patient_name: item.patients.fio,
                    service_name: item.serviceName,
                    service_price: item.summ,
                    paymentInCard: item.paymentInCard,
                    paymentInCash: item.paymentInCash,
                    summ: parseInt(item.paymentInCard) + parseInt(item.paymentInCash),
                    payed: item.finishPayment,
                    finish: item.finish,
                    serviceGroup: item.serviceType.hospitalServiceTypeGroup.name,
                    regisdate: item.registratedDate,
                    discount_persantage_qty: item.discount_persantage_qty,
                    discount_card_qty: item.discount_card_qty,
                    discount_qty: item.discount_qty,
                    dolg_summ: item.dolg_summ,
                    dolg_status: item.dolg_status,
                    serviceTypeId: item.serviceType.id,
                    payedDate: item.payedDate
                }
            });
            // console.log(state.daily_report_list)
        },
        Updatereport_by_data_time_service_bron(state, data) {
            console.log('fetch get')
            console.log(data)
            state.service_bron_data = [];
            state.report_by_time_card_cash.card = data.reduce((total, item) => {
                return total + item.paymentInCard
            }, 0)
            state.report_by_time_card_cash.cash = data.reduce((total, item) => {
                return total + item.paymentInCash
            }, 0)
            state.report_by_time_card_cash.summ = data.reduce((total, item) => {
                return total + item.summ
            }, 0)
            state.report_by_time_card_cash.discount_qty = data.reduce((total, item) => {
                return total + item.discount_qty
            }, 0)
            state.report_by_time_card_cash.qty = data.length;
            // console.log(state.report_by_time_card_cash)
            
            var list = [];
            var summaTest = 0;
            list = data;
            console.log(list)
            // var doc_service_operatsion = [];
            
            for(let i=0; i<list.length; i++){
                let item_service_data = {
                    user_id : null,
                    service_price: 0,
                    service_qty: 0,
                    service_vozvrat: 0,
                    operatsion_price: 0,
                    operatsion_qty:0,
                    operatsion_vozvrat: 0,
                    authorizationId:null,
                    payedDate: null, 
                    startsionar_price: 0,
                    startsionar_qty:0,
                }
                item_service_data.user_id = list[i].authorization.usersId
                item_service_data.authorizationId = list[i].authorizationId
                item_service_data.payedDate = list[i].payedDate
                if(list[i].serviceType.link_str2 == 'операция '){
                    item_service_data.operatsion_price = parseInt(list[i].paymentInCash + list[i].paymentInCard)
                    item_service_data.operatsion_qty = 1;
                    summaTest += parseInt(list[i].paymentInCash + list[i].paymentInCard)
                    if(list[i].pay_num1>0){
                        item_service_data.operatsion_vozvrat += parseInt(list[i].pay_num1);
                        item_service_data.operatsion_price -= parseInt(list[i].pay_num1);
                        item_service_data.operatsion_qty = 0;
                    }
                }
                else{
                    item_service_data.service_price = parseInt(list[i].paymentInCash + list[i].paymentInCard)
                    item_service_data.service_qty = 1;
                    summaTest += parseInt(list[i].paymentInCash + list[i].paymentInCard)
                    if(list[i].pay_num1> 0){
                        item_service_data.service_vozvrat += parseInt(list[i].pay_num1);
                        item_service_data.service_price -= parseInt(list[i].pay_num1);
                        item_service_data.service_qty = 0;
                    }
                }

                for(let j=i+1; j<list.length; j++){
                    if(item_service_data.user_id == list[j].authorization.usersId){
                        if(list[j].serviceType.link_str2 == 'операция '){
                            item_service_data.operatsion_price += parseInt(list[j].paymentInCash + list[j].paymentInCard)
                            summaTest += parseInt(list[j].paymentInCash + list[j].paymentInCard)
                            item_service_data.operatsion_qty ++; 
                            if(list[j].pay_num1> 0){
                                item_service_data.operatsion_vozvrat += parseInt(list[j].pay_num1);
                                item_service_data.operatsion_price -= parseInt(list[j].pay_num1);
                                item_service_data.operatsion_qty --; 
                            }
                        }
                        else{
                            item_service_data.service_price += parseInt(list[j].paymentInCash + list[j].paymentInCard)
                            summaTest += parseInt(list[j].paymentInCash + list[j].paymentInCard)
                            item_service_data.service_qty ++; 
                            if(list[j].pay_num1> 0){
                                item_service_data.service_vozvrat += parseInt(list[j].pay_num1);
                                item_service_data.service_price -= parseInt(list[j].pay_num1);
                                item_service_data.service_qty --;
                            }
                        }
                        list.splice(j,1);
                        j--;
                    }
                    
                }
                state.service_bron_data.push(item_service_data)

            }
            console.log(summaTest)
            console.log('state.service_bron_data')
            console.log(state.service_bron_data)
            
            
            // console.log(state.daily_report_list)
        },
        Updatereport_by_kassir_data_time(state, data) {
            console.log('fetch get')
            console.log()
            console.log(data)
            state.report_by_time_card_cash_kassir.card = data.reduce((total, item) => {
                return total + item.paymentInCard
            }, 0)
            state.report_by_time_card_cash_kassir.cash = data.reduce((total, item) => {
                return total + item.paymentInCash
            }, 0)
            state.report_by_time_card_cash_kassir.summ = data.reduce((total, item) => {
                return total + item.summ
            }, 0)
            state.report_by_time_card_cash_kassir.discount_qty = data.reduce((total, item) => {
                return total + item.discount_qty
            }, 0)
            state.report_by_time_card_cash_kassir.qty = data.length;
            // console.log(state.report_by_time_card_cash_kassir)
            state.report_by_data_time_kassir.rows = data.map(item => {
                return {
                    id: item.id,
                    contragent_name: item.contragent.name,
                    patient_name: item.patients.fio,
                    service_name: item.serviceName,
                    service_price: item.summ,
                    paymentInCard: item.paymentInCard,
                    paymentInCash: item.paymentInCash,
                    payed: item.finishPayment,
                    finish: item.finish,
                    serviceGroup: item.serviceType.hospitalServiceTypeGroup.name,
                    regisdate: item.registratedDate,
                    discount_persantage_qty: item.discount_persantage_qty,
                    discount_card_qty: item.discount_card_qty,
                    discount_qty: item.discount_qty,
                    dolg_summ: item.dolg_summ,
                    dolg_status: item.dolg_status,
                    serviceTypeId: item.serviceType.id,
                    payedDate: item.payedDate,
                    authorizationId: item.authorizationId,
                    contragentId: item.contragentId
                }
            });
            var list = [];
            list = data
            console.log(list)
            state.daily_report_list_kassir = [];
            for (let i = 0; i < list.length; i++) {
                var daily = {
                    service_group: '',
                    cash: 0,
                    card: 0,
                    date: null,
                    qty: 0,
                }
                if (list[i].finishPayment == true) {
                    daily.qty++;
                }
                daily.service_group = list[i].serviceType.hospitalServiceTypeGroup.name;
                daily.cash = list[i].paymentInCash;
                daily.card = list[i].paymentInCard;
                daily.date = list[i].registratedDate.slice(0, 10);
                console.log('list[i].serviceTypeId')
                console.log(daily.service_group)

                for (let j = i + 1; j < list.length; j++) {
                    if (daily.service_group == list[j].serviceType.hospitalServiceTypeGroup.name) {
                        if (list[j].finishPayment == true) {
                            daily.qty++;
                        }
                        daily.cash += list[j].paymentInCash;
                        daily.card += list[j].paymentInCard;
                        list.splice(j, 1);
                        j--;
                    }
                }
                state.daily_report_list_kassir.push(daily);
            }
            // console.log(state.daily_report_list)
        },
    },
    getters: {
        get_report_by_data_contragent(state) {
            return state.report_by_data_contragent;
        },
        get_report_by_data_time(state) {
            return state.report_by_data_time;
        },
        get_report_by_data_time_excel(state) {
            return state.report_by_data_time_excel;
        },
        get_report_qty_summ(state) {
            return state.report_qty_summ;
        },
        get_report_by_time_card_cash(state) {
            return state.report_by_time_card_cash;
        },
        get_report_by_cont_card_cash(state) {
            return state.report_by_cont_card_cash;
        },
        get_report_by_data(state) {
            return state.report_by_data;
        },
        get_daily_report_list(state) {
            return state.daily_report_list;
        },
        get_report_by_time_card_cash_bydoctorInfo(state) {
            return state.report_by_time_card_cash_bydoctorInfo;
        },
        get_service_bron_data(state){
            return state.service_bron_data;
        },
        get_report_operatsion_service(state){
            return state.report_operatsion_service;
        },
        get_check_payment_list(state){
            return state.check_payment_list;
        },
        get_payment_debit_pay_list(state){
            return state.payment_debit_pay_list;
        },
        get_report_by_data_time_kassir(state) {
            return state.report_by_data_time_kassir;
        },
        get_daily_report_list_kassir(state) {
            return state.daily_report_list_kassir;
        },
        get_report_by_time_card_cash_kassir(state) {
            return state.report_by_time_card_cash_kassir;
        },

    }
}