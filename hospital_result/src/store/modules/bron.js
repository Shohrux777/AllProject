export default {
    state: {
        bron_room_list: {
            rows: [],
            columns: ['name','beds_count','reserved_name_1' ,'reserved_name_2'],
            col: []
        },
        bron_room_list_pagination: [],
        bron_beds_list: {
            rows: [],
            columns: ['beds_name', 'beds_type'],
            col: []
        },
        bron_rb_list: {
            rows: [],
            columns: ['hospitalBronRoomsid', 'hospitalBronRoomBedsid'],
            col: []
        },
        price_type_room: {
            rows: [],
            columns: ['name', 'room_price', 'room_bed_price', 'room_bed_price_not_patient', 'created_date_time'],
            col: []
        },
        payment_payed_history: [],
        bron_cash_card: {
            cash: 0,
            card: 0,
            qty: 0,
            ovqat_puli: 0,
            qolgan_pul: 0,
            vozvrat_sum: 0,
            need_payed_summ: 0,
        },
        bron_check_data: {},
        payment_payed_history_doc_id: [],
        payment_payed_history_excel: [],
        bron_cash_summa :0,
        bron_card_summa :0,
        bron_need_summa :0,
        bron_dicId_summa: {
            ovqat_puli: 0,
            qolgan_pul: 0,
            vozvrat_sum : 0,
            need_payed_summ: 0,
        },
        bron_list_patient_id: {
            list: [],
            need_payed: 0,
            unpayedlist: [],
        }
    
    },
    actions: {
        async fetch_bron_room(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/HospitalBronRoomNs');
            const res_data = await res.json();
            // console.log(ctx.rootState.hostname);
            ctx.commit('Update_bron_room', res_data);
        },
        async fetch_bron_room_pagination(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/HospitalBronRoomNs/getPagination?page=0&size=200');
            const res_data = await res.json();
            // console.log(ctx.rootState.hostname);
            ctx.commit('Update_bron_room_pagination', res_data);
        },
        async fetch_bron_beds(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/HospitalBronRoomBeds');
            const res_data = await res.json();
            // console.log(ctx.rootState.hostname);
            ctx.commit('Update_bron_beds', res_data);
        },
        async fetch_bron_rb(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/HospitalBronRoomAddBeds');
            const res_data = await res.json();
            // console.log(ctx.rootState.hostname);
            ctx.commit('Update_bron_rb', res_data);
        },
        async fetch_price_type_room(ctx){
            const res = await fetch(ctx.rootState.hostname + '/HospitalRoomTypes');
            const res_data = await res.json();
            // console.log(ctx.rootState.hostname);
            ctx.commit('Update_price_room', res_data);
        },
        async fetch_report_by_bron_payed(ctx, a) {
            const res = await fetch(ctx.rootState.hostname + '/HospitalBronRoomPayments/getPaginationPulTolashTarixiYangi?page=0&size=10000&beginDate=' + a.time1 + '&endDate=' + a.time2);
            const res_data = await res.json();
            ctx.commit('Updatereport_by_bron_payed', res_data);
        },
        async fetch_report_by_bron_payed_excel(ctx, a) {
            const res = await fetch(ctx.rootState.hostname + '/HospitalBronRoomPayments/getPaginationPulTolashTarixiYangi?page=0&size=10000&beginDate=' + a.time1 + '&endDate=' + a.time2);
            const res_data = await res.json();
            ctx.commit('Updatereport_by_bron_payed_excel', res_data);
        },
        async fetch_report_by_bron_payed_doc_id(ctx, a) {
            const res = await fetch(ctx.rootState.hostname + '/HospitalBronRoomPayments/getPaginationPulTolashTarixiYangi?page=0&size=10000&beginDate=' + a.time1 + '&endDate=' + a.time2);
            const res_data = await res.json();
            ctx.commit('Updatereport_by_bron_payed_doc_id', {data:res_data, doc_id: a.doc_id});
        },
        async fetch_patient_bron_payment(ctx, id) {
            const res = await fetch(ctx.rootState.hostname + '/HospitalBronRoomPayments/getPaginationByPatientIdYangi?page=0&size=100&patient_id=' + id);
            const res_data = await res.json();
            ctx.commit('Update_patient_bron_payment', res_data);
        },
    },
    mutations: {
        Update_bron_room(state, data) {
            state.bron_room_list.rows = data;
        },
        Update_bron_room_pagination(state, data){
            state.bron_room_list_pagination = [];
            console.log('data')
            console.log(data.items_list)
            for( let i=0; i<data.items_list.length; i++){
                let bed_bron_qty = 0;
                for(let j=0; j<data.items_list[i].bedsList.length; j++){
                    for(let k=0; k<data.items_list[i].bedsList[j].not_finished_payment_list.length; k++){
                        if(data.items_list[i].bedsList[j].not_finished_payment_list[k].finish_payment == false){
                            bed_bron_qty ++;
                        }
                    }
                    // real qismi bu tepadagi forni uchirsa ishlaydi
                    
                    // if(data.items_list[i].bedsList[j].not_finished_payment_list.length>0){
                    //     bed_bron_qty ++;
                    // }
                }
                let a = {
                    room: data.items_list[i],
                    HospitalRoomTypeid : data.items_list[i].HospitalRoomTypeid,
                    bedsList : data.items_list[i].bedsList,
                    beds_count : data.items_list[i].beds_count,
                    hospitalRoomType : data.items_list[i].hospitalRoomType,
                    id : data.items_list[i].id,
                    name : data.items_list[i].name,
                    reserved_name_1 : data.items_list[i].reserved_name_1,
                    reserved_name_2 : data.items_list[i].reserved_name_2,
                    reserved_name_3 : data.items_list[i].reserved_name_3,
                    reserved_number_1 : data.items_list[i].reserved_number_1,
                    reserved_number_2 : data.items_list[i].reserved_number_2,
                    reserved_status_1 : data.items_list[i].reserved_status_1,
                    reserved_status_2 : data.items_list[i].reserved_status_2,
                    bed_bron_qty: bed_bron_qty,
                }
                state.bron_room_list_pagination.push(a)
            }
        },
        bron_room_row_delete(state, index) {
            state.bron_room_list.rows.splice(parseInt(index), 1);
        },

        Update_bron_beds(state, data) {
            state.bron_beds_list.rows = data;
        },
        bron_beds_row_delete(state, index) {
            state.bron_beds_list.rows.splice(parseInt(index), 1);
        },

        Update_bron_rb(state, data) {
            console.log(data)
            state.bron_rb_list.rows = data;
        },
        bron_rb_row_delete(state, index) {
            state.bron_rb_list.rows.splice(parseInt(index), 1);
        },
        price_room_row_delete(state, index) {
            state.price_type_room.rows.splice(parseInt(index), 1);
        },

        Update_price_room(state, data){
            state.price_type_room.rows = data;
        },
        Updatereport_by_bron_payed(state, data){
            console.log('data_by_bron_payed')
            console.log(data)
            state.bron_cash_card.cash = 0;
            state.bron_cash_card.card = 0;
            state.bron_cash_card.ovqat_puli = 0;
            state.bron_cash_card.qolgan_pul = 0;
            state.bron_cash_card.vozvrat_sum = 0;
            state.bron_cash_card.need_payed_summ = 0;
            state.payment_payed_history = data.items_list;
            for(let i=0; i<data.items_list.length; i++){
                state.bron_cash_card.cash += data.items_list[i].cash_sum;
                state.bron_cash_card.card += data.items_list[i].card_sum;
                state.bron_cash_card.ovqat_puli += data.items_list[i].ovqat_puli;
                state.bron_cash_card.qolgan_pul += data.items_list[i].qolgan_pul;
                state.bron_cash_card.need_payed_summ += data.items_list[i].hospitalBronRoomPayments.need_payed_summ;

                if(data.items_list[i].vozvrat_sum  != null){
                    state.bron_cash_card.vozvrat_sum += data.items_list[i].vozvrat_sum;
                }
                
            }
            state.bron_cash_card.qty = data.items_list.length;

        },
        Updatereport_by_bron_payed_excel(state, data){
            state.payment_payed_history_excel = data.items_list.map(item => {
                return {
                    reserved_number_db_5: item.reserved_number_db_5,
                    hospitalBronRoomPayments: item.hospitalBronRoomPayments,
                    card_sum: item.card_sum,
                    cash_sum: item.cash_sum,
                    id_payment: item.id_payment,
                    ovqat_puli: item.ovqat_puli,
                    payed_summ: item.payed_summ,
                    summ: parseInt(item.card_sum) + parseInt(item.cash_sum),
                    qolgan_pul: item.qolgan_pul,
                    vozvrat_sum: item.vozvrat_sum,
                }
            });
            

        },
        Updatereport_by_bron_payed_doc_id(state, doc_data){
            state.payment_payed_history_doc_id = [];
            state.bron_cash_summa = 0;
            state.bron_card_summa = 0;
            state.bron_need_summa = 0;
            state.bron_dicId_summa.ovqat_puli = 0;
            state.bron_dicId_summa.qolgan_pul = 0;
            state.bron_dicId_summa.vozvrat_sum = 0;
            state.bron_dicId_summa.need_payed_summ = 0;
            console.log('doc_data_hospital_id')
            console.log(doc_data)
            for(let i=0; i<doc_data.data.items_list.length; i++){
                if(doc_data.data.items_list[i].hospitalBronRoomPayments.UsersId == doc_data.doc_id){
                    state.payment_payed_history_doc_id.push(doc_data.data.items_list[i])
                    state.bron_cash_summa += doc_data.data.items_list[i].cash_sum;
                    state.bron_card_summa += doc_data.data.items_list[i].card_sum;
                    state.bron_need_summa += doc_data.data.items_list[i].hospitalBronRoomPayments.need_payed_summ;
                    state.bron_dicId_summa.need_payed_summ += doc_data.data.items_list[i].hospitalBronRoomPayments.need_payed_summ;
                    state.bron_dicId_summa.ovqat_puli += doc_data.data.items_list[i].ovqat_puli;
                    state.bron_dicId_summa.qolgan_pul += doc_data.data.items_list[i].qolgan_pul;
                    if(doc_data.data.items_list[i].vozvrat_sum  != null){
                        state.bron_dicId_summa.vozvrat_sum += doc_data.data.items_list[i].vozvrat_sum;
                    }
                }
            }
            console.log(state.payment_payed_history_doc_id)
        },
        updatebronCheck(state, data){
            console.log('updatebronCheck', data);
            state.bron_check_data = data;
        },
        Update_patient_bron_payment(state, data){
            console.log('bemorni dalniylariga kirdi', data)
            state.bron_list_patient_id.need_payed = 0;
            state.bron_list_patient_id.unpayedlist = [];
            state.bron_list_patient_id.list = data.items_list;
            for(let i=0; i<data.items_list.length; i++){
                if(data.items_list[i].finish_payment == false && data.items_list[i].need_payed_summ>0){
                    state.bron_list_patient_id.need_payed += data.items_list[i].need_payed_summ;
                    state.bron_list_patient_id.unpayedlist.push(data.items_list[i])
                }
            }
        },
        bron_patient_summ_nol(state){
            state.bron_list_patient_id.need_payed = 0;
            state.bron_list_patient_id.list = [];
        }

    },
    getters: {
        bron_room_list(state) {
            return state.bron_room_list;
        },

        bron_beds_list(state) {
            return state.bron_beds_list;
        },

        bron_rb_list(state) {
            return state.bron_rb_list;
        },

        price_type_room(state) {
            return state.price_type_room;
        },
        get_bron_room_list_pagination(state) {
            return state.bron_room_list_pagination;
        },
        get_payment_payed_history(state) {
            return state.payment_payed_history;
        },
        get_bron_cash_card(state){
            return state.bron_cash_card;
        },
        get_bron_check_data(state){
            return state.bron_check_data;
        },
        get_payment_payed_history_doc_id(state){
            return state.payment_payed_history_doc_id;
        },
        get_bron_cash_summa(state){
            return state.bron_cash_summa;
        },
        get_bron_card_summa(state){
            return state.bron_card_summa;
        },
        get_bron_need_summa(state){
            return state.bron_need_summa;
        },
        get_bron_dicId_summa(state){
            return state.bron_dicId_summa;
        },
        get_bron_list_patient_id(state){
            return state.bron_list_patient_id;
        },
        get_payment_payed_history_excel(state){
            return state.payment_payed_history_excel;
        }

    }
}