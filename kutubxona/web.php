<?php

use Illuminate\Support\Facades\Route;
use App\Http\Controllers\CategoryBook;
use App\Http\Controllers\BookController;
use App\Http\Controllers\NewsController;
use App\Http\Controllers\MainController;
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


Route::get('/', [MainController::class, 'index'])->name('main');
Route::post('/mainstore', [MainController::class, 'store'])->name('mainstore');
Route::get('/receive_book', [MainController::class, 'receive'])->name('receive_book');
Route::get('/categoryId/{id}', [MainController::class, 'category_index'])->name('categoryId');
Route::get('/bookId/{id}', [MainController::class, 'book_index'])->name('bookId');
Route::get('/newsId/{id}', [MainController::class, 'new_index'])->name('newsId');


// Route::get('/category_book', function () {
//     return view('bookCategory');
// });
Route::get('/bookCategory', [CategoryBook::class, 'index'])->name('bookCategory');
Route::post('/store', [CategoryBook::class, 'store'])->name('store');
Route::get('/bookCategoryEdit/{id}', [CategoryBook::class, 'edit'])->name('bookCategoryEdit');
Route::put('/CategoryUpdate', [CategoryBook::class, 'update'])->name('category_update');
Route::delete('/Categorydestroy', [CategoryBook::class, 'destroy'])->name('category_destroy');

Route::resource('test', TestController::class);

Route::get('/books', [BookController::class, 'index'])->name('books');
Route::post('/bookstore', [BookController::class, 'store'])->name('bookstore');
Route::get('/bookEdit/{id}', [BookController::class, 'edit'])->name('bookEdit');
Route::put('/bookUpdate', [BookController::class, 'update'])->name('book_update');
Route::delete('/bookdestroy', [BookController::class, 'destroy'])->name('book_destroy');


Route::get('/news', [NewsController::class, 'index'])->name('news');
Route::post('/newstore', [NewsController::class, 'store'])->name('newstore');
Route::get('/newsEdit/{id}', [NewsController::class, 'edit'])->name('newsEdit');
Route::put('/newsUpdate', [NewsController::class, 'update'])->name('news_update');
Route::delete('/newsdestroy', [NewsController::class, 'destroy'])->name('news_destroy');



Route::get('/home', function () {
    return view('index');
});
Route::get('/dashboard', function () {
    return view('dashboard');
});

// Route::get('/books', function () {
//     return view('books');
// });
// Route::get('/receive_book', function () {
//     return view('bookReceive');
// });
Route::get('/notes', function () {
    return view('elon');
});


