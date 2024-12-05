<?php

use Illuminate\Support\Facades\Route;
use App\http\Controllers\CompanyController;
use Illuminate\Support\Facades\Auth;
/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| contains the "web" middleware group. Now create something great!
|
*/

Route::get('/', function () {
  //  dd(1);
    return view("login");

     //return view('welcome');
})->name("home");

Route::get('/getDashboardPage', [
    "uses" => "PageController@getDashboardPage",
    "as" => "a.dashboard"
]);
Route::group(['middleware' => ['auth']], function(){

Route::get('/dashboard', [
    "uses" => "PageController@getDashboardPage",
    "as" => "a.dashboard"
]);
Route::resource('company', 'CompanyController');
Route::resource('otdel', 'OtdelController');
Route::resource('users', 'UsersController');
Route::post('usersstore', 'UsersController@store')->name('usersstore');
// -- edit -- //
Route::get('cupdate/{id}', 'CompanyController@update')->name('cupdate');
Route::get('oupdate/{id}', 'OtdelController@update')->name('oupdate');
Route::get('uupdate/{id}', 'UsersController@update')->name('uupdate');
// -- endedit -- //
Route::get('filter','PageController@filter')->name('filter');

Route::get('filter3','PageController@kelganlar')->name('filter3');
Route::get('kelganlar','PageController@kelganlar')->name('kelganlar');

Route::get('filter4','PageController@bolgan_kunlari')->name('filter4');
Route::get('bolgan_kunlari','PageController@bolgan_kunlari')->name('bolgan_kunlari');

//
Route::get('kelmaganlar','PageController@kelmaganlar')->name('kelmaganlar');
Route::get('filter2','PageController@filter2')->name('filter2');
// -- delete -- //
Route::get('companydelete/{id}', [
    "uses" => "CompanyController@destroy",
    "as" => "companydelete"
]);
Route::get('odelete/{id}','OtdelController@destroy')->name("odelete");

Route::get('userdelete/{id}', [
    "uses" => "UsersController@destroy",
    "as" => "usersdelete"
]);
/// end delete///
Route::get('/appeal/{id}/answer', [
    "uses" => "PageController@answerAppeal",
    "as" => "a.appeal.answer"
]);

Route::get('/appeal/{id}/view', [
    "uses" => "PageController@viewAppeal",
    "as" => "a.appeal.view"
]);

Route::get('/options', [
    "uses" => "PageController@getOptionsPage",
    "as" => "a.options.page"
]);

Route::post('/post/service/fee', [
    "uses" => "PageController@updateServiceFee",
    "as" => "a.service.fee"
]);

Route::get('/appeals', [
    "uses" => "PageController@getAppealsPage",
    "as" => "a.appeal.page"
]);

Route::post('/post/answer/appeal/', [
    "uses" => "PageController@postAnswerAppeal",
    "as" => "a.appeal.post.answer"
]);

    Route::post('/reset/password',  [
        "uses" => "PageController@resetPass",
        "as" => "a.reset.pass"
    ]);


});


Route::get('/login', function(){
    return view("login");
})->name("login");

Route::get('/logout',  [
    "uses" => "PageController@logout",
    "as" => "a.logout"
]);

Route::post('/post/login',  [
    "uses" => "PageController@postLogin",
    "as" => "a.login"
]);

Route::view('/welcome', 'welcome');



