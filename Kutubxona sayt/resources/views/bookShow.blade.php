@extends('layouts.mainLayout')
@section('title', 'Bosh sahifa')
<!-- <script src="https://unpkg.com/aos@2.3.1/dist/aos.js"></script> -->

@section('content')
<div class="categoryID py-5 container">
    <div class="row">
        <div class="item col-12 col-sm-12 col-md-4 col-lg-3 ">
            <div class="mt-3"></div>
            @foreach ($categories as $category)
                <a class="px-3 py-2 a_hover d-block border text-dark" href="{{route('categoryId', $category->id)}}"> {{$category->name}}</a>
            @endforeach
            <div class="mb-4"></div>
        </div>
        <div class="col-12 col-sm-12 col-md-8 col-lg-9 ">
            <div class="row p-0 m-0  w-100 ">
                <div class="col-12 col-sm-12 col-md-6 col-lg-4 mt-3">
                    <img style="min-height: 350px; max-width: 100%;" src="{{asset('/public/images/picture/' .$book_id->image) }}" class="img-fluid rounded shadow" alt="">
                </div>
                <div class="col-12 col-sm-12 col-md-6 col-lg-8 mt-3">
                    <h4 style="font-weight: bold;">{{$book_id->name}}</h4>
                    <h5><span class="text-dark" style="font-weight: bold; padding-right:10px;">Muallif:</span><span class="text-dark">{{$book_id->writer}}</span></h5>
                    <h5><span class="text-dark" style="font-weight: bold; padding-right:10px;">Til:</span><span class="text-dark">{{$book_id->lang}}</span></h5>
                    <h5><span class="text-dark" style="font-weight: bold; padding-right:10px;">Yozuv:</span><span class="text-dark">Lotin</span></h5>
                    <h5><span class="text-dark" style="font-weight: bold; padding-right:10px;">Nashiryot:</span><span class="text-dark">{{$book_id->publisher}}</span></h5>
                    <h5><span class="text-dark" style="font-weight: bold; padding-right:10px;">Nashr yili:</span><span class="text-dark">{{$book_id->title}}</span></h5>
                    <h5><span class="text-dark" style="font-weight: bold; padding-right:10px;">Ko'rishlar soni:</span><span class="text-dark">{{$book_id->id}}</span></h5>
                    <div class="mt-3"></div>
                    <a href="" style="font-size: 14px;" class="px-4 py-2 rounded text-white bg-info"> Yuklab olish</a>
                </div>
                {{-- @if(count($books_category)>0)
                    @foreach ($books_category as $book)
                        <div class="books_item col-12 col-sm-12 col-md-6 col-lg-4">
                            <div class="note_item">
                                <a href="" class="w-100 d-flex justify-content-center">
                                    <img src="{{asset('/storage/images/picture/' .$book->image) }}" class="img-fluid rounded shadow" style="width: 90%;">
                                </a>
                                <div class="tex_boxx">
                                    <a href=""><p>{{$book->name}}</p></a>
                                    <a href=""><h5>{{$book->writer}}</h5></a>
                                </div>
                            </div>
                        </div>
                    @endforeach
                @else
                <div class="text-center">
                    <h5>Kitob topilmadi</h5>
                </div>
                @endif --}}
            </div>
            <div class="row mt-4 p-3" style="border-radius: 5px; box-shadow: rgba(0, 0, 0, 0.16) 0px 1px 4px;">
                <div class="col-12">
                    <p>{{$book_id->note}}</p>
                </div>
            </div>
        </div>
    </div>
</div>
<style>
    .a_hover:hover{
        background: rgb(113, 198, 255);
    }
</style>
@endsection
