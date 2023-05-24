<?php

use Illuminate\Support\Facades\Route;
use App\Http\Controllers\CategoryBook;
use App\Http\Controllers\BookController;
use App\Http\Controllers\NewsController;
use App\Http\Controllers\MainController;
use App\Http\Controllers\regisUser;
use App\Http\Controllers\NotificationCont;


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
Route::get('/noteId/{id}', [MainController::class, 'note_index'])->name('noteId');
Route::get('/hestory', [MainController::class, 'index'])->name('hestory');
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
Route::post('/downloadBook', [BookController::class, 'download'])->name('downloadBook');



Route::get('/new', [NewsController::class, 'index'])->name('news');
Route::post('/newstore', [NewsController::class, 'store'])->name('newstore');
Route::get('/newsEdit/{id}', [NewsController::class, 'edit'])->name('newsEdit');
Route::put('/newsUpdate', [NewsController::class, 'update'])->name('news_update');
Route::delete('/newsdestroy', [NewsController::class, 'destroy'])->name('news_destroy');

Route::get('/notifications', [NotificationCont::class, 'index'])->name('notifications');
Route::post('/notestore', [NotificationCont::class, 'store'])->name('notestore');
Route::get('/notesEdit/{id}', [NotificationCont::class, 'edit'])->name('notesEdit');
Route::put('/notesUpdate', [NotificationCont::class, 'update'])->name('notes_update');
Route::delete('/notesdestroy', [NotificationCont::class, 'destroy'])->name('notes_destroy');




Route::get('/registrationList', [regisUser::class, 'index'])->name('registrationList');
Route::post('/registrationStore', [regisUser::class, 'store'])->name('registrationStore');
Route::delete('/regisdestroy', [regisUser::class, 'destroy'])->name('regis_destroy');
Route::get('/registration', [regisUser::class, 'view'])->name('registration');

Route::get('/login', function () {
    return view('login');
});
Route::post('/loginTest', [regisUser::class, 'login'])->name('loginTest');


Route::get('/home', function () {
    return view('index');
});
Route::get('/dashboard', function () {
    return view('dashboard');
});
Route::get('/hestory', function () {
    return view('hestory');
});
Route::get('/management', function () {
    return view('pages.management');
});
Route::get('/workplace', function () {
    return view('pages.workplace');
});
Route::get('/systemall', function () {
    return view('pages.systemall');
});
Route::get('/vazifa', function () {
    return view('pages.vazifa');
});
Route::get('/beograf', function () {
    return view('pages.biograf');
});
Route::get('/info1', function () {
    return view('pages.info1');
});
Route::get('/info2', function () {
    return view('pages.info2');
});
Route::get('/fond1', function () {
    return view('pages.fond1');
});
Route::get('/komuni', function () {
    return view('pages.komuni');
});
Route::get('/book-buy', function () {
    return view('pages.book_form');
});
Route::get('/all-books', function () {
    return view('pages.all_book1');
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


