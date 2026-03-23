export default {
    state: {
        m_kassa: {
            rows: [],
            columns: ['name', 'created_date_time'],
            col: []
        },
        kassa_user_list: [],
        kassa_info: {},
    },
    actions: {
        async fetchKassa(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/TegirmonKassa');
            const res_data = await res.json();
            // console.log(ctx.rootState.hostname);
            ctx.commit('updateKassa', res_data);
        },
        async fetchKassa_userId(ctx, user_id) {
            try{
                const res = await fetch(ctx.rootState.hostname + '/TegirmonKassa/getPaginationUserId?page=0&size=100&user_id=' + user_id);
                const res_data = await res.json();
                // console.log(ctx.rootState.hostname);
                ctx.commit('updateKassa_userId', res_data);
            }
            catch{
                console.log(error);
                ctx.commit('updateKassa_userId', {items_list: []});
                // state.kassa_user_list = [];
            }
            
        },
        async fetchKassa_info(ctx, kassa_id) {
            try{
                const res = await fetch(ctx.rootState.hostname + '/TegirmonKassaInfo/getKassaInfoLastKassaId?kassa_id=' + kassa_id);
                const res_data = await res.json();
                // console.log(ctx.rootState.hostname);
                ctx.commit('updateKassa_info', res_data);
            }
            catch{
                console.log(error);
                ctx.commit('updateKassa_info', {cash: 0, dollor: 0});
                // state.kassa_user_list = [];
            }
            
        },
    },
    mutations: {
        updateKassa(state, data) {
            state.m_kassa.rows = data;
        },
        updateKassa_userId(state, data) {
            state.kassa_user_list = data.items_list;
        },
        updateKassa_info(state, data) {
            state.kassa_info = data;
        },
        kassa_delete_row(state, index) {
            state.m_kassa.rows.splice(parseInt(index), 1);
        },
        updateKassa_column(state, data) {
            state.m_kassa.col = data;
            //status true bolgan ustun nomlarini olish uchun
            state.m_kassa.columns = data.column_default_obj.filter(obj => { return obj.status === true }).map(({ name }) => name);
        },
    },
    getters: {
        allKassa(state) {
            return state.m_kassa;
        },
        user_kassa_list(state){
            return state.kassa_user_list;
        },
        user_kassa_info(state){
            return state.kassa_info;
        },
        
    }
}