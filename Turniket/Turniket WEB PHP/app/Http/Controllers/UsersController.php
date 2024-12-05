<?php

namespace App\Http\Controllers;

use App\Otdel;
use App\Users;
use Illuminate\Http\Request;

class UsersController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        $data = Users::get();
        // dd($data);
          return view('users.index',["data" => $data]);
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function create()
    {
        $otdel = Otdel::get();
        return view('users.create',compact('otdel'));
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
        if ($request->hasFile('image') ) {
           // dd(1);
            $image = $request->file('image');
            //$name = time().'.'.$image->getClientOriginalExtension();
            $base64_image = base64_encode(file_get_contents($image));
          //  $base64_image = base64_encode($image);

            $image->move(public_path('images'), $base64_image);
            $input['image'] =$base64_image;
        }
        // $image = $request->rasim;
        // $base64_image = base64_encode($image);
        // dd($base64_image);
        // Users::insert([
        //     'rasim' => $base64_image
        // ]);
       
         Users::create($input);
      
          return redirect()->route('users.index');
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
        $data = Users::where('id',$id)->first();
        $otdel = Otdel::get();
        return view('users.edit',compact('data','otdel'));
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
        Users::where('id',$id)->update([
            'fio' => $request->fio,
            'card_number' => $request->card_number,
            'employeeID' => $request->employeeID,
            'note' => $request->note,
            'otdel_name' => $request->otdel_name,
           

        ]);

        return redirect()->route('users.index');
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  \App\Models\Otdel  $otdel
     * @return \Illuminate\Http\Response
     */
    public function destroy($id)
    {
        Users::where('id',$id)->delete();
        return redirect()->route('users.index');
    }
}
