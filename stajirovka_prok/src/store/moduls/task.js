import axios from 'axios';
export default {
    
    state: {
        task_list: {
            rows: [],
            columns: ['name','count', 'note',],
            col: []
        },
    },
    actions: {
        async fetch_task(ctx) {
            try{
                const token = localStorage.getItem('auth_token');

                console.log('Token:', token); // ← bu chiqsin, `Bearer`siz bo'lishi kerak

                const response = await axios.get(
                ctx.rootState.hostname + '/api/tasks',
                {
                    headers: {
                    Authorization: `Bearer ${token}`,
                    Accept: 'application/json',
                    'ngrok-skip-browser-warning': '69420'
                    }
                }
                );
                console.log('Natija:', response.data);
                ctx.commit('Updatetask_list', response.data);
            }
            catch (error) {
            console.error('Xatolik:', error.response?.data || error.message);
            alert("Xatolik yuz berdi!");
            }
            
            // console.log(ctx.rootState.hostname);
        },
    },
    mutations: {
        Updatetask_list(state, data) {
            console.log(data)
            state.task_list.rows = data;
        },
        
        task_row_delete(state, index) {
            state.task_list.rows.splice(parseInt(index), 1);
        }

    },
    getters: {
        get_task_list(state) {
            return state.task_list;
        },
    }
}
