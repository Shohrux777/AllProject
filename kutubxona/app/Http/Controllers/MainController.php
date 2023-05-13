<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Models\Book;
use App\Models\News;
use App\Models\Category;
use App\Models\Buyurtma;
use App\Http\Requests\MainRequest;

class MainController extends Controller
{
    public function index()
    {
        $news = News::latest()->paginate(8);
        $books = Book::latest()->paginate(8);
        $categories = Category::all();
        // dd($books);
         return view('index', compact('news', 'books', 'categories'));
    }
    public function category_index($id){
        $categories = Category::all();
        $books = Book::all();
        $books_category = Book::where('category_id',$id)->get();
        // dd($books_category);
        return view('categoryShow', compact('categories', 'books', 'books_category'));

    }
    public function book_index($id){
        $categories = Category::all();
        $book_id = Book::find($id);
        return view('bookShow', compact('categories', 'book_id'));

    }
    public function new_index($id){
        $new_id = News::find($id);
        return view('newsShow', compact( 'new_id'));

    }
    public function receive()
    {
        $buyurtmas = Buyurtma::all();
         return view('bookReceive', compact('buyurtmas'));
    }
    public function store(MainRequest $request)
    {
        // dd($request);
        Buyurtma::create([
            'name' => $request -> name,
            'tel'=> $request -> tel,
            'b_name'=> $request -> b_name,
            'creator'=> $request -> creator,
            'year'=> $request -> year,
        ]);
        $request->session()->flash('alert-success', 'Surovingiz qabul qilindi');
        return to_route('main');
    }
}
