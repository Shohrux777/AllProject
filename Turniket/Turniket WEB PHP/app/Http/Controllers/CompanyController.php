<?php

namespace App\Http\Controllers;

use App\Company;
//use App\Company as ModelsCompany;
use Illuminate\Http\Request;


class CompanyController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        $data = Company::get();
      //  dd($data);
        return view('company.index',["data" => $data]);
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function create()
    {
        return view('company.create');
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @return \Illuminate\Http\Response
     */
    public function store(Request $request)
    {
     
        $input = $request->all();
        Company::create($input);
      
       return redirect()->route('company.index');
       
    }

    /**
     * Display the specified resource.
     *
     * @param  \App\Models\Company  $company
     * @return \Illuminate\Http\Response
     */
    public function show(Company $company)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  \App\Models\Company  $company
     * @return \Illuminate\Http\Response
     */
    public function edit($id)
    {
        $data = Company::where('id',$id)->first();
       // dd($data);
        return view('company.edit',compact('data'));
    }

   
    public function update(Request $request,$id)
    {
      // dd(1);
        Company::where('id',$id)->update([
            'name' => $request->name,
            'number' => $request->number,
        ]);

        return redirect()->route('company.index');
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  \App\Models\Company  $company
     * @return \Illuminate\Http\Response
     */
    // public function delete($id)
    // {
    //     dd(1);
    // }
    public function destroy($id)
    {
        Company::where('id',$id)->delete();
        return redirect()->route('company.index');
    }
}
