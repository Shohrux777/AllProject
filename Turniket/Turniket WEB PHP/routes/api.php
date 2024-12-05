<?php

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| is assigned the "api" middleware group. Enjoy building your API!
|
*/

Route::middleware('auth:api')->get('/user', function (Request $request) {
    return $request->user();
});
Route::any('/employee', 'EmployeeController@store');
Route::any('/companyy', 'EmployeeController@add_company_store');
Route::any('/departmentt', 'EmployeeController@add_otdel_store');
Route::any('/workersss', 'EmployeeController@add_worker_store');
Route::any('/tozalash', 'EmployeeController@turnkate_otdel_and_company');


Route::get('/example', function () {
    return 'Hello, World!';
}); 
//Route::get('/employees/{employee}/worktime', 'EmployeeController@worktime');