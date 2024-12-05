<?php

namespace App;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Employee extends Model
{
    protected $table = "test";
    
    protected $fillable = [
       'id','fio','image','note','card_number','employeeID','otdel_name','otdel_id', 'number'
    ];

    // public function otdel(){
    //     return $this->belongsTo("App\Users", "employeeID");
    // }

    public function data(){
        return $this->hasMany("App\Users");
    }
}
