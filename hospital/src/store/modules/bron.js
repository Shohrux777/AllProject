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
            const res = await fetch(ctx.rootState.hostname + '/HospitalBronRoomPayments/getPaginationPulTolashTarixi?page=0&size=1000&beginDate=' + a.time1 + '&endDate=' + a.time2);
            const res_data = await res.json();
            ctx.commit('Updatereport_by_bron_payed', res_data);
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
                    if(data.items_list[i].bedsList[j].not_finished_payment_list.length>0){
                        bed_bron_qty ++;
                    }
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
            console.log('data')
            console.log(data)
            state.bron_cash_card.cash = 0;
            state.bron_cash_card.card = 0;
            state.payment_payed_history = data.items_list;
            for(let i=0; i<data.items_list.length; i++){
                state.bron_cash_card.cash += data.items_list[i].cash_summ;
                state.bron_cash_card.card += data.items_list[i].card_summ;
            }
            state.bron_cash_card.qty = data.items_list.length;

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

    }
}