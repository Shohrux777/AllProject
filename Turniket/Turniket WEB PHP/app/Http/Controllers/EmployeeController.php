<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use  Illuminate\Support\Facades\DB;
use App\Otchot;
use Illuminate\Support\Carbon;
use App\Employee;
class EmployeeController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function store(Request $request)
    {
       
    $data  = $request->all();
    $attepmt =  DB::table('otchot')
    ->where('employeeID',$request->employeeID)
    ->where('otchot.dateIN','>',Carbon::today())
    ->first();
   
    if(empty($attepmt) && $data['direction'] == 'I'){
        DB::table('otchot')->insert([
            'employeeID' => $data['employeeID'],
            'fio' => $data['personName'],
            'dateIN'     => $data['authDateTime'],
            'created_at' => now(),
            'updated_at' => now()
        ]);   
        DB::table('workers')->where('employeeID',$data['employeeID'])
        ->update([
            'status' => 1
        ]);     
    }else if($attepmt && $data['direction'] == 'O'){
        $diffInHours = \Carbon\Carbon::parse($attepmt->dateIN)->diffInHours(\Carbon\Carbon::parse($data['authDateTime']));
        DB::table('otchot')
        ->where('employeeID',$request->employeeID)
        ->where('otchot.dateIN','>',Carbon::today())->update([
            'dateOUT'   =>  $data['authDateTime'],
            'work_time'  =>  $attepmt->work_time + $diffInHours,
            'created_at' => now(),
            'updated_at' => now()
        ]);
        DB::table('workers')->where('employeeID',$data['employeeID'])
        ->update([
            'status' => 0
        ]);
    }else if($attepmt &&  $data['direction'] == 'I'){
       
        Otchot::where('employeeID',$request->employeeID)
       ->where('otchot.dateIN','>',Carbon::today())
       ->update([
            'dateIN' => $data['authDateTime'],
            'created_at' => now(),
            'updated_at' => now()
        ]);
        DB::table('workers')->where('employeeID',$data['employeeID'])
        ->update([
            'status' => 1
        ]);
    }
        
        DB::table('test')->insert([
            'employeeID' => $data['employeeID'],
            'authDateTime' => $data['authDateTime'],
            'authDate' => $data['authDate'],
            'authTime' => $data['authTime'],
            'direction' => $data['direction'],
            'deviceName' => $data['deviceName'],
            'cardNo' => $data['cardNo'],
            'deviceSN' => $data['deviceSN'],
            'personName' => $data['personName'],
            'created_at' => now(),
            'updated_at' => now()]);
            return response()->json(['message' => 'Data inserted successfully']);

    }    

    public function add_company_store(Request $request){
        $data  = $request->all();
            DB::table('company')->insert([
                'id' => $data['id'],
                'name' => $data['name']

            ]);
        return response()->json(['message' => 'Data inserted successfully']);

    }

    public function add_otdel_store(Request $request){
        $data  = $request->all();
            DB::table('otdel')->insert([
                'name' => $data['name'],
                'company_name' => $data['company_name']

            ]);
        return response()->json(['message' => 'Data inserted successfully']);

    }

    public function add_worker_store(Request $request){
        $data  = $request->all();
            DB::table('workers')->insert([
                'id' => $data['id'],
                'fio' => $data['fio'],
                'employeeID' => $data['employeeID'],
                'otdel_name' => $data['otdel_name']
                

            ]);
        return response()->json(['message' => 'Data inserted successfully']);

    }

    public function turnkate_otdel_and_company(Request $request){
        $data  = $request->all();
            DB::table('company')->truncate();
            DB::table('otdel')->truncate();
            DB::table('workers')->truncate();
        return response()->json(['message' => 'Data turncate successfully']);

    }
    
}

// INSERT INTO company`(`id, `name`) VALUES (2,'Companya 2')

// INSERT INTO otdel`( `name, `company_name`) VALUES ('department','companya 1')

//INSERT INTO `workers`(`id`, `fio`,  `employeeID`, `otdel_name`, `status`) VALUES ( 1,'eshmat',1,'otdel nomi', true)