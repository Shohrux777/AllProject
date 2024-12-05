<?php

namespace App\Http\Controllers;

use App\Appeal;
use App\BotManager\TelegramBotManager;
use App\Funcs;
use App\Media;
use App\Option;
use App\TelegramBotUser;
use App\User;
use GuzzleHttp\Client;
use GuzzleHttp\RequestOptions;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Session;

class PageController extends Controller
{
    public function getDashboardPage(){
      //  dd(1);
        // $data["users_number"] = TelegramBotUser::where("phone_number", "!=", null)->count();
        // $data["total_appeals"] = Appeal::where("status", "!=", "open")->count();
        // $data["waiting_appeals"] = Appeal::where("status", "pending")->count();
        // $data["closed_appeals"] = Appeal::where("status", "closed")->count();

        // $appeals = Appeal::where("status", "pending")->get();

      //  return view("private.dashboard", ["data" => $data, "appeals" => $appeals]);
        return view("private.dashboard");
    }

    public function answerAppeal($id){

        $appeal = Appeal::where("id", $id)->first();

        if($appeal === null){
            return redirect()->back()->with(["message"=>"Murojaat topilmadi", "m_status" => 0]);
        }

        if($appeal->status == "closed"){
            return redirect()->route("a.appeal.view", ["id" => $appeal->id]);
        }

        if($appeal->has_media == 1){
            $media = Media::where("appeal_id", $appeal->id)->get();
        }
        else
        {
            $media = null;
        }

        return view("private.appeal_answer", ["appeal" => $appeal, "media" => $media]);

    }

    public function postAnswerAppeal(Request $request){
        $this->validate($request, [
            "appeal_id" => "required",
            "answer" => "required",
        ]);

        try {
            $appeal = Appeal::where("id", $request->appeal_id)->first();

            if ($appeal === null) {
                return redirect()->back()->with(["message" => "Savol topilmadi", "m_status" => 0]);
            }

            $appeal->answer = $request->answer;
            $appeal->status = "closed";
            $appeal->update();

            $funcs = new Funcs();

            $url = "https://api.telegram.org/bot" . $funcs->getAPI() . "/sendMessage";

            $client = new Client();
            
           $text = "<b>#" . $appeal->id . " raqamli savolingizga javob berildi:</b>\n\n";
            $req_answer  = $request->answer;
           /*
            
            $str = substr($req_answer, 0 , 4000);
            $req_answer = substr($req_answer, 4000, $l - strlen($str));
             $data["text"] = $l . " " . ceil($l / 4000) . $req_answer;
               $data["chat_id"] = $appeal->user->chat_id;
                $data["parse_mode"] = "HTML";
                $response = $client->get($url, [RequestOptions::JSON => $data]);
                exit();*/
                $l = strlen($req_answer);
                // try{
            for($i = 0; $i < ceil($l / 4000); $i++){
            
                $n = strlen($req_answer);
                if($n >= 4000){
                    $str = substr($req_answer, 0 , 4000);
                    $req_answer = substr($req_answer, 4000 * ($i + 1), $n - strlen($str));
                }else{
                    $str = $req_answer;
                }
               
                if($i == 0){
                    $data["text"] = $text . $str;
                }else{
                    $data["text"] = $str;
                }
                
                $data["chat_id"] = $appeal->user->chat_id;
                $data["parse_mode"] = "HTML";
                $response = $client->get($url, [RequestOptions::JSON => $data]);
                
            }
                // }catch(Exception $e){
                //     $data["text"] = $e->getMessage()."Azim";
                // $data["chat_id"] = $appeal->user->chat_id;
                // $data["parse_mode"] = "HTML";
                // $response = $client->get($url, [RequestOptions::JSON => $data]);
                
                // }
           

            if ($response->getStatusCode() == 200) {
                return redirect()->back()->with(["message" => "Savol yuborildi", "m_status" => 1]);
            } else {
                return redirect()->back()->with(["message" => "Xatolik sodir bo'ldi", "m_status" => 1]);
            }
        }catch (\Exception $exception){
            echo $exception->getMessage()."<br/>";
            echo $exception->getTraceAsString();
        }
    }

    public function viewAppeal($id){
        $appeal = Appeal::where("id", $id)->first();

        if($appeal === null){
            return redirect()->back()->with(["message"=>"Murojaat topilmadi", "m_status" => 0]);
        }

        if($appeal->has_media == 1){
            $media = Media::where("appeal_id", $appeal->id)->get();
        }
        else
        {
            $media = null;
        }

        return view("private.appeal_view", ["appeal" => $appeal, "media" => $media]);

    }

    public function getAppealsPage(){

        $appeals = Appeal::where("status", "!=", "open")->get();

        return view("private.appeals", ["appeals" => $appeals]);
    }

    public function getOptionsPage(){

        if(!Option::where("option_key", "service_fee")->exists()){
            $option = new Option();
            $option->option_key = "service_fee";
            $option->option_value = 10000;
            $option->save();
        }

        $option = Option::where("option_key", "service_fee")->first();

        return view("private.options", ["option" => $option]);
    }

    public function updateServiceFee(Request $request){

        $this->validate($request, [
           "service_fee" => "required",
        ]);

        $option = Option::where("option_key", "service_fee")->first();
        if($option === null){
            return redirect()->back()->with(["message" => "Ma'lumot topilmadi", "m_status" => 0]);
        }
        $option->option_value = $request->service_fee;
        $option->update();

        return redirect()->back()->with(["message" => "Ma'lumot saqlandi", "m_status" => 1]);

    }

    public function postLogin(Request $request){
       //dd(1);
        $this->validate($request, [
           "username" => "required",
           "password" => "required"
        ]);

        $user = User::where("username", $request->username)->first();
        //  dump(md5($request->password));
        //  dd($user->password);
        if($user === null){
            return redirect()->back()->with(["message" => "Имя пользователя не найдено", "m_status" => 0]);
        }

        if($user->password != md5($request->password))
          // dd($user->password);
            return redirect()->back()->with(["message" => "Неверный пароль", "m_status" => 0]);

        Auth::loginUsingId($user->id);

        session([
            'username' => $user->username,
            'full_name' => $user->full_name,
            'role' => $user->role
        ]);

        return redirect()->route("a.dashboard");
    }

    public function logOut()
    {
        Auth::logout();
        session()->forget('familyName');
        session()->forget('givenName');
        session()->forget('email');
        session()->forget('status');
        Session::flush();
        return redirect()->route('home');
    }

    public function resetPass(Request $request){
        $this->validate($request, [
           "cpass" => "required",
           "npass" => "required|min:6",
           "rpass" => "required|min:6",
        ]);

        $user = User::where("id", Auth::id())->first();

        if($user === null){
            return redirect()->route('a.logout');
        }

        if($user->password !== md5($request->cpass)){
            return redirect()->back()->with(["message" => "Joriy parol xato kiritildi", "m_status" => 0]);
        }

        if($request->npass !== $request->rpass){
            return redirect()->back()->with(["message" => "Yangi parol takrorlanishi xato kiritildi", "m_status" => 0]);
        }

        $user->password = md5($request->npass);
        $user->update();

        return redirect()->back()->with(["message" => "Parol yangilandi", "m_status" => 1]);

    }
}
