<?php

namespace App;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Otchot extends Model
{
    protected $table = "otchot";
    
   
    protected $fillable = [
       'id','fio','work_time'
    ];

   
}
