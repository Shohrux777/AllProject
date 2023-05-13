<?php

namespace App\Http\Controllers;

use App\Http\Requests\RegisRequest;
use App\Http\Requests\LoginRequest;
use App\Models\regis;
use Illuminate\Http\Request;


class regisUser extends Controller
{
    public function index()
    {
        $users = regis::all();
        //  dd($users);
        return view('registList', [
            'user' => $users
        ]);
    }

    public function view()
    {
        return view('registratura');
    }

    public function store(RegisRequest $request)
    {
        // dd($request);
        if($request -> password != $request -> returnPassword){
            $request->session()->flash('alert-error', 'Parol notug`ri kiritildi');
        }
        regis::create([
            'gmail' => $request -> gmail,
            'password' => $request -> password,
            'returnPassword' => $request -> returnPassword,
            'fio' => $request -> fio,
            'phoneNumber' => $request -> phoneNumber,
            'faks'=> $request -> faks,
            'note'=> $request -> note
        ]);
        $request->session()->flash('alert-success', 'Ro`yxatdan utkazildi');
        return to_route('registration');
    }
    public function login(LoginRequest $request)
    {
        // dd($request);
        if($request -> password != $request -> login){
            $request->session()->flash('alert-error', 'Login yoki Parol xato');
        }
        return to_route('main');

        // else{
        //     dd($request);
        // }

        // regis::create([
        //     'gmail' => $request -> gmail,
        //     'password' => $request -> password,
        //     'returnPassword' => $request -> returnPassword,
        //     'fio' => $request -> fio,
        //     'phoneNumber' => $request -> phoneNumber,
        //     'faks'=> $request -> faks,
        //     'note'=> $request -> note
        // ]);
        // $request->session()->flash('alert-success', 'Ro`yxatdan utkazildi');
        // return to_route('main');
    }
}
