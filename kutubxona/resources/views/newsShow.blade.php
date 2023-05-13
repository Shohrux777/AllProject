@extends('layouts.mainLayout')
@section('title', 'Bosh sahifa')
<!-- <script src="https://unpkg.com/aos@2.3.1/dist/aos.js"></script> -->

@section('content')
<div class="categoryID py-5 container">
    <div class="container new_show_box">
        <img src="{{asset('/public/news/picture/' .$new_id->image) }}" class="img-fluid rounded new_show_img">
    </div>
    <div class="container mt-4">
        <div class="text-center">
            <h2 class="new_text_h2">
                {{$new_id->name}}
            </h2>
        </div>

    </div>
    <section class="py-2 px-2">
        <div class="m-0 py-2 px-3" style="box-shadow: rgba(0, 0, 0, 0.16) 0px 1px 4px;">
            <p>
                {{$new_id->text}}
            </p>
        </div>
    </section>

    <section class="py-2 px-2">
        <div class="text-center">
            <a class="text-primary" href="{{$new_id->link}}">{{$new_id->link}}</a>
        </div>
    </section>
</div>
<style>
    .a_hover:hover{
        background: rgb(113, 198, 255);
    }
</style>
@endsection
