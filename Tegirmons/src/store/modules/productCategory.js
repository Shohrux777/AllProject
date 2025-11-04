export default {
    state: {
        category: {
            rows: [],
            columns: ['name'],
            col: []
        },

    },
    actions: {
        async fetchcategory(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/TegirmonCategoryControler');
            const res_data = await res.json();
            // console.log(ctx.rootState.hostname);
            ctx.commit('updatecategory', res_data);
        },
        // async fetchcategory_column(ctx, id) {
        //     const res = await fetch(ctx.rootState.hostname + '/TexColumnConfigs/getColumnListAsJsonObj?tableName=pos_category&auth_id=' + id);
        //     const res_data = await res.json();
        //     ctx.commit('updatecategory_column', res_data);
        // },
    },
    mutations: {
        updatecategory(state, data) {
            console.log(data)
            state.category.rows = data;
        },
        category_delete_row(state, index) {
            state.category.rows.splice(parseInt(index), 1);
        },
        

    },
    getters: {
        allcategory(state) {
            return state.category;
        }

    }
}