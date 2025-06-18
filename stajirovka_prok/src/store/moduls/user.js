import axios from 'axios';
export default {
    
    state: {
        user_list: {
            rows: [],
            columns: [ 'name','full_name', 'passport', 'phone', 'mobile_phone' ],
            col: []
        },
        nosalary_user: [],
        user_all_foiz: 0,
    },
    actions: {
        async fetch_user(ctx) {
            try{
                const token = localStorage.getItem('auth_token');

                console.log('Token:', token); // ← bu chiqsin, `Bearer`siz bo'lishi kerak

                const response = await axios.get(
                ctx.rootState.hostname + '/api/admin/users',
                {
                    headers: {
                    Authorization: `Bearer ${token}`,
                    Accept: 'application/json',
                    'ngrok-skip-browser-warning': '69420'
                    }
                }
                );
                console.log('Natija:', response.data);
                ctx.commit('Updateuser_list', response.data);
            }
            catch (error) {
            console.error('Xatolik:', error.response?.data || error.message);
            alert("Xatolik yuz berdi!");
            }
            
            console.log(ctx.rootState.hostname);
        },
    },
    mutations: {
        Updateuser_list(state, data) {
            console.log(data)
            state.user_all_foiz = 0;
            state.user_list.rows = data;
            for(let i=0; i<state.user_list.rows.length; i++){
                if(state.user_list.rows[i].foiz > 0){
                    state.user_all_foiz += state.user_list.rows[i].foiz;
                }
            }
            state.user_all_foiz = state.user_all_foiz / state.user_list.rows.length;
        },
        
        user_row_delete(state, index) {
            state.user_list.rows.splice(parseInt(index), 1);
        }

    },
    getters: {
        get_user_list(state) {
            return state.user_list;
        },
        get_user_all_foiz(state){
            return state.user_all_foiz;
        }
    }
}
