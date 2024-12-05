<?php

namespace App\Http\Controllers;

use App\User;
use App\Appeal;
use App\Funcs;
use App\Media;
use App\Option;
use App\Employee;
use App\TelegramBotUser;
use App\BotManager\TelegramBotManager;
use GuzzleHttp\Client;
use GuzzleHttp\RequestOptions;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Session;
use  Illuminate\Support\Facades\DB;
use Carbon\Carbon;


class PageController extends Controller
{
    public function getDashboardPage(Request $request)
    {
      //  dd(1);
        // $data["users_number"] = TelegramBotUser::where("phone_number", "!=", null)->count();
        // $data["total_appeals"] = Appeal::where("status", "!=", "open")->count();
        // $data["waiting_appeals"] = Appeal::where("status", "pending")->count();
        // $data["closed_appeals"] = Appeal::where("status", "closed")->count();
        // $appeals = Appeal::where("status", "pending")->get();
       //dd(Carbon::today());

        $today = Carbon::now()->toDateString();
        $xodimlar_soni = Employee::whereDate('created_at','>', $today)->count();
        $data = Employee::orderBy('authDateTime','desc')->paginate(40);

       // $data = Employee::get();
        return view("private.dashboard",compact('data','xodimlar_soni'));
        //return view("private.dashboard", ["data" => $data, "appeals" => $appeals]);
    }

    public function filter(Request $request){
        $from_date = $request->from_date;
        $to_date = $request->to_date;
        $worker = $request->worker;
        $workers = DB::table('workers')->select('fio')->get();
        if(!empty($request->from_date) && !empty($request->to_date))
        {
          
           if($request->worker)
           {
           
            $datas =  DB::table('otchot')->where('fio',$request->worker)
            ->whereBetween('otchot.updated_at', array($request->from_date, $request->to_date))
            ->orderBy('otchot.created_at', 'DESC')
            ->get();
            return view('private.filter',compact('datas','from_date','to_date','worker','workers'));
           } 
        
           $datas =  DB::table('otchot')->whereBetween('otchot.updated_at', array($request->from_date, $request->to_date))
            ->orderBy('otchot.created_at', 'DESC')
            ->get();
            return view('private.filter',compact('datas','from_date','to_date','workers','worker'));
        }else if($request->worker)
        {
         $datas =DB::table('otchot')->where('fio',$request->worker)
         ->where('created_at','>',Carbon::today())
         //->whereBetween('balance_histories.updated_at', array($request->from_date, $request->to_date))
         ->orderBy('otchot.created_at', 'DESC')
         ->get();
       //   dd($datas);
       return view('private.filter',compact('datas','from_date','to_date','worker','workers'));
        } 
        else{
        $datas = DB::table('otchot')->where('created_at','>',Carbon::today())
        ->orderBy('created_at', 'desc')->get(); 
        }

        return view('private.filter',compact('datas','from_date','to_date','worker','workers'));
    }


    public function kelmaganlar(Request $request){
        $from_date = $request->from_date;
        $to_date = $request->to_date;
     

        if(!empty($request->from_date))
        {
          
           $datas = DB::select("SELECT w.fio as fio ,w.employeeID as employeeID FROM workers w 
           WHERE  w.employeeID NOT  IN (SELECT t.employeeID 
           FROM test t WHERE CAST(t.authDateTime as DATE) = '{$request->from_date}')
           GROUP BY w.fio, w.employeeID");


            return view('private.filter',compact('datas','from_date','to_date'));
        }else {

            $datas = DB::select("SELECT w.fio as fio ,w.employeeID as employeeID FROM workers w 
            WHERE  w.employeeID NOT  IN (SELECT t.employeeID 
            FROM test t WHERE CAST(t.authDateTime as DATE) = CURDATE())
            GROUP BY w.fio, w.employeeID");
        }

        return view('private.filter',compact('datas','from_date','to_date'));
    }

    public function bolgan_kunlari(Request $request){
        $from_date = $request->from_date;
        $to_date = $request->to_date;
     

        if(!empty($request->from_date) && !empty($request->to_date) )
        {
            $datas = DB::select("SELECT w.id, 
            w.fio,
            (SELECT COUNT(t.id) FROM test t WHERE t.employeeID = w.id AND (CAST(t.authDateTime as DATE) = CURDATE())) as ext, (SELECT tt.authDateTime FROM test tt WHERE tt.employeeID = w.id AND tt.direction = 'K' AND (CAST(tt.authDateTime as DATE) = CURDATE()) ORDER BY tt.authDateTime ASC LIMIT 1) as kirish, 
            (SELECT tt.authDateTime FROM test tt WHERE tt.employeeID = w.id AND tt.direction = 'C' AND (CAST(tt.authDateTime as DATE) = CURDATE()) ORDER BY tt.authDateTime DESC LIMIT 1) as chiqish,
            COALESCE((SELECT COUNT(*) FROM (SELECT t.employeeID as idt FROM test t WHERE (CAST(t.authDateTime as DATE) 
            BETWEEN '{$request->from_date}' AND '{$request->to_date}' ) GROUP BY CAST(t.authDateTime as DATE), t.employeeID) k WHERE idt = w.id),0) as work_ext, 
            DATEDIFF('{$request->to_date}' ,'{$request->from_date}') AS all_days FROM workers w WHERE 1;");

            return view('private.filter4',compact('datas','from_date','to_date'));
        }else {

            $datas = DB::select("SELECT w.id, 
            w.fio,
             (SELECT COUNT(t.id) FROM test t WHERE t.employeeID = w.id AND (CAST(t.authDateTime as DATE) = CURDATE() )) as ext, (SELECT tt.authDateTime FROM test tt WHERE tt.employeeID = w.id AND tt.direction = 'K' AND (CAST(tt.authDateTime as DATE) = CURDATE()) ORDER BY tt.authDateTime ASC LIMIT 1) as kirish, 
             (SELECT tt.authDateTime FROM test tt WHERE tt.employeeID = w.id AND tt.direction = 'C' AND (CAST(tt.authDateTime as DATE) = CURDATE()) ORDER BY tt.authDateTime DESC LIMIT 1) as chiqish,
              COALESCE((SELECT COUNT(*) FROM (SELECT t.employeeID as idt FROM test t WHERE (CAST(t.authDateTime as DATE) 
              BETWEEN (DATE_SUB(CURDATE(), INTERVAL DAYOFMONTH(CURDATE())-1 DAY)) AND LAST_DAY(CURRENT_DATE())) GROUP BY CAST(t.authDateTime as DATE), t.employeeID) k WHERE idt = w.id),0) as work_ext, 
              DATEDIFF(CURDATE(),DATE_SUB(CURDATE(), INTERVAL DAYOFMONTH(CURDATE())-1 DAY)) AS all_days FROM workers w WHERE 1;");
        }

        return view('private.filter4',compact('datas','from_date','to_date'));
    }

    public function kelganlar(Request $request){
        $from_date = $request->from_date;
        $to_date = $request->to_date;
     

        if(!empty($request->from_date))
        {
            $datas = DB::select("SELECT w.fio as fio ,
            w.employeeID as employeeID,
            (SELECT tt.authDateTime FROM test tt WHERE tt.employeeID = w.employeeID AND tt.direction = 'K' AND (CAST(tt.authDateTime as DATE) = '{$request->from_date}') ORDER BY tt.authDateTime ASC LIMIT 1) as kirish,
            (SELECT tt.authDateTime FROM test tt WHERE tt.employeeID = w.employeeID AND tt.direction = 'C' AND (CAST(tt.authDateTime as DATE) = '{$request->from_date}') ORDER BY tt.authDateTime DESC LIMIT 1) as chiqish
            FROM workers w 
            WHERE  w.employeeID IN (SELECT t.employeeID 
            FROM test t WHERE CAST(t.authDateTime as DATE) = '{$request->from_date}')
            GROUP BY w.fio, w.employeeID"); 


            return view('private.filter3',compact('datas','from_date','to_date'));
        }else {

            $datas = DB::select("SELECT w.fio as fio ,
            w.employeeID as employeeID,
            (SELECT tt.authDateTime FROM test tt WHERE tt.employeeID = w.employeeID AND tt.direction = 'K' AND (CAST(tt.authDateTime as DATE) = CURDATE()) ORDER BY tt.authDateTime ASC LIMIT 1) as kirish,
            (SELECT tt.authDateTime FROM test tt WHERE tt.employeeID = w.employeeID AND tt.direction = 'C' AND (CAST(tt.authDateTime as DATE) = CURDATE()) ORDER BY tt.authDateTime DESC LIMIT 1) as chiqish
            FROM workers w 
            WHERE  w.employeeID IN (SELECT t.employeeID 
            FROM test t WHERE CAST(t.authDateTime as DATE) = CURDATE())
            GROUP BY w.fio, w.employeeID"); 
        }

        return view('private.filter3',compact('datas','from_date','to_date'));
    }

    public function filter2(Request $request){
        $from_date = $request->from_date;
        $to_date = $request->to_date;
      
        if(!empty($request->from_date) && !empty($request->to_date))
        {
          
           $datas =  Employee::whereBetween('test.updated_at', array($request->from_date, $request->to_date))
            ->orderBy('test.created_at', 'DESC')
            ->get();
            return view('private.filter2',compact('datas','from_date','to_date'));
        }else {

         $datas =  Employee::where('created_at','>',Carbon::today())
        ->orderBy('created_at', 'desc')->get(); 
        }

        return view('private.filter2',compact('datas','from_date','to_date'));
    }

    public function answerAppeal($id){

        $appeal = Appeal::where("id", $id)->first();

        if($appeal === null){
            return redirect()->back()->with(["message"=>"Savol topilmadi", "m_status" => 0]);
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
            return redirect()->back()->with(["message"=>"Savol topilmadi", "m_status" => 0]);
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
     // dd(23);
        $this->validate($request, [
           "username" => "required",
           "password" => "required"
        ]);
       
        $user = User::where("username", $request->username)->first();
        // dump(md5($request->password));
        // dd($user->password);
        if($user === null){
            return redirect()->back()->with(["message" => "Имя пользователя не найдено", "m_status" => 0]);
        }

        if($user->password != md5($request->password))
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
