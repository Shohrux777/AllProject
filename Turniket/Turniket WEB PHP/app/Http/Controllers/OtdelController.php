<?php

namespace App\Http\Controllers;

use App\Company;
use App\Otdel;
use COM;
use Illuminate\Http\Request;

class OtdelController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        $data = Otdel::get();
        //  dd($data);
          return view('otdel.index',["data" => $data]);
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function create()
    {
        $company =  Company::get();
        return view('otdel.create',compact('company'));
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
        Otdel::create($input);
      
       return redirect()->route('otdel.index');
    }

    /**
     * Display the specified resource.
     *
     * @param  \App\Models\Otdel  $otdel
     * @return \Illuminate\Http\Response
     */
    public function show()
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  \App\Models\Otdel  $otdel
     * @return \Illuminate\Http\Response
     */
    public function edit($id)
    {
        $data = Otdel::where('id',$id)->first();
        $company = Company::get();
        return view('otdel.edit',compact('data','company'));
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @param  \App\Models\Otdel  $otdel
     * @return \Illuminate\Http\Response
     */
    public function update(Request $request,$id)
    {
        Otdel::where('id',$id)->update([
            'name' => $request->name,
            'number' => $request->number,
            'string' => $request->string,
            'company_id' => $request->company_id
         ]);

        return redirect()->route('otdel.index');
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  \App\Models\Otdel  $otdel
     * @return \Illuminate\Http\Response
     */
    public function destroy($id)
    {
        Otdel::where('id',$id)->delete();
        return redirect()->route('otdel.index');
    }
}
