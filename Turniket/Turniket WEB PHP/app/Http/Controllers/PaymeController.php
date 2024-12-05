<?php

namespace App\Http\Controllers;

use App\BalanceHistory;
use App\Funcs;
use App\PaymeTransfer;
use App\TelegramBotUser;
use App\User;
use Illuminate\Http\Request;
use Validator;
class PaymeController extends Controller
{

    public function transfer(Request $request){
		/*$rules = [
					'user_id' => 'required',
					'amount' => 'required',
				];

		$validator = Validator::make($request->all(), $rules);

		if($validator->fails()){
			return response()->json(["message" => "Required data has not been received", "error" => 1], 200);
		}*/

		//$funcs = new Funcs();

        $data = request()->json()->all();

        if(!$this->isJson(json_encode($data))){
            $result["error"]["code"] = -32700;
            $result["error"]["message"]["uz"] = "So'rov haqiqiy JSON obyekti emas.";
            $result["error"]["message"]["en"] = " Request is not a valid JSON object";
            $result["error"]["message"]["ru"] = "Запрос не является допустимым объектом JSON";
            $result["error"]["data"] = null;
            return response()->json($result, 200);
        }

        $method = $data["method"];
        $params = $data["params"];

        if(array_key_exists("account", $params) && array_key_exists("user_id", $params["account"])) {
            $account = $params["account"];
            $user_id = $params["account"]["user_id"];
            $user_id = $this->decode_user_id($user_id);
        }
        else {
            $user_id = null;
            $account = null;
        }

        switch ($method){
            case "CheckPerformTransaction":{
                $amount = $params["amount"];
                $amount_data = $this->validateAmount($amount);
                $user_data = $this->validateUser($account, $user_id);

                if(isset($user_data["error"]) && $user_data["error"] == 1){
                    $user_data["result"]["allow"] = false;

                    return response()->json($user_data["result"], 200);
                }

                if(isset($amount_data["error"]) && $amount_data["error"] == 1){
                    $amount_data["result"]["allow"] = false;
                    return response()->json($amount_data["result"], 200);
                }


                $result["result"]["allow"] = true;
                return response()->json($result, 200);
                break;
            }

            case "CreateTransaction":{

                $amount = $params["amount"];
                $amount_data = $this->validateAmount($amount);
                $user_data = $this->validateUser($account, $user_id);


                if(isset($user_data["error"]) && $user_data["error"] == 1){
                    return response()->json($user_data["result"], 200);
                }

                if(isset($amount_data["error"]) && $amount_data["error"] == 1){
                    return response()->json($amount_data["result"], 200);
                }

                $transaction_id = $params["id"];
                $transfer_db = PaymeTransfer::where("transaction_id", $transaction_id)->first();

                $transaction_data = $this->validateTransaction($transfer_db);

                // if no error, transaction found
                if(!isset($transaction_data["error"])){
                    $result["result"]["create_time"] = $transfer_db->transaction_create_time;
                    $result["result"]["transaction"] = strval($transfer_db->id);
                    $result["result"]["state"] = 1;
                    return response()->json($result, 200);
                }

                $user = TelegramBotUser::where("id", $user_id)->first();
                $transfer = new PaymeTransfer();
                $transfer->transaction_id = $params["id"];
                $transfer->transaction_create_time = $params["time"];
                $transfer->amount = $amount;
                $transfer->user_id = $user->id;//order_id ga ozgartir
                $transfer->status = "pending";
                $transfer->status_code = 1;
                $transfer->save();

                $result["result"]["create_time"] = $params["time"];
                $result["result"]["transaction"] = strval($transfer->id);
                $result["result"]["state"] = 1;
                return response()->json($result, 200);

                break;
            }

            case "PerformTransaction":{

                $transaction_id = $params["id"];
                $transfer = PaymeTransfer::where("transaction_id", $transaction_id)->first();

                $transaction_data = $this->validateTransaction($transfer);

                if(isset($transaction_data["error"]) && $transaction_data["error"] == 1){
                    return response()->json($transaction_data["result"], 200);
                }

                // order tekshirish
                $user = TelegramBotUser::where("id", $transfer->user_id)->first();

                if($user === null){
                    $result["error"]["code"] = -31050;
                    $result["error"]["message"]["uz"] = "Foydalanuvchi topilmadi";
                    $result["error"]["message"]["en"] = "User is not found";
                    $result["error"]["message"]["ru"] = "Пользователь не найден";
                    return response()->json($result, 200);
                }


                if($transfer->status_code === -2){
                    $result["error"]["code"] = -31008;
                    $result["error"]["message"]["uz"] = "Operatsiyani amalga oshirib bo'lmaydi";
                    $result["error"]["message"]["en"] = "Operation cannot be performed";
                    $result["error"]["message"]["ru"] = "Операция не может быть выполнена";
                    $result["error"]["data"] = null;
                    return response()->json($result, 200);
                }


                if($transfer->status === "pending" && $transfer->transaction_perform_time === null) {

                    // dazada orderi yagila 
                    $user->balance = $user->balance + round($transfer->amount / 100);
                    $user->update();

                    $transfer->status = "transferred";
                    $transfer->status_code = 2;
                    $transfer->transaction_perform_time = $milliseconds = round(microtime(true) * 1000);
                    $transfer->update();
                }
                elseif($transfer->status === "cancelled" && $transfer->transaction_cancel_time !== null){
                    $result["error"]["code"] = -31008;
                    $result["error"]["message"]["uz"] = "Operatsiyani amalga oshirib bo'lmaydi";
                    $result["error"]["message"]["en"] = "Operation cannot be performed";
                    $result["error"]["message"]["ru"] = "Операция не может быть выполнена";
                    $result["error"]["data"] = null;
                    return response()->json($result, 200);
                }


                $result["result"]["perform_time"] = $transfer->transaction_perform_time;
                $result["result"]["transaction"] = strval($transfer->id);
                $result["result"]["state"] = 2;
                return response()->json($result, 200);

                break;
            }

            case "CancelTransaction":{
                $transaction_id = $params["id"];
                $transfer = PaymeTransfer::where("transaction_id", $transaction_id)->first();

                $transaction_data = $this->validateTransaction($transfer);

                if(isset($transaction_data["error"]) && $transaction_data["error"] == 1){
                    return response()->json($transaction_data["result"], 200);
                }

                if(array_key_exists("reason", $params) && $params["reason"] === 5){

                    // check user balance available

                    if($transfer->status_code === 2) {

                        // order  dekor qilisa

                        $user = TelegramBotUser::where("id", $transfer->user_id)->first();

                        $user->balance = $user->balance - ($transfer->amount / 100);

                        $user->update();


                        $transfer->status_code = -2;
                        $transfer->status = "cancelled";
                        $transfer->transaction_cancel_time = $milliseconds = round(microtime(true) * 1000);
                        $transfer->update();
                    }

                    $result["id"] = $params["id"];
                    $result["error"] = null;
                    $result["result"]["cancel_time"] = $transfer->transaction_cancel_time;
                    $result["result"]["transaction"] = strval($transfer->id);
                    $result["result"]["state"] = $transfer->status_code;
                    return response()->json($result, 200);

                }

                if($transfer->status === "transferred"){
                    $result["error"]["code"] = -31007;
                    $result["error"]["message"]["uz"] = "Transfer allaqachon amalga oshirilgan";
                    $result["error"]["message"]["en"] = "Money already has been transferred";
                    $result["error"]["message"]["ru"] = "Деньги уже переведены";
                    return response()->json($result, 200);
                }

                $result_state = -1;

                if ($transfer->status !== "cancelled" &&  $transfer->transaction_cancel_time === null){

                    $transfer->status_code = -1;
                    $transfer->status = "cancelled";
                    $transfer->transaction_cancel_time = $milliseconds = round(microtime(true) * 1000);
                    $transfer->update();
                }

                if($transfer->transaction_perform_time !== null && $transfer->transaction_cancel_time !== null)
                {
                    $result_state = -2;
                }


                $result["result"]["cancel_time"] = $transfer->transaction_cancel_time;
                $result["result"]["transaction"] = strval($transfer->id);
                $result["result"]["state"] = $result_state;
                return response()->json($result, 200);



                break;
            }

            case "CheckTransaction":{
                $transaction_id = $params["id"];
                $transfer = PaymeTransfer::where("transaction_id", $transaction_id)->first();

                $transaction_data = $this->validateTransaction($transfer);

                if(isset($transaction_data["error"]) && $transaction_data["error"] == 1){
                    return response()->json($transaction_data["result"], 200);
                }

                $reason = null;
                if($transfer->status === "pending"){
                    $result["result"]["state"] = $transfer->status_code;
                }
                elseif($transfer->status === "transferred"){
                    $result["result"]["state"] = $transfer->status_code;
                }
                elseif($transfer->status_code === -1){
                    $result["result"]["state"] = $transfer->status_code;
                    $reason = 3;
                }
                elseif($transfer->status_code === -2){
                    $result["result"]["state"] = $transfer->status_code;
                    $reason = 5;
                }

                /*if($transfer->status_code === 2)
                {
                    $result["result"]["state"] = -2;
                    $reason = 5;
                }*/

                $result["result"]["perform_time"] = $transfer->transaction_perform_time !== null ? $transfer->transaction_perform_time : 0;
                $result["result"]["cancel_time"] = $transfer->transaction_cancel_time !== null ? $transfer->transaction_cancel_time : 0;
                $result["result"]["create_time"] = $transfer->transaction_create_time;
                $result["result"]["transaction"] = strval($transfer->id);
                $result["result"]["reason"] = $reason;
                return response()->json($result, 200);

                break;
            }

            case "GetStatement":
                {

                    $from = $params["from"];
                    $to = $params["to"];

                    $transfers = PaymeTransfer::where("transaction_create_time", ">=", $from)->where("transaction_create_time", "<=", $to)->get();

                    $transaction = array();
                    foreach ($transfers as $transfer) {
                        $transaction["id"] = $transfer->transaction_id;
                        $transaction["time"] = $transfer->transaction_create_time;
                        $transaction["amount"] = $transfer->amount;
                        $user_id = $this->make_user_id($transfer->user_id);
                        $transaction["account"]["user_id"] = $user_id;
                        $transaction["create_time"] = $transfer->transaction_create_time;
                        $transaction["perform_time"] = $transfer->transaction_perform_time;
                        $transaction["cancel_time"] = $transfer->transaction_cancel_time;
                        $transaction["transaction"] = $transfer->id;
                        $transaction["state"] = 2;
                        $transaction["reason"] = null;
                        $transaction["receivers"]["id"] = $transfer->transaction_id;
                        $transaction["receivers"]["amount"] = $transfer->amount;

                    }

                    $result["result"]["transactions"] = $transaction;
                    return response()->json($result, 200);

                    break;

                }


        }// end switch case

    }

    private function isJson($string) {
        json_decode($string);
        return (json_last_error() == JSON_ERROR_NONE);
    }

    private function validateAmount($amount) {

        if($amount < 100000){
            $result["error"]["code"] = -31001;
            $result["error"]["message"]["uz"] = "Minimal miqdor 1000 so'm";
            $result["error"]["message"]["en"] = "The minimum amount is 1000 soums";
            $result["error"]["message"]["ru"] = "Минимальная сумма 1000 сумов.";
            $result["error"]["data"] = null;
            $data["error"] = true;
            $data["result"] = $result;
            return $data;
        }

        if($amount % 100 != 0){
            $result["error"]["code"] = -31001;
            $result["error"]["message"]["uz"] = "Qiymat qabul qilinmaydi";
            $result["error"]["message"]["en"] = "Amount is not accepted";
            $result["error"]["message"]["ru"] = "Сумма не принимается";
            $result["error"]["data"] = null;
            $data["error"] = true;
            $data["result"] = $result;
            return $data;
        }

    }

    private function validateUser($account, $user_id){
        $user = TelegramBotUser::where("id", $user_id)->exists();

        if(!$user || strlen($account["user_id"]) < 5 || (strlen($account["user_id"]) > 5 && strlen($user_id) <= 4)){
            $result["error"]["code"] = -31050;
            $result["error"]["message"]["uz"] = "Foydalanuvchi topilmadi";
            $result["error"]["message"]["en"] = "User is not found";
            $result["error"]["message"]["ru"] = "Пользователь не найден";
            $result["error"]["data"] = $account;

            $data["error"] = true;
            $data["result"] = $result;
            return $data;
        }
    }

    private function validateTransaction($transfer){
        if($transfer === null){
            $result["error"]["code"] = -31003;
            $result["error"]["message"]["uz"] = "Tranzaksiya topilmadi";
            $result["error"]["message"]["en"] = "Transaction is not found";
            $result["error"]["message"]["ru"] = "Транзакция не найдена";

            $data["error"] = true;
            $data["result"] = $result;
            return $data;
        }
    }

    private function decode_user_id($id){

        $real_id = intval(str_replace("U", "", $id));

        return $real_id;
    }

    public function make_user_id($id){

        $new_id = "U";
        for($i=0; $i<(4-strlen($id)); $i++){
            $new_id .= "0";
        }

        $new_id .= $id;

        return $new_id;
    }
}
