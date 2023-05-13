@extends('layouts.mainLayout')
@section('title', 'workplace')

@section('content')
    <div class="container hestory_container">
        <div class="row hestory_row" style="padding: 30px; background:#fff; margin: 40px 0;
         border-radius: 10px; box-shadow: 0px 0px 30px rgba(127, 137, 161, 0.25); display: flex;
         align-items: self-start;
         justify-content: flex-start;
         width: 100%;">
            <div class="col-12 main_boxx m-0 p-0">
                <div class="boxcha w-100" style="display: flex;
                    align-items: center;
                    justify-content: center;
                    text-align: start;
                    flex-direction: column;
                    padding: 10px;">
                    <h2 style="font-size: 30px; width:100%;
                    font-weight: 700;
                    font-family: sans-serif;
                    padding: 0;
                    line-height: 45px;
                    margin: 0;
                    border-bottom: 1px solid #adb5bd;
                    color: #000; text-align:center;">Axborot-bibliografiya xizmatining nizomi</h2>
                    <h2 style="margin: 0;
                            font-size: 25px;
                            font-weight: 700;
                            padding:15px 0; color: #444; font-family: sans-serif;">
                        I. Umumiy qoidalar
                    </h2>
                    <p style="padding: 0;
                        padding-top:10px;
                        margin: 0;
                        font-size: 20px;
                        font-weight: 600;
                        color: #636464;
                        font-family: sans-serif; 
                        width: 100%;
                        text-align: start; text-indent:30px">
                    1. Axborot-bibliografiya xizmati Axborot-kutubxona markazi (kyeyingi o'rinlarda - AKM) ning tuzilmaviy bo'linmasi bo'lib, hududda chiqariladigan nashrlarning davlat bibliografik hisobini olib boradi, o'lkashunoslik bibliografik ma'lumotlar bazasini shakllantiradi va viloyatning shahar, tumanlardagi boshqa axborot-kutubxona muassasalariga bibliografiya yo'nalishi bo'yicha myetodik rahbarlik qiladi.
                    </p>
                    <p style="padding: 0;
                        padding-top:10px;
                        margin: 0;
                        font-size: 20px;
                        font-weight: 600;
                        color: #636464;
                        font-family: sans-serif; 
                        width: 100%;
                        text-align: start; text-indent:30px">
                    2. Xizmat ishini diryektor o'rinbosari nazorat qiladi.
                    </p>
                    <p style="padding: 0;
                        padding-top:10px;
                        margin: 0;
                        font-size: 20px;
                        font-weight: 600;
                        color: #636464;
                        font-family: sans-serif; 
                        width: 100%;
                        text-align: start; text-indent:30px">
                    3.Xizmat O'zbyekiston Ryespublikasining  “Axborot-kutubxona faoliyati to'g'risida”gi Qonuniga, O'zbyekiston Ryespublikasining axborot-kutubxona faoliyatiga oid mye'yoriy hujjatlariga, O'zbyekiston Ryespublikasi Pryezidyentining va Vazirlar Mahkamasining Qaror, Farmon, Farmoyishlariga, O'zbyekiston Ryespublika Pryezidyenti Administraцiyasi huzuridagi Axborot va ommaviy kommunikaцiyalar agyentligining, Alishyer Navoiy nomidagi O'zbyekiston Milliy kutubxonasining, Sharof Rashidov nomidagi Jizzax viloyat Axborot-kutubxona markazining buyruq va ko'rsatmalariga,  AKM Nizomiga hamda mazkur Nizomga muvofiq faoliyatini olib boradi. 
                    </p>
                    <p style="padding: 0;
                        padding-top:10px;
                        margin: 0;
                        font-size: 20px;
                        font-weight: 600;
                        color: #636464;
                        font-family: sans-serif; 
                        width: 100%;
                        text-align: start;">
                    Xizmatga axborot-kutubxona markazi diryektori tomonidan tayinlangan xizmat rahbari  rahbarlik qiladi. 
                    </p>
                    <h2 style="margin: 0;
                            font-size: 25px;
                            font-weight: 700;
                            padding:15px 0; color: #444; font-family: sans-serif;">
                        II. Xizmatning asosiy vazifalari
                    </h2>
                    <p style="padding: 0;
                        padding-top:10px;
                        margin: 0;
                        font-size: 20px;
                        font-weight: 600;
                        color: #636464;
                        font-family: sans-serif; 
                        width: 100%;
                        text-align: start; text-indent:30px">
                    4. Ma'lumot-bibliografik apparati (katalog, kartotyeka, axborot ma'lumot bazalari (shu jumladan elyektron MB)ni  ma'lumotnoma va bibliografik nashrlar fondini shakllantiradi.  
                    </p>
                    <p style="padding: 0;
                        padding-top:10px;
                        margin: 0;
                        font-size: 20px;
                        font-weight: 600;
                        color: #636464;
                        font-family: sans-serif; 
                        width: 100%;
                        text-align: start; text-indent:30px">
                    5. Hududda nashr etilgan milliy hujjatlarning, o'lkashunoslik, bibliografiya va mye'yoriy huquqiy hujjatlar bo'yicha ma'lumotlar bazasini shakllantiradi.
                    </p>
                    <p style="padding: 0;
                        padding-top:10px;
                        margin: 0;
                        font-size: 20px;
                        font-weight: 600;
                        color: #636464;
                        font-family: sans-serif; 
                        width: 100%;
                        text-align: start; text-indent:30px">
                    6. O'lkashunoslik va huquqqa oid axborotlarning elyektron ma'lumotlar bazasini to'ldirib boradi.
                    </p>
                    <p style="padding: 0;
                        padding-top:10px;
                        margin: 0;
                        font-size: 20px;
                        font-weight: 600;
                        color: #636464;
                        font-family: sans-serif; 
                        width: 100%;
                        text-align: start; text-indent:30px">
                    7. “Norma”, lex.uz ma'lumotlar bazasidan foydalanib  foydalanuvchilarni mye'yoriy huquqiy axborot bilan ta'minlaydi. 
                    </p>
                    <p style="padding: 0;
                        padding-top:10px;
                        margin: 0;
                        font-size: 20px;
                        font-weight: 600;
                        color: #636464;
                        font-family: sans-serif; 
                        width: 100%;
                        text-align: start; text-indent:30px">
                    8. Bibliografik qo'llanmalar, muhim va unutilmas sanalar taqvimi (kalyendar)ni, bibliografik ko'rsatkichlar, yangi adabiyotlar axborot byullyetyenlari hamda adabiyotlar ro'yxatini tuzadi, ularning sifati va samaradorligini oshirib boradi.   
                    </p>
                    <p style="padding: 0;
                        padding-top:10px;
                        margin: 0;
                        font-size: 20px;
                        font-weight: 600;
                        color: #636464;
                        font-family: sans-serif; 
                        width: 100%;
                        text-align: start; text-indent:30px">
                    9. O'lka va uning mashhur shaxslariga bag'ishlangan qo'llanmalar tuzadi. Mazkur mavzularda yozma bibliografik sharh, eslatmalar, tasiya ro'yxatlari va boshqalarni tuzish bilan birga foydalanuvchilar o'rtasida targ'ibot ishlarini olib boradi.   
                    </p>
                    <p style="padding: 0;
                        padding-top:10px;
                        margin: 0;
                        font-size: 20px;
                        font-weight: 600;
                        color: #636464;
                        font-family: sans-serif; 
                        width: 100%;
                        text-align: start; text-indent:30px">
                    10. Tumanning boshqa axborot-kutubxona muassalari bilan hamkorlikda bibliografik qo'llanmalar tayyorlash va chop etish ishlarini amalga oshiradi.  
                    </p>
                    <p style="padding: 0;
                        padding-top:10px;
                        margin: 0;
                        font-size: 20px;
                        font-weight: 600;
                        color: #636464;
                        font-family: sans-serif; 
                        width: 100%;
                        text-align: start; text-indent:30px">
                    11. Foydalanuvchilar, ilmiy va ishlab chiqarish jamoalarining bibliografik axborotga bo'lgan ehtiyojlarini tyezkorlik bilan qondiradi.  
                    </p>
                    <p style="padding: 0;
                        padding-top:10px;
                        margin: 0;
                        font-size: 20px;
                        font-weight: 600;
                        color: #636464;
                        font-family: sans-serif; 
                        width: 100%;
                        text-align: start; text-indent:30px">
                    12. Tumandagi axborot-kutubxona muassasalarining bibliografik ishini o'rganadi va muvofiqlashtirishda ishtirok etadi.  
                    </p>
                    <p style="padding: 0;
                        padding-top:10px;
                        margin: 0;
                        font-size: 20px;
                        font-weight: 600;
                        color: #636464;
                        font-family: sans-serif; 
                        width: 100%;
                        text-align: start; text-indent:30px">
                    13. Foydalanuvchilarning siyosiy, huquqiy bilim, huquqiy madaniyatini oshirish, vatanparvarlik ruhida tarbiyalashga qaratilgan adabiyotlarni targ'ib etadi. 
                    </p>
                    <p style="padding: 0;
                        padding-top:10px;
                        margin: 0;
                        font-size: 20px;
                        font-weight: 600;
                        color: #636464;
                        font-family: sans-serif; 
                        width: 100%;
                        text-align: start; text-indent:30px">
                    14. Axborot-bibliografiya yo'nalishi bo'yicha syeminarlar tashkil etadi. 
                    </p>
                    <p style="padding: 0;
                        padding-top:10px;
                        margin: 0;
                        font-size: 20px;
                        font-weight: 600;
                        color: #636464;
                        font-family: sans-serif; 
                        width: 100%;
                        text-align: start; text-indent:30px">
                    15. “Axborot kunlari”, Mutaxassis kunlarini o'tkazadi, “Yangi adabiyotlar” axborot byullyetyenlari orqali foydalanuvchilarga yangi adabiyotlar to'g'risida axborot byeradi.
                    </p>
                    <p style="padding: 0;
                        padding-top:10px;
                        margin: 0;
                        font-size: 20px;
                        font-weight: 600;
                        color: #636464;
                        font-family: sans-serif; 
                        width: 100%;
                        text-align: start; text-indent:30px">
                    16. Foydalanuvchilarga bibliografik bilimlarni targ'ib qiladi. 
                    </p>
                    <p style="padding: 0;
                        padding-top:10px;
                        margin: 0;
                        font-size: 20px;
                        font-weight: 600;
                        color: #636464;
                        font-family: sans-serif; 
                        width: 100%;
                        text-align: start; text-indent:30px">
                    17. Yakka tartibda va guruhlarga bibliografik xizmat ko'rsatishni amalga oshiradi.  
                    </p>
                </div>
            </div>
        </div>
    </div>
@endsection