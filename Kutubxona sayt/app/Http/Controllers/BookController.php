<?php

namespace App\Http\Controllers;

use App\Http\Requests\BookRequest;
use Illuminate\Http\Request;
use App\Models\Book;
use App\Models\Category;


class BookController extends Controller
{
    public function index()
    {
        $categories = Category::all();
        $books = Book::all();
    //   dd(1);
       // dd($categories);
         return view('books', compact('categories', 'books'));
    }
    public function store(BookRequest $request)
    {
        // $request->validated();
        $category = Category::find($request->category_id);
        $category_name = $category->name;

        // if($request->hasFile('image'))
        $picture_path = 'public/images/picture';
        $picture_file = $request->file('image');
        $picture_name = $picture_file->getClientOriginalName();
        $request->file('image')->move($picture_path, $picture_name);

        $pdf_path = 'public/pdf';
        $pdf_file = $request->file('pdf');
        $pdf_name = $pdf_file->getClientOriginalName();
        $request->file('pdf')->move($pdf_path, $pdf_name);


        // dd($pdf_name);
        Book::create([
            'name' => $request -> name,
            'writer'=> $request -> writer,
            'lang'=> $request -> lang,
            'publisher'=> $request -> publisher,
            'letter'=> '',
            'see'=> $pdf_name,
            'title'=> $request -> title,
            'image'=> $picture_name,
            'pdf'=> $pdf_name,
            'note'=> $request -> note,
            'category_id'=> $request -> category_id,
            'category_name'=> $category_name,
            'pdfFile'=> $pdf_name,
        ]);
        $request->session()->flash('alert-success', 'Kitob qushildi');
        return to_route('books');
    }
    public function edit($id)
    {
        $books = Book::find($id);
        $categories = Category::all();
        if(! $books){
            request()->session()->flash('error', 'Bu id li kitob topilmadi');
            return to_route('books')->withErrors([
                'error' => 'Bu id li kitob topilmadi'
            ]);
        }

        // dd($books);

        return view('bookEdit', ['book'=> $books, 'categories' => $categories]);
    }
    public function update(BookRequest $request)
    {
        $book = Book::find($request->book_id);
        if(! $book){
            request()->session()->flash('error', 'Serverda uzilish bor');
            return to_route('books')->withErrors([
                'error' => 'Serverda uzilish bor'
            ]);
        }

        $category = Category::find($request->category_id);
        $category_name = $category->name;

        // if($request->hasFile('image'))
        $picture_path = 'public/images/picture';
        $picture_file = $request->file('image');
        $picture_name = $picture_file->getClientOriginalName();
        $request->file('image')->move($picture_path, $picture_name);

        $pdf_path = 'public/pdf';
        $pdf_file = $request->file('pdf');
        $pdf_name = $pdf_file->getClientOriginalName();
        $request->file('pdf')->move($pdf_path, $pdf_name);

        $book->update([
            'name' => $request -> name,
            'writer'=> $request -> writer,
            'lang'=> $request -> lang,
            'publisher'=> $request -> publisher,
            'letter'=> '',
            'see'=> '',
            'title'=> $request -> title,
            'image'=> $picture_name,
            'pdf'=> $pdf_name,
            'note'=> $request -> note,
            'category_id'=> $request -> category_id,
            'category_name'=> $category_name,
            'pdfFile'=> '',
        ]);
        $request->session()->flash('alert-info', 'Kitob tahrirlandi');
        return to_route('books');
    }
    public function destroy(Request $request){
        $book = Book::find($request->book_id);
        if(! $book){
            request()->session()->flash('server_error', 'Serverda uzilish bor');
            return to_route('books')->withErrors([
                'server_error' => 'Serverda uzilish bor'
            ]);
        }
        // dd($category);
        $book->delete();
        $request->session()->flash('alert-success', 'Kitob muvaffaqiyatli uchirildi');
        return to_route('books');
    }
}
