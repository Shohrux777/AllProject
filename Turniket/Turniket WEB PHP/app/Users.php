<?php

namespace App;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Users extends Model
{
    protected $table = "workers";
    
    protected $fillable = [
       'id','status','fio','image','note','card_number','employeeID','otdel_name','otdel_id', 'number'
    ];

    public function employee(){
        return $this->belongsTo("App\Employee", "employeeID");
    }
}
