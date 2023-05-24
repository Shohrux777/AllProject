<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Models\Notification;
use App\Http\Requests\NotesRequest;

class NotificationCont extends Controller
{
    //
    public function index()
    {
        $notifications = Notification::all();
    //   dd(1);
       // dd($categories);
         return view('notes', compact('notifications'));
    }
    public function store(NotesRequest $request)
    {
        // if($request->hasFile('image'))
        // dd($request);
        $picture_path = 'public/news/picture';
        $picture_file = $request->file('image');
        $picture_name = $picture_file->getClientOriginalName();
        $request->file('image')->move($picture_path, $picture_name);
        // dd($request);
        Notification::create([
            'menu' => $request -> mavzu,
            'datestr'=> $request -> sana,
            'timestr'=> $request -> vaqt,
            'text'=> $request -> text,
            'link'=> $request -> link,
            'image'=> $picture_name,
        ]);
        $request->session()->flash('alert-success', 'Elon qushildi');
        return to_route('notifications');
    }
    public function edit($id)
    {
        $note = Notification::find($id);
        if(! $note){
            request()->session()->flash('error', 'Bu id li elon topilmadi');
            return to_route('notifications')->withErrors([
                'error' => 'Bu id li elon topilmadi'
            ]);
        }

        // dd($books);

        return view('notesEdit', ['note'=> $note]);
    }
    public function update(NotesRequest $request)
    {
        $note = Notification::find($request->note_id);
        if(! $note){
            request()->session()->flash('error', 'Serverda uzilish bor');
            return to_route('notifications')->withErrors([
                'error' => 'Serverda uzilish bor'
            ]);
        }

        // if($request->hasFile('image'))
        $picture_path = 'public/news/picture';
        $picture_file = $request->file('image');
        $picture_name = $picture_file->getClientOriginalName();
        $request->file('image')->move($picture_path, $picture_name);

        $note->update([
            'menu' => $request -> mavzu,
            'datestr'=> $request -> sana,
            'timestr'=> $request -> vaqt,
            'text'=> $request -> text,
            'link'=> $request -> link,
            'image'=> $picture_name,
        ]);
        $request->session()->flash('alert-info', 'Elon tahrirlandi');
        return to_route('notifications');
    }
    public function destroy(Request $request){
        $note = Notification::find($request->note_id);
        if(! $note){
            request()->session()->flash('server_error', 'Serverda uzilish bor');
            return to_route('notifications')->withErrors([
                'server_error' => 'Serverda uzilish bor'
            ]);
        }
        // dd($category);
        $note->delete();
        $request->session()->flash('alert-success', 'Elon muvaffaqiyatli uchirildi');
        return to_route('notifications');
    }
}
