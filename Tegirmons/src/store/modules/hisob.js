export default {
    state: {
        m_hisob: {
            rows: [],
            columns: ['name', 'created_date_time'],
            col: []
        },
        hisob_user_list: [],
        hisob_info: {},
    },
    actions: {
        async fetchHisob(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/TegirmonHisoblar');
            const res_data = await res.json();
            // console.log(ctx.rootState.hostname);
            ctx.commit('updateHisob', res_data);
        },
        // async fetchHisob_userId(ctx, user_id) {
        //     try{
        //         const res = await fetch(ctx.rootState.hostname + '/TegirmonKassa/getPaginationUserId?page=0&size=100&user_id=' + user_id);
        //         const res_data = await res.json();
        //         // console.log(ctx.rootState.hostname);
        //         ctx.commit('updateHisob_userId', res_data);
        //     }
        //     catch{
        //         console.log(error);
        //         ctx.commit('updateHisob_userId', {items_list: []});
        //         // state.kassa_user_list = [];
        //     }
            
        // },
        async fetchHisob_info(ctx, hisob_id) {
            try{
                const res = await fetch(ctx.rootState.hostname + '/TegirmonHisoblarInfo/getKassaInfoLastHisobId?hisob_id=' + hisob_id);
                const res_data = await res.json();
                // console.log(ctx.rootState.hostname);
                ctx.commit('updateHisob_info', res_data);
            }
            catch{
                console.log(error);
                ctx.commit('updateHisob_info', {cash: 0, dollor: 0});
                // state.kassa_user_list = [];
            }
            
        },
    },
    mutations: {
        updateHisob(state, data) {
            state.m_hisob.rows = data;
        },
        updateHisob_userId(state, data) {
            state.hisob_user_list = data.items_list;
        },
        updateHisob_info(state, data) {
            state.hisob_info = data;
        },
        hisob_delete_row(state, index) {
            state.m_hisob.rows.splice(parseInt(index), 1);
        },
        updateHisob_column(state, data) {
            state.m_hisob.col = data;
            //status true bolgan ustun nomlarini olish uchun
            state.m_hisob.columns = data.column_default_obj.filter(obj => { return obj.status === true }).map(({ name }) => name);
        },
    },
    getters: {
        allHisob(state) {
            return state.m_hisob;
        },
        user_hisob_list(state){
            return state.hisob_user_list;
        },
        hisob_info(state){
            return state.hisob_info;
        },
        
    }
}