<?php

namespace App\Http\Controllers;

use App\Http\Requests\NewsRequest;
use Illuminate\Http\Request;
use App\Models\News;

class NewsController extends Controller
{
    public function index()
    {
        $news = News::all();
    //   dd(1);
       // dd($categories);
         return view('news', compact('news'));
    }
    public function store(NewsRequest $request)
    {
        // if($request->hasFile('image'))
        $picture_path = 'public/news/picture';
        $picture_file = $request->file('image');
        $picture_name = $picture_file->getClientOriginalName();
        $request->file('image')->move($picture_path, $picture_name);
        // dd($request);
        News::create([
            'name' => $request -> name,
            'date'=> $request -> date,
            'link'=> $request -> link,
            'text'=> $request -> text,
            'image'=> $picture_name,
        ]);
        $request->session()->flash('alert-success', 'Yangilik qushildi');
        return to_route('news');
    }
    public function edit($id)
    {
        $news = News::find($id);
        if(! $news){
            request()->session()->flash('error', 'Bu id li yangilik topilmadi');
            return to_route('news')->withErrors([
                'error' => 'Bu id li yangilik topilmadi'
            ]);
        }

        // dd($books);

        return view('newsEdit', ['new'=> $news]);
    }
    public function update(NewsRequest $request)
    {
        $new = News::find($request->new_id);
        if(! $new){
            request()->session()->flash('error', 'Serverda uzilish bor');
            return to_route('news')->withErrors([
                'error' => 'Serverda uzilish bor'
            ]);
        }

        // if($request->hasFile('image'))
        $picture_path = 'public/news/picture';
        $picture_file = $request->file('image');
        $picture_name = $picture_file->getClientOriginalName();
        $request->file('image')->move($picture_path, $picture_name);

        $new->update([
            'name' => $request -> name,
            'date'=> $request -> date,
            'link'=> $request -> link,
            'text'=> $request -> text,
            'image'=> $picture_name,
        ]);
        $request->session()->flash('alert-info', 'Yangilik tahrirlandi');
        return to_route('news');
    }
    public function destroy(Request $request){
        $new = News::find($request->new_id);
        if(! $new){
            request()->session()->flash('server_error', 'Serverda uzilish bor');
            return to_route('news')->withErrors([
                'server_error' => 'Serverda uzilish bor'
            ]);
        }
        // dd($category);
        $new->delete();
        $request->session()->flash('alert-success', 'Yangilik muvaffaqiyatli uchirildi');
        return to_route('news');
    }
}
