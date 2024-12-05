<?php

namespace App;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Company extends Model
{
    protected $table = "company";

   
    protected $fillable = [
       'id','name','ture','string','number'
    ];

    public function data(){
        return $this->hasMany("App\Otdel");
    }
}
