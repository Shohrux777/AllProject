<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class regis extends Model
{
    use HasFactory;
    protected $fillable = ['gmail', 'password', 'returnPassword', 'fio', 'phoneNumber', 'faks', 'note'];
}
