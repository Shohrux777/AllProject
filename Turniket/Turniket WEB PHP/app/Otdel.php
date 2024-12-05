<?php

namespace App;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Otdel extends Model
{
    protected $table = "otdel";

   
    protected $fillable = [
       'id','name','company_id','company_name','string','true','number'
    ];
    // public function company(){
    //     return $this->belongsTo("App\Company", "company_id");
    // }

    public function data(){
        return $this->hasMany("App\Users");
    }
}
