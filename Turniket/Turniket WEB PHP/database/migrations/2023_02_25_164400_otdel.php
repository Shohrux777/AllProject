<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('otdel', function (Blueprint $table) {
            $table->id();
          //  $table->longText("content");
            $table->bigInteger("company_id")->unsigned();
            $table->foreign("company_id")->references("id")->on("company")->onUpdate("cascade")->onDelete("cascade");
            $table->boolean('true');
            $table->integer('number');
            $table->string('string');
            $table->timestamps();
        });
    }
    

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('otdel');
    }
};
