@extends('layouts.mainLayout')
@section('title', 'Bosh sahifa')
<!-- <script src="https://unpkg.com/aos@2.3.1/dist/aos.js"></script> -->

@section('content')
<div class="categoryID py-5 container">
    <div class="row">
        <div class="item col-12 col-sm-12 col-md-4 col-lg-3 ">
            @foreach ($categories as $category)
                <a class="px-3 py-2 a_hover d-block border text-dark" href="{{route('categoryId', $category->id)}}"> {{$category->name}}</a>
            @endforeach
        </div>
        <div class="col-12 col-sm-12 col-md-8 col-lg-9">
            <div class="row row_contents books_items p-0 m-0 mt-4 w-100">
                @if(count($books_category)>0)
                    @foreach ($books_category as $book)
                        <div class="books_item col-12 col-sm-12 col-md-6 col-lg-4">
                            <div class="note_item">
                                <a href="{{route('bookId', $book->id)}}" class="w-100 d-flex justify-content-center">
                                    <img src="{{asset('/public/images/picture/' .$book->image) }}" class="img-fluid rounded shadow" style="width: 90%;">
                                </a>
                                <div class="tex_boxx">
                                    <a href="{{route('bookId', $book->id)}}"><p>{{$book->name}}</p></a>
                                    <a href="{{route('bookId', $book->id)}}"><h5>{{$book->writer}}</h5></a>
                                </div>
                            </div>
                        </div>
                    @endforeach
                @else
                <div class="text-center">
                    <h5>Kitob topilmadi</h5>
                </div>
                @endif
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
