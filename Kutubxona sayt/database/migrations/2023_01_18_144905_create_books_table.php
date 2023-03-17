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
        Schema::create('books', function (Blueprint $table) {
            $table->id();
            $table->string('name');
            $table->string('writer')->nullable();
            $table->string('lang')->nullable();
            $table->string('letter')->nullable();
            $table->string('publisher')->nullable();
            $table->string('see')->nullable();
            $table->string('title')->nullable();
            $table->text('note')->nullable();
            $table->string('image')->nullable();
            $table->string('pdf')->nullable();
            $table->bigInteger('category_id');
            $table->string('category_name')->nullable();
            $table->text('pdfFile')->nullable();
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
        Schema::dropIfExists('books');
    }
};
