<?php

namespace App\Http\Controllers;

use App\Http\Requests\CategoryRequest;
use App\Models\Category;
use Illuminate\Http\Request;

class CategoryBook extends Controller
{
    public function index()
    {
        $categories = Category::all();
        return view('bookCategory', [
            'categories' => $categories
        ]);
    }
    public function edit($id)
    {
        $categories = Category::find($id);
        if(! $categories){
            request()->session()->flash('error', 'Bu id li kategoriya topilmadi');
            return to_route('bookCategory')->withErrors([
                'error' => 'Bu id li kategoriya topilmadi'
            ]);
        }
        return view('bookCategoryEdit', ['category'=> $categories]);

    }
    public function store(CategoryRequest $request)
    {
        // dd($request);
        Category::create([
            'name' => $request -> name,
            'title'=> '',
            'note'=> ''
        ]);
        $request->session()->flash('alert-success', 'Kategoriya qushildi');
        return to_route('bookCategory');
    }
    public function update(CategoryRequest $request)
    {
        $category = Category::find($request->category_id);
        if(! $category){
            request()->session()->flash('error', 'Serverda uzilish bor');
            return to_route('bookCategory')->withErrors([
                'error' => 'Serverda uzilish bor'
            ]);
        }
        $category->update([
            'name' => $request -> name,
            'title'=> '',
            'note'=> ''
        ]);
        $request->session()->flash('alert-info', 'Kategoriya tahrirlandi');
        return to_route('bookCategory');
    }
    public function destroy(Request $request){
        $category = Category::find($request->category_id);
        if(! $category){
            request()->session()->flash('server_error', 'Serverda uzilish bor');
            return to_route('bookCategory')->withErrors([
                'server_error' => 'Serverda uzilish bor'
            ]);
        }
        // dd($category);
        $category->delete();
        $request->session()->flash('alert-success', 'Kategoriya muvaffaqiyatli uchirildi');
        return to_route('bookCategory');
    }
}
