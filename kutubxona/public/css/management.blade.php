@extends('layouts.mainLayout')
@section('title', '')

@section('content')
    <div class="container hestory_container">
        <div class="row hestory_row" style="padding: 30px; background:#fff; margin: 40px 0; height:39vh;
         border-radius: 10px; box-shadow: 0px 0px 30px rgba(127, 137, 161, 0.25); display: flex;
         align-items: self-start;
         justify-content: space-between;
         width: 100%;">
            <div class="col-lg-3 main_boxx m-0 p-0">
                <div class="boxcha" style="display: flex;
                    align-items: center;
                    justify-content: center;
                    text-align: start;
                    flex-direction: column;
                    padding: 10px;">
                    <img src="assets/images/s.jpg" alt="jpg">
                </div>
            </div>
            <div class="col-lg-9 main_boxx m-0 p-0">
                <div class="boxcha" style="display: flex;
                    align-items: center;
                    justify-content: center;
                    text-align: start;
                    flex-direction: column;
                    padding: 10px;">
                    <div class="shef_title" style="width: 100%;
                        display: flex;
                        align-items: center;
                        justify-content: start;
                        padding-left: 10px;">
                        <h2 style="padding:0;font-size: 25px;
                        font-family: sans-serif;
                        font-weight: 600;
                        margin: 8px 0;
                        color: rgb(98, 98, 98) !important;"><span style="color:#000">Direktor: </span>Babayev Ruslan Berdibayevich </h2>
                    </div>
                    <div style="width: 100%;
                        display: flex;
                        align-items: center;
                        justify-content: start;
                        padding-left: 10px;">
                        <h2 style="margin:8px 0;font-size:22px;
                        font-weight: 600;
                        font-family: sans-serif;color: rgb(98, 98, 98) !important;
                        "><span style="color:#000">Telefon: </span>+998(93)-564-45-30</h2>
                    </div>
                    <div style="width: 100%;
                        display: flex;
                        align-items: center;
                        justify-content: start;
                        padding-left: 10px;">
                        <h2 style="margin:8px 0;font-size: 22px;
                        font-weight: 600;
                        font-family: sans-serif;
                        line-height: 35px;
                        color: rgb(98, 98, 98) !important;"><span style="color:#000">Qabul kunlari: </span>Dushanbadan jumagacha, har ish kuni  
                        soat 09:00 dan 11:00 gacha   
                        </h2>
                    </div>
                    <div style="width: 100%;
                        display: flex;
                        align-items: center;
                        justify-content: start;
                        padding-left: 10px;">
                        <h2 style="margin:8px 0;font-size: 22px;
                        font-weight: 600;
                        font-family: sans-serif;
                        color: rgb(98, 98, 98) !important;"><span style="color:#000">E-mail: </span>mirzachol_akm@natib.uz</h2>
                    </div>
                    <div class="btns_box" style="
                        width: 100%;
                        display: flex;
                        align-items: center;
                        justify-content: flex-start;
                        padding: 20px 10px;">
                        <a href="/vazifa" style="border: none;
                            background: #008dff;
                            color: #fff;
                            padding: 8px 25px;
                            font-size: 20px;
                            font-weight: 600;
                            border-radius: 10px;
                            cursor: pointer; margin-right:25px;">Vazifalari</a>
                        <a href="/beograf" style="border: none;
                            background: #008dff;
                            color: #fff;
                            padding: 8px 25px;
                            font-size: 20px;
                            font-weight: 600;
                            border-radius: 10px;
                            cursor: pointer;">Tarjimai hol</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
@endsection