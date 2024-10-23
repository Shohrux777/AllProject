<template>
<div class=" box_connect_user">
    <div class="grafik_list border">
        <div class="item_user" :class="{'activ_item_choosen': i==activ_smena}"
            v-for="(item, i) in this.get_smena_list.rows" :key="i" 
            @click="choose_smena(item, i)">
            <label style="font-weight:500; font-size:13.5px" class="m-0">
                {{item.smena_nomi}}
            </label>
        </div>
    </div>
    <div class="selected_user">
        
    </div>
    <div class="user_list border">
        <div class="all_choose border-bottom d-flex align-items-center px-1">
            <input type="checkbox" style="width: 16px; height: 16px; margin-top: 3px; margin-right: 3px;" 
                :value="all_check" 
                id="all_select"
                @click="check_all($event)" 
            />
            <label for="all_select" class="m-0 p-0 mt-1" style="font-weight:500; font-size:13.5px">Все выбрать</label>
        </div>
        <div class="item_user d-flex" v-for="(item, i) in this.get_user_list.rows" :key="i">
            <input type="checkbox" style="width: 17px; height: 17px; margin-top: 3px; margin-right: 3px;" 
                :value="item.userid" 
                v-model="checkedUser[item.userid]" 
                :id="item.ism"
                @click="check_service($event, item)" 
            />
            <label style="font-weight:500; font-size:13.5px"
                :for="item.ism" class="m-0">{{item.ism}}
            </label>
        </div>
    </div>
</div>
</template>

<script>
    import {mapActions, mapGetters} from 'vuex'

export default {
    data(){
        return{
            checkedUser: [

            ],
            user_id_list: [],
            all_check: false,
            activ_smena: -1,
            smena_name: '',
            smena_id: null,
        }
    },
    computed: mapGetters(['get_user_list','get_smena_list']),
    async mounted(){
        await this.fetch_user();
        await this.fetch_Smena();
        console.log(this.get_smena_list.rows)
    },
    methods:{
          ...mapActions(['fetch_user', 'fetch_Smena']),
          choose_smena(item, i){
            this.activ_smena = i;
            console.log(item);
            this.smena_name = item.smena_nomi;
            this.smena_od = item.id;
          },
          check_service: function(e, item){
            if (e.target.checked){
                console.log('true', item)
                console.log(this.checkedUser)
                this.user_id_list.push(item.userid);
            }
            else{
                console.log('false')
                console.log(this.user_id_list)
                for(let i=0; i<this.user_id_list.length; i++){
                    if(this.user_id_list[i] == item.userid){
                        this.user_id_list.splice(i,1)
                    }
                }
                console.log('this.user_id_list')
                console.log(this.user_id_list)

            }
          },
          check_all(e){
            if (e.target.checked){
                for(let i=1; i<=500; i++){
                    this.checkedUser.push(true)
                }
                for(let j=0; j<this.get_user_list.rows.length; j++){
                    this.user_id_list.push(this.get_user_list.rows[j].userid)
                }
                console.log(this.user_id_list)
                console.log('ture')
            }
            else{
                console.log('false')
                this.checkedUser = [];
                this.user_id_list = [];
            }
          }
      }
}
</script>

<style>
.box_connect_user{
    display: flex;
    width: 100%;
    height: 94vh;

}
.grafik_list{
    width: 25%;
    height: 94vh;
}
.user_list{
    width: 25%;
    height: 94vh;
    overflow-y: scroll;
}
.selected_user{
    width: 50%;
}
.user_list .item_user{
    padding: 3px 5px;
    
}
.user_list .item_user:hover{
    background: #cccccc;
}

.grafik_list .item_user{
    padding: 3px 5px;
}
.grafik_list .item_user:hover{
    background: #cccccc;
}

.activ_item_choosen{
    background: #8d8d8d;
    color:white;
}
</style>