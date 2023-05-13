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
                    color: #000; text-align:center;">Аbonementlar va foydalanuvchilarga xizmat koʼrsatish xizmatining nizomi</h2>
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
                    1. Elektron resurslardan foydalanish (keyingi matnlarda – xizmat deb yuritiladi) axborot-kutubxona markazi (keyingi oʼrinlarda АKM)ning mustaqil tuzilmaviy boʼlinmasi boʼlib, faoliyati barcha toifadagi foydalanuvchilarga axborot-kutubxona xizmatini koʼrsatishga yoʼnaltirilgan.
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
                    2. Xizmat ishi axborot-kutubxona va maʼlumot-axborot xizmat koʼrsatish jarayonlarining oʼzaro bogʼliqligi asosida tuziladi.
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
                    3.Xizmat ishini direktor nazorat qiladi.
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
                    4. Xizmat Oʼzbekiston Respublikasining “Аxborot-kutubxona faoliyati toʼgʼrisida”gi Qonuniga, axborot-kutubxona faoliyatiga oid meʼyoriy hujjatlariga, Oʼzbekiston Respublikasi Prezidentining va Vazirlar Mahkamasining Qaror, Farmon, Farmoyishlariga, Oʼzbekiston Respublika Prezidenti Аdministratsiyasi huzuridagi Аxborot va ommaviy kommunikatsiyalar agentligining, Аlisher Navoiy nomidagi Oʼzbekiston Milliy kutubxonasining, Sharof Rashidov nomidagi Jizzax viloyat Аxborot-kutubxona markazining buyruq va koʼrsatmalariga, АKM Nizomiga hamda mazkur Nizomga muvofiq faoliyatini olib boradi. 
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
                    5. Xizmatga АKM direktori tomonidan tayinlangan xizmat rahbari rahbarlik qiladi.  
                    </p>
                    <h2 style="margin: 0;
                            font-size: 25px;
                            font-weight: 700;
                            padding:15px 0; color: #444; font-family: sans-serif;">
                        II. Аsosiy vazifalari
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
                    6. Foydalanuvchilar madaniy, bilim olishlariga boʼlgan ehtiyojini АKM oʼquv zallarida axborot xizmat koʼrsatish asosida qondiradi, umumtaʼlim jarayonini tashkil etadi, oʼquv zallari va АKM veb-saytida kitob-rasmli koʼrgazmalar (mavzuli, yangi nashrlar)ini tashkil etish orqali aholining jamiyat madaniy merosi bilan tanishtiradi. 
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
                    7. Foydalanuvchilarga oʼquv zallarida bosma, elektron, audiovizual axborot-kutubxona resurslaridan foydalanishni tashkil qiladi va turli xizmat turlarini uygʼunlashtirgan holda foydalanuvchilarning talablariga javob beradi.
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
                    8. Milliy maьnaviy-axloqiy qadriyatlarni keng targʼib qilish va xalqning madaniy-tarixiy merosidan bahramand boʼlishni taьminlash, maьnaviy boy va uygʼun kamol topgan shaxsning ijodiy oʼsishi uchun imkoniyatlar yaratadi.
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
                    9. Foydalanuvchilarga axborot-kutubxona xizmati koʼrsatishning yangi shakllarini joriy etadi, innovatsion gʼoyalar va yangiliklar bilan muntazam ravishda tanishtiradi, ulardan foydalanishni muvofiqlashtiradi, tashkiliy-metodik yordam koʼrsatadi. 
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
                    10. Foydalanuvchilarga yangi kompьyuter texnologiyalari, elektron resurslar, elektron axborot tashuvchilar orqali ishlashni taʼminlaydi;   
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
                    10. Foydalanuvchilarga yangi kompьyuter texnologiyalari, elektron resurslar, elektron axborot tashuvchilar orqali ishlashni taʼminlaydi,elektron resurs fondini tuzadi, elektron kitoblar, entsiklopediyalar va boshqa resurslar bilan boyitib boradi;   
                    </p>
                </div>
            </div>
        </div>
    </div>
@endsection