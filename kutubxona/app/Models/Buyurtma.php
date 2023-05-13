<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Buyurtma extends Model
{
    use HasFactory;
    protected $fillable = ['name', 'tel', 'b_name', 'creator', 'year'];
}
