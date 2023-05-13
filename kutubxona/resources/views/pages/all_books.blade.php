@extends('layouts.mainLayout')
@section('title', 'all-books')

@section('content')
  <h1>Salom</h1>
  
  <!-- <div class="books news container">
      <div class="row row_contents p-0 m-0 w-100">
        <div class="col-12">
          <div class="category-item">
            @foreach ($categories as $category)
                <a href="{{route('categoryId', $category->id)}}"> {{$category->name}}</a>
            @endforeach
          </div>
        </div>
      </div>

      <div class="row row_contents books_items p-0 m-0 mt-4 w-100">
        @foreach ($books as $book)
            <div class="books_item col-10 col-sm-10 col-md-4 col-lg-3">
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
      </div>
  </div> -->
@endsection